using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PowerDocu.Common;

namespace PowerDocu.SolutionDocumenter
{
    class SolutionHtmlBuilder : HtmlBuilder
    {
        private readonly SolutionDocumentationContent content;
        private readonly string solutionFileName;
        private readonly bool documentDefaultColumns;
        private IReadOnlyList<RoleEntity>? _sortedRoles;
        private IReadOnlyList<TableEntity>? _sortedEntities;
        private IReadOnlyList<WebResourceEntity>? _sortedWebResources;

        private IReadOnlyList<RoleEntity> GetSortedRoles()
            => _sortedRoles ??= content.solution.Customizations.getRoles().OrderBy(r => r.Name).ToList();

        private IReadOnlyList<TableEntity> GetSortedEntities()
            => _sortedEntities ??= content.solution.Customizations.getEntities().OrderBy(e => e.getLocalizedName()).ToList();

        private IReadOnlyList<WebResourceEntity> GetSortedWebResources()
            => _sortedWebResources ??= content.solution.Customizations.getWebResources().OrderBy(w => w.DisplayName ?? w.Name).ToList();

        public SolutionHtmlBuilder(SolutionDocumentationContent contentDocumentation, bool documentDefaultColumns = false)
        {
            content = contentDocumentation;
            this.documentDefaultColumns = documentDefaultColumns;
            Directory.CreateDirectory(content.folderPath);
            WriteDefaultStylesheet(content.folderPath);

            solutionFileName = ("solution-" + content.filename + ".html").Replace(" ", "-");

            StringBuilder body = new StringBuilder();
            addSolutionOverview(body);
            addSolutionComponents(body);

            SaveHtmlFile(Path.Combine(content.folderPath, solutionFileName),
                WrapInHtmlPage("Solution - " + content.solution.UniqueName, body.ToString(), getNavigationHtml()));
            NotificationHelper.SendNotification("Created HTML documentation for solution " + content.solution.UniqueName);
        }

        private string getNavigationHtml()
        {
            var navItems = new List<(string label, string href, int level)>
            {
                ("Solution Overview", solutionFileName, 0),
                ("Publisher Details", solutionFileName + "#publisher-details", 0),
                ("Statistics", solutionFileName + "#statistics", 0),
                ("Solution Components", solutionFileName + "#solution-components", 0)
            };

            // Build component section groups (level-1 entry + level-2 children), then sort alphabetically
            var componentSections = new List<List<(string label, string href, int level)>>();

            if (content.solution.EnvironmentVariables.Count > 0)
            {
                var section = new List<(string label, string href, int level)>
                {
                    ("Environment Variables", solutionFileName + "#environment-variables", 1)
                };
                foreach (EnvironmentVariableEntity envVar in content.solution.EnvironmentVariables.OrderBy(e => e.DisplayName))
                {
                    section.Add((envVar.DisplayName, solutionFileName + "#" + SanitizeAnchorId("envvar-" + envVar.Name), 2));
                }
                componentSections.Add(section);
            }

            if (content.agents.Count > 0)
            {
                var section = new List<(string label, string href, int level)>
                {
                    ("Agents", solutionFileName + "#agents", 1)
                };
                foreach (AgentEntity agent in content.agents.OrderBy(a => a.Name))
                {
                    section.Add((agent.Name, solutionFileName + "#" + SanitizeAnchorId("agent-" + agent.Name), 2));
                }
                componentSections.Add(section);
            }

            if (content.dataflows.Count > 0)
            {
                var section = new List<(string label, string href, int level)>
                {
                    ("Dataflows", solutionFileName + "#dataflows", 1)
                };
                foreach (DataflowEntity df in content.dataflows.OrderBy(d => d.GetDisplayName()))
                {
                    section.Add((df.GetDisplayName(), solutionFileName + "#" + SanitizeAnchorId("dataflow-" + df.GetDisplayName()), 2));
                }
                componentSections.Add(section);
            }

            if (content.solution.AppActions.Count > 0)
            {
                componentSections.Add(new List<(string label, string href, int level)>
                {
                    ("Command Bar Buttons", solutionFileName + "#app-actions", 1)
                });
            }

            if (content.solution.SettingDefinitions.Count > 0)
            {
                componentSections.Add(new List<(string label, string href, int level)>
                {
                    ("Setting Definitions", solutionFileName + "#setting-definitions", 1)
                });
            }

            if (content.solution.FormulaDefinitions.Count > 0)
            {
                componentSections.Add(new List<(string label, string href, int level)>
                {
                    ("Formula Definitions", solutionFileName + "#formula-definitions", 1)
                });
            }

            foreach (string componentType in content.solution.GetComponentTypes())
            {
                string label = GetComponentSectionLabel(componentType);
                string anchorId = GetComponentSectionAnchorId(componentType);
                var section = new List<(string label, string href, int level)>
                {
                    (label, solutionFileName + "#" + anchorId, 1)
                };

                switch (componentType)
                {
                    case "Role":
                        foreach (RoleEntity role in GetSortedRoles())
                        {
                            section.Add((role.Name, solutionFileName + "#" + SanitizeAnchorId("role-" + role.Name), 2));
                        }
                        break;
                    case "Entity":
                        foreach (TableEntity table in GetSortedEntities())
                        {
                            string tableName = table.getLocalizedName();
                            if (String.IsNullOrEmpty(tableName)) tableName = table.getName();
                            section.Add((tableName, solutionFileName + "#" + SanitizeAnchorId("table-" + table.getName()), 2));
                        }
                        break;
                    case "Option Set":
                        foreach (OptionSetEntity optionSet in content.solution.Customizations.getOptionSets().OrderBy(o => o.GetDisplayName()))
                        {
                            string osName = optionSet.GetDisplayName();
                            if (String.IsNullOrEmpty(osName)) osName = optionSet.Name;
                            section.Add((osName, solutionFileName + "#" + SanitizeAnchorId("optionset-" + optionSet.Name), 2));
                        }
                        break;
                    case "AI Project":
                        foreach (AIModel aiModel in content.solution.Customizations.getAIModels().OrderBy(o => o.getName()))
                        {
                            section.Add((aiModel.getName(), solutionFileName + "#" + SanitizeAnchorId("aimodel-" + aiModel.getName()), 2));
                        }
                        break;
                    default:
                        var navSortedNames = content.solution.Components
                            .Where(c => c.Type == componentType)
                            .Select(c => content.GetDisplayNameForComponent(c))
                            .OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList();
                        foreach (string compName in navSortedNames)
                        {
                            section.Add((compName, solutionFileName + "#" + SanitizeAnchorId("comp-" + compName), 2));
                        }
                        break;
                }
                componentSections.Add(section);
            }

            foreach (var section in componentSections.OrderBy(s => s[0].label, StringComparer.OrdinalIgnoreCase))
            {
                navItems.AddRange(section);
            }

            navItems.Add(("Component Relationships", solutionFileName + "#component-relationships", 0));
            navItems.Add(("Dependencies", solutionFileName + "#dependencies", 0));

            // Add sub-entries for each dependency solution
            List<string> dependencySolutions = content
                .solution
                .Dependencies
                .GroupBy(p => p.Required.reqdepSolution)
                .Select(g => g.First())
                .OrderBy(t => t.Required.reqdepSolution)
                .Select(t => t.Required.reqdepSolution)
                .ToList();
            foreach (string solution in dependencySolutions)
            {
                navItems.Add((solution, solutionFileName + "#dep-" + SanitizeAnchorId(solution), 1));
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<div class=\"nav-title\">{Encode(content.solution.UniqueName)}</div>");
            sb.Append(NavigationList(navItems));
            return sb.ToString();
        }

