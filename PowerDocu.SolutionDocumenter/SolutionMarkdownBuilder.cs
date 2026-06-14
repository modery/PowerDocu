using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Grynwald.MarkdownGenerator;

namespace PowerDocu.SolutionDocumenter
{
    class SolutionMarkdownBuilder : MarkdownBuilder
    {
        private readonly SolutionDocumentationContent content;
        private readonly string solutionDocumentFileName;
        private readonly MdDocument solutionDoc;
        private readonly bool documentDefaultColumns;
        public SolutionMarkdownBuilder(SolutionDocumentationContent contentDocumentation, bool documentDefaultColumns = false)
        {
            content = contentDocumentation;
            this.documentDefaultColumns = documentDefaultColumns;
            Directory.CreateDirectory(content.folderPath);
            solutionDocumentFileName = ("solution " + content.filename + ".md").Replace(" ", "-");
            solutionDoc = new MdDocument();

            addSolutionOverview();
            addSolutionComponents();
            solutionDoc.Save(content.folderPath + "/" + solutionDocumentFileName);
            createOrderFile();
            NotificationHelper.SendNotification("Created Markdown documentation for solution" + content.solution.UniqueName);
        }

        private void addSolutionOverview()
        {
            List<MdTableRow> tableRows = new List<MdTableRow>();
            solutionDoc.Root.Add(new MdHeading(content.solution.UniqueName, 1));
            tableRows.Add(new MdTableRow("Status", content.solution.isManaged ? "Managed" : "Unmanaged"));
            tableRows.Add(new MdTableRow("Version", content.solution.Version));
            tableRows.Add(new MdTableRow("Documentation generated at", PowerDocuReleaseHelper.GetTimestampWithVersion()));
            solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Details"), tableRows));
            AddPublisherInfo();
            AddStatistics();
        }

