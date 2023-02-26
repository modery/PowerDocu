using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PowerDocu.Common;

namespace PowerDocu.SolutionDocumenter
{
    class SolutionWordDocBuilder : WordDocBuilder
    {
        private readonly SolutionDocumentationContent content;

        public SolutionWordDocBuilder(SolutionDocumentationContent contentDocumentation, string template)
        {
            content = contentDocumentation;
            Directory.CreateDirectory(content.folderPath);
            string filename = InitializeWordDocument(content.folderPath + "Solution - " + content.filename, template);
            using WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true);
            mainPart = wordDocument.MainDocumentPart;
            body = mainPart.Document.Body;
            PrepareDocument(!String.IsNullOrEmpty(template));
            addSolutionMetadata();
            addSolutionComponents();
        }

        private void addSolutionMetadata()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(content.solution.UniqueName));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Status"), new Text(content.solution.isManaged ? "Managed" : "Unmanaged")));
            table.Append(CreateRow(new Text("Version"), new Text(content.solution.Version)));
            table.Append(CreateRow(new Text("Publisher"), GetPublisherInfo()));
            body.Append(table);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("Statistics"));
            ApplyStyleToParagraph("Heading1", para);
            table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Component Type"), new Text("Number of Components")));
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == componentType).OrderBy(c => c.reqdepDisplayName).ToList();
                table.Append(CreateRow(new Text(componentType), new Text(components.Count.ToString())));
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

        private void addSolutionComponents()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Solution Components"));
            ApplyStyleToParagraph("Heading1", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("This solution contains the following components"));
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                switch (componentType)
                {
                    case "Role":
                        renderSecurityRoles();
                        break;
                    case "Entity":
                        renderEntities();
                        break;
                    default:
                        para = body.AppendChild(new Paragraph());
                        run = para.AppendChild(new Run());
                        run.AppendChild(new Text(componentType));
                        ApplyStyleToParagraph("Heading2", para);
                        List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == componentType).OrderBy(c => c.reqdepDisplayName).ToList();
                        Table table = CreateTable();
                        table.Append(CreateHeaderRow(new Text(componentType)));
                        foreach (SolutionComponent component in components)
                        {
                            table.Append(CreateRow(new Text(content.solution.GetDisplayNameForComponent(component))));
                        }
                        body.Append(table);
                        para = body.AppendChild(new Paragraph());
                        run = para.AppendChild(new Run());
                        break;
                }
            }
            run.AppendChild(new Text("Solution Component Dependencies"));
            ApplyStyleToParagraph("Heading1", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
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
                run.AppendChild(new Text("This solution has the following dependencies: "));
                foreach (string solution in dependencies)
                {
                    para = body.AppendChild(new Paragraph());
                    run = para.AppendChild(new Run());
                    run.AppendChild(new Text("Solution: " + solution));
                    ApplyStyleToParagraph("Heading2", para);
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
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Tables"));
            ApplyStyleToParagraph("Heading2", para);
            foreach (TableEntity tableEntity in content.solution.Customizations.getEntities())
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run(new Text(tableEntity.getLocalizedName() + " (" + tableEntity.getName() + ")")));
                //run.AppendChild(new Text(entity..Name + " (" + role.ID + ")"));
                ApplyStyleToParagraph("Heading3", para);
                Table table = CreateTable();
                table.Append(CreateRow(new Text("Primary Column"), new Text(tableEntity.getPrimaryColumn())));
                table.Append(CreateRow(new Text("Description"), new Text(tableEntity.getDescription())));
                body.Append(table);
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                table = CreateTable();
                table.Append(CreateHeaderRow(new Text("Display Name"),
                                             new Text("Name"),
                                             new Text("Data type"),
                                             new Text("Customizable"),
                                             new Text("Required"),
                                             new Text("Searchable")));
                foreach (ColumnEntity columnEntity in tableEntity.GetColumns())
                {
                    string primaryNameColumn = columnEntity.getDisplayMask().Contains("PrimaryName") ? " (Primary name column)" : "";
                    table.Append(CreateRow(
                        new Text(columnEntity.getDisplayName() + primaryNameColumn),
                        new Text(columnEntity.getName()),
                        new Text(columnEntity.getDataType()),
                        new Text(columnEntity.isCustomizable().ToString()),
                        new Text(columnEntity.isRequired().ToString()),
                        new Text(columnEntity.isSearchable().ToString())));
                }
                body.Append(table);
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
            }
        }

        private void renderSecurityRoles()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Security Roles"));
            ApplyStyleToParagraph("Heading2", para);
            foreach (RoleEntity role in content.solution.Customizations.getRoles())
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(role.Name + " (" + role.ID + ")"));
                ApplyStyleToParagraph("Heading3", para);
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
            using FileStream stream = new FileStream(iconFile, FileMode.Open);
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
                "Loca" => AccessLevel.Local,
                "Basic" => AccessLevel.Basic,
                _ => AccessLevel.None
            };
            return getAccessLevelIcon(level);
        }
    }
}