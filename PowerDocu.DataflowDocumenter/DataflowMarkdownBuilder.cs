using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Grynwald.MarkdownGenerator;

namespace PowerDocu.DataflowDocumenter
{
    class DataflowMarkdownBuilder : MarkdownBuilder
    {
        private readonly DataflowDocumentationContent content;
        private readonly string mainDocumentFileName;
        private readonly MdDocument mainDocument;
        private readonly DocumentSet<MdDocument> set;

        public DataflowMarkdownBuilder(DataflowDocumentationContent contentDocumentation)
        {
            content = contentDocumentation;
            Directory.CreateDirectory(content.folderPath);
            mainDocumentFileName = ("dataflow-" + content.filename + ".md").Replace(" ", "-");
            set = new DocumentSet<MdDocument>();
            mainDocument = set.CreateMdDocument(mainDocumentFileName);

            addOverview();
            addMetadata();
            if (content.dataflow.Queries.Count > 0)
                addQueries();
            if (!string.IsNullOrEmpty(content.dataflow.MashupDocument))
                addMCode();
            if (content.dataflow.ConnectionOverrides.Count > 0)
                addConnectionOverrides();
            if (content.dataflow.RefreshSettings != null)
                addRefreshSettings();
            addSettings();

            set.Save(content.folderPath);
            NotificationHelper.SendNotification("Created Markdown documentation for Dataflow: " + content.dataflow.GetDisplayName());
        }

        private void addOverview()
        {
            mainDocument.Root.Add(new MdHeading(content.dataflow.GetDisplayName(), 1));

            if (content.context?.Solution != null)
            {
                if (content.context?.Config?.documentSolution == true)
                    mainDocument.Root.Add(new MdParagraph(new MdCompositeSpan(new MdTextSpan("Solution: "), new MdLinkSpan(content.context.Solution.UniqueName, "../" + CrossDocLinkHelper.GetSolutionDocMdPath(content.context.Solution.UniqueName)))));
                else
                    mainDocument.Root.Add(new MdParagraph(new MdTextSpan("Solution: " + content.context.Solution.UniqueName)));
            }

            List<MdTableRow> tableRows = new List<MdTableRow>
            {
                new MdTableRow("Name", content.dataflow.GetDisplayName()),
                new MdTableRow("Dataflow ID", content.dataflow.DataflowId ?? ""),
                new MdTableRow("Original Dataflow ID", content.dataflow.OriginalDataflowId ?? ""),
                new MdTableRow("State", content.dataflow.GetStateLabel()),
                new MdTableRow("Status", content.dataflow.GetStatusLabel()),
                new MdTableRow("Is Customizable", content.dataflow.IsCustomizable ? "Yes" : "No"),
                new MdTableRow("Queries", content.dataflow.Queries.Count.ToString()),
                new MdTableRow(content.headerDocumentationGenerated, PowerDocuReleaseHelper.GetTimestampWithVersion())
            };
            mainDocument.Root.Add(new MdTable(new MdTableRow("Property", "Value"), tableRows));
        }

        private void addMetadata()
        {
            mainDocument.Root.Add(new MdHeading(content.headerMetadata, 2));
            List<MdTableRow> tableRows = new List<MdTableRow>();
            if (!string.IsNullOrEmpty(content.dataflow.OwnerName))
                tableRows.Add(new MdTableRow("Owner", content.dataflow.OwnerName));
            if (!string.IsNullOrEmpty(content.dataflow.DataflowType))
                tableRows.Add(new MdTableRow("Dataflow Type", content.dataflow.DataflowType));
            if (!string.IsNullOrEmpty(content.dataflow.CreatedTime))
                tableRows.Add(new MdTableRow("Created", content.dataflow.CreatedTime));
            if (!string.IsNullOrEmpty(content.dataflow.LastUpdateTime))
                tableRows.Add(new MdTableRow("Last Updated", content.dataflow.LastUpdateTime));
            if (!string.IsNullOrEmpty(content.dataflow.PublishStatus))
                tableRows.Add(new MdTableRow("Publish Status", content.dataflow.PublishStatus));
            if (!string.IsNullOrEmpty(content.dataflow.HostContextType))
                tableRows.Add(new MdTableRow("Host Type", content.dataflow.HostContextType));
            if (!string.IsNullOrEmpty(content.dataflow.HostEnvironmentId))
                tableRows.Add(new MdTableRow("Environment ID", content.dataflow.HostEnvironmentId));
            if (!string.IsNullOrEmpty(content.dataflow.DocumentLocale))
                tableRows.Add(new MdTableRow("Locale", content.dataflow.DocumentLocale));
            if (!string.IsNullOrEmpty(content.dataflow.InternalVersion))
                tableRows.Add(new MdTableRow("Internal Version", content.dataflow.InternalVersion));
            if (tableRows.Count > 0)
                mainDocument.Root.Add(new MdTable(new MdTableRow("Property", "Value"), tableRows));
        }

