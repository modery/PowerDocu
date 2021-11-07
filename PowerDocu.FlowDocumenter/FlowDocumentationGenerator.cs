using System;
using System.IO;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{
    public class FlowDocumentationGenerator
    {
        public static string GenerateWordDocumentation(string filePath, string wordTemplate = null)
        {
            if (File.Exists(filePath))
            {
                string path = Path.GetDirectoryName(filePath);
                DateTime startDocGeneration = DateTime.Now;
                FlowParser flowParserFromZip = new FlowParser(filePath);
                if (flowParserFromZip.packageType == FlowParser.PackageType.SolutionPackage)
                {
                    path += @"\Solution " + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath));
                }
                foreach (FlowEntity flow in flowParserFromZip.getFlows())
                {
                    GraphBuilder gbzip = new GraphBuilder(flow, path);
                    gbzip.buildTopLevelGraph();
                    gbzip.buildDetailedGraph();
                    if (String.IsNullOrEmpty(wordTemplate) || !File.Exists(wordTemplate))
                    {
                        WordDocBuilder wordzip = new WordDocBuilder(flow, path, null);
                    }
                    else
                    {
                        WordDocBuilder wordzip = new WordDocBuilder(flow, path, wordTemplate);
                    }
                }
                DateTime endDocGeneration = DateTime.Now;
                return "Created Word documentation for " + filePath + ". A total of " + flowParserFromZip.getFlows().Count + " files were processed in " + (endDocGeneration - startDocGeneration).TotalSeconds + " seconds.";
            }
            return "File not found: " + filePath;
        }
    }
}