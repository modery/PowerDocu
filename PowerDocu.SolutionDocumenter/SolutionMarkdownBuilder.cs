using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using PowerDocu.Common;
using Grynwald.MarkdownGenerator;
using Svg;

namespace PowerDocu.SolutionDocumenter
{
    class SolutionMarkdownBuilder : MarkdownBuilder
    {
        private readonly SolutionDocumentationContent content;
        private readonly string solutionDocumentFileName;
        private readonly MdDocument solutionDoc;
        public SolutionMarkdownBuilder(SolutionDocumentationContent contentDocumentation)
        {
            content = contentDocumentation;
            Directory.CreateDirectory(content.folderPath);
            solutionDocumentFileName = ("solution " + content.filename + ".md").Replace(" ", "-");
            solutionDoc = new MdDocument();

            addSolutionOverview();
            addSolutionComponents();
            solutionDoc.Save(content.folderPath + "/" + solutionDocumentFileName);
            NotificationHelper.SendNotification("Created Markdown documentation for solution" + content.solution.UniqueName);
        }

        private void addSolutionOverview()
        {
            List<MdTableRow> tableRows = new List<MdTableRow>();
            solutionDoc.Root.Add(new MdHeading(content.solution.UniqueName, 1));
            tableRows.Add(new MdTableRow("Status", content.solution.isManaged ? "Managed" : "Unmanaged"));
            tableRows.Add(new MdTableRow("Version", content.solution.Version));
            solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Details"), tableRows));
            solutionDoc.Root.Add(GetPublisherInfo());
        }

        private MdTable GetPublisherInfo()
        {
            List<MdTableRow> tableRows = new List<MdTableRow>();
            tableRows.Append(new MdTableRow("Name", content.solution.Publisher.UniqueName));
            tableRows.Append(new MdTableRow("Email", content.solution.Publisher.EMailAddress));
            tableRows.Append(new MdTableRow("CustomizationPrefix", content.solution.Publisher.CustomizationPrefix));
            tableRows.Append(new MdTableRow("CustomizationOptionValuePrefix", content.solution.Publisher.CustomizationOptionValuePrefix));
            tableRows.Append(new MdTableRow("SupportingWebsiteUrl", content.solution.Publisher.SupportingWebsiteUrl));
            /*
            if (content.solution.Publisher.Descriptions.Count > 0)
            {
                List<MdTableRow> descriptionsTableRows = new List<MdTableRow>();
                descriptionsTableRows.Append(CreateHeaderRow(new Text("Language Code", "Description")));
                foreach (KeyValuePair<string, string> description in content.solution.Publisher.Descriptions)
                {
                    descriptionsTableRows.Append(new MdTableRow(description.Key, description.Value));
                }
                tableRows.Append(new MdTableRow("Descriptions"), descriptionsTable));
            }
            if (content.solution.Publisher.LocalizedNames.Count > 0)
            {
                List<MdTableRow> localizedNamesTableRows = new List<MdTableRow>();
                localizedNamesTable.Append(CreateHeaderRow(new Text("Language Code", "Description")));
                foreach (KeyValuePair<string, string> localizedName in content.solution.Publisher.LocalizedNames)
                {
                    localizedNamesTableRows.Append(new MdTableRow(localizedName.Key, localizedName.Value));
                }
                tableRows.Append(new MdTableRow("Localized Names"), localizedNamesTable));
            }
            if (content.solution.Publisher.Addresses.Count > 0)
            {
                List<MdTableRow> addressesTableRows = new List<MdTableRow>();
                foreach (Address address in content.solution.Publisher.Addresses)
                {
                    List<MdTableRow> addressTableRows = new List<MdTableRow>();
                    addressTable.Append(CreateHeaderRow(new Text("Property", "Value")));
                    if (!String.IsNullOrEmpty(address.Name))
                        addressTableRows.Append(new MdTableRow("Name", address.Name));
                    if (!String.IsNullOrEmpty(address.AddressNumber))
                        addressTableRows.Append(new MdTableRow("AddressNumber", address.AddressNumber));
                    if (!String.IsNullOrEmpty(address.AddressTypeCode))
                        addressTableRows.Append(new MdTableRow("AddressTypeCode", address.AddressTypeCode));
                    if (!String.IsNullOrEmpty(address.City))
                        addressTableRows.Append(new MdTableRow("City", address.City));
                    if (!String.IsNullOrEmpty(address.County))
                        addressTableRows.Append(new MdTableRow("County", address.County));
                    if (!String.IsNullOrEmpty(address.Country))
                        addressTableRows.Append(new MdTableRow("Country", address.Country));
                    if (!String.IsNullOrEmpty(address.Fax))
                        addressTableRows.Append(new MdTableRow("Fax", address.Fax));
                    if (!String.IsNullOrEmpty(address.FreightTermsCode))
                        addressTableRows.Append(new MdTableRow("FreightTermsCode", address.FreightTermsCode));
                    if (!String.IsNullOrEmpty(address.ImportSequenceNumber))
                        addressTableRows.Append(new MdTableRow("ImportSequenceNumber", address.ImportSequenceNumber));
                    if (!String.IsNullOrEmpty(address.Latitude))
                        addressTableRows.Append(new MdTableRow("Latitude", address.Latitude));
                    if (!String.IsNullOrEmpty(address.Line1))
                        addressTableRows.Append(new MdTableRow("Line1", address.Line1));
                    if (!String.IsNullOrEmpty(address.Line2))
                        addressTableRows.Append(new MdTableRow("Line2", address.Line2));
                    if (!String.IsNullOrEmpty(address.Line3))
                        addressTableRows.Append(new MdTableRow("Line3", address.Line3));
                    if (!String.IsNullOrEmpty(address.Longitude))
                        addressTableRows.Append(new MdTableRow("Longitude", address.Longitude));
                    if (!String.IsNullOrEmpty(address.PostalCode))
                        addressTableRows.Append(new MdTableRow("PostalCode", address.PostalCode));
                    if (!String.IsNullOrEmpty(address.PostOfficeBox))
                        addressTableRows.Append(new MdTableRow("PostOfficeBox", address.PostOfficeBox));
                    if (!String.IsNullOrEmpty(address.PrimaryContactName))
                        addressTableRows.Append(new MdTableRow("PrimaryContactName", address.PrimaryContactName));
                    if (!String.IsNullOrEmpty(address.ShippingMethodCode))
                        addressTableRows.Append(new MdTableRow("ShippingMethodCode", address.ShippingMethodCode));
                    if (!String.IsNullOrEmpty(address.StateOrProvince))
                        addressTableRows.Append(new MdTableRow("StateOrProvince", address.StateOrProvince));
                    if (!String.IsNullOrEmpty(address.Telephone1))
                        addressTableRows.Append(new MdTableRow("Telephone1", address.Telephone1));
                    if (!String.IsNullOrEmpty(address.Telephone2))
                        addressTableRows.Append(new MdTableRow("Telephone2", address.Telephone2));
                    if (!String.IsNullOrEmpty(address.Telephone3))
                        addressTableRows.Append(new MdTableRow("Telephone3", address.Telephone3));
                    if (!String.IsNullOrEmpty(address.TimeZoneRuleVersionNumber))
                        addressTableRows.Append(new MdTableRow("TimeZoneRuleVersionNumber", address.TimeZoneRuleVersionNumber));
                    if (!String.IsNullOrEmpty(address.UPSZone))
                        addressTableRows.Append(new MdTableRow("UPSZone", address.UPSZone));
                    if (!String.IsNullOrEmpty(address.UTCOffset))
                        addressTableRows.Append(new MdTableRow("UTCOffset", address.UTCOffset));
                    if (!String.IsNullOrEmpty(address.UTCConversionTimeZoneCode))
                        addressTableRows.Append(new MdTableRow("UTCConversionTimeZoneCode", address.UTCConversionTimeZoneCode));
                    addressesTable.Append(CreateRow(addressTable));
                }
                tableRows.Append(new MdTableRow("Addresses"), addressesTable));
            }*/
            return new MdTable(new MdTableRow("Publisher", "Details"), tableRows);
        }

