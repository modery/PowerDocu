using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PowerDocu.Common;

namespace PowerDocu.DataflowDocumenter
{
    class DataflowHtmlBuilder : HtmlBuilder
    {
        private readonly DataflowDocumentationContent content;
        private readonly string mainFileName;

        public DataflowHtmlBuilder(DataflowDocumentationContent contentDocumentation)
        {
            content = contentDocumentation;
            Directory.CreateDirectory(content.folderPath);
            WriteDefaultStylesheet(content.folderPath);
            mainFileName = ("dataflow-" + content.filename + ".html").Replace(" ", "-");

            addOverviewPage();
            NotificationHelper.SendNotification("Created HTML documentation for Dataflow: " + content.dataflow.GetDisplayName());
        }

        private string getNavigationHtml()
        {
            var navItemsList = new List<(string label, string href)>();
            if (content.context?.Solution != null)
            {
                if (content.context?.Config?.documentSolution == true)
                    navItemsList.Add(("Solution", "../" + CrossDocLinkHelper.GetSolutionDocHtmlPath(content.context.Solution.UniqueName)));
                else
                    navItemsList.Add((content.context.Solution.UniqueName, ""));
            }
            navItemsList.Add(("Overview", "#overview"));
            navItemsList.Add(("Metadata", "#metadata"));
            if (content.dataflow.Queries.Count > 0)
            {
                navItemsList.Add(("Queries", "#queries"));
                foreach (var query in content.dataflow.Queries)
                    navItemsList.Add(("  " + (query.QueryName ?? query.EntityName ?? "Query"), "#query-" + (query.QueryId ?? "").ToLowerInvariant()));
            }
            if (!string.IsNullOrEmpty(content.dataflow.MashupDocument))
                navItemsList.Add(("M Code", "#mcode"));
            if (content.dataflow.ConnectionOverrides.Count > 0)
                navItemsList.Add(("Connections", "#connections"));
            if (content.dataflow.RefreshSettings != null)
                navItemsList.Add(("Refresh Settings", "#refresh-settings"));
            navItemsList.Add(("Settings", "#settings"));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<div class=\"nav-title\">{Encode(content.dataflow.GetDisplayName())}</div>");
            sb.Append(NavigationList(navItemsList));
            return sb.ToString();
        }

        private void addOverviewPage()
        {
            StringBuilder body = new StringBuilder();

            // Overview
            body.AppendLine(HeadingWithId(1, content.dataflow.GetDisplayName(), "overview"));

            body.Append(TableStart("Property", "Value"));
            body.Append(TableRow("Name", content.dataflow.GetDisplayName()));
            body.Append(TableRow("Dataflow ID", content.dataflow.DataflowId ?? ""));
            body.Append(TableRow("Original Dataflow ID", content.dataflow.OriginalDataflowId ?? ""));
            body.Append(TableRow("State", content.dataflow.GetStateLabel()));
            body.Append(TableRow("Status", content.dataflow.GetStatusLabel()));
            body.Append(TableRow("Is Customizable", content.dataflow.IsCustomizable ? "Yes" : "No"));
            body.Append(TableRow("Queries", content.dataflow.Queries.Count.ToString()));
            body.Append(TableRow(content.headerDocumentationGenerated, PowerDocuReleaseHelper.GetTimestampWithVersion()));
            body.AppendLine(TableEnd());

            // Metadata
            addMetadata(body);

            // Queries
            if (content.dataflow.Queries.Count > 0)
                addQueries(body);

            // M Code
            if (!string.IsNullOrEmpty(content.dataflow.MashupDocument))
                addMCode(body);

            // Connection Overrides
            if (content.dataflow.ConnectionOverrides.Count > 0)
                addConnectionOverrides(body);

            // Refresh Settings
            if (content.dataflow.RefreshSettings != null)
                addRefreshSettings(body);

            // Settings
            addSettings(body);

            SaveHtmlFile(Path.Combine(content.folderPath, mainFileName),
                WrapInHtmlPage($"Dataflow - {content.dataflow.GetDisplayName()}", body.ToString(), getNavigationHtml()));
        }

