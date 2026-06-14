using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PowerDocu.Common;

namespace PowerDocu.DataflowDocumenter
{
    class DataflowWordDocBuilder : WordDocBuilder
    {
        private readonly DataflowDocumentationContent content;

        public DataflowWordDocBuilder(DataflowDocumentationContent contentDocumentation, string template)
        {
            content = contentDocumentation;
            Directory.CreateDirectory(content.folderPath);
            string filename = InitializeWordDocument(content.folderPath + content.filename, template);
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true))
            {
                mainPart = wordDocument.MainDocumentPart;
                body = mainPart.Document.Body;
                PrepareDocument(!String.IsNullOrEmpty(template));

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
            }
            NotificationHelper.SendNotification("Created Word documentation for Dataflow: " + content.dataflow.GetDisplayName());
        }

        private void addOverview()
        {
            AddHeading(content.dataflow.GetDisplayName(), "Heading1");
            body.AppendChild(new Paragraph(new Run()));

            Table table = CreateTable();
            table.Append(CreateRow(new Text("Name"), new Text(content.dataflow.GetDisplayName())));
            table.Append(CreateRow(new Text("Dataflow ID"), new Text(content.dataflow.DataflowId ?? "")));
            table.Append(CreateRow(new Text("Original Dataflow ID"), new Text(content.dataflow.OriginalDataflowId ?? "")));
            table.Append(CreateRow(new Text("State"), new Text(content.dataflow.GetStateLabel())));
            table.Append(CreateRow(new Text("Status"), new Text(content.dataflow.GetStatusLabel())));
            table.Append(CreateRow(new Text("Is Customizable"), new Text(content.dataflow.IsCustomizable ? "Yes" : "No")));
            table.Append(CreateRow(new Text("Queries"), new Text(content.dataflow.Queries.Count.ToString())));
            table.Append(CreateRow(new Text(content.headerDocumentationGenerated),
                new Text(PowerDocuReleaseHelper.GetTimestampWithVersion())));
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addMetadata()
        {
            AddHeading(content.headerMetadata, "Heading2");

            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Property"), new Text("Value")));
            if (!string.IsNullOrEmpty(content.dataflow.OwnerName))
                table.Append(CreateRow(new Text("Owner"), new Text(content.dataflow.OwnerName)));
            if (!string.IsNullOrEmpty(content.dataflow.DataflowType))
                table.Append(CreateRow(new Text("Dataflow Type"), new Text(content.dataflow.DataflowType)));
            if (!string.IsNullOrEmpty(content.dataflow.CreatedTime))
                table.Append(CreateRow(new Text("Created"), new Text(content.dataflow.CreatedTime)));
            if (!string.IsNullOrEmpty(content.dataflow.LastUpdateTime))
                table.Append(CreateRow(new Text("Last Updated"), new Text(content.dataflow.LastUpdateTime)));
            if (!string.IsNullOrEmpty(content.dataflow.PublishStatus))
                table.Append(CreateRow(new Text("Publish Status"), new Text(content.dataflow.PublishStatus)));
            if (!string.IsNullOrEmpty(content.dataflow.HostContextType))
                table.Append(CreateRow(new Text("Host Type"), new Text(content.dataflow.HostContextType)));
            if (!string.IsNullOrEmpty(content.dataflow.HostEnvironmentId))
                table.Append(CreateRow(new Text("Environment ID"), new Text(content.dataflow.HostEnvironmentId)));
            if (!string.IsNullOrEmpty(content.dataflow.DocumentLocale))
                table.Append(CreateRow(new Text("Locale"), new Text(content.dataflow.DocumentLocale)));
            if (!string.IsNullOrEmpty(content.dataflow.InternalVersion))
                table.Append(CreateRow(new Text("Internal Version"), new Text(content.dataflow.InternalVersion)));
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addQueries()
        {
            AddHeading(content.headerQueries, "Heading2");
            body.AppendChild(new Paragraph(new Run(
                new Text($"This Dataflow has {content.dataflow.Queries.Count} query/queries."))));

            foreach (var query in content.dataflow.Queries)
            {
                string queryTitle = query.EntityName ?? query.QueryName ?? "Query";
                AddHeading(queryTitle, "Heading3");

                Table table = CreateTable();
                table.Append(CreateHeaderRow(new Text("Property"), new Text("Value")));
                if (!string.IsNullOrEmpty(query.QueryName))
                    table.Append(CreateRow(new Text("Query Name"), new Text(query.QueryName)));
                if (!string.IsNullOrEmpty(query.QueryId))
                    table.Append(CreateRow(new Text("Query ID"), new Text(query.QueryId)));
                if (!string.IsNullOrEmpty(query.EntityName))
                {
                    string entityDisplay = query.EntityName;
                    if (content.context != null)
                    {
                        string resolved = content.context.GetTableDisplayName(query.EntityName);
                        if (resolved != null && resolved != query.EntityName)
                            entityDisplay = $"{resolved} ({query.EntityName})";
                    }
                    table.Append(CreateRow(new Text("Target Entity"), new Text(entityDisplay)));
                }
                table.Append(CreateRow(new Text("Load Enabled"), new Text(query.LoadEnabled ? "Yes" : "No")));
                table.Append(CreateRow(new Text("Delete Existing Data"), new Text(query.DeleteExistingDataOnLoad ? "Yes" : "No")));
                if (query.IsCalculatedEntity)
                    table.Append(CreateRow(new Text("Calculated Entity"), new Text("Yes")));
                if (query.IsLinkedEntity)
                    table.Append(CreateRow(new Text("Linked Entity"), new Text("Yes")));
                if (!string.IsNullOrEmpty(query.ResultTypeName))
                    table.Append(CreateRow(new Text("Result Type"), new Text(query.ResultTypeName)));
                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));

                // Field Mappings
                if (query.FieldMappings.Count > 0)
                {
                    AddHeading("Field Mappings", "Heading4");
                    Table fieldTable = CreateTable();
                    fieldTable.Append(CreateHeaderRow(new Text("Destination Field"), new Text("Source Column"), new Text("Destination Type")));
                    foreach (var field in query.FieldMappings.OrderBy(f => f.DestinationFieldName))
                    {
                        fieldTable.Append(CreateRow(
                            new Text(field.DestinationFieldName ?? ""),
                            new Text(field.SourceColumnName ?? ""),
                            new Text(field.DestinationFieldType ?? "")
                        ));
                    }
                    body.Append(fieldTable);
                    body.AppendChild(new Paragraph(new Run(new Break())));
                }
            }
        }

        private void addMCode()
        {
            AddHeading(content.headerMCode, "Heading2");
            // Render M code as monospace paragraph
            RunProperties runProps = new RunProperties();
            runProps.Append(new RunFonts() { Ascii = "Courier New", HighAnsi = "Courier New" });
            runProps.Append(new FontSize() { Val = "16" });
            Run run = new Run(runProps, new Text(content.dataflow.MashupDocument) { Space = SpaceProcessingModeValues.Preserve });
            body.AppendChild(new Paragraph(run));
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addConnectionOverrides()
        {
            AddHeading(content.headerConnectionOverrides, "Heading2");

            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Kind"), new Text("Path"), new Text("Provider"), new Text("Environment")));
            foreach (var co in content.dataflow.ConnectionOverrides)
            {
                table.Append(CreateRow(
                    new Text(co.Kind ?? ""),
                    new Text(co.Path ?? ""),
                    new Text(co.Provider ?? ""),
                    new Text(co.EnvironmentName ?? "")
                ));
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addRefreshSettings()
        {
            var rs = content.dataflow.RefreshSettings;
            AddHeading(content.headerRefreshSettings, "Heading2");

            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Property"), new Text("Value")));
            if (!string.IsNullOrEmpty(rs.ScheduleRefreshType))
                table.Append(CreateRow(new Text("Schedule Type"), new Text(rs.ScheduleRefreshType)));
            if (!string.IsNullOrEmpty(rs.RefreshPeriod))
                table.Append(CreateRow(new Text("Refresh Period"), new Text(rs.RefreshPeriod)));
            if (!string.IsNullOrEmpty(rs.TimeBasedRefreshPeriod))
                table.Append(CreateRow(new Text("Time-Based Period"), new Text(rs.TimeBasedRefreshPeriod)));
            if (!string.IsNullOrEmpty(rs.TimeZoneId))
                table.Append(CreateRow(new Text("Time Zone"), new Text(rs.TimeZoneId)));
            if (!string.IsNullOrEmpty(rs.StartDateTime) && rs.StartDateTime != "0001-01-01T00:00:00+00:00")
                table.Append(CreateRow(new Text("Start Date/Time"), new Text(rs.StartDateTime)));
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addSettings()
        {
            AddHeading(content.headerSettings, "Heading2");

            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Setting"), new Text("Value")));
            table.Append(CreateRow(new Text("Fast Combine"), new Text(content.dataflow.FastCombine ? "Enabled" : "Disabled")));
            table.Append(CreateRow(new Text("Allow Native Queries"), new Text(content.dataflow.AllowNativeQueries ? "Yes" : "No")));
            if (!string.IsNullOrEmpty(content.dataflow.OutputFileFormat))
                table.Append(CreateRow(new Text("Output File Format"), new Text(content.dataflow.OutputFileFormat)));
            table.Append(CreateRow(new Text("Skip Auto Type Detection"), new Text(content.dataflow.SkipAutomaticTypeAndHeaderDetection ? "Yes" : "No")));
            table.Append(CreateRow(new Text("Disable Auto Anonymous Connection"), new Text(content.dataflow.DisableAutoAnonymousConnectionUpsert ? "Yes" : "No")));
            body.Append(table);
        }
    }
}
