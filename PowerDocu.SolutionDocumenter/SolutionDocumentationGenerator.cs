using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using PowerDocu.AgentDocumenter;
using PowerDocu.AIModelDocumenter;
using PowerDocu.AppDocumenter;
using PowerDocu.AppModuleDocumenter;
using PowerDocu.BPFDocumenter;
using PowerDocu.ClassicWorkflowDocumenter;
using PowerDocu.DesktopFlowDocumenter;
using PowerDocu.DataflowDocumenter;
using PowerDocu.FlowDocumenter;

namespace PowerDocu.SolutionDocumenter
{
    /// <summary>
    /// Orchestrates the two-phase documentation pipeline for solution zip files:
    /// Phase 1 - Parse all components (solution, flows, apps, agents, customizations)
    /// Phase 2 - Generate all documentation output with full cross-reference access
    /// </summary>
    public static class SolutionDocumentationGenerator
    {
        public static void GenerateDocumentation(string filePath, bool fullDocumentation, ConfigHelper config, string outputPath = null)
        {
            if (!File.Exists(filePath))
            {
                NotificationHelper.SendNotification($"File not found: {filePath}");
                return;
            }

            DateTime startDocGeneration = DateTime.Now;
            NotificationHelper.SendPhaseUpdate("Parsing");

            // ── Phase 1: Parse everything ──────────────────────────────────
            NotificationHelper.SendNotification("Phase 1: Parsing all components...");

            DocumentationContext context = new DocumentationContext
            {
                Config = config,
                FullDocumentation = fullDocumentation,
                OutputPath = outputPath,
                SourceZipPath = filePath
            };

            // Parse flows
            var (flows, flowPath) = FlowDocumentationGenerator.ParseFlows(filePath, outputPath);
            context.Flows = flows ?? new List<FlowEntity>();

            // Parse apps
            var (apps, appPath) = AppDocumentationGenerator.ParseApps(filePath, outputPath);
            context.Apps = apps ?? new List<AppEntity>();

            // Parse agents
            var (agents, agentPath) = AgentDocumentationGenerator.ParseAgents(filePath, outputPath);
            context.Agents = agents ?? new List<AgentEntity>();

            // Parse solution metadata and customizations (provides tables, roles, views, flow names, app names, etc.)
            SolutionParser solutionParser = new SolutionParser(filePath);
            if (solutionParser.solution != null)
            {
                context.Solution = solutionParser.solution;
                context.Customizations = solutionParser.solution.Customizations;
                if (context.Customizations != null)
                {
                    context.Tables = context.Customizations.getEntities();
                    context.Roles = context.Customizations.getRoles();
                    // Extract AppModules from customizations
                    if (config.documentModelDrivenApps)
                    {
                        context.AppModules = context.Customizations.getAppModules() ?? new List<AppModuleEntity>();
                    }

                    // Enrich flows with ModernFlowType from customizations.xml
                    foreach (FlowEntity flow in context.Flows)
                    {
                        if (!string.IsNullOrEmpty(flow.ID))
                        {
                            flow.modernFlowType = context.Customizations.getModernFlowTypeById(flow.ID);
                        }
                    }

                    // Extract Business Process Flows from customizations
                    if (config.documentBusinessProcessFlows)
                    {
                        context.BusinessProcessFlows = context.Customizations.getBusinessProcessFlows() ?? new List<BPFEntity>();
                        // Parse XAML files to populate stages/steps
                        if (solutionParser.solution.WorkflowXamlFiles != null)
                        {
                            foreach (BPFEntity bpf in context.BusinessProcessFlows)
                            {
                                if (!string.IsNullOrEmpty(bpf.XamlFileName))
                                {
                                    // Match by filename - XamlFileName is like "/Workflows/Name-GUID.xaml"
                                    string normalizedPath = bpf.XamlFileName.TrimStart('/');
                                    foreach (var kvp in solutionParser.solution.WorkflowXamlFiles)
                                    {
                                        if (kvp.Key.Equals(normalizedPath, StringComparison.OrdinalIgnoreCase) ||
                                            kvp.Key.EndsWith(System.IO.Path.GetFileName(normalizedPath), StringComparison.OrdinalIgnoreCase))
                                        {
                                            BPFXamlParser.ParseBPFXaml(bpf, kvp.Value);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Extract Desktop Flows from customizations (Category=6, UIFlowType>=0)
                    if (config.documentDesktopFlows)
                    {
                        context.DesktopFlows = context.Customizations.getDesktopFlows() ?? new List<DesktopFlowEntity>();
                    }

                    // Extract Classic Workflows from customizations (Category=0)
                    if (config.documentClassicWorkflows)
                    {
                        context.ClassicWorkflows = context.Customizations.getClassicWorkflows() ?? new List<ClassicWorkflowEntity>();
                        // Parse XAML files to populate steps
                        if (solutionParser.solution.WorkflowXamlFiles != null)
                        {
                            foreach (ClassicWorkflowEntity workflow in context.ClassicWorkflows)
                            {
                                if (!string.IsNullOrEmpty(workflow.XamlFileName))
                                {
                                    string normalizedPath = workflow.XamlFileName.TrimStart('/');
                                    foreach (var kvp in solutionParser.solution.WorkflowXamlFiles)
                                    {
                                        if (kvp.Key.Equals(normalizedPath, StringComparison.OrdinalIgnoreCase) ||
                                            kvp.Key.EndsWith(System.IO.Path.GetFileName(normalizedPath), StringComparison.OrdinalIgnoreCase))
                                        {
                                            ClassicWorkflowXamlParser.ParseClassicWorkflowXaml(workflow, kvp.Value);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        // Enrich custom activity steps with metadata from PluginAssemblies
                        // and resolve condition field references to display names
                        _enrichContext = context;
                        foreach (ClassicWorkflowEntity workflow in context.ClassicWorkflows)
                        {
                            EnrichCustomActivityMetadata(workflow.Steps, context.Customizations);
                        }
                        _enrichContext = null;
                        // Extract Dataverse Dataflows from customizations
                        if (config.documentDataflows)
                        {
                            context.Dataflows = context.Customizations.getDataflows() ?? new List<DataflowEntity>();
                        }
                    }
                }


                NotificationHelper.SendNotification(
                    $"Phase 1 complete: {context.Flows.Count} flow(s), {context.Apps.Count} app(s), " +
                    $"{context.Agents.Count} agent(s), {context.AppModules.Count} app module(s), " +
                    $"{context.BusinessProcessFlows.Count} BPF(s), {context.DesktopFlows.Count} desktop flow(s), " +
                    $"{context.ClassicWorkflows.Count} classic workflow(s), " +
                    $"{context.Dataflows.Count} dataflow(s), " +
                    $"{context.Tables.Count} table(s), {context.Roles.Count} role(s)."
                );

                // Build progress tracker from discovered counts
                var progress = new ProgressTracker();
                if (config.documentFlows && context.Flows.Count > 0)
                    progress.Register("Flows", context.Flows.Count);
                if (config.documentApps && context.Apps.Count > 0)
                    progress.Register("Apps", context.Apps.Count);
                if (config.documentAgents && context.Agents.Count > 0)
                    progress.Register("Agents", context.Agents.Count);
                if (config.documentBusinessProcessFlows && context.BusinessProcessFlows.Count > 0)
                    progress.Register("BPFs", context.BusinessProcessFlows.Count);
                if (config.documentDesktopFlows && context.DesktopFlows.Count > 0)
                    progress.Register("DesktopFlows", context.DesktopFlows.Count);
                if (config.documentClassicWorkflows && context.ClassicWorkflows.Count > 0)
                    progress.Register("Classic Workflows", context.ClassicWorkflows.Count);
                if (config.documentDataflows && context.Dataflows.Count > 0)
                    progress.Register("Dataflows", context.Dataflows.Count);
                if (config.documentModelDrivenApps && context.AppModules.Count > 0)
                    progress.Register("Model-Driven Apps", context.AppModules.Count);
                int aiModelCount = context.Customizations?.getAIModels()?.Count ?? 0;
                if (config.documentSolution && context.Solution != null && aiModelCount > 0)
                    progress.Register("AI Models", aiModelCount);
                context.Progress = progress;
                if (progress.BuildString().Length > 0)
                    NotificationHelper.SendStatusUpdate(progress.BuildString());

                // ── Phase 2: Generate all documentation ────────────────────────
                NotificationHelper.SendNotification("Phase 2: Generating documentation...");
                NotificationHelper.SendPhaseUpdate("Documenting");

                // Compute centralised solution base path so that all sub-documenters
                // write into the same Solution folder, regardless of how individual
                // parsers classify the package.
                string solutionBasePath = outputPath == null
                    ? Path.GetDirectoryName(filePath) + @"\Solution " + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath))
                    : outputPath + @"\" + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath));

                // Generate flow documentation
                if (flows != null)
                {
                    FlowDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }

                // Generate app documentation
                if (apps != null)
                {
                    AppDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }

                // Generate agent documentation
                if (agents != null)
                {
                    AgentDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }

                // Generate AI Model documentation
                if (config.documentSolution && context.Solution != null)
                {
                    AIModelDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }

                // Generate Business Process Flow documentation
                if (config.documentBusinessProcessFlows)
                {
                    BPFDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }

                // Generate Desktop Flow documentation
                if (config.documentDesktopFlows)
                {
                    DesktopFlowDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }

                // Generate Classic Workflow documentation
                if (config.documentClassicWorkflows)
                {
                    ClassicWorkflowDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }
                // Generate Dataflow documentation
                if (config.documentDataflows)
                {
                    DataflowDocumentationGenerator.GenerateOutput(context, solutionBasePath);
                }

                // Generate solution-level documentation (solution overview, model-driven apps, Dataverse graph)
                if (config.documentSolution && context.Solution != null)
                {
                    string solutionPath = solutionBasePath + @"\";

                    // Generate Model-Driven App documentation
                    if (config.documentModelDrivenApps)
                    {
                        AppModuleDocumentationGenerator.GenerateOutput(context, solutionPath);
                    }

                    // Generate solution overview documentation
                    SolutionDocumentationContent solutionContent = new SolutionDocumentationContent(context, solutionPath);

                    try
                    {
                        DataverseGraphBuilder dataverseGraphBuilder = new DataverseGraphBuilder(solutionContent);
                    }
                    catch (Exception ex)
                    {
                        NotificationHelper.SendNotification("Warning: Could not generate Dataverse relationship graph: " + ex.Message);
                    }

                    // Generate solution component relationship graph
                    try
                    {
                        SolutionComponentGraphBuilder componentGraphBuilder = new SolutionComponentGraphBuilder(
                            solutionContent, solutionPath, config.showAllComponentsInGraph);
                        componentGraphBuilder.Build();
                    }
                    catch (Exception ex)
                    {
                        NotificationHelper.SendNotification("Warning: Could not generate solution component graph: " + ex.Message);
                    }

                    if (fullDocumentation)
                    {
                        if (config.outputFormat.Equals(OutputFormatHelper.Word) || config.outputFormat.Equals(OutputFormatHelper.All))
                        {
                            NotificationHelper.SendNotification("Creating Solution documentation");
                            SolutionWordDocBuilder wordzip = new SolutionWordDocBuilder(solutionContent, config.wordTemplate, config.documentDefaultColumns, config.addTableOfContents);
                            WebResourceWordDocBuilder wrWordDoc = new WebResourceWordDocBuilder(solutionContent, config.wordTemplate);
                        }
                        if (config.outputFormat.Equals(OutputFormatHelper.Markdown) || config.outputFormat.Equals(OutputFormatHelper.All))
                        {
                            SolutionMarkdownBuilder mdDoc = new SolutionMarkdownBuilder(solutionContent, config.documentDefaultColumns);
                            WebResourceMarkdownBuilder wrMdDoc = new WebResourceMarkdownBuilder(solutionContent);
                        }
                        if (config.outputFormat.Equals(OutputFormatHelper.Html) || config.outputFormat.Equals(OutputFormatHelper.All))
                        {
                            NotificationHelper.SendNotification("Creating HTML Solution documentation");
                            SolutionHtmlBuilder htmlDoc = new SolutionHtmlBuilder(solutionContent, config.documentDefaultColumns);
                            WebResourceHtmlBuilder wrDoc = new WebResourceHtmlBuilder(solutionContent);
                        }
                        FormSvgBuilder.ClearCache();
                    }
                }

                DateTime endDocGeneration = DateTime.Now;
                NotificationHelper.SendNotification($"Documentation completed for {filePath}. Total time: {(endDocGeneration - startDocGeneration).TotalSeconds} seconds.");
            }
        }

        private static void EnrichCustomActivityMetadata(List<ClassicWorkflowStep> steps, CustomizationsEntity customizations)
        {
            foreach (var step in steps)
            {
                if (step.StepType == ClassicWorkflowStepType.Custom && !string.IsNullOrEmpty(step.CustomActivityClass))
                {
                    var (friendlyName, description, groupName) = customizations.getWorkflowActivityMetadata(step.CustomActivityClass);
                    step.CustomActivityFriendlyName = friendlyName;
                    step.CustomActivityDescription = description;
                    step.CustomActivityGroupName = groupName;
                }

                // Enrich condition tree field references with display names
                if (step.ConditionTree != null)
                {
                    EnrichConditionFieldNames(step.ConditionTree);
                    step.ConditionDescription = step.ConditionTree.ToFlatString();
                }

                // Enrich field assignment values
                if (step.Fields.Count > 0)
                {
                    foreach (var field in step.Fields)
                    {
                        // Resolve option set integer values: "1" → "Active (1)"
                        if (!string.IsNullOrEmpty(field.Value) && !string.IsNullOrEmpty(step.TargetEntity) &&
                            field.Value.StartsWith("\"") && field.Value.EndsWith("\""))
                        {
                            string intVal = field.Value.Trim('"');
                            if (int.TryParse(intVal, out _))
                            {
                                string resolved = ResolveOptionSetValues(step.TargetEntity, field.FieldName, field.Value);
                                if (resolved != field.Value)
                                    field.Value = resolved;
                            }
                        }

                        // Resolve {entity.field} dynamic references to display names
                        if (!string.IsNullOrEmpty(field.Value))
                        {
                            field.Value = ResolveEntityFieldReferences(field.Value);
                        }

                        // Resolve field logical name to display name for the field label itself
                        if (!string.IsNullOrEmpty(field.FieldName) && !string.IsNullOrEmpty(step.TargetEntity))
                        {
                            string fieldDisplay = ResolveFieldDisplayName(step.TargetEntity, field.FieldName);
                            if (fieldDisplay != field.FieldName)
                                field.FieldName = fieldDisplay + " (" + field.FieldName + ")";
                        }
                    }
                }

                if (step.ChildSteps.Count > 0)
                    EnrichCustomActivityMetadata(step.ChildSteps, customizations);
            }
        }

        private static DocumentationContext _enrichContext;

        private static void EnrichConditionFieldNames(ConditionExpression expr)
        {
            if (expr.IsLeaf && !string.IsNullOrEmpty(expr.Field))
            {
                // Parse "{entityName.fieldName}" format and resolve to display names
                string field = expr.Field.Trim('{', '}');
                int dotIdx = field.IndexOf('.');
                string entityLogical = null;
                string fieldLogical = null;
                if (dotIdx > 0)
                {
                    entityLogical = field.Substring(0, dotIdx);
                    // Strip " (Related)" suffix if present
                    string cleanEntity = entityLogical.Replace(" (Related)", "");
                    fieldLogical = field.Substring(dotIdx + 1);
                    bool isRelated = entityLogical.Contains("(Related)");

                    // Resolve display names
                    string entityDisplay = _enrichContext?.GetTableDisplayName(cleanEntity) ?? cleanEntity;
                    string fieldDisplay = ResolveFieldDisplayName(cleanEntity, fieldLogical);

                    if (isRelated)
                        expr.Field = fieldDisplay + " on " + entityDisplay + " (Related)";
                    else
                        expr.Field = fieldDisplay + " on " + entityDisplay;

                    entityLogical = cleanEntity;
                }

                // Resolve option set values in the Value field
                if (!string.IsNullOrEmpty(expr.Value) && !string.IsNullOrEmpty(entityLogical) && !string.IsNullOrEmpty(fieldLogical))
                {
                    expr.Value = ResolveOptionSetValues(entityLogical, fieldLogical, expr.Value);
                }
            }
            else if (expr.IsGroup)
            {
                foreach (var child in expr.Children)
                    EnrichConditionFieldNames(child);
            }
        }

        private static string ResolveFieldDisplayName(string entityLogicalName, string fieldLogicalName)
        {
            if (_enrichContext?.Tables == null) return fieldLogicalName;
            var table = _enrichContext.Tables.FirstOrDefault(t =>
                t.getName().Equals(entityLogicalName, System.StringComparison.OrdinalIgnoreCase));
            if (table == null) return fieldLogicalName;
            var column = table.GetColumns().FirstOrDefault(c =>
                c.getLogicalName().Equals(fieldLogicalName, System.StringComparison.OrdinalIgnoreCase));
            if (column == null) return fieldLogicalName;
            string displayName = column.getDisplayName();
            if (!string.IsNullOrEmpty(displayName) && !displayName.Equals(fieldLogicalName, System.StringComparison.OrdinalIgnoreCase))
                return displayName + " (" + fieldLogicalName + ")";
            return fieldLogicalName;
        }

        private static string ResolveOptionSetValues(string entityLogicalName, string fieldLogicalName, string rawValue)
        {
            if (_enrichContext?.Tables == null) return rawValue;
            var table = _enrichContext.Tables.FirstOrDefault(t =>
                t.getName().Equals(entityLogicalName, System.StringComparison.OrdinalIgnoreCase));
            if (table == null) return rawValue;
            var column = table.GetColumns().FirstOrDefault(c =>
                c.getLogicalName().Equals(fieldLogicalName, System.StringComparison.OrdinalIgnoreCase));
            if (column == null) return rawValue;

            // Value may be a single quoted value like "100000002" or comma-separated like "100000001", "100000002"
            string[] parts = rawValue.Split(',');
            var resolved = new List<string>();
            foreach (string part in parts)
            {
                string trimmed = part.Trim().Trim('"');
                string label = column.GetOptionSetLabel(trimmed);
                if (!string.IsNullOrEmpty(label))
                    resolved.Add(label + " (" + trimmed + ")");
                else
                    resolved.Add("\"" + trimmed + "\"");
            }
            return string.Join(", ", resolved);
        }

        /// <summary>
        /// Resolves {entity.field} references in a value string to friendly display names.
        /// Handles patterns like:
        ///   {account.name} → Account Name (name) on Account (account)
        ///   {systemuser (Related).address1_composite} → Address (address1_composite) on User (systemuser) (Related)
        ///   {Process.Execution Time} → {Process.Execution Time} (unchanged)
        ///   Concatenated: {account.name} + "literal" → resolved individually
        /// </summary>
        private static string ResolveEntityFieldReferences(string value)
        {
            if (string.IsNullOrEmpty(value) || _enrichContext?.Tables == null) return value;

            // Match all {entity.field} patterns, including those with " (Related)" suffix
            return System.Text.RegularExpressions.Regex.Replace(value,
                @"\{([^}]+)\}",
                match =>
                {
                    string inner = match.Groups[1].Value;

                    // Skip system/process references like {Process.Execution Time}
                    if (inner.StartsWith("Process.", System.StringComparison.OrdinalIgnoreCase))
                        return match.Value;

                    int dotIdx = inner.IndexOf('.');
                    if (dotIdx <= 0) return match.Value;

                    string entityPart = inner.Substring(0, dotIdx);
                    string fieldPart = inner.Substring(dotIdx + 1);

                    // Handle "(Related)" suffix
                    bool isRelated = entityPart.Contains("(Related)");
                    string cleanEntity = entityPart.Replace(" (Related)", "").Trim();

                    // Resolve display names
                    string entityDisplay = _enrichContext.GetTableDisplayName(cleanEntity);
                    if (string.IsNullOrEmpty(entityDisplay)) entityDisplay = cleanEntity;

                    string fieldDisplay = ResolveFieldDisplayName(cleanEntity, fieldPart);

                    string relatedSuffix = isRelated ? " (Related)" : "";
                    return fieldDisplay + " on " + entityDisplay + relatedSuffix;
                });
        }
    }
}