        private void AddStatistics()
        {
            List<MdTableRow> tableRows = new List<MdTableRow>();
            solutionDoc.Root.Add(new MdHeading("Statistics", 2));
            var statisticsEntries = new List<(string Name, int Count, MdSpan Link)>();
            if (content.solution.EnvironmentVariables.Count > 0)
            {
                var envLink = new MdLinkSpan("Environment Variables", "#environment-variables");
                statisticsEntries.Add(("Environment Variables", content.solution.EnvironmentVariables.Count, envLink));
            }
            if (content.agents.Count > 0)
            {
                var agentLink = new MdLinkSpan("Agents", "#agents");
                statisticsEntries.Add(("Agents", content.agents.Count, agentLink));
            }
            if (content.dataflows.Count > 0)
            {
                var dfLink = new MdLinkSpan("Dataflows", "#dataflows");
                statisticsEntries.Add(("Dataflows", content.dataflows.Count, dfLink));
            }
            if (content.solution.AppActions.Count > 0)
            {
                var link = new MdLinkSpan("Command Bar Buttons", "#app-actions");
                statisticsEntries.Add(("Command Bar Buttons", content.solution.AppActions.Count, link));
            }
            if (content.solution.SettingDefinitions.Count > 0)
            {
                var link = new MdLinkSpan("Setting Definitions", "#setting-definitions");
                statisticsEntries.Add(("Setting Definitions", content.solution.SettingDefinitions.Count, link));
            }
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                var link = new MdLinkSpan("Formulas", "#formulas");
                statisticsEntries.Add(("Formulas", content.solution.FormulaDefinitions.Count, link));
            }
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                int count = content.solution.Components.Where(c => c.Type == componentType).Count();
                string sectionHeading = GetComponentSectionHeading(componentType);
                var link = new MdLinkSpan(componentType, "#" + sectionHeading.ToLowerInvariant().Replace(" ", "-"));
                statisticsEntries.Add((componentType, count, link));
            }
            foreach (var entry in statisticsEntries.OrderBy(e => e.Name, StringComparer.OrdinalIgnoreCase))
            {
                tableRows.Add(new MdTableRow(entry.Link, new MdTextSpan(entry.Count.ToString())));
            }
            if (tableRows.Count > 0)
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Component Type", "Number of Components"), tableRows));
        }

        /// <summary>
        /// Returns the heading text used for the section of a given component type.
        /// Must be kept in sync with the headings used in addSolutionComponents / render methods.
        /// </summary>
        private static string GetComponentSectionHeading(string componentType)
        {
            return componentType switch
            {
                "EnvironmentVariable" => "Environment Variables",
                "Role" => "Security Roles",
                "Entity" => "Tables",
                "AI Project" => "AI Models",
                "Option Set" => "Option Sets",
                "Agent" => "Agents",
                "Dataflow" => "Dataflows",
                "Web Resource" => "Web Resources",
                "AppAction" => "Command Bar Buttons",
                "SettingDefinition" => "Setting Definitions",
                "FormulaDefinition" => "Formulas",
                _ => componentType
            };
        }

        private void AddPublisherInfo()
        {
            List<MdTableRow> tableRows = new List<MdTableRow>();
            solutionDoc.Root.Add(new MdHeading("Publisher Details", 2));
            tableRows.Add(new MdTableRow("Name", content.solution.Publisher.UniqueName));
            tableRows.Add(new MdTableRow("Email", content.solution.Publisher.EMailAddress));
            tableRows.Add(new MdTableRow("CustomizationPrefix", content.solution.Publisher.CustomizationPrefix));
            tableRows.Add(new MdTableRow("CustomizationOptionValuePrefix", content.solution.Publisher.CustomizationOptionValuePrefix));
            tableRows.Add(new MdTableRow("SupportingWebsiteUrl", content.solution.Publisher.SupportingWebsiteUrl));
            solutionDoc.Root.Add(new MdTable(new MdTableRow("Publisher", "Details"), tableRows));

            if (content.solution.Publisher.Descriptions.Count > 0)
            {
                solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("Descriptions:")));
                List<MdTableRow> descriptionsTableRows = new List<MdTableRow>();
                foreach (KeyValuePair<string, string> description in content.solution.Publisher.Descriptions)
                {
                    descriptionsTableRows.Add(new MdTableRow(description.Key, description.Value));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Language Code", "Description"), descriptionsTableRows));
            }
            if (content.solution.Publisher.LocalizedNames.Count > 0)
            {
                solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("Localized Names:")));
                List<MdTableRow> localizedNamesTableRows = new List<MdTableRow>();
                foreach (KeyValuePair<string, string> localizedName in content.solution.Publisher.LocalizedNames)
                {
                    localizedNamesTableRows.Add(new MdTableRow(localizedName.Key, localizedName.Value));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Language Code", "Description"), localizedNamesTableRows));
            }
            if (content.solution.Publisher.Addresses.Count > 0)
            {
                solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("Addresses:")));
                foreach (Address address in content.solution.Publisher.Addresses)
                {
                    List<MdTableRow> addressTableRows = new List<MdTableRow>();
                    if (!String.IsNullOrEmpty(address.Name))
                        addressTableRows.Add(new MdTableRow("Name", address.Name));
                    if (!String.IsNullOrEmpty(address.AddressNumber))
                        addressTableRows.Add(new MdTableRow("AddressNumber", address.AddressNumber));
                    if (!String.IsNullOrEmpty(address.AddressTypeCode))
                        addressTableRows.Add(new MdTableRow("AddressTypeCode", address.AddressTypeCode));
                    if (!String.IsNullOrEmpty(address.City))
                        addressTableRows.Add(new MdTableRow("City", address.City));
                    if (!String.IsNullOrEmpty(address.County))
                        addressTableRows.Add(new MdTableRow("County", address.County));
                    if (!String.IsNullOrEmpty(address.Country))
                        addressTableRows.Add(new MdTableRow("Country", address.Country));
                    if (!String.IsNullOrEmpty(address.Fax))
                        addressTableRows.Add(new MdTableRow("Fax", address.Fax));
                    if (!String.IsNullOrEmpty(address.FreightTermsCode))
                        addressTableRows.Add(new MdTableRow("FreightTermsCode", address.FreightTermsCode));
                    if (!String.IsNullOrEmpty(address.ImportSequenceNumber))
                        addressTableRows.Add(new MdTableRow("ImportSequenceNumber", address.ImportSequenceNumber));
                    if (!String.IsNullOrEmpty(address.Latitude))
                        addressTableRows.Add(new MdTableRow("Latitude", address.Latitude));
                    if (!String.IsNullOrEmpty(address.Line1))
                        addressTableRows.Add(new MdTableRow("Line1", address.Line1));
                    if (!String.IsNullOrEmpty(address.Line2))
                        addressTableRows.Add(new MdTableRow("Line2", address.Line2));
                    if (!String.IsNullOrEmpty(address.Line3))
                        addressTableRows.Add(new MdTableRow("Line3", address.Line3));
                    if (!String.IsNullOrEmpty(address.Longitude))
                        addressTableRows.Add(new MdTableRow("Longitude", address.Longitude));
                    if (!String.IsNullOrEmpty(address.PostalCode))
                        addressTableRows.Add(new MdTableRow("PostalCode", address.PostalCode));
                    if (!String.IsNullOrEmpty(address.PostOfficeBox))
                        addressTableRows.Add(new MdTableRow("PostOfficeBox", address.PostOfficeBox));
                    if (!String.IsNullOrEmpty(address.PrimaryContactName))
                        addressTableRows.Add(new MdTableRow("PrimaryContactName", address.PrimaryContactName));
                    if (!String.IsNullOrEmpty(address.ShippingMethodCode))
                        addressTableRows.Add(new MdTableRow("ShippingMethodCode", address.ShippingMethodCode));
                    if (!String.IsNullOrEmpty(address.StateOrProvince))
                        addressTableRows.Add(new MdTableRow("StateOrProvince", address.StateOrProvince));
                    if (!String.IsNullOrEmpty(address.Telephone1))
                        addressTableRows.Add(new MdTableRow("Telephone1", address.Telephone1));
                    if (!String.IsNullOrEmpty(address.Telephone2))
                        addressTableRows.Add(new MdTableRow("Telephone2", address.Telephone2));
                    if (!String.IsNullOrEmpty(address.Telephone3))
                        addressTableRows.Add(new MdTableRow("Telephone3", address.Telephone3));
                    if (!String.IsNullOrEmpty(address.TimeZoneRuleVersionNumber))
                        addressTableRows.Add(new MdTableRow("TimeZoneRuleVersionNumber", address.TimeZoneRuleVersionNumber));
                    if (!String.IsNullOrEmpty(address.UPSZone))
                        addressTableRows.Add(new MdTableRow("UPSZone", address.UPSZone));
                    if (!String.IsNullOrEmpty(address.UTCOffset))
                        addressTableRows.Add(new MdTableRow("UTCOffset", address.UTCOffset));
                    if (!String.IsNullOrEmpty(address.UTCConversionTimeZoneCode))
                        addressTableRows.Add(new MdTableRow("UTCConversionTimeZoneCode", address.UTCConversionTimeZoneCode));
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Value"), addressTableRows));
                }
            }
        }


        private void addEnvironmentVariables()
        {
            solutionDoc.Root.Add(new MdHeading("Environment Variables", 3));
            foreach (EnvironmentVariableEntity environmentVariable in content.solution.EnvironmentVariables.OrderBy(e => e.DisplayName))
            {
                solutionDoc.Root.Add(new MdHeading(environmentVariable.DisplayName, 4));
                List<MdTableRow> environmentVariableTableRows = new List<MdTableRow>();
                environmentVariableTableRows.Add(new MdTableRow("Internal Name", environmentVariable.Name));
                environmentVariableTableRows.Add(new MdTableRow("Type", environmentVariable.getTypeDisplayName()));
                environmentVariableTableRows.Add(new MdTableRow("Default Value", environmentVariable.DefaultValue ?? ""));
                environmentVariableTableRows.Add(new MdTableRow("Description", environmentVariable.DescriptionDefault ?? ""));
                environmentVariableTableRows.Add(new MdTableRow("Is Required", environmentVariable.IsRequired ? "Yes" : "No"));
                environmentVariableTableRows.Add(new MdTableRow("Is Customizable", environmentVariable.IsCustomizable ? "Yes" : "No"));
                environmentVariableTableRows.Add(new MdTableRow("IntroducedVersion", environmentVariable.IntroducedVersion));
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Value"), environmentVariableTableRows));
                if (environmentVariable.LocalizedNames.Count > 0 || environmentVariable.Descriptions.Count > 0)
                {
                    var langCodes = environmentVariable.LocalizedNames.Keys
                        .Union(environmentVariable.Descriptions.Keys)
                        .OrderBy(k => k).ToList();
                    List<MdTableRow> localizedRows = new List<MdTableRow>();
                    foreach (string langCode in langCodes)
                    {
                        environmentVariable.LocalizedNames.TryGetValue(langCode, out string name);
                        environmentVariable.Descriptions.TryGetValue(langCode, out string description);
                        localizedRows.Add(new MdTableRow(langCode, name ?? "", description ?? ""));
                    }
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Language Code", "Name", "Description"), localizedRows));
                }
            }
        }

        private void addSolutionComponents()
        {
            solutionDoc.Root.Add(new MdHeading("Solution Components", 2));
            solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("This solution contains the following components")));

            // Build a list of all sections with their display headings for correct alphabetical ordering
            var sections = new List<(string SortName, string ComponentType)>();
            if (content.solution.EnvironmentVariables.Count > 0)
            {
                sections.Add((GetComponentSectionHeading("EnvironmentVariable"), "EnvironmentVariable"));
            }
            if (content.agents.Count > 0)
            {
                sections.Add((GetComponentSectionHeading("Agent"), "Agent"));
            }
            if (content.dataflows.Count > 0)
            {
                sections.Add((GetComponentSectionHeading("Dataflow"), "Dataflow"));
            }
            if (content.solution.AppActions.Count > 0)
            {
                sections.Add((GetComponentSectionHeading("AppAction"), "AppAction"));
            }
            if (content.solution.SettingDefinitions.Count > 0)
            {
                sections.Add((GetComponentSectionHeading("SettingDefinition"), "SettingDefinition"));
            }
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                sections.Add((GetComponentSectionHeading("FormulaDefinition"), "FormulaDefinition"));
            }
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                sections.Add((GetComponentSectionHeading(componentType), componentType));
            }

            foreach (var section in sections.OrderBy(s => s.SortName, StringComparer.OrdinalIgnoreCase))
            {
                switch (section.ComponentType)
                {
                    case "EnvironmentVariable":
                        addEnvironmentVariables();
                        break;
                    case "Role":
                        renderSecurityRoles();
                        break;
                    case "Entity":
                        renderEntities();
                        break;
                    case "Option Set":
                        renderOptionSets();
                        break;
                    case "Workflow":
                        renderWorkflows();
                        break;
                    case "AI Project":
                        renderAIModels();
                        break;
                    case "Agent":
                        renderAgents();
                        break;
                    case "Dataflow":
                        renderDataflows();
                        break;
                    case "Web Resource":
                        renderWebResources();
                        break;
                    case "AppAction":
                        renderAppActions();
                        break;
                    case "SettingDefinition":
                        renderSettingDefinitions();
                        break;
                    case "FormulaDefinition":
                        renderFormulaDefinitions();
                        break;
                    default:
                        solutionDoc.Root.Add(new MdHeading(section.ComponentType, 3));
                        List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == section.ComponentType).ToList();
                        var sortedComponents = components
                            .Select(c => (comp: c, displayName: content.GetDisplayNameForComponent(c)))
                            .OrderBy(x => x.displayName, StringComparer.OrdinalIgnoreCase).ToList();
                        List<MdTableRow> componentTableRows = new List<MdTableRow>();
                        foreach (var (comp, compName) in sortedComponents)
                        {
                            MdSpan cell = GetCrossDocLinkMdForComponent(comp, compName);
                            componentTableRows.Add(new MdTableRow(cell));
                        }
                        if (componentTableRows.Count > 0)
                        {
                            solutionDoc.Root.Add(new MdTable(new MdTableRow(section.ComponentType), componentTableRows));
                        }
                        break;
                }
            }

            // Business Process Flows
            if (content.businessProcessFlows.Count > 0)
            {
                renderBusinessProcessFlows();
            }

            // Solution Component Relationships graph
            if (File.Exists(content.folderPath + "/solution-components.svg"))
            {
                solutionDoc.Root.Add(new MdHeading("Solution Component Relationships", 2));
                solutionDoc.Root.Add(new MdParagraph(new MdImageSpan("Solution Component Relationships", "solution-components.svg")));
            }

            solutionDoc.Root.Add(new MdHeading("Solution Component Dependencies", 2));
            List<string> dependencies = content
                                        .solution
                                        .Dependencies
                                        .GroupBy(p => p.Required.reqdepSolution)
                                        .Select(g => g.First())
                                        .OrderBy(t => t.Required.reqdepSolution)
                                        .Select(t => t.Required.reqdepSolution)
                                        .ToList();
            if (dependencies.Count > 0)
            {
                solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("This solution has the following dependencies: ")));
                foreach (string solution in dependencies)
                {
                    solutionDoc.Root.Add(new MdHeading("Solution: " + solution, 3));
                    foreach (SolutionDependency dependency in content.solution.Dependencies.Where(p => p.Required.reqdepSolution.Equals(solution)))
                    {
                        List<MdTableRow> dependencyTableRows = new List<MdTableRow>();
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepDisplayName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepDisplayName))
                            dependencyTableRows.Add(new MdTableRow("Display Name", dependency.Required.reqdepDisplayName ?? "", dependency.Dependent.reqdepDisplayName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.Type) || !String.IsNullOrEmpty(dependency.Dependent.Type))
                            dependencyTableRows.Add(new MdTableRow("Type", dependency.Required.Type ?? "", dependency.Dependent.Type ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.SchemaName) || !String.IsNullOrEmpty(dependency.Dependent.SchemaName))
                            dependencyTableRows.Add(new MdTableRow("Schema Name", dependency.Required.SchemaName ?? "", dependency.Dependent.SchemaName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepSolution) || !String.IsNullOrEmpty(dependency.Dependent.reqdepSolution))
                            dependencyTableRows.Add(new MdTableRow("Solution", dependency.Required.reqdepSolution ?? "", dependency.Dependent.reqdepSolution ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.ID) || !String.IsNullOrEmpty(dependency.Dependent.ID))
                            dependencyTableRows.Add(new MdTableRow("ID", dependency.Required.ID ?? "", dependency.Dependent.ID ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepIdSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepIdSchemaName))
                            dependencyTableRows.Add(new MdTableRow("ID Schema Name", dependency.Required.reqdepIdSchemaName ?? "", dependency.Dependent.reqdepIdSchemaName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepParentDisplayName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepParentDisplayName))
                            dependencyTableRows.Add(new MdTableRow("Parent Display Name", dependency.Required.reqdepParentDisplayName ?? "", dependency.Dependent.reqdepParentDisplayName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepParentSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepParentSchemaName))
                            dependencyTableRows.Add(new MdTableRow("Parent Schema Name", dependency.Required.reqdepParentSchemaName ?? "", dependency.Dependent.reqdepParentSchemaName ?? ""));
                        if (dependencyTableRows.Count > 0)
                        {
                            solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Required Component", "Required By"), dependencyTableRows));
                        }
                    }
                }
            }
            else
            {
                solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("This solution has no dependencies.")));
            }
        }

        private void renderWorkflows()
        {
            solutionDoc.Root.Add(new MdHeading("Workflow", 3));
            List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == "Workflow").ToList();
            if (components.Count > 0)
            {
                var sortedComponents = components
                    .Select(c => (comp: c, parts: content.GetWorkflowDisplayParts(c)))
                    .OrderBy(x => x.parts.Name, StringComparer.OrdinalIgnoreCase).ToList();
                List<MdTableRow> rows = new List<MdTableRow>();
                foreach (var (comp, parts) in sortedComponents)
                {
                    MdSpan nameCell = GetCrossDocLinkMdForComponent(comp, parts.Name);
                    rows.Add(new MdTableRow(nameCell, new MdTextSpan(parts.TriggerInfo), new MdTextSpan(parts.FlowType)));
                }
                if (rows.Count > 0)
                {
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Name", "Trigger Type", "Flow Type"), rows));
                }
            }
        }

        private void renderBusinessProcessFlows()
        {
            solutionDoc.Root.Add(new MdHeading("Business Process Flows", 2));
            solutionDoc.Root.Add(new MdParagraph(new MdTextSpan($"This solution contains {content.businessProcessFlows.Count} Business Process Flow(s).")));
            List<MdTableRow> rows = new List<MdTableRow>();
            foreach (var bpf in content.businessProcessFlows.OrderBy(b => b.GetDisplayName(), StringComparer.OrdinalIgnoreCase))
            {
                string entity = content.context?.GetTableDisplayName(bpf.PrimaryEntity) ?? bpf.PrimaryEntity ?? "";
                rows.Add(new MdTableRow(bpf.GetDisplayName(), entity, bpf.Stages.Count.ToString(), bpf.GetStateLabel()));
            }
            if (rows.Count > 0)
            {
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Name", "Primary Entity", "Stages", "State"), rows));
            }
        }

        private void renderAIModels()
        {
            solutionDoc.Root.Add(new MdHeading("AI Models", 3));
            List<AIModel> aiModels = content.solution.Customizations.getAIModels();
            if (aiModels.Count > 0)
            {
                List<MdTableRow> rows = new List<MdTableRow>();
                foreach (AIModel aiModel in aiModels.OrderBy(o => o.getName()))
                {
                    string modelName = aiModel.getName();
                    MdSpan cell = new MdLinkSpan(modelName, CrossDocLinkHelper.GetAIModelDocMdPath(modelName));
                    rows.Add(new MdTableRow(cell));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("AI Model"), rows));
            }
        }

        private void renderAgents()
        {
            solutionDoc.Root.Add(new MdHeading("Agents", 3));
            if (content.agents.Count > 0)
            {
                List<MdTableRow> rows = new List<MdTableRow>();
                foreach (AgentEntity agent in content.agents.OrderBy(a => a.Name))
                {
                    MdSpan cell = (content.context?.Config?.documentAgents == true)
                        ? (MdSpan)new MdLinkSpan(agent.Name, CrossDocLinkHelper.GetAgentDocMdPath(agent.Name))
                        : new MdTextSpan(agent.Name);
                    rows.Add(new MdTableRow(cell));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Agent"), rows));
            }
        }

        private void renderDataflows()
        {
            solutionDoc.Root.Add(new MdHeading("Dataflows", 3));
            if (content.dataflows.Count > 0)
            {
                List<MdTableRow> rows = new List<MdTableRow>();
                foreach (DataflowEntity df in content.dataflows.OrderBy(d => d.GetDisplayName()))
                {
                    MdSpan cell = (content.context?.Config?.documentDataflows == true)
                        ? (MdSpan)new MdLinkSpan(df.GetDisplayName(), CrossDocLinkHelper.GetDataflowDocMdPath(df.GetDisplayName()))
                        : new MdTextSpan(df.GetDisplayName());
                    rows.Add(new MdTableRow(cell, new MdTextSpan(df.Queries.Count.ToString()), new MdTextSpan(df.GetStateLabel())));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Dataflow", "Queries", "State"), rows));
            }
        }

        private void renderWebResources()
        {
            solutionDoc.Root.Add(new MdHeading("Web Resources", 3));
            List<WebResourceEntity> webResources = content.solution.Customizations.getWebResources();
            if (webResources.Count > 0)
            {
                string wrPagePath = CrossDocLinkHelper.GetWebResourceDocMdPath(content.solution.UniqueName);
                solutionDoc.Root.Add(new MdParagraph(
                    new MdTextSpan("See the "),
                    new MdLinkSpan("dedicated Web Resources page", wrPagePath),
                    new MdTextSpan(" for full details, image previews, and source code.")));
                List<MdTableRow> rows = new List<MdTableRow>();
                foreach (WebResourceEntity wr in webResources)
                {
                    rows.Add(new MdTableRow(!string.IsNullOrEmpty(wr.DisplayName) ? wr.DisplayName : wr.Name ?? "", wr.Name ?? "", wr.GetTypeDisplayName(), wr.IntroducedVersion ?? ""));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Display Name", "Name", "Type", "Introduced Version"), rows));
            }
        }

        private void renderAppActions()
        {
            solutionDoc.Root.Add(new MdHeading("Command Bar Buttons", 3));
            if (content.solution.AppActions.Count > 0)
            {
                List<MdTableRow> rows = new List<MdTableRow>();
                foreach (AppActionEntity action in content.solution.AppActions.OrderBy(a => a.ButtonLabel ?? a.Name))
                {
                    rows.Add(new MdTableRow(
                        action.ButtonLabel ?? action.Name ?? "",
                        action.ContextEntity ?? "",
                        action.AppModuleName ?? "",
                        action.FontIcon ?? "",
                        action.IsHidden ? "Yes" : "No"));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Label", "Table", "App Module", "Icon", "Hidden"), rows));
            }
        }

        private void renderSettingDefinitions()
        {
            solutionDoc.Root.Add(new MdHeading("Setting Definitions", 3));
            if (content.solution.SettingDefinitions.Count > 0)
            {
                foreach (SettingDefinitionEntity setting in content.solution.SettingDefinitions.OrderBy(s => s.DisplayName ?? s.UniqueName))
                {
                    solutionDoc.Root.Add(new MdHeading(setting.DisplayName ?? setting.UniqueName, 4));
                    List<MdTableRow> rows = new List<MdTableRow>
                    {
                        new MdTableRow("Internal Name", setting.UniqueName ?? ""),
                        new MdTableRow("Data Type", setting.GetDataTypeDisplayName()),
                        new MdTableRow("Default Value", setting.DefaultValue ?? ""),
                        new MdTableRow("Description", setting.Description ?? ""),
                        new MdTableRow("Is Customizable", setting.IsCustomizable ? "Yes" : "No"),
                        new MdTableRow("Is Hidden", setting.IsHidden ? "Yes" : "No"),
                        new MdTableRow("Is Overridable", setting.IsOverridable ? "Yes" : "No")
                    };
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Value"), rows));
                }
            }
        }

        private void renderFormulaDefinitions()
        {
            solutionDoc.Root.Add(new MdHeading("Formulas", 3));
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                var grouped = content.solution.FormulaDefinitions
                    .OrderBy(f => f.TableName).ThenBy(f => f.ColumnName)
                    .GroupBy(f => f.TableName);
                foreach (var group in grouped)
                {
                    solutionDoc.Root.Add(new MdHeading(group.Key, 4));
                    List<MdTableRow> rows = new List<MdTableRow>();
                    foreach (FormulaDefinitionEntity formula in group)
                    {
                        rows.Add(new MdTableRow(formula.ColumnName ?? "", formula.Type ?? "", formula.Content ?? ""));
                    }
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Column", "Type", "Formula"), rows));
                }
            }
        }

        private void renderOptionSets()
        {
            solutionDoc.Root.Add(new MdHeading("Option Sets", 3));
            List<OptionSetEntity> optionSets = content.solution.Customizations.getOptionSets();
            if (optionSets.Count > 0)
            {
                foreach (OptionSetEntity optionSet in optionSets.OrderBy(o => o.GetDisplayName()))
                {
                    solutionDoc.Root.Add(new MdHeading(optionSet.GetDisplayName() + " (" + optionSet.Name + ")", 4));
                    List<MdTableRow> tableRows = new List<MdTableRow>
                    {
                        new MdTableRow("Type", optionSet.OptionSetType ?? ""),
                        new MdTableRow("Is Global", optionSet.IsGlobal ? "Yes" : "No"),
                        new MdTableRow("Is Customizable", optionSet.IsCustomizable ? "Yes" : "No")
                    };
                    if (!String.IsNullOrEmpty(optionSet.Description))
                        tableRows.Add(new MdTableRow("Description", optionSet.Description));
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Value"), tableRows));
                    
                    if (optionSet.Options.Count > 0)
                    {
                        solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("Options:")));
                        List<MdTableRow> optionTableRows = new List<MdTableRow>();
                        foreach (OptionSetOption option in optionSet.Options)
                        {
                            optionTableRows.Add(new MdTableRow(option.Value ?? "", option.Label ?? ""));
                        }
                        solutionDoc.Root.Add(new MdTable(new MdTableRow("Value", "Label"), optionTableRows));
                    }
                }
            }
        }

        private void renderSecurityRoles()
        {
            solutionDoc.Root.Add(new MdHeading("Security Roles", 3));
            foreach (RoleEntity role in content.solution.Customizations.getRoles().OrderBy(r => r.Name))
            {
                solutionDoc.Root.Add(new MdHeading(role.Name + " (" + role.ID + ")", 4));
                List<MdTableRow> componentTableRows = new List<MdTableRow>();
                foreach (TableAccess tableAccess in role.Tables.OrderBy(o => o.Name))
                {
                    MdTableRow row = new MdTableRow(tableAccess.Name,
                                           getAccessLevelIcon(tableAccess.Create),
                                           getAccessLevelIcon(tableAccess.Read),
                                           getAccessLevelIcon(tableAccess.Write),
                                           getAccessLevelIcon(tableAccess.Delete),
                                           getAccessLevelIcon(tableAccess.Append),
                                           getAccessLevelIcon(tableAccess.AppendTo),
                                           getAccessLevelIcon(tableAccess.Assign),
                                           getAccessLevelIcon(tableAccess.Share)
                    );
                    componentTableRows.Add(row);
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Table", "Create", "Read", "Write", "Delete", "Append", "Append To", "Assign", "Share"), componentTableRows));

                if (role.miscellaneousPrivileges.Count > 0)
                {
                    solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("Miscellaneous Privileges associated with this role:")));
                    List<MdTableRow> miscPrivTableRows = new List<MdTableRow>();
                    foreach (KeyValuePair<string, string> miscPrivilege in role.miscellaneousPrivileges)
                    {
                        miscPrivTableRows.Add(new MdTableRow(miscPrivilege.Key, getAccessLevelIcon(miscPrivilege.Value)));
                    }
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Miscellaneous Privilege", "Level"), miscPrivTableRows));
                }
            }
        }

        private void renderEntities()
        {
            solutionDoc.Root.Add(new MdHeading("Tables", 3));
            foreach (TableEntity tableEntity in content.solution.Customizations.getEntities().OrderBy(e => e.getLocalizedName()))
            {
                solutionDoc.Root.Add(new MdHeading(tableEntity.getLocalizedName() + " (" + tableEntity.getName() + ")", 4));
                List<MdTableRow> tableRows = new List<MdTableRow>
                {
                    new MdTableRow("Primary Column", tableEntity.getPrimaryColumn()),
                    new MdTableRow("Description", tableEntity.getDescription()),
                    new MdTableRow("Entity Set Name", tableEntity.GetEntitySetName()),
                    new MdTableRow("Record Ownership", tableEntity.GetOwnershipType()),
                    new MdTableRow("Auditing", tableEntity.IsAuditEnabled()?"Enabled":"Disabled"),
                    new MdTableRow("Customizable", tableEntity.IsCustomizable()?"Yes":"No"),
                    new MdTableRow("Change Tracking", tableEntity.IsChangeTrackingEnabled()?"Enabled":"Disabled"),
                    new MdTableRow("Is Activity", tableEntity.IsActivity()?"Yes":"No"),
                    new MdTableRow("Quick Create", tableEntity.IsQuickCreateEnabled()?"Enabled":"Disabled"),
                    new MdTableRow("Connections", tableEntity.IsConnectionsEnabled()?"Enabled":"Disabled"),
                    new MdTableRow("Duplicate Detection", tableEntity.IsDuplicateCheckSupported()?"Enabled":"Disabled"),
                    new MdTableRow("Mobile Visible", tableEntity.IsVisibleInMobile()?"Yes":"No"),
                    new MdTableRow("Introduced Version", tableEntity.GetIntroducedVersion())
                };
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Value"), tableRows));
                tableRows = new List<MdTableRow>();

                if (tableEntity.GetColumns().Count > 0)
                {
                    var columns = documentDefaultColumns
                        ? tableEntity.GetColumns()
                        : tableEntity.GetColumns().Where(c => !c.isDefaultColumn()).ToList();
                    if (columns.Count > 0)
                    {
                    solutionDoc.Root.Add(new MdHeading("Columns", 5));
                    foreach (ColumnEntity columnEntity in columns)
                    {
                        string primaryNameColumn = columnEntity.getDisplayMask().Contains("PrimaryName") ? " (Primary name column)" : "";
                        tableRows.Add(new MdTableRow(columnEntity.getDisplayName() + primaryNameColumn,
                                                    columnEntity.getLogicalName(),
                                                    columnEntity.getName(),
                                                    columnEntity.getDataType()));
                    }
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Display Name", "Logical Name", "Name", "Data type"), tableRows));

                    foreach (ColumnEntity columnEntity in columns)
                    {
                        string primaryNameColumn = columnEntity.getDisplayMask().Contains("PrimaryName") ? " (Primary name column)" : "";
                        string columnHeading = !String.IsNullOrEmpty(columnEntity.getDisplayName())
                            ? columnEntity.getDisplayName() + " (" + columnEntity.getLogicalName() + ")"
                            : columnEntity.getLogicalName();
                        solutionDoc.Root.Add(new MdHeading(columnHeading + primaryNameColumn, 6));
                        List<MdTableRow> propRows = new List<MdTableRow>
                        {
                            new MdTableRow("Display Name", columnEntity.getDisplayName()),
                            new MdTableRow("Logical Name", columnEntity.getLogicalName()),
                            new MdTableRow("Physical Name", columnEntity.getName()),
                            new MdTableRow("Data Type", columnEntity.getDataType()),
                            new MdTableRow("Custom Field", columnEntity.IsCustomField()?"Yes":"No"),
                            new MdTableRow("Auditing", columnEntity.IsAuditEnabled()?"Enabled":"Disabled"),
                            new MdTableRow("Customizable", columnEntity.isCustomizable().ToString()),
                            new MdTableRow("Required", columnEntity.isRequired().ToString()),
                            new MdTableRow("Searchable", columnEntity.isSearchable().ToString()),
                            new MdTableRow("Secured", columnEntity.IsSecured()?"Yes":"No"),
                            new MdTableRow("Filterable", columnEntity.IsFilterable()?"Yes":"No")
                        };
                        solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Value"), propRows));
                    }
                    }
                }

                if (tableEntity.GetForms().Count > 0)
                {
                    solutionDoc.Root.Add(new MdHeading("Forms", 5));
                    List<MdTableRow> formRows = new List<MdTableRow>();
                    foreach (FormEntity formEntity in tableEntity.GetForms())
                    {
                        formRows.Add(new MdTableRow(
                            formEntity.GetFormName(),
                            formEntity.GetFormTypeDisplayName(),
                            formEntity.IsDefault() ? "Yes" : "No",
                            formEntity.IsActive() ? "Active" : "Inactive",
                            formEntity.IsCustomizable() ? "Yes" : "No"
                        ));
                    }
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Name", "Type", "Default", "State", "Customizable"), formRows));

                    // Generate SVG mockup files for forms
                    Dictionary<string, string> columnDisplayNames = tableEntity.GetColumns().ToDictionary(c => c.getLogicalName(), c => c.getDisplayName(), StringComparer.OrdinalIgnoreCase);
                    Dictionary<string, string> formSvgFiles = FormSvgBuilder.GenerateFormSvgs(tableEntity, content.folderPath, columnDisplayNames);

                    foreach (FormEntity formEntity in tableEntity.GetForms())
                    {
                        List<FormTab> tabs = formEntity.GetTabs();
                        if (tabs.Count > 0)
                        {
                            string formTypeLabel = formEntity.GetFormTypeDisplayName();
                            solutionDoc.Root.Add(new MdHeading("Form (" + formTypeLabel + "): " + formEntity.GetFormName(), 6));

                            // SVG wireframe mockup image
                            string formKey = formEntity.GetFormName() + "|" + formTypeLabel;
                            if (formSvgFiles.TryGetValue(formKey, out string svgFile))
                            {
                                solutionDoc.Root.Add(new MdParagraph(new MdImageSpan("Form layout: " + formEntity.GetFormName(), svgFile)));
                            }

                            // Rendering Forms visually now, keeping this code for reference
                            // foreach (FormTab tab in tabs)
                            // {
                            //     solutionDoc.Root.Add(new MdParagraph(new MdStrongEmphasisSpan("Tab: " + tab.GetName() + (tab.IsVisible() ? "" : " (hidden)"))));
                            //     foreach (FormSection section in tab.GetSections())
                            //     {
                            //         List<FormControl> controls = section.GetControls();
                            //         if (controls.Count > 0)
                            //         {
                            //             solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("Section: " + section.GetName() + (section.IsVisible() ? "" : " (hidden)"))));
                            //             List<MdTableRow> controlRows = new List<MdTableRow>();
                            //             int controlIndex = 1;
                            //             foreach (FormControl control in controls)
                            //             {
                            //                 string fieldName = !String.IsNullOrEmpty(control.GetDataFieldName()) ? control.GetDataFieldName() : control.GetId();
                            //                 controlRows.Add(new MdTableRow(controlIndex.ToString(), control.GetId(), fieldName));
                            //                 controlIndex++;
                            //             }
                            //             solutionDoc.Root.Add(new MdTable(new MdTableRow("#", "Control", "Field"), controlRows));
                            //         }
                            //     }
                            // }
                        }
                    }
                }

                if (tableEntity.GetViews().Count > 0)
                {
                    solutionDoc.Root.Add(new MdHeading("Views", 5));
                    List<MdTableRow> viewRows = new List<MdTableRow>();
                    foreach (ViewEntity viewEntity in tableEntity.GetViews())
                    {
                        viewRows.Add(new MdTableRow(
                            viewEntity.GetViewName(),
                            viewEntity.GetQueryTypeDisplayName(),
                            viewEntity.IsDefault() ? "Yes" : "No",
                            viewEntity.IsCustomizable() ? "Yes" : "No"
                        ));
                    }
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Name", "Type", "Default", "Customizable"), viewRows));

                    Dictionary<string, string> columnDisplayNames = tableEntity.GetColumns().ToDictionary(c => c.getLogicalName(), c => c.getDisplayName(), StringComparer.OrdinalIgnoreCase);
                    foreach (ViewEntity viewEntity in tableEntity.GetViews())
                    {
                        List<ViewColumn> viewColumns = viewEntity.GetColumns();
                        if (viewColumns.Count > 0)
                        {
                            solutionDoc.Root.Add(new MdHeading("View: " + viewEntity.GetViewName(), 6));
                            List<MdTableRow> colRows = new List<MdTableRow>();
                            foreach (ViewColumn vc in viewColumns)
                            {
                                string colName = vc.GetName();
                                string displayName = columnDisplayNames.TryGetValue(colName, out string dn) && !String.IsNullOrEmpty(dn) ? dn + " (" + colName + ")" : colName;
                                colRows.Add(new MdTableRow(vc.Order.ToString(), displayName, vc.GetWidth()));
                            }
                            solutionDoc.Root.Add(new MdTable(new MdTableRow("#", "Column", "Width"), colRows));

                            // View controls table (sort orders, filters)
                            List<ViewSortOrder> sortOrders = viewEntity.GetSortOrders();
                            ViewFilter filter = viewEntity.GetFilter();
                            string filterText = filter?.ToDisplayString(columnDisplayNames) ?? "";
                            if (sortOrders.Count > 0 || !string.IsNullOrEmpty(filterText))
                            {
                                List<MdTableRow> controlRows = new List<MdTableRow>();
                                if (sortOrders.Count > 0)
                                {
                                    string sortText = string.Join(", ", sortOrders.Select(s => s.ToDisplayString(columnDisplayNames)));
                                    controlRows.Add(new MdTableRow("Sort by", sortText));
                                }
                                if (!string.IsNullOrEmpty(filterText))
                                {
                                    controlRows.Add(new MdTableRow("Filter", filterText));
                                }
                                solutionDoc.Root.Add(new MdTable(new MdTableRow("View Controls", "Details"), controlRows));
                            }
                        }
                    }
                }
            }
            // Ribbon Customization Summary
            List<RibbonCustomizationEntity> ribbonCustomizations = content.solution.Customizations.getRibbonCustomizations();
            if (ribbonCustomizations.Count > 0)
            {
                solutionDoc.Root.Add(new MdHeading("Ribbon Customizations", 4));
                List<MdTableRow> ribbonRows = new List<MdTableRow>();
                foreach (RibbonCustomizationEntity ribbon in ribbonCustomizations)
                {
                    ribbonRows.Add(new MdTableRow(
                        ribbon.EntityName ?? "",
                        ribbon.HiddenActions.Count.ToString(),
                        ribbon.CommandDefinitionCount.ToString(),
                        ribbon.DisplayRuleCount.ToString(),
                        ribbon.EnableRuleCount.ToString()));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Table", "Hidden Actions", "Command Definitions", "Display Rules", "Enable Rules"), ribbonRows));
            }
            solutionDoc.Root.Add(new MdHeading("Table Relationships", 4));
            solutionDoc.Root.Add(new MdParagraph(new MdImageSpan("Dataverse Table Relationships", "dataverse.svg")));
        }

        private MdImageSpan getAccessLevelIcon(AccessLevel accessLevel)
        {
            Directory.CreateDirectory(content.folderPath + "Resources");
            string iconFile = @"Resources\security-role-access-level-";
            iconFile += accessLevel switch
            {
                AccessLevel.Global => "global.png",
                AccessLevel.Deep => "deep.png",
                AccessLevel.Local => "local.png",
                AccessLevel.Basic => "basic.png",
                _ => "none.png",
            };
            if (!File.Exists(content.folderPath + iconFile))
                File.Copy(AssemblyHelper.GetExecutablePath() + iconFile, content.folderPath + iconFile);
            return new MdImageSpan(accessLevel.ToString(), iconFile.Replace(@"\", "/"));
        }

        private MdImageSpan getAccessLevelIcon(string accessLevel)
        {
            AccessLevel level = accessLevel switch
            {
                "Global" => AccessLevel.Global,
                "Deep" => AccessLevel.Deep,
                "Loca" => AccessLevel.Local,
                "Basic" => AccessLevel.Basic,
                _ => AccessLevel.None
            };
            return getAccessLevelIcon(level);
        }

        private MdSpan GetCrossDocLinkMdForComponent(SolutionComponent component, string displayName)
        {
            string mdPath = null;
            switch (component.Type)
            {
                case "Workflow":
                    if (content.context?.Config?.documentFlows == true)
                    {
                        FlowEntity flow = content.context.GetFlowById(component.ID);
                        if (flow != null)
                            mdPath = CrossDocLinkHelper.GetFlowDocMdPath(flow.Name);
                    }
                    break;
                case "Canvas App":
                    if (content.context?.Config?.documentApps == true)
                    {
                        string appName = content.context.GetAppNameBySchemaName(component.SchemaName);
                        AppEntity app = content.context.GetAppByName(appName);
                        if (app != null)
                            mdPath = CrossDocLinkHelper.GetAppDocMdPath(app.Name);
                    }
                    break;
                case "Model-Driven App":
                    if (content.context?.Config?.documentModelDrivenApps == true)
                    {
                        AppModuleEntity mda = content.appModules?.FirstOrDefault(a =>
                            a.UniqueName?.Equals(component.SchemaName, StringComparison.OrdinalIgnoreCase) == true);
                        if (mda != null)
                            mdPath = CrossDocLinkHelper.GetMDADocMdPath(mda.GetDisplayName());
                    }
                    break;
            }

            if (mdPath != null)
                return new MdLinkSpan(displayName, mdPath);
            return new MdTextSpan(displayName);
        }

        private void createOrderFile()
        {
            using var sw = new StreamWriter($"{content.folderPath}/.order");
            foreach (var flow in content.flows)
            {
                sw.WriteLine(CharsetHelper.GetSafeName(@"FlowDoc " + flow.Name));
            }

            foreach (var app in content.apps)
            {
                sw.WriteLine(CharsetHelper.GetSafeName(@"AppDoc " + app.Name));
            }

            foreach (var agent in content.agents)
            {
                sw.WriteLine(CharsetHelper.GetSafeName(@"AgentDoc " + agent.Name));
            }

            foreach (var df in content.dataflows)
            {
                sw.WriteLine(CharsetHelper.GetSafeName(@"DataflowDoc " + df.GetDisplayName()));
            }
        }
    }
}
