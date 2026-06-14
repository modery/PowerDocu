using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PowerDocu.Common;

namespace PowerDocu.SolutionDocumenter
{
    class SolutionWordDocBuilder : WordDocBuilder
    {
        private readonly SolutionDocumentationContent content;
        private readonly bool documentDefaultColumns;

        public SolutionWordDocBuilder(SolutionDocumentationContent contentDocumentation, string template, bool documentDefaultColumns = false, bool addTableOfContents = false)
        {
            content = contentDocumentation;
            this.documentDefaultColumns = documentDefaultColumns;
            Directory.CreateDirectory(content.folderPath);
            string filename = InitializeWordDocument(content.folderPath + "Solution - " + content.filename, template);
            using WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true);
            mainPart = wordDocument.MainDocumentPart;
            body = mainPart.Document.Body;
            PrepareDocument(!String.IsNullOrEmpty(template));
            addSolutionMetadata();
            if (addTableOfContents) AddTableOfContents();
            addSolutionComponents();
        }

        private void addSolutionMetadata()
        {
            AddHeading(content.solution.UniqueName, "Heading1");
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Status"), new Text(content.solution.isManaged ? "Managed" : "Unmanaged")));
            table.Append(CreateRow(new Text("Version"), new Text(content.solution.Version)));
            table.Append(CreateRow(new Text("Publisher"), GetPublisherInfo()));
            table.Append(CreateRow(new Text("Documentation generated at"), new Text(PowerDocuReleaseHelper.GetTimestampWithVersion())));
            body.Append(table);
            AddHeading("Statistics", "Heading1");
            table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Component Type"), new Text("Number of Components")));
            var statisticsEntries = new List<(string Name, int Count)>();
            if (content.solution.EnvironmentVariables.Count > 0)
            {
                statisticsEntries.Add((GetComponentSectionHeading("EnvironmentVariable"), content.solution.EnvironmentVariables.Count));
            }
            if (content.agents.Count > 0)
            {
                statisticsEntries.Add((GetComponentSectionHeading("Agent"), content.agents.Count));
            }
            if (content.dataflows.Count > 0)
            {
                statisticsEntries.Add((GetComponentSectionHeading("Dataflow"), content.dataflows.Count));
            }
            if (content.solution.AppActions.Count > 0)
            {
                statisticsEntries.Add((GetComponentSectionHeading("AppAction"), content.solution.AppActions.Count));
            }
            if (content.solution.SettingDefinitions.Count > 0)
            {
                statisticsEntries.Add((GetComponentSectionHeading("SettingDefinition"), content.solution.SettingDefinitions.Count));
            }
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                statisticsEntries.Add((GetComponentSectionHeading("FormulaDefinition"), content.solution.FormulaDefinitions.Count));
            }
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                int count = content.solution.Components.Where(c => c.Type == componentType).Count();
                statisticsEntries.Add((GetComponentSectionHeading(componentType), count));
            }
            foreach (var entry in statisticsEntries.OrderBy(e => e.Name, StringComparer.OrdinalIgnoreCase))
            {
                table.Append(CreateRow(new Text(entry.Name), new Text(entry.Count.ToString())));
            }
            body.Append(table);
        }

        private Table GetPublisherInfo()
        {
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Name"), new Text(content.solution.Publisher.UniqueName)));
            table.Append(CreateRow(new Text("Email"), new Text(content.solution.Publisher.EMailAddress)));
            table.Append(CreateRow(new Text("CustomizationPrefix"), new Text(content.solution.Publisher.CustomizationPrefix)));
            table.Append(CreateRow(new Text("CustomizationOptionValuePrefix"), new Text(content.solution.Publisher.CustomizationOptionValuePrefix)));
            table.Append(CreateRow(new Text("SupportingWebsiteUrl"), new Text(content.solution.Publisher.SupportingWebsiteUrl)));
            if (content.solution.Publisher.Descriptions.Count > 0)
            {
                Table descriptionsTable = CreateTable();
                descriptionsTable.Append(CreateHeaderRow(new Text("Language Code"), new Text("Description")));
                foreach (KeyValuePair<string, string> description in content.solution.Publisher.Descriptions)
                {
                    descriptionsTable.Append(CreateRow(new Text(description.Key), new Text(description.Value)));
                }
                table.Append(CreateRow(new Text("Descriptions"), descriptionsTable));
            }
            if (content.solution.Publisher.LocalizedNames.Count > 0)
            {
                Table localizedNamesTable = CreateTable();
                localizedNamesTable.Append(CreateHeaderRow(new Text("Language Code"), new Text("Description")));
                foreach (KeyValuePair<string, string> localizedName in content.solution.Publisher.LocalizedNames)
                {
                    localizedNamesTable.Append(CreateRow(new Text(localizedName.Key), new Text(localizedName.Value)));
                }
                table.Append(CreateRow(new Text("Localized Names"), localizedNamesTable));
            }
            if (content.solution.Publisher.Addresses.Count > 0)
            {
                Table addressesTable = CreateTable();
                foreach (Address address in content.solution.Publisher.Addresses)
                {
                    Table addressTable = CreateTable();
                    addressTable.Append(CreateHeaderRow(new Text("Property"), new Text("Value")));
                    if (!String.IsNullOrEmpty(address.Name))
                        addressTable.Append(CreateRow(new Text("Name"), new Text(address.Name)));
                    if (!String.IsNullOrEmpty(address.AddressNumber))
                        addressTable.Append(CreateRow(new Text("AddressNumber"), new Text(address.AddressNumber)));
                    if (!String.IsNullOrEmpty(address.AddressTypeCode))
                        addressTable.Append(CreateRow(new Text("AddressTypeCode"), new Text(address.AddressTypeCode)));
                    if (!String.IsNullOrEmpty(address.City))
                        addressTable.Append(CreateRow(new Text("City"), new Text(address.City)));
                    if (!String.IsNullOrEmpty(address.County))
                        addressTable.Append(CreateRow(new Text("County"), new Text(address.County)));
                    if (!String.IsNullOrEmpty(address.Country))
                        addressTable.Append(CreateRow(new Text("Country"), new Text(address.Country)));
                    if (!String.IsNullOrEmpty(address.Fax))
                        addressTable.Append(CreateRow(new Text("Fax"), new Text(address.Fax)));
                    if (!String.IsNullOrEmpty(address.FreightTermsCode))
                        addressTable.Append(CreateRow(new Text("FreightTermsCode"), new Text(address.FreightTermsCode)));
                    if (!String.IsNullOrEmpty(address.ImportSequenceNumber))
                        addressTable.Append(CreateRow(new Text("ImportSequenceNumber"), new Text(address.ImportSequenceNumber)));
                    if (!String.IsNullOrEmpty(address.Latitude))
                        addressTable.Append(CreateRow(new Text("Latitude"), new Text(address.Latitude)));
                    if (!String.IsNullOrEmpty(address.Line1))
                        addressTable.Append(CreateRow(new Text("Line1"), new Text(address.Line1)));
                    if (!String.IsNullOrEmpty(address.Line2))
                        addressTable.Append(CreateRow(new Text("Line2"), new Text(address.Line2)));
                    if (!String.IsNullOrEmpty(address.Line3))
                        addressTable.Append(CreateRow(new Text("Line3"), new Text(address.Line3)));
                    if (!String.IsNullOrEmpty(address.Longitude))
                        addressTable.Append(CreateRow(new Text("Longitude"), new Text(address.Longitude)));
                    if (!String.IsNullOrEmpty(address.PostalCode))
                        addressTable.Append(CreateRow(new Text("PostalCode"), new Text(address.PostalCode)));
                    if (!String.IsNullOrEmpty(address.PostOfficeBox))
                        addressTable.Append(CreateRow(new Text("PostOfficeBox"), new Text(address.PostOfficeBox)));
                    if (!String.IsNullOrEmpty(address.PrimaryContactName))
                        addressTable.Append(CreateRow(new Text("PrimaryContactName"), new Text(address.PrimaryContactName)));
                    if (!String.IsNullOrEmpty(address.ShippingMethodCode))
                        addressTable.Append(CreateRow(new Text("ShippingMethodCode"), new Text(address.ShippingMethodCode)));
                    if (!String.IsNullOrEmpty(address.StateOrProvince))
                        addressTable.Append(CreateRow(new Text("StateOrProvince"), new Text(address.StateOrProvince)));
                    if (!String.IsNullOrEmpty(address.Telephone1))
                        addressTable.Append(CreateRow(new Text("Telephone1"), new Text(address.Telephone1)));
                    if (!String.IsNullOrEmpty(address.Telephone2))
                        addressTable.Append(CreateRow(new Text("Telephone2"), new Text(address.Telephone2)));
                    if (!String.IsNullOrEmpty(address.Telephone3))
                        addressTable.Append(CreateRow(new Text("Telephone3"), new Text(address.Telephone3)));
                    if (!String.IsNullOrEmpty(address.TimeZoneRuleVersionNumber))
                        addressTable.Append(CreateRow(new Text("TimeZoneRuleVersionNumber"), new Text(address.TimeZoneRuleVersionNumber)));
                    if (!String.IsNullOrEmpty(address.UPSZone))
                        addressTable.Append(CreateRow(new Text("UPSZone"), new Text(address.UPSZone)));
                    if (!String.IsNullOrEmpty(address.UTCOffset))
                        addressTable.Append(CreateRow(new Text("UTCOffset"), new Text(address.UTCOffset)));
                    if (!String.IsNullOrEmpty(address.UTCConversionTimeZoneCode))
                        addressTable.Append(CreateRow(new Text("UTCConversionTimeZoneCode"), new Text(address.UTCConversionTimeZoneCode)));
                    addressesTable.Append(CreateRow(addressTable));
                }
                table.Append(CreateRow(new Text("Addresses"), addressesTable));
            }
            return table;
        }


        private void addEnvironmentVariables()
        {
            AddHeading("Environment Variables", "Heading2");
            body.AppendChild(new Paragraph());
            foreach (EnvironmentVariableEntity environmentVariable in content.solution.EnvironmentVariables.OrderBy(e => e.DisplayName))
            {
                AddHeading(environmentVariable.DisplayName, "Heading3");
                Table table = CreateTable();
                table.Append(CreateHeaderRow(new Text("Property"), new Text("Value")));
                table.Append(CreateRow(new Text("Internal Name"), new Text(environmentVariable.Name)));
                table.Append(CreateRow(new Text("Type"), new Text(environmentVariable.getTypeDisplayName())));
                table.Append(CreateRow(new Text("Default Value"), new Text(environmentVariable.DefaultValue)));
                table.Append(CreateRow(new Text("Description"), new Text(environmentVariable.DescriptionDefault)));
                table.Append(CreateRow(new Text("Is Required"), new Text(environmentVariable.IsRequired ? "Yes" : "No")));
                table.Append(CreateRow(new Text("Is Customizable"), new Text(environmentVariable.IsCustomizable ? "Yes" : "No")));
                table.Append(CreateRow(new Text("IntroducedVersion"), new Text(environmentVariable.IntroducedVersion)));
                body.Append(table);
                if (environmentVariable.LocalizedNames.Count > 0 || environmentVariable.Descriptions.Count > 0)
                {
                    var langCodes = environmentVariable.LocalizedNames.Keys
                        .Union(environmentVariable.Descriptions.Keys)
                        .OrderBy(k => k).ToList();
                    Table localizedTable = CreateTable();
                    localizedTable.Append(CreateHeaderRow(new Text("Language Code"), new Text("Name"), new Text("Description")));
                    foreach (string langCode in langCodes)
                    {
                        environmentVariable.LocalizedNames.TryGetValue(langCode, out string name);
                        environmentVariable.Descriptions.TryGetValue(langCode, out string description);
                        localizedTable.Append(CreateRow(new Text(langCode), new Text(name ?? ""), new Text(description ?? "")));
                    }
                    body.Append(localizedTable);
                }
                body.AppendChild(new Paragraph());
            }
        }
        /// <summary>
        /// Returns the heading text used for the section of a given component type.
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

        private void addSolutionComponents()
        {
            AddHeading("Solution Components", "Heading1");
            body.AppendChild(new Paragraph(new Run(new Text("This solution contains the following components"))));

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
                    case "AI Project":
                        renderAIModels();
                        break;
                    case "Option Set":
                        renderOptionSets();
                        break;
                    case "Workflow":
                        renderWorkflows();
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
                        AddHeading(section.ComponentType, "Heading2");
                        List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == section.ComponentType).ToList();
                        var sortedNames = components.Select(c => content.GetDisplayNameForComponent(c)).OrderBy(n => n, StringComparer.OrdinalIgnoreCase).ToList();
                        Table table = CreateTable();
                        table.Append(CreateHeaderRow(new Text(section.ComponentType)));
                        foreach (string compName in sortedNames)
                        {
                            table.Append(CreateRow(new Text(compName)));
                        }
                        body.Append(table);
                        body.AppendChild(new Paragraph());
                        break;
                }
            }

            // Business Process Flows
            if (content.businessProcessFlows.Count > 0)
            {
                renderBusinessProcessFlows();
            }

            // Solution Component Relationships graph
            if (File.Exists(content.folderPath + "solution-components.png") && File.Exists(content.folderPath + "solution-components.svg"))
            {
                AddHeading("Solution Component Relationships", "Heading1");
                ImagePart relImagePart = mainPart.AddImagePart(ImagePartType.Png);
                int relImageWidth, relImageHeight;
                using (FileStream stream = new FileStream(content.folderPath + "solution-components.png", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var image = Image.FromStream(stream, false, false))
                    {
                        relImageWidth = image.Width;
                        relImageHeight = image.Height;
                    }
                    stream.Position = 0;
                    relImagePart.FeedData(stream);
                }
                ImagePart relSvgPart = mainPart.AddNewPart<ImagePart>("image/svg+xml", "rId" + (new Random()).Next(100000, 999999));
                using (FileStream stream = new FileStream(content.folderPath + "solution-components.svg", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    relSvgPart.FeedData(stream);
                }
                body.AppendChild(new Paragraph(new Run(
                    InsertSvgImage(mainPart.GetIdOfPart(relSvgPart), mainPart.GetIdOfPart(relImagePart), relImageWidth, relImageHeight)
                )));
                body.AppendChild(new Paragraph(new Run()));
            }

            AddHeading("Solution Component Dependencies", "Heading1");
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            List<string> dependencies = content.solution.Dependencies
                .GroupBy(p => p.Required.reqdepSolution)
                .Select(g => g.First())
                .OrderBy(t => t.Required.reqdepSolution)
                .Select(t => t.Required.reqdepSolution)
                .ToList();
            if (dependencies.Count > 0)
            {
                run.AppendChild(new Text("This solution has the following dependencies: "));
                foreach (string solution in dependencies)
                {
                    AddHeading("Solution: " + solution, "Heading2");
                    foreach (SolutionDependency dependency in content.solution.Dependencies.Where(p => p.Required.reqdepSolution.Equals(solution)))
                    {
                        Table table = CreateTable();
                        table.Append(CreateHeaderRow(new Text("Property"), new Text("Required Component"), new Text("Required By")));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepDisplayName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepDisplayName))
                            table.Append(CreateRow(new Text("Display Name"), new Text(dependency.Required.reqdepDisplayName), new Text(dependency.Dependent.reqdepDisplayName)));
                        if (!String.IsNullOrEmpty(dependency.Required.Type) || !String.IsNullOrEmpty(dependency.Dependent.Type))
                            table.Append(CreateRow(new Text("Type"), new Text(dependency.Required.Type), new Text(dependency.Dependent.Type)));
                        if (!String.IsNullOrEmpty(dependency.Required.SchemaName) || !String.IsNullOrEmpty(dependency.Dependent.SchemaName))
                            table.Append(CreateRow(new Text("Schema Name"), new Text(dependency.Required.SchemaName), new Text(dependency.Dependent.SchemaName)));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepSolution) || !String.IsNullOrEmpty(dependency.Dependent.reqdepSolution))
                            table.Append(CreateRow(new Text("Solution"), new Text(dependency.Required.reqdepSolution), new Text(dependency.Dependent.reqdepSolution)));
                        if (!String.IsNullOrEmpty(dependency.Required.ID) || !String.IsNullOrEmpty(dependency.Dependent.ID))
                            table.Append(CreateRow(new Text("ID"), new Text(dependency.Required.ID), new Text(dependency.Dependent.ID)));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepIdSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepIdSchemaName))
                            table.Append(CreateRow(new Text("ID Schema Name"), new Text(dependency.Required.reqdepIdSchemaName), new Text(dependency.Dependent.reqdepIdSchemaName)));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepParentDisplayName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepParentDisplayName))
                            table.Append(CreateRow(new Text("Parent Display Name"), new Text(dependency.Required.reqdepParentDisplayName), new Text(dependency.Dependent.reqdepParentDisplayName)));
                        if (!String.IsNullOrEmpty(dependency.Required.reqdepParentSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.reqdepParentSchemaName))
                            table.Append(CreateRow(new Text("Parent Schema Name"), new Text(dependency.Required.reqdepParentSchemaName), new Text(dependency.Dependent.reqdepParentSchemaName)));
                        body.Append(table);
                        para = body.AppendChild(new Paragraph());
                        run = para.AppendChild(new Run());
                    }
                }
            }
            else
            {
                run.AppendChild(new Text("This solution has no dependencies."));
            }
        }

        private void renderEntities()
        {
            AddHeading("Tables", "Heading2");
            Paragraph para;
            Run run;
            foreach (TableEntity tableEntity in content.solution.Customizations.getEntities().OrderBy(e => e.getLocalizedName()))
            {
                AddHeading(tableEntity.getLocalizedName() + " (" + tableEntity.getName() + ")", "Heading3");
                Table table = CreateTable();
                table.Append(CreateRow(new Text("Primary Column"), new Text(tableEntity.getPrimaryColumn())));
                table.Append(CreateRow(new Text("Description"), new Text(tableEntity.getDescription())));
                table.Append(CreateRow(new Text("Entity Set Name"), new Text(tableEntity.GetEntitySetName())));
                table.Append(CreateRow(new Text("Record Ownership"), new Text(tableEntity.GetOwnershipType())));
                table.Append(CreateRow(new Text("Auditing"), new Text(tableEntity.IsAuditEnabled() ? "Enabled" : "Disabled")));
                table.Append(CreateRow(new Text("Customizable"), new Text(tableEntity.IsCustomizable() ? "Yes" : "No")));
                table.Append(CreateRow(new Text("Change Tracking"), new Text(tableEntity.IsChangeTrackingEnabled() ? "Enabled" : "Disabled")));
                table.Append(CreateRow(new Text("Is Activity"), new Text(tableEntity.IsActivity() ? "Yes" : "No")));
                table.Append(CreateRow(new Text("Quick Create"), new Text(tableEntity.IsQuickCreateEnabled() ? "Enabled" : "Disabled")));
                table.Append(CreateRow(new Text("Connections"), new Text(tableEntity.IsConnectionsEnabled() ? "Enabled" : "Disabled")));
                table.Append(CreateRow(new Text("Duplicate Detection"), new Text(tableEntity.IsDuplicateCheckSupported() ? "Enabled" : "Disabled")));
                table.Append(CreateRow(new Text("Mobile Visible"), new Text(tableEntity.IsVisibleInMobile() ? "Yes" : "No")));
                table.Append(CreateRow(new Text("Introduced Version"), new Text(tableEntity.GetIntroducedVersion())));
                body.Append(table);
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                if (tableEntity.GetColumns().Count > 0)
                {
                    var columns = documentDefaultColumns
                        ? tableEntity.GetColumns()
                        : tableEntity.GetColumns().Where(c => !c.isDefaultColumn()).ToList();
                    if (columns.Count > 0)
                    {
                    AddHeading("Columns", "Heading4");
                    table = CreateTable();
                    table.Append(CreateHeaderRow(new Text("Display Name"),
                                                 new Text("Logical Name"),
                                                 new Text("Name"),
                                                 new Text("Data type")));
                    foreach (ColumnEntity columnEntity in columns)
                    {
                        string primaryNameColumn = columnEntity.getDisplayMask().Contains("PrimaryName") ? " (Primary name column)" : "";
                        table.Append(CreateRow(
                            new Text(columnEntity.getDisplayName() + primaryNameColumn),
                            new Text(columnEntity.getLogicalName()),
                            new Text(columnEntity.getName()),
                            new Text(columnEntity.getDataType())));
                    }
                    body.Append(table);
                    para = body.AppendChild(new Paragraph());
                    run = para.AppendChild(new Run());

                    foreach (ColumnEntity columnEntity in columns)
                    {
                        string primaryNameColumn = columnEntity.getDisplayMask().Contains("PrimaryName") ? " (Primary name column)" : "";
                        string columnHeading = !String.IsNullOrEmpty(columnEntity.getDisplayName())
                            ? columnEntity.getDisplayName() + " (" + columnEntity.getLogicalName() + ")"
                            : columnEntity.getLogicalName();
                        AddHeading(columnHeading + primaryNameColumn, "Heading5");
                        table = CreateTable();
                        table.Append(CreateRow(new Text("Display Name"), new Text(columnEntity.getDisplayName())));
                        table.Append(CreateRow(new Text("Logical Name"), new Text(columnEntity.getLogicalName())));
                        table.Append(CreateRow(new Text("Physical Name"), new Text(columnEntity.getName())));
                        table.Append(CreateRow(new Text("Data Type"), new Text(columnEntity.getDataType())));
                        table.Append(CreateRow(new Text("Custom Field"), new Text(columnEntity.IsCustomField() ? "Yes" : "No")));
                        table.Append(CreateRow(new Text("Auditing"), new Text(columnEntity.IsAuditEnabled() ? "Enabled" : "Disabled")));
                        table.Append(CreateRow(new Text("Customizable"), new Text(columnEntity.isCustomizable().ToString())));
                        table.Append(CreateRow(new Text("Required"), new Text(columnEntity.isRequired().ToString())));
                        table.Append(CreateRow(new Text("Searchable"), new Text(columnEntity.isSearchable().ToString())));
                        table.Append(CreateRow(new Text("Secured"), new Text(columnEntity.IsSecured() ? "Yes" : "No")));
                        table.Append(CreateRow(new Text("Filterable"), new Text(columnEntity.IsFilterable() ? "Yes" : "No")));
                        body.Append(table);
                        para = body.AppendChild(new Paragraph());
                        run = para.AppendChild(new Run());
                    }
                    }
                }

                if (tableEntity.GetForms().Count > 0)
                {
                    AddHeading("Forms", "Heading4");
                    table = CreateTable();
                    table.Append(CreateHeaderRow(new Text("Name"),
                                                 new Text("Type"),
                                                 new Text("Default"),
                                                 new Text("State"),
                                                 new Text("Customizable")));
                    foreach (FormEntity formEntity in tableEntity.GetForms())
                    {
                        table.Append(CreateRow(
                            new Text(formEntity.GetFormName()),
                            new Text(formEntity.GetFormTypeDisplayName()),
                            new Text(formEntity.IsDefault() ? "Yes" : "No"),
                            new Text(formEntity.IsActive() ? "Active" : "Inactive"),
                            new Text(formEntity.IsCustomizable() ? "Yes" : "No")));
                    }
                    body.Append(table);
                    para = body.AppendChild(new Paragraph());
                    run = para.AppendChild(new Run());

                    // Generate SVG + PNG files for all forms in this table
                    Dictionary<string, string> columnDisplayNames = tableEntity.GetColumns().ToDictionary(c => c.getLogicalName(), c => c.getDisplayName(), StringComparer.OrdinalIgnoreCase);
                    Dictionary<string, string> formSvgFiles = FormSvgBuilder.GenerateFormSvgs(tableEntity, content.folderPath, columnDisplayNames);

                    foreach (FormEntity formEntity in tableEntity.GetForms())
                    {
                        List<FormTab> tabs = formEntity.GetTabs();
                        if (tabs.Count > 0)
                        {
                            string formTypeLabel = formEntity.GetFormTypeDisplayName();
                            AddHeading("Form (" + formTypeLabel + "): " + formEntity.GetFormName(), "Heading5");

                            // SVG wireframe mockup with PNG fallback
                            string formKey = formEntity.GetFormName() + "|" + formTypeLabel;
                            if (formSvgFiles.TryGetValue(formKey, out string svgRelPath))
                            {
                                string svgFilePath = Path.Combine(content.folderPath, svgRelPath);
                                string pngFilePath = Path.ChangeExtension(svgFilePath, ".png");
                                if (File.Exists(svgFilePath) && File.Exists(pngFilePath))
                                {
                                    ImagePart formImagePart = mainPart.AddImagePart(ImagePartType.Png);
                                    int formImageWidth, formImageHeight;
                                    using (FileStream stream = new FileStream(pngFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                    {
                                        using (var image = Image.FromStream(stream, false, false))
                                        {
                                            formImageWidth = image.Width;
                                            formImageHeight = image.Height;
                                        }
                                        stream.Position = 0;
                                        formImagePart.FeedData(stream);
                                    }
                                    ImagePart formSvgPart = mainPart.AddNewPart<ImagePart>("image/svg+xml", "rId" + (new Random()).Next(100000, 999999));
                                    using (FileStream stream = new FileStream(svgFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                                    {
                                        formSvgPart.FeedData(stream);
                                    }
                                    body.AppendChild(new Paragraph(new Run(
                                        InsertSvgImage(mainPart.GetIdOfPart(formSvgPart), mainPart.GetIdOfPart(formImagePart), formImageWidth, formImageHeight)
                                    )));
                                }
                            }

                            // Rendering Forms visually now, keeping this code for reference
                            // foreach (FormTab tab in tabs)
                            // {
                            //     para = body.AppendChild(new Paragraph());
                            //     Run boldRun = para.AppendChild(new Run());
                            //     boldRun.AppendChild(new RunProperties(new Bold()));
                            //     boldRun.AppendChild(new Text("Tab: " + tab.GetName() + (tab.IsVisible() ? "" : " (hidden)")));
                            //     foreach (FormSection section in tab.GetSections())
                            //     {
                            //         List<FormControl> controls = section.GetControls();
                            //         if (controls.Count > 0)
                            //         {
                            //             para = body.AppendChild(new Paragraph());
                            //             run = para.AppendChild(new Run(new Text("Section: " + section.GetName() + (section.IsVisible() ? "" : " (hidden)"))));
                            //             table = CreateTable();
                            //             table.Append(CreateHeaderRow(new Text("#"), new Text("Control"), new Text("Field")));
                            //             int controlIndex = 1;
                            //             foreach (FormControl control in controls)
                            //             {
                            //                 string fieldName = !String.IsNullOrEmpty(control.GetDataFieldName()) ? control.GetDataFieldName() : control.GetId();
                            //                 table.Append(CreateRow(new Text(controlIndex.ToString()), new Text(control.GetId()), new Text(fieldName)));
                            //                 controlIndex++;
                            //             }
                            //             body.Append(table);
                            //             para = body.AppendChild(new Paragraph());
                            //             run = para.AppendChild(new Run());
                            //         }
                            //     }
                            // }
                        }
                    }
                }

                if (tableEntity.GetViews().Count > 0)
                {
                    AddHeading("Views", "Heading4");
                    table = CreateTable();
                    table.Append(CreateHeaderRow(new Text("Name"),
                                                 new Text("Type"),
                                                 new Text("Default"),
                                                 new Text("Customizable")));
                    foreach (ViewEntity viewEntity in tableEntity.GetViews())
                    {
                        table.Append(CreateRow(
                            new Text(viewEntity.GetViewName()),
                            new Text(viewEntity.GetQueryTypeDisplayName()),
                            new Text(viewEntity.IsDefault() ? "Yes" : "No"),
                            new Text(viewEntity.IsCustomizable() ? "Yes" : "No")));
                    }
                    body.Append(table);
                    para = body.AppendChild(new Paragraph());
                    run = para.AppendChild(new Run());

                    Dictionary<string, string> columnDisplayNames = tableEntity.GetColumns().ToDictionary(c => c.getLogicalName(), c => c.getDisplayName(), StringComparer.OrdinalIgnoreCase);
                    foreach (ViewEntity viewEntity in tableEntity.GetViews())
                    {
                        List<ViewColumn> viewColumns = viewEntity.GetColumns();
                        if (viewColumns.Count > 0)
                        {
                            AddHeading("View: " + viewEntity.GetViewName(), "Heading5");
                            table = CreateTable();
                            table.Append(CreateHeaderRow(new Text("#"), new Text("Column"), new Text("Width")));
                            foreach (ViewColumn vc in viewColumns)
                            {
                                string colName = vc.GetName();
                                string displayName = columnDisplayNames.TryGetValue(colName, out string dn) && !String.IsNullOrEmpty(dn) ? dn + " (" + colName + ")" : colName;
                                table.Append(CreateRow(new Text(vc.Order.ToString()), new Text(displayName), new Text(vc.GetWidth())));
                            }
                            body.Append(table);

                            // View controls table (sort orders, filters)
                            List<ViewSortOrder> sortOrders = viewEntity.GetSortOrders();
                            ViewFilter filter = viewEntity.GetFilter();
                            string filterText = filter?.ToDisplayString(columnDisplayNames) ?? "";
                            if (sortOrders.Count > 0 || !string.IsNullOrEmpty(filterText))
                            {
                                table = CreateTable();
                                table.Append(CreateHeaderRow(new Text("View Controls"), new Text("Details")));
                                if (sortOrders.Count > 0)
                                {
                                    string sortText = string.Join(", ", sortOrders.Select(s => s.ToDisplayString(columnDisplayNames)));
                                    table.Append(CreateRow(new Text("Sort by"), new Text(sortText)));
                                }
                                if (!string.IsNullOrEmpty(filterText))
                                {
                                    table.Append(CreateRow(new Text("Filter"), new Text(filterText)));
                                }
                                body.Append(table);
                            }

                            para = body.AppendChild(new Paragraph());
                            run = para.AppendChild(new Run());
                        }
                    }
                }
            }
            // Ribbon Customization Summary
            List<RibbonCustomizationEntity> ribbonCustomizations = content.solution.Customizations.getRibbonCustomizations();
            if (ribbonCustomizations.Count > 0)
            {
                AddHeading("Ribbon Customizations", "Heading3");
                Table ribbonTable = CreateTable();
                ribbonTable.Append(CreateHeaderRow(new Text("Table"), new Text("Hidden Actions"), new Text("Command Definitions"), new Text("Display Rules"), new Text("Enable Rules")));
                foreach (RibbonCustomizationEntity ribbon in ribbonCustomizations)
                {
                    ribbonTable.Append(CreateRow(
                        new Text(ribbon.EntityName ?? ""),
                        new Text(ribbon.HiddenActions.Count.ToString()),
                        new Text(ribbon.CommandDefinitionCount.ToString()),
                        new Text(ribbon.DisplayRuleCount.ToString()),
                        new Text(ribbon.EnableRuleCount.ToString())));
                }
                body.Append(ribbonTable);
                body.AppendChild(new Paragraph(new Run()));
            }
            AddHeading("Table Relationships", "Heading3");
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Png);
            int imageWidth, imageHeight;
            using (FileStream stream = new FileStream(content.folderPath + "dataverse.png", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var image = Image.FromStream(stream, false, false))
                {
                    imageWidth = image.Width;
                    imageHeight = image.Height;
                }
                stream.Position = 0;
                imagePart.FeedData(stream);
            }
            ImagePart svgPart = mainPart.AddNewPart<ImagePart>("image/svg+xml", "rId" + (new Random()).Next(100000, 999999));
            using (FileStream stream = new FileStream(content.folderPath + "dataverse.svg", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                svgPart.FeedData(stream);
            }
            body.AppendChild(new Paragraph(new Run(
                InsertSvgImage(mainPart.GetIdOfPart(svgPart), mainPart.GetIdOfPart(imagePart), imageWidth, imageHeight)
            )));
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
        }

        private void renderAIModels()
        {
            AddHeading("AI Models", "Heading2");
            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("AI Model")));
            foreach (AIModel aiModel in content.solution.Customizations.getAIModels().OrderBy(o => o.getName()))
            {
                table.Append(CreateRow(new Text(aiModel.getName())));
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run()));
        }

        private void renderAgents()
        {
            AddHeading("Agents", "Heading2");
            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Agent")));
            foreach (AgentEntity agent in content.agents.OrderBy(a => a.Name))
            {
                table.Append(CreateRow(new Text(agent.Name)));
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run()));
        }

        private void renderDataflows()
        {
            AddHeading("Dataflows", "Heading2");
            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Dataflow"), new Text("Queries"), new Text("State")));
            foreach (DataflowEntity df in content.dataflows.OrderBy(d => d.GetDisplayName()))
            {
                table.Append(CreateRow(new Text(df.GetDisplayName()), new Text(df.Queries.Count.ToString()), new Text(df.GetStateLabel())));
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run()));
        }

        private void renderWebResources()
        {
            AddHeading("Web Resources", "Heading2");
            List<WebResourceEntity> webResources = content.solution.Customizations.getWebResources();
            if (webResources.Count > 0)
            {
                body.AppendChild(new Paragraph(new Run(new Text(
                    "See the separate Web Resources document for full details, image previews, and source code."))));
                Table table = CreateTable();
                table.Append(CreateHeaderRow(new Text("Display Name"), new Text("Name"), new Text("Type"), new Text("Introduced Version")));
                foreach (WebResourceEntity wr in webResources)
                {
                    table.Append(CreateRow(
                        new Text(!string.IsNullOrEmpty(wr.DisplayName) ? wr.DisplayName : wr.Name ?? ""),
                        new Text(wr.Name ?? ""),
                        new Text(wr.GetTypeDisplayName()),
                        new Text(wr.IntroducedVersion ?? "")));
                }
                body.Append(table);
            }
            body.AppendChild(new Paragraph(new Run()));
        }

        private void renderAppActions()
        {
            AddHeading("Command Bar Buttons", "Heading2");
            if (content.solution.AppActions.Count > 0)
            {
                Table table = CreateTable();
                table.Append(CreateHeaderRow(new Text("Label"), new Text("Table"), new Text("App Module"), new Text("Icon"), new Text("Hidden")));
                foreach (AppActionEntity action in content.solution.AppActions.OrderBy(a => a.ButtonLabel ?? a.Name))
                {
                    table.Append(CreateRow(
                        new Text(action.ButtonLabel ?? action.Name ?? ""),
                        new Text(action.ContextEntity ?? ""),
                        new Text(action.AppModuleName ?? ""),
                        new Text(action.FontIcon ?? ""),
                        new Text(action.IsHidden ? "Yes" : "No")));
                }
                body.Append(table);
            }
            body.AppendChild(new Paragraph(new Run()));
        }

        private void renderSettingDefinitions()
        {
            AddHeading("Setting Definitions", "Heading2");
            if (content.solution.SettingDefinitions.Count > 0)
            {
                foreach (SettingDefinitionEntity setting in content.solution.SettingDefinitions.OrderBy(s => s.DisplayName ?? s.UniqueName))
                {
                    AddHeading(setting.DisplayName ?? setting.UniqueName, "Heading3");
                    Table table = CreateTable();
                    table.Append(CreateHeaderRow(new Text("Property"), new Text("Value")));
                    table.Append(CreateRow(new Text("Internal Name"), new Text(setting.UniqueName ?? "")));
                    table.Append(CreateRow(new Text("Data Type"), new Text(setting.GetDataTypeDisplayName())));
                    table.Append(CreateRow(new Text("Default Value"), new Text(setting.DefaultValue ?? "")));
                    table.Append(CreateRow(new Text("Description"), new Text(setting.Description ?? "")));
                    table.Append(CreateRow(new Text("Is Customizable"), new Text(setting.IsCustomizable ? "Yes" : "No")));
                    table.Append(CreateRow(new Text("Is Hidden"), new Text(setting.IsHidden ? "Yes" : "No")));
                    table.Append(CreateRow(new Text("Is Overridable"), new Text(setting.IsOverridable ? "Yes" : "No")));
                    body.Append(table);
                    body.AppendChild(new Paragraph());
                }
            }
        }

        private void renderFormulaDefinitions()
        {
            AddHeading("Formulas", "Heading2");
            if (content.solution.FormulaDefinitions.Count > 0)
            {
                // Group by table name
                var grouped = content.solution.FormulaDefinitions
                    .OrderBy(f => f.TableName).ThenBy(f => f.ColumnName)
                    .GroupBy(f => f.TableName);
                foreach (var group in grouped)
                {
                    AddHeading(group.Key, "Heading3");
                    Table table = CreateTable();
                    table.Append(CreateHeaderRow(new Text("Column"), new Text("Type"), new Text("Formula")));
                    foreach (FormulaDefinitionEntity formula in group)
                    {
                        table.Append(CreateRow(
                            new Text(formula.ColumnName ?? ""),
                            new Text(formula.Type ?? ""),
                            new Text(formula.Content ?? "")));
                    }
                    body.Append(table);
                    body.AppendChild(new Paragraph());
                }
            }
        }

        private void renderWorkflows()
        {
            AddHeading("Workflow", "Heading2");
            List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == "Workflow").ToList();
            if (components.Count > 0)
            {
                var sortedComponents = components
                    .Select(c => (comp: c, parts: content.GetWorkflowDisplayParts(c)))
                    .OrderBy(x => x.parts.Name, StringComparer.OrdinalIgnoreCase).ToList();
                Table table = CreateTable();
                table.Append(CreateHeaderRow(new Text("Name"), new Text("Trigger Type"), new Text("Flow Type")));
                foreach (var (comp, parts) in sortedComponents)
                {
                    table.Append(CreateRow(new Text(parts.Name), new Text(parts.TriggerInfo), new Text(parts.FlowType)));
                }
                body.Append(table);
                body.AppendChild(new Paragraph());
            }
        }

        private void renderBusinessProcessFlows()
        {
            AddHeading("Business Process Flows", "Heading2");
            body.AppendChild(new Paragraph(new Run(
                new Text($"This solution contains {content.businessProcessFlows.Count} Business Process Flow(s)."))));
            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Name"), new Text("Primary Entity"), new Text("Stages"), new Text("State")));
            foreach (var bpf in content.businessProcessFlows.OrderBy(b => b.GetDisplayName(), StringComparer.OrdinalIgnoreCase))
            {
                string entity = content.context?.GetTableDisplayName(bpf.PrimaryEntity) ?? bpf.PrimaryEntity ?? "";
                table.Append(CreateRow(
                    new Text(bpf.GetDisplayName()),
                    new Text(entity),
                    new Text(bpf.Stages.Count.ToString()),
                    new Text(bpf.GetStateLabel())
                ));
            }
            body.Append(table);
            body.AppendChild(new Paragraph());
        }

        private void renderOptionSets()
        {
            AddHeading("Option Sets", "Heading2");
            Paragraph para;
            Run run;
            List<OptionSetEntity> optionSets = content.solution.Customizations.getOptionSets();
            if (optionSets.Count > 0)
            {
                foreach (OptionSetEntity optionSet in optionSets.OrderBy(o => o.GetDisplayName()))
                {
                    AddHeading(optionSet.GetDisplayName() + " (" + optionSet.Name + ")", "Heading3");
                    Table table = CreateTable();
                    table.Append(CreateRow(new Text("Type"), new Text(optionSet.OptionSetType)));
                    table.Append(CreateRow(new Text("Is Global"), new Text(optionSet.IsGlobal ? "Yes" : "No")));
                    table.Append(CreateRow(new Text("Is Customizable"), new Text(optionSet.IsCustomizable ? "Yes" : "No")));
                    if (!String.IsNullOrEmpty(optionSet.Description))
                        table.Append(CreateRow(new Text("Description"), new Text(optionSet.Description)));
                    body.Append(table);

                    if (optionSet.Options.Count > 0)
                    {
                        para = body.AppendChild(new Paragraph());
                        run = para.AppendChild(new Run());
                        run.AppendChild(new Text("Options:"));
                        table = CreateTable();
                        table.Append(CreateHeaderRow(new Text("Value"), new Text("Label")));
                        foreach (OptionSetOption option in optionSet.Options)
                        {
                            table.Append(CreateRow(new Text(option.Value ?? ""), new Text(option.Label ?? "")));
                        }
                        body.Append(table);
                    }
                    para = body.AppendChild(new Paragraph());
                    run = para.AppendChild(new Run());
                }
            }
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
        }

        private void renderSecurityRoles()
        {
            AddHeading("Security Roles", "Heading2");
            Paragraph para;
            Run run;
            foreach (RoleEntity role in content.solution.Customizations.getRoles().OrderBy(r => r.Name))
            {
                AddHeading(role.Name + " (" + role.ID + ")", "Heading3");
                Table table = CreateTable();
                table.Append(CreateHeaderRow(new Text("Table"), new Text("Create"), new Text("Read"), new Text("Write"), new Text("Delete"), new Text("Append"), new Text("Append To"), new Text("Assign"), new Text("Share")));
                foreach (TableAccess tableAccess in role.Tables.OrderBy(o => o.Name))
                {
                    TableRow row = CreateRow(new Text(tableAccess.Name),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.Create)),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.Read)),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.Write)),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.Delete)),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.Append)),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.AppendTo)),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.Assign)),
                                            new Paragraph(new ParagraphProperties(new Justification() { Val = JustificationValues.Center }), getAccessLevelIcon(tableAccess.Share))
                    );
                    table.Append(row);
                }
                body.Append(table);
                if (role.miscellaneousPrivileges.Count > 0)
                {
                    para = body.AppendChild(new Paragraph());
                    run = para.AppendChild(new Run());
                    run.AppendChild(new Text("Miscellaneous Privileges associated with this role:"));
                    table = CreateTable();
                    table.Append(CreateHeaderRow(new Text("Miscellaneous Privilege"), new Text("Level")));
                    foreach (KeyValuePair<string, string> miscPrivilege in role.miscellaneousPrivileges)
                    {
                        table.Append(CreateRow(new Text(miscPrivilege.Key), getAccessLevelIcon(miscPrivilege.Value)));
                    }
                    body.Append(table);
                }
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
            }
        }

        private Drawing getAccessLevelIcon(AccessLevel accessLevel)
        {
            string iconFile = AssemblyHelper.GetExecutablePath() + @"Resources\security-role-access-level-";
            iconFile += accessLevel switch
            {
                AccessLevel.Global => "global.png",
                AccessLevel.Deep => "deep.png",
                AccessLevel.Local => "local.png",
                AccessLevel.Basic => "basic.png",
                _ => "none.png",
            };
            using FileStream stream = new FileStream(iconFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
            imagePart.FeedData(stream);
            return InsertImage(mainPart.GetIdOfPart(imagePart), 12, 12);
        }

        private Drawing getAccessLevelIcon(string accessLevel)
        {
            AccessLevel level = accessLevel switch
            {
                "Global" => AccessLevel.Global,
                "Deep" => AccessLevel.Deep,
                "Local" => AccessLevel.Local,
                "Basic" => AccessLevel.Basic,
                _ => AccessLevel.None
            };
            return getAccessLevelIcon(level);
        }
    }
}