        private void addSolutionComponents()
        {
            solutionDoc.Root.Add(new MdHeading("Solution Components", 2));
            solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("This solution contains the following components")));
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                solutionDoc.Root.Add(new MdHeading(componentType, 3));
                List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == componentType).OrderBy(c => c.reqdepDisplayName).ToList();
                List<MdTableRow> componentTableRows = new List<MdTableRow>();
                foreach (SolutionComponent component in components)
                {
                    //todo the name should be retrieved from the component itself, logic should be moved there
                    string name = "";
                    switch (componentType)
                    {
                        case "Canvas App":
                            // todo how can we safely identify the app from here?
                            //name = content.apps.First(a => a.ID.Equals(component.ID))?.Name;
                            name = String.IsNullOrEmpty(component.SchemaName) ? component.ID : component.SchemaName;
                            break;
                        case "Workflow":
                            name = content.flows.First(a => a.Name.ToLower().EndsWith(component.ID.ToLower().Replace("{", "").Replace("}", "")))?.Name;
                            break;
                        default:
                            name = String.IsNullOrEmpty(component.SchemaName) ? component.ID : component.SchemaName;
                            break;
                    }
                    name ??= String.IsNullOrEmpty(component.SchemaName) ? component.ID : component.SchemaName;
                    componentTableRows.Add(new MdTableRow(new MdTextSpan(name)));
                }
                solutionDoc.Root.Add(new MdTable(new MdTableRow(componentType), componentTableRows));
            }

            solutionDoc.Root.Add(new MdHeading("Solution Component Dependencies", 2));
            solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("This solution has the following dependencies")));
            foreach (string solution in content
                                        .solution
                                        .Dependencies
                                        .GroupBy(p => p.Required.reqdepSolution)
                                        .Select(g => g.First())
                                        .OrderBy(t => t.Required.reqdepSolution)
                                        .Select(t => t.Required.reqdepSolution)
                                        .ToList())
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
                    solutionDoc.Root.Add(new MdTable(new MdTableRow("Property", "Requirement", "Dependency"), dependencyTableRows));
                    /*List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == componentType).OrderBy(c => c.DisplayName).ToList();
                    table.Append(CreateHeaderRow(new Text(componentType)));
                    foreach (SolutionComponent component in components)
                    {
                        string name = String.IsNullOrEmpty(component.SchemaName) ? component.ID : component.SchemaName;
                        dependencyTableRows.Add(new MdTableRow(name)));
                        ApplyStyleToParagraph("Heading2", para);
                    }*/
                }
            }
        }
    }
}