        private void addMetadata(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, content.headerMetadata, "metadata"));
            body.Append(TableStart("Property", "Value"));
            if (!string.IsNullOrEmpty(content.dataflow.OwnerName))
                body.Append(TableRow("Owner", content.dataflow.OwnerName));
            if (!string.IsNullOrEmpty(content.dataflow.DataflowType))
                body.Append(TableRow("Dataflow Type", content.dataflow.DataflowType));
            if (!string.IsNullOrEmpty(content.dataflow.CreatedTime))
                body.Append(TableRow("Created", content.dataflow.CreatedTime));
            if (!string.IsNullOrEmpty(content.dataflow.LastUpdateTime))
                body.Append(TableRow("Last Updated", content.dataflow.LastUpdateTime));
            if (!string.IsNullOrEmpty(content.dataflow.PublishStatus))
                body.Append(TableRow("Publish Status", content.dataflow.PublishStatus));
            if (!string.IsNullOrEmpty(content.dataflow.HostContextType))
                body.Append(TableRow("Host Type", content.dataflow.HostContextType));
            if (!string.IsNullOrEmpty(content.dataflow.HostEnvironmentId))
                body.Append(TableRow("Environment ID", content.dataflow.HostEnvironmentId));
            if (!string.IsNullOrEmpty(content.dataflow.DocumentLocale))
                body.Append(TableRow("Locale", content.dataflow.DocumentLocale));
            if (!string.IsNullOrEmpty(content.dataflow.InternalVersion))
                body.Append(TableRow("Internal Version", content.dataflow.InternalVersion));
            body.AppendLine(TableEnd());
        }

        private void addQueries(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, content.headerQueries, "queries"));
            body.AppendLine($"<p>This Dataflow has {content.dataflow.Queries.Count} query/queries.</p>");

            foreach (var query in content.dataflow.Queries)
            {
                string queryTitle = query.EntityName ?? query.QueryName ?? "Query";
                string resolvedTableName = null;
                if (!string.IsNullOrEmpty(query.EntityName) && content.context != null)
                    resolvedTableName = content.context.GetTableDisplayName(query.EntityName);

                body.AppendLine(HeadingWithId(3, queryTitle, "query-" + (query.QueryId ?? "").ToLowerInvariant()));

                body.Append(TableStart("Property", "Value"));
                if (!string.IsNullOrEmpty(query.QueryName))
                    body.Append(TableRow("Query Name", query.QueryName));
                if (!string.IsNullOrEmpty(query.QueryId))
                    body.Append(TableRow("Query ID", query.QueryId));
                if (!string.IsNullOrEmpty(query.EntityName))
                {
                    string entityDisplay = query.EntityName;
                    if (resolvedTableName != null && resolvedTableName != query.EntityName)
                        entityDisplay = $"{resolvedTableName} ({query.EntityName})";
                    body.Append(TableRow("Target Entity", entityDisplay));
                }
                body.Append(TableRow("Load Enabled", query.LoadEnabled ? "Yes" : "No"));
                body.Append(TableRow("Delete Existing Data", query.DeleteExistingDataOnLoad ? "Yes" : "No"));
                if (query.IsCalculatedEntity)
                    body.Append(TableRow("Calculated Entity", "Yes"));
                if (query.IsLinkedEntity)
                    body.Append(TableRow("Linked Entity", "Yes"));
                if (!string.IsNullOrEmpty(query.ResultTypeName))
                    body.Append(TableRow("Result Type", query.ResultTypeName));
                body.AppendLine(TableEnd());

                // Field Mappings
                if (query.FieldMappings.Count > 0)
                {
                    body.AppendLine(Heading(4, "Field Mappings"));
                    body.Append(TableStart("Destination Field", "Source Column", "Destination Type"));
                    foreach (var field in query.FieldMappings.OrderBy(f => f.DestinationFieldName))
                    {
                        body.Append(TableRow(
                            field.DestinationFieldName ?? "",
                            field.SourceColumnName ?? "",
                            field.DestinationFieldType ?? ""
                        ));
                    }
                    body.AppendLine(TableEnd());
                }
            }
        }

        private void addMCode(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, content.headerMCode, "mcode"));
            body.AppendLine($"<pre><code>{Encode(content.dataflow.MashupDocument)}</code></pre>");
        }

        private void addConnectionOverrides(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, content.headerConnectionOverrides, "connections"));
            body.Append(TableStart("Kind", "Path", "Provider", "Environment"));
            foreach (var co in content.dataflow.ConnectionOverrides)
            {
                body.Append(TableRow(
                    co.Kind ?? "",
                    co.Path ?? "",
                    co.Provider ?? "",
                    co.EnvironmentName ?? ""
                ));
            }
            body.AppendLine(TableEnd());
        }

        private void addRefreshSettings(StringBuilder body)
        {
            var rs = content.dataflow.RefreshSettings;
            body.AppendLine(HeadingWithId(2, content.headerRefreshSettings, "refresh-settings"));
            body.Append(TableStart("Property", "Value"));
            if (!string.IsNullOrEmpty(rs.ScheduleRefreshType))
                body.Append(TableRow("Schedule Type", rs.ScheduleRefreshType));
            if (!string.IsNullOrEmpty(rs.RefreshPeriod))
                body.Append(TableRow("Refresh Period", rs.RefreshPeriod));
            if (!string.IsNullOrEmpty(rs.TimeBasedRefreshPeriod))
                body.Append(TableRow("Time-Based Period", rs.TimeBasedRefreshPeriod));
            if (!string.IsNullOrEmpty(rs.TimeZoneId))
                body.Append(TableRow("Time Zone", rs.TimeZoneId));
            if (!string.IsNullOrEmpty(rs.StartDateTime) && rs.StartDateTime != "0001-01-01T00:00:00+00:00")
                body.Append(TableRow("Start Date/Time", rs.StartDateTime));
            body.AppendLine(TableEnd());
        }

        private void addSettings(StringBuilder body)
        {
            body.AppendLine(HeadingWithId(2, content.headerSettings, "settings"));
            body.Append(TableStart("Setting", "Value"));
            body.Append(TableRow("Fast Combine", content.dataflow.FastCombine ? "Enabled" : "Disabled"));
            body.Append(TableRow("Allow Native Queries", content.dataflow.AllowNativeQueries ? "Yes" : "No"));
            if (!string.IsNullOrEmpty(content.dataflow.OutputFileFormat))
                body.Append(TableRow("Output File Format", content.dataflow.OutputFileFormat));
            body.Append(TableRow("Skip Auto Type Detection", content.dataflow.SkipAutomaticTypeAndHeaderDetection ? "Yes" : "No"));
            body.Append(TableRow("Disable Auto Anonymous Connection", content.dataflow.DisableAutoAnonymousConnectionUpsert ? "Yes" : "No"));
            body.AppendLine(TableEnd());
        }
    }
}