        private void addSolutionOverview(StringBuilder body)
        {
            body.AppendLine(Heading(1, content.solution.UniqueName));
            body.Append(TableStart("Property", "Details"));
            body.Append(TableRow("Status", content.solution.isManaged ? "Managed" : "Unmanaged"));
            body.Append(TableRow("Version", content.solution.Version));
            body.Append(TableRow("Documentation generated at", PowerDocuReleaseHelper.GetTimestampWithVersion()));
            body.AppendLine(TableEnd());
            AddPublisherInfo(body);
            AddStatistics(body);
        }

        private void AddStatistics(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, "Statistics", "statistics"));
            body.Append(TableStart("Component Type", "Number of Components"));
            var statisticsEntries = new List<(string Name, string LinkHtml, int Count)>();
            if (content.solution.EnvironmentVariables.Count > 0)
            {
                string envLink = $"<a href=\"#environment-variables\">Environment Variables</a>";
                statisticsEntries.Add((GetComponentSectionLabel("EnvironmentVariable"), envLink, content.solution.EnvironmentVariables.Count));
            }
            if (content.agents.Count > 0)
            {
                string agentLink = $"<a href=\"#agents\">Agents</a>";
                statisticsEntries.Add((GetComponentSectionLabel("Agent"), agentLink, content.agents.Count));
            }
            if (content.dataflows.Count > 0)
            {
                string dfLink = $"<a href=\"#dataflows\">Dataflows</a>";
                statisticsEntries.Add((GetComponentSectionLabel("Dataflow"), dfLink, content.dataflows.Count));
            }
            if (content.solution.AppActions.Count > 0)
            {
                string link = $"<a href=\"#app-actions\">Command Bar Buttons</a>";
                statisticsEntries.Add((GetComponentSectionLabel("AppAction"), link, content.solution.AppActions.Count));
            }
            if (content.solution.SettingDefinitions.Count > 0)
            {
                string link = $"<a href=\"#setting-definitions\">Setting Definitions</a>";
                statisticsEntries.Add((GetComponentSectionLabel("SettingDefinition"), link, content.solution.SettingDefinitions.Count));
            }
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                string link = $"<a href=\"#formula-definitions\">Formula Definitions</a>";
                statisticsEntries.Add((GetComponentSectionLabel("FormulaDefinition"), link, content.solution.FormulaDefinitions.Count));
            }
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                int count = content.solution.Components.Where(c => c.Type == componentType).Count();
                string anchorId = GetComponentSectionAnchorId(componentType);
                string label = GetComponentSectionLabel(componentType);
                string link = $"<a href=\"#{Encode(anchorId)}\">{Encode(label)}</a>";
                statisticsEntries.Add((label, link, count));
            }
            foreach (var entry in statisticsEntries.OrderBy(e => e.Name, StringComparer.OrdinalIgnoreCase))
            {
                body.Append(TableRowRaw(entry.LinkHtml, Encode(entry.Count.ToString())));
            }
            body.AppendLine(TableEnd());
        }

        /// <summary>
        /// Returns the anchor ID used for the section heading of a given component type.
        /// Must be kept in sync with the IDs used in addSolutionComponents / render methods.
        /// </summary>
        private static string GetComponentSectionAnchorId(string componentType)
        {
            return componentType switch
            {
                "EnvironmentVariable" => "environment-variables",
                "Role" => "security-roles",
                "Entity" => "tables",
                "AI Project" => "ai-models",
                "Option Set" => "option-sets",
                "Agent" => "agents",
                "Dataflow" => "dataflows",
                "Web Resource" => "web-resources",
                "AppAction" => "app-actions",
                "SettingDefinition" => "setting-definitions",
                "FormulaDefinition" => "formula-definitions",
                _ => SanitizeAnchorId(componentType)
            };
        }

        /// <summary>
        /// Returns the display label for a component type section.
        /// Must be kept in sync with the heading text used in addSolutionComponents / render methods.
        /// </summary>
        private static string GetComponentSectionLabel(string componentType)
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
                "FormulaDefinition" => "Formula Definitions",
                _ => componentType
            };
        }

        private void AddPublisherInfo(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, "Publisher Details", "publisher-details"));
            body.Append(TableStart("Publisher", "Details"));
            body.Append(TableRow("Name", content.solution.Publisher.UniqueName));
            body.Append(TableRow("Email", content.solution.Publisher.EMailAddress));
            body.Append(TableRow("CustomizationPrefix", content.solution.Publisher.CustomizationPrefix));
            body.Append(TableRow("CustomizationOptionValuePrefix", content.solution.Publisher.CustomizationOptionValuePrefix));
            body.Append(TableRow("SupportingWebsiteUrl", content.solution.Publisher.SupportingWebsiteUrl));
            body.AppendLine(TableEnd());

            if (content.solution.Publisher.Descriptions.Count > 0)
            {
                body.AppendLine(Paragraph("Descriptions:"));
                body.Append(TableStart("Language Code", "Description"));
                foreach (KeyValuePair<string, string> description in content.solution.Publisher.Descriptions)
                {
                    body.Append(TableRow(description.Key, description.Value));
                }
                body.AppendLine(TableEnd());
            }
            if (content.solution.Publisher.LocalizedNames.Count > 0)
            {
                body.AppendLine(Paragraph("Localized Names:"));
                body.Append(TableStart("Language Code", "Description"));
                foreach (KeyValuePair<string, string> localizedName in content.solution.Publisher.LocalizedNames)
                {
                    body.Append(TableRow(localizedName.Key, localizedName.Value));
                }
                body.AppendLine(TableEnd());
            }
            if (content.solution.Publisher.Addresses.Count > 0)
            {
                body.AppendLine(Paragraph("Addresses:"));
                foreach (Address address in content.solution.Publisher.Addresses)
                {
                    body.Append(TableStart("Property", "Value"));
                    if (!String.IsNullOrEmpty(address.Name)) body.Append(TableRow("Name", address.Name));
                    if (!String.IsNullOrEmpty(address.AddressNumber)) body.Append(TableRow("AddressNumber", address.AddressNumber));
                    if (!String.IsNullOrEmpty(address.AddressTypeCode)) body.Append(TableRow("AddressTypeCode", address.AddressTypeCode));
                    if (!String.IsNullOrEmpty(address.City)) body.Append(TableRow("City", address.City));
                    if (!String.IsNullOrEmpty(address.County)) body.Append(TableRow("County", address.County));
                    if (!String.IsNullOrEmpty(address.Country)) body.Append(TableRow("Country", address.Country));
                    if (!String.IsNullOrEmpty(address.Fax)) body.Append(TableRow("Fax", address.Fax));
                    if (!String.IsNullOrEmpty(address.FreightTermsCode)) body.Append(TableRow("FreightTermsCode", address.FreightTermsCode));
                    if (!String.IsNullOrEmpty(address.ImportSequenceNumber)) body.Append(TableRow("ImportSequenceNumber", address.ImportSequenceNumber));
                    if (!String.IsNullOrEmpty(address.Latitude)) body.Append(TableRow("Latitude", address.Latitude));
                    if (!String.IsNullOrEmpty(address.Line1)) body.Append(TableRow("Line1", address.Line1));
                    if (!String.IsNullOrEmpty(address.Line2)) body.Append(TableRow("Line2", address.Line2));
                    if (!String.IsNullOrEmpty(address.Line3)) body.Append(TableRow("Line3", address.Line3));
                    if (!String.IsNullOrEmpty(address.Longitude)) body.Append(TableRow("Longitude", address.Longitude));
                    if (!String.IsNullOrEmpty(address.PostalCode)) body.Append(TableRow("PostalCode", address.PostalCode));
                    if (!String.IsNullOrEmpty(address.PostOfficeBox)) body.Append(TableRow("PostOfficeBox", address.PostOfficeBox));
                    if (!String.IsNullOrEmpty(address.PrimaryContactName)) body.Append(TableRow("PrimaryContactName", address.PrimaryContactName));
                    if (!String.IsNullOrEmpty(address.ShippingMethodCode)) body.Append(TableRow("ShippingMethodCode", address.ShippingMethodCode));
                    if (!String.IsNullOrEmpty(address.StateOrProvince)) body.Append(TableRow("StateOrProvince", address.StateOrProvince));
                    if (!String.IsNullOrEmpty(address.Telephone1)) body.Append(TableRow("Telephone1", address.Telephone1));
                    if (!String.IsNullOrEmpty(address.Telephone2)) body.Append(TableRow("Telephone2", address.Telephone2));
                    if (!String.IsNullOrEmpty(address.Telephone3)) body.Append(TableRow("Telephone3", address.Telephone3));
                    if (!String.IsNullOrEmpty(address.TimeZoneRuleVersionNumber)) body.Append(TableRow("TimeZoneRuleVersionNumber", address.TimeZoneRuleVersionNumber));
                    if (!String.IsNullOrEmpty(address.UPSZone)) body.Append(TableRow("UPSZone", address.UPSZone));
                    if (!String.IsNullOrEmpty(address.UTCOffset)) body.Append(TableRow("UTCOffset", address.UTCOffset));
                    if (!String.IsNullOrEmpty(address.UTCConversionTimeZoneCode)) body.Append(TableRow("UTCConversionTimeZoneCode", address.UTCConversionTimeZoneCode));
                    body.AppendLine(TableEnd());
                }
            }
        }

        private void addEnvironmentVariables(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Environment Variables", "environment-variables"));
            foreach (EnvironmentVariableEntity environmentVariable in content.solution.EnvironmentVariables.OrderBy(e => e.DisplayName))
            {
                body.AppendLine(HeadingWithId(4, environmentVariable.DisplayName, SanitizeAnchorId("envvar-" + environmentVariable.Name)));
                body.Append(TableStart("Property", "Value"));
                body.Append(TableRow("Internal Name", environmentVariable.Name));
                body.Append(TableRow("Type", environmentVariable.getTypeDisplayName()));
                body.Append(TableRow("Default Value", environmentVariable.DefaultValue ?? ""));
                body.Append(TableRow("Description", environmentVariable.DescriptionDefault ?? ""));
                body.Append(TableRow("IntroducedVersion", environmentVariable.IntroducedVersion));
                body.Append(TableRow("Is Required", environmentVariable.IsRequired ? "Yes" : "No"));
                body.Append(TableRow("Is Customizable", environmentVariable.IsCustomizable ? "Yes" : "No"));
                body.AppendLine(TableEnd());
                if (environmentVariable.LocalizedNames.Count > 0 || environmentVariable.Descriptions.Count > 0)
                {
                    var langCodes = environmentVariable.LocalizedNames.Keys
                        .Union(environmentVariable.Descriptions.Keys)
                        .OrderBy(k => k).ToList();
                    body.Append(TableStart("Language Code", "Name", "Description"));
                    foreach (string langCode in langCodes)
                    {
                        environmentVariable.LocalizedNames.TryGetValue(langCode, out string name);
                        environmentVariable.Descriptions.TryGetValue(langCode, out string description);
                        body.Append(TableRow(langCode, name ?? "", description ?? ""));
                    }
                    body.AppendLine(TableEnd());
                }
            }
        }

        private void addSolutionComponents(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, "Solution Components", "solution-components"));
            body.AppendLine(Paragraph("This solution contains the following components"));

            // Build a list of all sections with their display headings for correct alphabetical ordering
            var sections = new List<(string SortName, string ComponentType)>();
            if (content.solution.EnvironmentVariables.Count > 0)
            {
                sections.Add((GetComponentSectionLabel("EnvironmentVariable"), "EnvironmentVariable"));
            }
            if (content.agents.Count > 0)
            {
                sections.Add((GetComponentSectionLabel("Agent"), "Agent"));
            }
            if (content.dataflows.Count > 0)
            {
                sections.Add((GetComponentSectionLabel("Dataflow"), "Dataflow"));
            }
            if (content.solution.AppActions.Count > 0)
            {
                sections.Add((GetComponentSectionLabel("AppAction"), "AppAction"));
            }
            if (content.solution.SettingDefinitions.Count > 0)
            {
                sections.Add((GetComponentSectionLabel("SettingDefinition"), "SettingDefinition"));
            }
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                sections.Add((GetComponentSectionLabel("FormulaDefinition"), "FormulaDefinition"));
            }
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                sections.Add((GetComponentSectionLabel(componentType), componentType));
            }

            foreach (var section in sections.OrderBy(s => s.SortName, StringComparer.OrdinalIgnoreCase))
            {
                switch (section.ComponentType)
                {
                    case "EnvironmentVariable":
                        addEnvironmentVariables(body);
                        break;
                    case "Role":
                        renderSecurityRoles(body);
                        break;
                    case "Entity":
                        renderEntities(body);
                        break;
                    case "Option Set":
                        renderOptionSets(body);
                        break;
                    case "Workflow":
                        renderWorkflows(body);
                        break;
                    case "AI Project":
                        renderAIModels(body);
                        break;
                    case "Agent":
                        renderAgents(body);
                        break;
                    case "Dataflow":
                        renderDataflows(body);
                        break;
                    case "Web Resource":
                        renderWebResources(body);
                        break;
                    case "AppAction":
                        renderAppActions(body);
                        break;
                    case "SettingDefinition":
                        renderSettingDefinitions(body);
                        break;
                    case "FormulaDefinition":
                        renderFormulaDefinitions(body);
                        break;
                    default:
                        body.AppendLine(HeadingWithId(3, section.ComponentType, SanitizeAnchorId(section.ComponentType)));
                        List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == section.ComponentType).ToList();
                        if (components.Count > 0)
                        {
                            var sortedComponents = components
                                .Select(c => (comp: c, displayName: content.GetDisplayNameForComponent(c)))
                                .OrderBy(x => x.displayName, StringComparer.OrdinalIgnoreCase).ToList();
                            body.Append(TableStart(section.ComponentType));
                            foreach (var (comp, compName) in sortedComponents)
                            {
                                string anchorId = SanitizeAnchorId("comp-" + compName);
                                string cellContent = GetCrossDocLinkHtmlForComponent(comp, compName);
                                body.Append($"<tr id=\"{Encode(anchorId)}\"><td>{cellContent}</td></tr>");
                            }
                            body.AppendLine(TableEnd());
                        }
                        break;
                }
            }

            // Business Process Flows
            if (content.businessProcessFlows.Count > 0)
            {
                renderBusinessProcessFlows(body);
            }

            // Solution Component Relationships graph
            if (File.Exists(Path.Combine(content.folderPath, "solution-components.svg")))
            {
                body.AppendLine(HeadingWithId(2, "Solution Component Relationships", "component-relationships"));
                body.AppendLine(ParagraphRaw(Image("Solution Component Relationships", "solution-components.svg")));
            }

            // Dependencies
            body.AppendLine(HeadingWithId(2, "Solution Component Dependencies", "dependencies"));
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
                body.AppendLine(Paragraph("This solution has the following dependencies: "));
                foreach (string solution in dependencies)
                {
                    body.AppendLine(HeadingWithId(3, "Solution: " + solution, "dep-" + SanitizeAnchorId(solution)));
                    foreach (SolutionDependency dependency in content.solution.Dependencies.Where(p => p.Required.reqdepSolution.Equals(solution)))
                    {
                        body.Append(TableStart("Property", "Required Component", "Required By"));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepDisplayName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepDisplayName))
                            body.Append(TableRow("Display Name", dependency.Required.reqdepDisplayName ?? "", dependency.Dependent.reqdepDisplayName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.Type) || !String.IsNullOrEmpty(dependency.Dependent.Type))
                            body.Append(TableRow("Type", dependency.Required.Type ?? "", dependency.Dependent.Type ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.SchemaName) || !String.IsNullOrEmpty(dependency.Dependent.SchemaName))
                            body.Append(TableRow("Schema Name", dependency.Required.SchemaName ?? "", dependency.Dependent.SchemaName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepSolution) || !String.IsNullOrEmpty(dependency.Dependent.reqdepSolution))
                            body.Append(TableRow("Solution", dependency.Required.reqdepSolution ?? "", dependency.Dependent.reqdepSolution ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.ID) || !String.IsNullOrEmpty(dependency.Dependent.ID))
                            body.Append(TableRow("ID", dependency.Required.ID ?? "", dependency.Dependent.ID ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepIdSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepIdSchemaName))
                            body.Append(TableRow("ID Schema Name", dependency.Required.reqdepIdSchemaName ?? "", dependency.Dependent.reqdepIdSchemaName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepParentDisplayName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepParentDisplayName))
                            body.Append(TableRow("Parent Display Name", dependency.Required.reqdepParentDisplayName ?? "", dependency.Dependent.reqdepParentDisplayName ?? ""));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepParentSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepParentSchemaName))
                            body.Append(TableRow("Parent Schema Name", dependency.Required.reqdepParentSchemaName ?? "", dependency.Dependent.reqdepParentSchemaName ?? ""));
                        body.AppendLine(TableEnd());
                    }
                }
            }
            else
            {
                body.AppendLine(Paragraph("This solution has no dependencies."));
            }
        }

        /// <summary>
        /// Returns an HTML link to the cross-document documentation for a solution component,
        /// or plain encoded text if the target documentation is not being generated.
        /// Solution is at root level, so paths go directly into subfolders (no ../ needed).
        /// </summary>
        private string GetCrossDocLinkHtmlForComponent(SolutionComponent component, string displayName)
        {
            switch (component.Type)
            {
                case "Workflow":
                    if (content.context?.Config?.documentFlows == true)
                    {
                        FlowEntity flow = content.context.GetFlowById(component.ID);
                        if (flow != null)
                            return Link(displayName, CrossDocLinkHelper.GetFlowDocHtmlPath(flow.Name));
                    }
                    break;
                case "Canvas App":
                    if (content.context?.Config?.documentApps == true)
                    {
                        string appName = content.context.GetAppNameBySchemaName(component.SchemaName);
                        AppEntity app = content.context.GetAppByName(appName);
                        if (app != null)
                            return Link(displayName, CrossDocLinkHelper.GetAppDocHtmlPath(app.Name));
                    }
                    break;
                case "Model-Driven App":
                    if (content.context?.Config?.documentModelDrivenApps == true)
                    {
                        AppModuleEntity mda = content.appModules?.FirstOrDefault(a =>
                            a.UniqueName?.Equals(component.SchemaName, StringComparison.OrdinalIgnoreCase) == true);
                        if (mda != null)
                            return Link(displayName, CrossDocLinkHelper.GetMDADocHtmlPath(mda.GetDisplayName()));
                    }
                    break;
            }
            return Encode(displayName);
        }

        private void renderWorkflows(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Workflow", SanitizeAnchorId("Workflow")));
            List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == "Workflow").ToList();
            if (components.Count > 0)
            {
                var sortedComponents = components
                    .Select(c => (comp: c, parts: content.GetWorkflowDisplayParts(c)))
                    .OrderBy(x => x.parts.Name, StringComparer.OrdinalIgnoreCase).ToList();
                body.Append(TableStart("Name", "Trigger Type", "Flow Type"));
                foreach (var (comp, parts) in sortedComponents)
                {
                    string anchorId = SanitizeAnchorId("comp-" + parts.Name);
                    string nameCell = GetCrossDocLinkHtmlForComponent(comp, parts.Name);
                    body.Append($"<tr id=\"{Encode(anchorId)}\"><td>{nameCell}</td><td>{Encode(parts.TriggerInfo)}</td><td>{Encode(parts.FlowType)}</td></tr>");
                }
                body.AppendLine(TableEnd());
            }
        }

        private void renderBusinessProcessFlows(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, "Business Process Flows", "business-process-flows"));
            body.AppendLine($"<p>This solution contains {content.businessProcessFlows.Count} Business Process Flow(s).</p>");
            body.Append(TableStart("Name", "Primary Entity", "Stages", "State"));
            foreach (var bpf in content.businessProcessFlows.OrderBy(b => b.GetDisplayName(), StringComparer.OrdinalIgnoreCase))
            {
                string name = Encode(bpf.GetDisplayName());
                string entity = Encode(content.context?.GetTableDisplayName(bpf.PrimaryEntity) ?? bpf.PrimaryEntity ?? "");
                string stages = bpf.Stages.Count.ToString();
                string state = Encode(bpf.GetStateLabel());
                body.Append($"<tr><td>{name}</td><td>{entity}</td><td>{stages}</td><td>{state}</td></tr>");
            }
            body.AppendLine(TableEnd());
        }

        private void renderAIModels(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "AI Models", "ai-models"));
            List<AIModel> aiModels = content.solution.Customizations.getAIModels();
            if (aiModels.Count > 0)
            {
                body.Append(TableStart("AI Model"));
                foreach (AIModel aiModel in aiModels.OrderBy(o => o.getName()))
                {
                    string modelName = aiModel.getName();
                    string anchorId = SanitizeAnchorId("aimodel-" + modelName);
                    string linkHtml = Link(modelName, CrossDocLinkHelper.GetAIModelDocHtmlPath(modelName));
                    body.Append($"<tr id=\"{Encode(anchorId)}\"><td>{linkHtml}</td></tr>");
                }
                body.AppendLine(TableEnd());
            }
        }

        private void renderAgents(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Agents", "agents"));
            if (content.agents.Count > 0)
            {
                body.Append(TableStart("Agent"));
                foreach (AgentEntity agent in content.agents.OrderBy(a => a.Name))
                {
                    string anchorId = SanitizeAnchorId("agent-" + agent.Name);
                    string linkHtml = (content.context?.Config?.documentAgents == true)
                        ? Link(agent.Name, CrossDocLinkHelper.GetAgentDocHtmlPath(agent.Name))
                        : Encode(agent.Name);
                    body.Append($"<tr id=\"{Encode(anchorId)}\"><td>{linkHtml}</td></tr>");
                }
                body.AppendLine(TableEnd());
            }
        }

        private void renderDataflows(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Dataflows", "dataflows"));
            if (content.dataflows.Count > 0)
            {
                body.Append(TableStart("Dataflow", "Queries", "State"));
                foreach (DataflowEntity df in content.dataflows.OrderBy(d => d.GetDisplayName()))
                {
                    string anchorId = SanitizeAnchorId("dataflow-" + df.GetDisplayName());
                    string linkHtml = (content.context?.Config?.documentDataflows == true)
                        ? Link(df.GetDisplayName(), CrossDocLinkHelper.GetDataflowDocHtmlPath(df.GetDisplayName()))
                        : Encode(df.GetDisplayName());
                    body.Append($"<tr id=\"{Encode(anchorId)}\"><td>{linkHtml}</td><td>{df.Queries.Count}</td><td>{Encode(df.GetStateLabel())}</td></tr>");
                }
                body.AppendLine(TableEnd());
            }
        }

        private void renderWebResources(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Web Resources", "web-resources"));
            IReadOnlyList<WebResourceEntity> webResources = GetSortedWebResources();
            if (webResources.Count > 0)
            {
                string wrPagePath = CrossDocLinkHelper.GetWebResourceDocHtmlPath(content.solution.UniqueName);
                body.AppendLine(ParagraphRaw("See the " + Link("dedicated Web Resources page", wrPagePath) + " for full details, image previews, and source code."));
                body.Append(TableStart("Display Name", "Name", "Type", "Introduced Version"));
                foreach (WebResourceEntity wr in webResources)
                {
                    string displayName = !string.IsNullOrEmpty(wr.DisplayName) ? wr.DisplayName : wr.Name ?? "";
                    if (wr.IsTextType())
                    {
                        string detailPath = CrossDocLinkHelper.GetWebResourceDetailHtmlPath(content.solution.UniqueName, wr.Name);
                        string nameLink = Link(displayName, detailPath);
                        body.Append(TableRowRaw(nameLink, Encode(wr.Name ?? ""), Encode(wr.GetTypeDisplayName()), Encode(wr.IntroducedVersion ?? "")));
                    }
                    else if (wr.IsImageType())
                    {
                        string nameLink = Link(displayName, wrPagePath + "#" + SanitizeAnchorId("wr-" + wr.Name));
                        body.Append(TableRowRaw(nameLink, Encode(wr.Name ?? ""), Encode(wr.GetTypeDisplayName()), Encode(wr.IntroducedVersion ?? "")));
                    }
                    else
                    {
                        body.Append(TableRow(
                            displayName,
                            wr.Name ?? "",
                            wr.GetTypeDisplayName(),
                            wr.IntroducedVersion ?? ""
                        ));
                    }
                }
                body.AppendLine(TableEnd());
            }
        }

        private void renderAppActions(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Command Bar Buttons", "app-actions"));
            if (content.solution.AppActions.Count > 0)
            {
                body.Append(TableStart("Label", "Table", "App Module", "Icon", "Hidden"));
                foreach (AppActionEntity action in content.solution.AppActions.OrderBy(a => a.ButtonLabel ?? a.UniqueName))
                {
                    body.Append(TableRow(
                        action.ButtonLabel ?? action.UniqueName ?? "",
                        action.ContextEntity ?? "",
                        action.AppModuleName ?? "",
                        action.FontIcon ?? "",
                        action.IsHidden ? "Yes" : "No"
                    ));
                }
                body.AppendLine(TableEnd());
            }
        }

        private void renderSettingDefinitions(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Setting Definitions", "setting-definitions"));
            if (content.solution.SettingDefinitions.Count > 0)
            {
                foreach (SettingDefinitionEntity setting in content.solution.SettingDefinitions.OrderBy(s => s.DisplayName ?? s.UniqueName))
                {
                    body.AppendLine(HeadingWithId(4, setting.DisplayName ?? setting.UniqueName, SanitizeAnchorId("setting-" + setting.UniqueName)));
                    body.Append(TableStart("Property", "Value"));
                    body.Append(TableRow("Internal Name", setting.UniqueName ?? ""));
                    body.Append(TableRow("Data Type", setting.GetDataTypeDisplayName()));
                    body.Append(TableRow("Default Value", setting.DefaultValue ?? ""));
                    body.Append(TableRow("Description", setting.Description ?? ""));
                    body.Append(TableRow("Is Customizable", setting.IsCustomizable ? "Yes" : "No"));
                    body.Append(TableRow("Is Hidden", setting.IsHidden ? "Yes" : "No"));
                    body.Append(TableRow("Is Overridable", setting.IsOverridable ? "Yes" : "No"));
                    body.AppendLine(TableEnd());
                }
            }
        }

        private void renderFormulaDefinitions(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Formula Definitions", "formula-definitions"));
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                var grouped = content.solution.FormulaDefinitions
                    .GroupBy(f => f.TableName)
                    .OrderBy(g => g.Key, StringComparer.OrdinalIgnoreCase);
                foreach (var group in grouped)
                {
                    body.AppendLine(HeadingWithId(4, group.Key, SanitizeAnchorId("formula-table-" + group.Key)));
                    body.Append(TableStart("Column", "Type", "Formula"));
                    foreach (FormulaDefinitionEntity formula in group.OrderBy(f => f.ColumnName))
                    {
                        body.Append(TableRow(
                            formula.ColumnName ?? "",
                            formula.Type ?? "",
                            formula.Content ?? ""
                        ));
                    }
                    body.AppendLine(TableEnd());
                }
            }
        }

        private void renderOptionSets(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Option Sets", "option-sets"));
            List<OptionSetEntity> optionSets = content.solution.Customizations.getOptionSets();
            if (optionSets.Count > 0)
            {
                foreach (OptionSetEntity optionSet in optionSets.OrderBy(o => o.GetDisplayName()))
                {
                    body.AppendLine(HeadingWithId(4, optionSet.GetDisplayName() + " (" + optionSet.Name + ")", SanitizeAnchorId("optionset-" + optionSet.Name)));
                    body.Append(TableStart("Property", "Value"));
                    body.Append(TableRow("Type", optionSet.OptionSetType ?? ""));
                    body.Append(TableRow("Is Global", optionSet.IsGlobal ? "Yes" : "No"));
                    body.Append(TableRow("Is Customizable", optionSet.IsCustomizable ? "Yes" : "No"));
                    if (!String.IsNullOrEmpty(optionSet.Description))
                        body.Append(TableRow("Description", optionSet.Description));
                    body.AppendLine(TableEnd());

                    if (optionSet.Options.Count > 0)
                    {
                        body.AppendLine(Paragraph("Options:"));
                        body.Append(TableStart("Value", "Label"));
                        foreach (OptionSetOption option in optionSet.Options)
                        {
                            body.Append(TableRow(option.Value ?? "", option.Label ?? ""));
                        }
                        body.AppendLine(TableEnd());
                    }
                }
            }
        }

        private void renderSecurityRoles(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Security Roles", "security-roles"));
            foreach (RoleEntity role in GetSortedRoles())
            {
                body.AppendLine(HeadingWithId(4, role.Name + " (" + role.ID + ")", SanitizeAnchorId("role-" + role.Name)));
                body.Append(TableStart("Table", "Create", "Read", "Write", "Delete", "Append", "Append To", "Assign", "Share"));
                foreach (TableAccess tableAccess in role.Tables.OrderBy(o => o.Name))
                {
                    body.Append(TableRowRaw(
                        Encode(tableAccess.Name),
                        getAccessLevelHtml(tableAccess.Create),
                        getAccessLevelHtml(tableAccess.Read),
                        getAccessLevelHtml(tableAccess.Write),
                        getAccessLevelHtml(tableAccess.Delete),
                        getAccessLevelHtml(tableAccess.Append),
                        getAccessLevelHtml(tableAccess.AppendTo),
                        getAccessLevelHtml(tableAccess.Assign),
                        getAccessLevelHtml(tableAccess.Share)
                    ));
                }
                body.AppendLine(TableEnd());

                if (role.miscellaneousPrivileges.Count > 0)
                {
                    body.AppendLine(Paragraph("Miscellaneous Privileges associated with this role:"));
                    body.Append(TableStart("Miscellaneous Privilege", "Level"));
                    foreach (KeyValuePair<string, string> miscPrivilege in role.miscellaneousPrivileges)
                    {
                        body.Append(TableRowRaw(Encode(miscPrivilege.Key), getAccessLevelHtml(miscPrivilege.Value)));
                    }
                    body.AppendLine(TableEnd());
                }
            }
        }

        private void renderEntities(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(3, "Tables", "tables"));
            foreach (TableEntity tableEntity in GetSortedEntities())
            {
                body.AppendLine(HeadingWithId(4, tableEntity.getLocalizedName() + " (" + tableEntity.getName() + ")", SanitizeAnchorId("table-" + tableEntity.getName())));
                body.Append(TableStart("Property", "Value"));
                body.Append(TableRow("Primary Column", tableEntity.getPrimaryColumn()));
                body.Append(TableRow("Description", tableEntity.getDescription()));
                body.Append(TableRow("Entity Set Name", tableEntity.GetEntitySetName()));
                body.Append(TableRow("Record Ownership", tableEntity.GetOwnershipType()));
                body.Append(TableRow("Auditing", tableEntity.IsAuditEnabled() ? "Enabled" : "Disabled"));
                body.Append(TableRow("Customizable", tableEntity.IsCustomizable() ? "Yes" : "No"));
                body.Append(TableRow("Change Tracking", tableEntity.IsChangeTrackingEnabled() ? "Enabled" : "Disabled"));
                body.Append(TableRow("Is Activity", tableEntity.IsActivity() ? "Yes" : "No"));
                body.Append(TableRow("Quick Create", tableEntity.IsQuickCreateEnabled() ? "Enabled" : "Disabled"));
                body.Append(TableRow("Connections", tableEntity.IsConnectionsEnabled() ? "Enabled" : "Disabled"));
                body.Append(TableRow("Duplicate Detection", tableEntity.IsDuplicateCheckSupported() ? "Enabled" : "Disabled"));
                body.Append(TableRow("Mobile Visible", tableEntity.IsVisibleInMobile() ? "Yes" : "No"));
                body.Append(TableRow("Introduced Version", tableEntity.GetIntroducedVersion()));
                body.AppendLine(TableEnd());

                if (tableEntity.GetColumns().Count > 0)
                {
                    var columns = documentDefaultColumns
                        ? tableEntity.GetColumns()
                        : tableEntity.GetColumns().Where(c => !c.isDefaultColumn()).ToList();
                    if (columns.Count > 0)
                    {
                    body.AppendLine(Heading(5, "Columns"));
                    body.Append(TableStart("Display Name", "Logical Name", "Name", "Data type"));
                    foreach (ColumnEntity columnEntity in columns)
                    {
                        string primaryNameColumn = columnEntity.getDisplayMask().Contains("PrimaryName") ? " (Primary name column)" : "";
                        body.Append(TableRow(
                            columnEntity.getDisplayName() + primaryNameColumn,
                            columnEntity.getLogicalName(),
                            columnEntity.getName(),
                            columnEntity.getDataType()
                        ));
                    }
                    body.AppendLine(TableEnd());

                    foreach (ColumnEntity columnEntity in columns)
                    {
                        string primaryNameColumn = columnEntity.getDisplayMask().Contains("PrimaryName") ? " (Primary name column)" : "";
                        string columnHeading = !String.IsNullOrEmpty(columnEntity.getDisplayName())
                            ? columnEntity.getDisplayName() + " (" + columnEntity.getLogicalName() + ")"
                            : columnEntity.getLogicalName();
                        body.AppendLine(Heading(6, columnHeading + primaryNameColumn));
                        body.Append(TableStart("Property", "Value"));
                        body.Append(TableRow("Display Name", columnEntity.getDisplayName()));
                        body.Append(TableRow("Logical Name", columnEntity.getLogicalName()));
                        body.Append(TableRow("Physical Name", columnEntity.getName()));
                        body.Append(TableRow("Data Type", columnEntity.getDataType()));
                        body.Append(TableRow("Custom Field", columnEntity.IsCustomField() ? "Yes" : "No"));
                        body.Append(TableRow("Auditing", columnEntity.IsAuditEnabled() ? "Enabled" : "Disabled"));
                        body.Append(TableRow("Customizable", columnEntity.isCustomizable().ToString()));
                        body.Append(TableRow("Required", columnEntity.isRequired().ToString()));
                        body.Append(TableRow("Searchable", columnEntity.isSearchable().ToString()));
                        body.Append(TableRow("Secured", columnEntity.IsSecured() ? "Yes" : "No"));
                        body.Append(TableRow("Filterable", columnEntity.IsFilterable() ? "Yes" : "No"));
                        body.AppendLine(TableEnd());
                    }
                    }
                }

                if (tableEntity.GetForms().Count > 0)
                {
                    body.AppendLine(Heading(5, "Forms"));
                    body.Append(TableStart("Name", "Type", "Default", "State", "Customizable"));
                    foreach (FormEntity formEntity in tableEntity.GetForms())
                    {
                        body.Append(TableRow(
                            formEntity.GetFormName(),
                            formEntity.GetFormTypeDisplayName(),
                            formEntity.IsDefault() ? "Yes" : "No",
                            formEntity.IsActive() ? "Active" : "Inactive",
                            formEntity.IsCustomizable() ? "Yes" : "No"
                        ));
                    }
                    body.AppendLine(TableEnd());

                    // Generate SVG mockup files for forms
                    Dictionary<string, string> columnDisplayNames = tableEntity.GetColumns().ToDictionary(c => c.getLogicalName(), c => c.getDisplayName(), StringComparer.OrdinalIgnoreCase);
                    Dictionary<string, string> formSvgFiles = FormSvgBuilder.GenerateFormSvgs(tableEntity, content.folderPath, columnDisplayNames);

                    foreach (FormEntity formEntity in tableEntity.GetForms())
                    {
                        List<FormTab> tabs = formEntity.GetTabs();
                        if (tabs.Count > 0)
                        {
                            string formTypeLabel = formEntity.GetFormTypeDisplayName();
                            body.AppendLine(Heading(6, "Form (" + formTypeLabel + "): " + formEntity.GetFormName()));

                            // SVG wireframe mockup as external file reference
                            string formKey = formEntity.GetFormName() + "|" + formTypeLabel;
                            if (formSvgFiles.TryGetValue(formKey, out string svgFile))
                            {
                                body.AppendLine("<div class=\"form-svg-mockup\" style=\"margin: 12px 0; overflow-x: auto;\">");
                                body.AppendLine($"<img src=\"{Encode(svgFile)}\" alt=\"{Encode("Form layout: " + formEntity.GetFormName())}\" style=\"max-width: 100%;\" />");
                                body.AppendLine("</div>");
                            }

                            // Rendering Forms visually now, keeping this code for reference
                            // foreach (FormTab tab in tabs)
                            // {
                            //     body.AppendLine(ParagraphRaw("<strong>Tab:</strong> " + Encode(tab.GetName()) + (tab.IsVisible() ? "" : " (hidden)")));
                            //     foreach (FormSection section in tab.GetSections())
                            //     {
                            //         List<FormControl> controls = section.GetControls();
                            //         if (controls.Count > 0)
                            //         {
                            //             body.AppendLine(Paragraph("Section: " + Encode(section.GetName()) + (section.IsVisible() ? "" : " (hidden)")));
                            //             body.Append(TableStart("#", "Control", "Field"));
                            //             int controlIndex = 1;
                            //             foreach (FormControl control in controls)
                            //             {
                            //                 string fieldName = !String.IsNullOrEmpty(control.GetDataFieldName()) ? control.GetDataFieldName() : control.GetId();
                            //                 body.Append(TableRow(controlIndex.ToString(), control.GetId(), fieldName));
                            //                 controlIndex++;
                            //             }
                            //             body.AppendLine(TableEnd());
                            //         }
                            //     }
                            // }
                        }
                    }
                }

                if (tableEntity.GetViews().Count > 0)
                {
                    body.AppendLine(Heading(5, "Views"));
                    body.Append(TableStart("Name", "Type", "Default", "Customizable"));
                    foreach (ViewEntity viewEntity in tableEntity.GetViews())
                    {
                        body.Append(TableRow(
                            viewEntity.GetViewName(),
                            viewEntity.GetQueryTypeDisplayName(),
                            viewEntity.IsDefault() ? "Yes" : "No",
                            viewEntity.IsCustomizable() ? "Yes" : "No"
                        ));
                    }
                    body.AppendLine(TableEnd());

                    Dictionary<string, string> columnDisplayNames = tableEntity.GetColumns().ToDictionary(c => c.getLogicalName(), c => c.getDisplayName(), StringComparer.OrdinalIgnoreCase);
                    foreach (ViewEntity viewEntity in tableEntity.GetViews())
                    {
                        List<ViewColumn> viewColumns = viewEntity.GetColumns();
                        if (viewColumns.Count > 0)
                        {
                            body.AppendLine(Heading(6, "View: " + viewEntity.GetViewName()));
                            body.Append(TableStart("#", "Column", "Width"));
                            foreach (ViewColumn vc in viewColumns)
                            {
                                string colName = vc.GetName();
                                string displayName = columnDisplayNames.TryGetValue(colName, out string dn) && !String.IsNullOrEmpty(dn) ? dn + " (" + colName + ")" : colName;
                                body.Append(TableRow(vc.Order.ToString(), displayName, vc.GetWidth()));
                            }
                            body.AppendLine(TableEnd());

                            // View controls table (sort orders, filters)
                            List<ViewSortOrder> sortOrders = viewEntity.GetSortOrders();
                            ViewFilter filter = viewEntity.GetFilter();
                            string filterText = filter?.ToDisplayString(columnDisplayNames) ?? "";
                            if (sortOrders.Count > 0 || !string.IsNullOrEmpty(filterText))
                            {
                                body.Append(TableStart("View Controls", "Details"));
                                if (sortOrders.Count > 0)
                                {
                                    string sortText = string.Join(", ", sortOrders.Select(s => s.ToDisplayString(columnDisplayNames)));
                                    body.Append(TableRow("Sort by", sortText));
                                }
                                if (!string.IsNullOrEmpty(filterText))
                                {
                                    body.Append(TableRow("Filter", filterText));
                                }
                                body.AppendLine(TableEnd());
                            }
                        }
                    }
                }
            }
            // Ribbon Customization Summary
            List<RibbonCustomizationEntity> ribbonCustomizations = content.solution.Customizations.getRibbonCustomizations();
            if (ribbonCustomizations.Count > 0)
            {
                body.AppendLine(Heading(4, "Ribbon Customizations"));
                body.Append(TableStart("Table", "Hidden Actions", "Command Definitions", "Display Rules", "Enable Rules"));
                foreach (RibbonCustomizationEntity ribbon in ribbonCustomizations.OrderBy(r => r.EntityName))
                {
                    body.Append(TableRow(
                        ribbon.EntityName,
                        ribbon.HiddenActions.Count.ToString(),
                        ribbon.CommandDefinitionCount.ToString(),
                        ribbon.DisplayRuleCount.ToString(),
                        ribbon.EnableRuleCount.ToString()
                    ));
                }
                body.AppendLine(TableEnd());
            }
            body.AppendLine(Heading(4, "Table Relationships"));
            body.AppendLine(ParagraphRaw(Image("Dataverse Table Relationships", "dataverse.svg")));
        }

        private string getAccessLevelHtml(AccessLevel accessLevel)
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
            {
                string sourcePath = AssemblyHelper.GetExecutablePath() + iconFile;
                if (File.Exists(sourcePath))
                    File.Copy(sourcePath, content.folderPath + iconFile);
            }
            return ImageWithClass(accessLevel.ToString(), iconFile.Replace(@"\", "/"), "icon-inline");
        }

        private string getAccessLevelHtml(string accessLevel)
        {
            AccessLevel level = accessLevel switch
            {
                "Global" => AccessLevel.Global,
                "Deep" => AccessLevel.Deep,
                "Loca" => AccessLevel.Local,
                "Basic" => AccessLevel.Basic,
                _ => AccessLevel.None
            };
            return getAccessLevelHtml(level);
        }
    }
}
