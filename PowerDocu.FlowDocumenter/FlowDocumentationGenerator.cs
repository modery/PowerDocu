using System;
using System.IO;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{
    public class FlowDocumentationGenerator
    {
        public static string GenerateWordDocumentation(string filePath)
        {
            if (File.Exists(filePath))
            {
                string path = Path.GetDirectoryName(filePath);
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
                    WordDocBuilder wordzip = new WordDocBuilder(flow, path);
                }
                return "Created Word documentation for " + filePath + ". A total of " + flowParserFromZip.getFlows().Count + " files were processed";
            }
            return "File not found: " + filePath;
        }
    }
}