using System;
using System.Collections.Generic;
using System.IO;
using PowerDocu.Common;
using PowerDocu.AppDocumenter;
using PowerDocu.FlowDocumenter;

namespace PowerDocu.SolutionDocumenter
{
    public static class SolutionDocumentationGenerator
    {

        private static List<FlowEntity> flows;
        private static List<AppEntity> apps;
        public static void GenerateDocumentation(string filePath, string fileFormat, bool fullDocumentation, bool documentDefaultChangesOnly, bool documentDefaults, bool documentSampleData, string flowActionSortOrder, string wordTemplate = null, string outputPath = null)
        {
            if (File.Exists(filePath))
            {
                DateTime startDocGeneration = DateTime.Now;
                flows = FlowDocumentationGenerator.GenerateDocumentation(
                    filePath,
                    fileFormat,
                    fullDocumentation,
                    flowActionSortOrder,
                    wordTemplate,
                    outputPath
                );
                apps = AppDocumentationGenerator.GenerateDocumentation(
                    filePath,
                    fileFormat,
                    fullDocumentation,
                    documentDefaultChangesOnly,
                    documentDefaults,
                    documentSampleData,
                    wordTemplate,
                    outputPath
                );
                SolutionParser solutionParser = new SolutionParser(filePath);
                if (solutionParser.solution != null)
                {
                    string path = outputPath == null ?
                        Path.GetDirectoryName(filePath) + @"\Solution " + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath) + @"\") :
                        outputPath + @"\" + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath) + @"\");

                    SolutionDocumentationContent solutionContent = new SolutionDocumentationContent(solutionParser.solution, apps, flows, path);
                    DataverseGraphBuilder dataverseGraphBuilder = new DataverseGraphBuilder(solutionContent);
                    if (fullDocumentation)
                    {
                        if (fileFormat.Equals(OutputFormatHelper.Word) || fileFormat.Equals(OutputFormatHelper.All))
                        {
                            //create the Word document
                            NotificationHelper.SendNotification("Creating Solution documentation");
                            if (String.IsNullOrEmpty(wordTemplate) || !File.Exists(wordTemplate))
                            {
                                SolutionWordDocBuilder wordzip = new SolutionWordDocBuilder(solutionContent, null);
                            }
                            else
                            {
                                SolutionWordDocBuilder wordzip = new SolutionWordDocBuilder(solutionContent, wordTemplate);
                            }
                        }
                        if (fileFormat.Equals(OutputFormatHelper.Markdown) || fileFormat.Equals(OutputFormatHelper.All))
                        {
                            SolutionMarkdownBuilder mdDoc = new SolutionMarkdownBuilder(solutionContent);
                        }
                    }
                    DateTime endDocGeneration = DateTime.Now;
                    NotificationHelper.SendNotification("SolutionDocumenter: Created documentation for " + filePath + ". Total solution documentation completed in " + (endDocGeneration - startDocGeneration).TotalSeconds + " seconds.");
                }
            }
            else
            {
                NotificationHelper.SendNotification("File not found: " + filePath);
            }
        }
    }
}