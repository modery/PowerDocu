using System;
using System.IO;
using PowerDocu.Common;

namespace PowerDocu.DataflowDocumenter
{
    public static class DataflowDocumentationGenerator
    {
        public static void GenerateOutput(DocumentationContext context, string path)
        {
            if (context.Dataflows == null || context.Dataflows.Count == 0 || !context.Config.documentDataflows) return;

            DateTime startDocGeneration = DateTime.Now;
            NotificationHelper.SendNotification($"Found {context.Dataflows.Count} Dataflow(s) in the solution.");

            if (context.FullDocumentation)
            {
                foreach (DataflowEntity dataflow in context.Dataflows)
                {
                    DataflowDocumentationContent content = new DataflowDocumentationContent(dataflow, path, context);

                    string wordTemplate = (!String.IsNullOrEmpty(context.Config.wordTemplate) && File.Exists(context.Config.wordTemplate))
                        ? context.Config.wordTemplate : null;
                    if (context.Config.outputFormat.Equals(OutputFormatHelper.Word) || context.Config.outputFormat.Equals(OutputFormatHelper.All))
                    {
                        NotificationHelper.SendNotification("Creating Word documentation for Dataflow: " + dataflow.GetDisplayName());
                        DataflowWordDocBuilder wordDoc = new DataflowWordDocBuilder(content, wordTemplate);
                    }
                    if (context.Config.outputFormat.Equals(OutputFormatHelper.Markdown) || context.Config.outputFormat.Equals(OutputFormatHelper.All))
                    {
                        NotificationHelper.SendNotification("Creating Markdown documentation for Dataflow: " + dataflow.GetDisplayName());
                        DataflowMarkdownBuilder markdownDoc = new DataflowMarkdownBuilder(content);
                    }
                    if (context.Config.outputFormat.Equals(OutputFormatHelper.Html) || context.Config.outputFormat.Equals(OutputFormatHelper.All))
                    {
                        NotificationHelper.SendNotification("Creating HTML documentation for Dataflow: " + dataflow.GetDisplayName());
                        DataflowHtmlBuilder htmlDoc = new DataflowHtmlBuilder(content);
                    }
                    context.Progress?.Increment("Dataflows");
                }
            }
            else
            {
                context.Progress?.Complete("Dataflows");
            }

            DateTime endDocGeneration = DateTime.Now;
            NotificationHelper.SendNotification(
                $"DataflowDocumenter: Processed {context.Dataflows.Count} Dataflow(s) in {(endDocGeneration - startDocGeneration).TotalSeconds} seconds."
            );
        }
    }
}
