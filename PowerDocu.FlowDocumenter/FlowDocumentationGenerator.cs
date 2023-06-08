using System;
using System.Collections.Generic;
using System.IO;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{
    public static class FlowDocumentationGenerator
    {
        public static List<FlowEntity> GenerateDocumentation(string filePath, string fileFormat, string flowActionSortOrder, string wordTemplate = null, string outputPath = null)
        {
            if (File.Exists(filePath))
            {
                string path = outputPath == null ? Path.GetDirectoryName(filePath) : $"{outputPath}/{Path.GetFileNameWithoutExtension(filePath)}";
                DateTime startDocGeneration = DateTime.Now;
                FlowParser flowParserFromZip = new FlowParser(filePath);
                filePath = Path.GetDirectoryName(filePath);
                if (flowParserFromZip.packageType == FlowParser.PackageType.SolutionPackage)
                {
                    filePath += @"\Solution " + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath));
                }
                List<FlowEntity> flows = flowParserFromZip.getFlows();
                foreach (FlowEntity flow in flows)
                {
                    GraphBuilder gbzip = new GraphBuilder(flow, filePath);
                    gbzip.buildTopLevelGraph();
                    gbzip.buildDetailedGraph();
                    FlowActionSortOrder sortOrder = flowActionSortOrder switch {
                        "By order of appearance" => FlowActionSortOrder.SortByOrder,
                        "By name" => FlowActionSortOrder.SortByName,
                        _ => FlowActionSortOrder.SortByName
                    };
                    FlowDocumentationContent content = new FlowDocumentationContent(flow, path, sortOrder);
                    if (fileFormat.Equals(OutputFormatHelper.Word) || fileFormat.Equals(OutputFormatHelper.All))
                    {
                        NotificationHelper.SendNotification("Creating Word documentation");
                        if (String.IsNullOrEmpty(wordTemplate) || !File.Exists(wordTemplate))
                        {
                            FlowWordDocBuilder wordzip = new FlowWordDocBuilder(content, null);
                        }
                        else
                        {
                            FlowWordDocBuilder wordzip = new FlowWordDocBuilder(content, wordTemplate);
                        }
                    }
                    if (fileFormat.Equals(OutputFormatHelper.Markdown) || fileFormat.Equals(OutputFormatHelper.All))
                    {
                        NotificationHelper.SendNotification("Creating Markdown documentation");
                        FlowMarkdownBuilder markdownFile = new FlowMarkdownBuilder(content);
                    }
                }
                DateTime endDocGeneration = DateTime.Now;
                NotificationHelper.SendNotification("FlowDocumenter: Created documentation for " + filePath + ". A total of " + flowParserFromZip.getFlows().Count + " files were processed in " + (endDocGeneration - startDocGeneration).TotalSeconds + " seconds.");
                return flows;
            }
            else
            {
                NotificationHelper.SendNotification("File not found: " + filePath);
            }
            return null;
        }
    }
}