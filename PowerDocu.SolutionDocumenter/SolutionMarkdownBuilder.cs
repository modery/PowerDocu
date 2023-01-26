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
            AddPublisherInfo();
            AddStatistics();
        }

        private void AddStatistics()
        {
            List<MdTableRow> tableRows = new List<MdTableRow>();
            solutionDoc.Root.Add(new MdHeading("Statistics", 2));
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == componentType).OrderBy(c => c.reqdepDisplayName).ToList();
                tableRows.Add(new MdTableRow(componentType, components.Count.ToString()));
            }
            if (tableRows.Count > 0)
                solutionDoc.Root.Add(new MdTable(new MdTableRow("Component Type", "Number of Components"), tableRows));
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
                    //todo add link to documentation
                    componentTableRows.Add(new MdTableRow(new MdTextSpan(content.solution.GetDisplayNameForComponent(component))));
                }
                if (componentTableRows.Count > 0)
                {
                    solutionDoc.Root.Add(new MdTable(new MdTableRow(componentType), componentTableRows));
                }
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
                solutionDoc.Root.Add(new MdParagraph(new MdTextSpan("This solution has the following dependencies")));
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
    }
}
