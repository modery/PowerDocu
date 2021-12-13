using System;
using System.IO;
using PowerDocu.Common;

namespace PowerDocu.AppDocumenter
{
    public static class AppDocumentationGenerator
    {
        public static void GenerateWordDocumentation(string filePath, string wordTemplate = null)
        {
            if (File.Exists(filePath))
            {
                string path = Path.GetDirectoryName(filePath);
                DateTime startDocGeneration = DateTime.Now;
                AppParser appParserFromZip = new AppParser(filePath);
                if (appParserFromZip.packageType == AppParser.PackageType.SolutionPackage)
                {
                    path += @"\Solution " + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath));
                }
                foreach (AppEntity app in appParserFromZip.getApps())
                {
                    /*GraphBuilder gbzip = new GraphBuilder(flow, path);
                    gbzip.buildTopLevelGraph();
                    gbzip.buildDetailedGraph();*/
                    if (String.IsNullOrEmpty(wordTemplate) || !File.Exists(wordTemplate))
                    {
                        AppWordDocBuilder wordzip = new AppWordDocBuilder(app, path, null);
                    }
                    else
                    {
                        AppWordDocBuilder wordzip = new AppWordDocBuilder(app, path, wordTemplate);
                    }
                }
                DateTime endDocGeneration = DateTime.Now;
                NotificationHelper.SendNotification("Created Word documentation for " + filePath + ". A total of " + appParserFromZip.getApps().Count + " files were processed in " + (endDocGeneration - startDocGeneration).TotalSeconds + " seconds.");
            }
            else
            {
                NotificationHelper.SendNotification("File not found: " + filePath);
            }
        }
    }
}