        private void addQueries()
        {
            mainDocument.Root.Add(new MdHeading(content.headerQueries, 2));
            mainDocument.Root.Add(new MdParagraph(new MdTextSpan($"This Dataflow has {content.dataflow.Queries.Count} query/queries.")));

            foreach (var query in content.dataflow.Queries)
            {
                string queryTitle = query.EntityName ?? query.QueryName ?? "Query";
                mainDocument.Root.Add(new MdHeading(queryTitle, 3));

                List<MdTableRow> tableRows = new List<MdTableRow>();
                if (!string.IsNullOrEmpty(query.QueryName))
                    tableRows.Add(new MdTableRow("Query Name", query.QueryName));
                if (!string.IsNullOrEmpty(query.QueryId))
                    tableRows.Add(new MdTableRow("Query ID", query.QueryId));
                if (!string.IsNullOrEmpty(query.EntityName))
                {
                    string entityDisplay = query.EntityName;
                    if (content.context != null)
                    {
                        string resolved = content.context.GetTableDisplayName(query.EntityName);
                        if (resolved != null && resolved != query.EntityName)
                            entityDisplay = $"{resolved} ({query.EntityName})";
                    }
                    tableRows.Add(new MdTableRow("Target Entity", entityDisplay));
                }
                tableRows.Add(new MdTableRow("Load Enabled", query.LoadEnabled ? "Yes" : "No"));
                tableRows.Add(new MdTableRow("Delete Existing Data", query.DeleteExistingDataOnLoad ? "Yes" : "No"));
                if (query.IsCalculatedEntity)
                    tableRows.Add(new MdTableRow("Calculated Entity", "Yes"));
                if (query.IsLinkedEntity)
                    tableRows.Add(new MdTableRow("Linked Entity", "Yes"));
                if (!string.IsNullOrEmpty(query.ResultTypeName))
                    tableRows.Add(new MdTableRow("Result Type", query.ResultTypeName));
                if (tableRows.Count > 0)
                    mainDocument.Root.Add(new MdTable(new MdTableRow("Property", "Value"), tableRows));

                // Field Mappings
                if (query.FieldMappings.Count > 0)
                {
                    mainDocument.Root.Add(new MdHeading("Field Mappings", 4));
                    List<MdTableRow> fieldRows = new List<MdTableRow>();
                    foreach (var field in query.FieldMappings.OrderBy(f => f.DestinationFieldName))
                    {
                        fieldRows.Add(new MdTableRow(
                            field.DestinationFieldName ?? "",
                            field.SourceColumnName ?? "",
                            field.DestinationFieldType ?? ""
                        ));
                    }
                    mainDocument.Root.Add(new MdTable(
                        new MdTableRow("Destination Field", "Source Column", "Destination Type"),
                        fieldRows));
                }
            }
        }

        private void addMCode()
        {
            mainDocument.Root.Add(new MdHeading(content.headerMCode, 2));
            mainDocument.Root.Add(new MdParagraph(new MdRawMarkdownSpan("```\n" + content.dataflow.MashupDocument + "\n```")));
        }

        private void addConnectionOverrides()
        {
            mainDocument.Root.Add(new MdHeading(content.headerConnectionOverrides, 2));
            List<MdTableRow> tableRows = new List<MdTableRow>();
            foreach (var co in content.dataflow.ConnectionOverrides)
            {
                tableRows.Add(new MdTableRow(
                    co.Kind ?? "",
                    co.Path ?? "",
                    co.Provider ?? "",
                    co.EnvironmentName ?? ""
                ));
            }
            mainDocument.Root.Add(new MdTable(
                new MdTableRow("Kind", "Path", "Provider", "Environment"),
                tableRows));
        }

        private void addRefreshSettings()
        {
            var rs = content.dataflow.RefreshSettings;
            mainDocument.Root.Add(new MdHeading(content.headerRefreshSettings, 2));
            List<MdTableRow> tableRows = new List<MdTableRow>();
            if (!string.IsNullOrEmpty(rs.ScheduleRefreshType))
                tableRows.Add(new MdTableRow("Schedule Type", rs.ScheduleRefreshType));
            if (!string.IsNullOrEmpty(rs.RefreshPeriod))
                tableRows.Add(new MdTableRow("Refresh Period", rs.RefreshPeriod));
            if (!string.IsNullOrEmpty(rs.TimeBasedRefreshPeriod))
                tableRows.Add(new MdTableRow("Time-Based Period", rs.TimeBasedRefreshPeriod));
            if (!string.IsNullOrEmpty(rs.TimeZoneId))
                tableRows.Add(new MdTableRow("Time Zone", rs.TimeZoneId));
            if (!string.IsNullOrEmpty(rs.StartDateTime) && rs.StartDateTime != "0001-01-01T00:00:00+00:00")
                tableRows.Add(new MdTableRow("Start Date/Time", rs.StartDateTime));
            if (tableRows.Count > 0)
                mainDocument.Root.Add(new MdTable(new MdTableRow("Property", "Value"), tableRows));
        }

        private void addSettings()
        {
            mainDocument.Root.Add(new MdHeading(content.headerSettings, 2));
            List<MdTableRow> tableRows = new List<MdTableRow>
            {
                new MdTableRow("Fast Combine", content.dataflow.FastCombine ? "Enabled" : "Disabled"),
                new MdTableRow("Allow Native Queries", content.dataflow.AllowNativeQueries ? "Yes" : "No"),
                new MdTableRow("Skip Auto Type Detection", content.dataflow.SkipAutomaticTypeAndHeaderDetection ? "Yes" : "No"),
                new MdTableRow("Disable Auto Anonymous Connection", content.dataflow.DisableAutoAnonymousConnectionUpsert ? "Yes" : "No")
            };
            if (!string.IsNullOrEmpty(content.dataflow.OutputFileFormat))
                tableRows.Insert(2, new MdTableRow("Output File Format", content.dataflow.OutputFileFormat));
            mainDocument.Root.Add(new MdTable(new MdTableRow("Setting", "Value"), tableRows));
        }
    }
}
