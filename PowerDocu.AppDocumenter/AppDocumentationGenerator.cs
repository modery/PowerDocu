using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Rubjerg.Graphviz;

namespace PowerDocu.AppDocumenter
{
    public static class AppDocumentationGenerator
    {
        public static List<AppEntity> GenerateDocumentation(string filePath, string fileFormat, bool documentDefaultChangesOnly, bool documentDefaults, bool documentSampleData = false, string wordTemplate = null, string outputPath = null)
        {
            if (File.Exists(filePath))
            {
                string path = outputPath == null ? Path.GetDirectoryName(filePath) : $"{outputPath}/{Path.GetFileNameWithoutExtension(filePath)}";
                DateTime startDocGeneration = DateTime.Now;
                AppParser appParserFromZip = new AppParser(filePath);

                if (outputPath == null && appParserFromZip.packageType == AppParser.PackageType.SolutionPackage)
                {
                    path += @"\Solution " + CharsetHelper.GetSafeName(Path.GetFileNameWithoutExtension(filePath));
                }

                List<AppEntity> apps = appParserFromZip.getApps();
                foreach (AppEntity app in apps)
                {
                    string folderPath = path + CharsetHelper.GetSafeName(@"\AppDoc " + app.Name + @"\");
                    Directory.CreateDirectory(folderPath);
                    //build the graph showing the navigations between the different screens
                    RootGraph rootGraph = RootGraph.CreateNew(GraphType.Directed, CharsetHelper.GetSafeName(app.Name));
                    Graph.IntroduceAttribute(rootGraph, "compound", "true");
                    Graph.IntroduceAttribute(rootGraph, "fontname", "helvetica");
                    Node.IntroduceAttribute(rootGraph, "shape", "rectangle");
                    Node.IntroduceAttribute(rootGraph, "color", "");
                    Node.IntroduceAttribute(rootGraph, "style", "");
                    Node.IntroduceAttribute(rootGraph, "fillcolor", "");
                    Node.IntroduceAttribute(rootGraph, "label", "");
                    Node.IntroduceAttribute(rootGraph, "fontname", "helvetica");
                    //add all the screens
                    foreach (ControlEntity ce in app.ScreenNavigations.Keys)
                    {
                        List<string> destinations = app.ScreenNavigations[ce];
                        if (destinations != null)
                        {
                            foreach (string destination in destinations)
                            {
                                if (!destination.Contains("(") && !destination.Contains(","))
                                {
                                    ControlEntity screen = ce.Screen();
                                    if (screen != null)
                                    {
                                        Node source = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(screen.Name));
                                        source.SetAttributeHtml("label", "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(ce.Screen().Name) + "</td></tr></table>");
                                        Node dest = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(destination));
                                        dest.SetAttributeHtml("label", "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(destination) + "</td></tr></table>");
                                        rootGraph.GetOrAddEdge(source, dest, ce.Screen().Name + "-" + destination);
                                    }
                                    else
                                    {
                                        if (ce.Type == "appinfo")
                                        {
                                            Node source = rootGraph.GetOrAddNode("App");
                                            source.SetAttributeHtml("label", "<table border=\"0\"><tr><td>App</td></tr></table>");
                                            source.SetAttribute("shape", "oval");
                                            Node dest = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(destination));
                                            dest.SetAttributeHtml("label", "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(destination) + "</td></tr></table>");
                                            rootGraph.GetOrAddEdge(source, dest, "App -" + destination);
                                        }
                                        else
                                        {
                                        }
                                    }
                                }
                                else
                                {
                                    //doing a "dumb approach" and simply checking if screens are mentioned in the destination. If so, we add them
                                    foreach (ControlEntity screen in app.Controls.Where(o => o.Type == "screen").ToList())
                                    {
                                        if (destination.Contains(screen.Name))
                                        {
                                            Node source = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(ce.Screen().Name));
                                            source.SetAttributeHtml("label", "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(ce.Screen().Name) + "</td></tr></table>");
                                            Node dest = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(screen.Name));
                                            dest.SetAttributeHtml("label", "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(screen.Name) + "</td></tr></table>");
                                            rootGraph.GetOrAddEdge(source, dest, ce.Screen().Name + "-" + screen.Name);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    rootGraph.CreateLayout();
                    rootGraph.ToPngFile(folderPath + "ScreenNavigation.png");
                    rootGraph.ToSvgFile(folderPath + "ScreenNavigation.svg");
                     //the following code is no longer required, as saving directly to PNG is now possible through GraphViz. Keeping it in case it is required in the future
                    /*
                    var svgDocument = SvgDocument.Open(folderPath + "ScreenNavigation.svg");
                    //generating the PNG from the SVG
                    using (var bitmap = svgDocument.Draw())
                    {
                        bitmap?.Save(folderPath + "ScreenNavigation.png");
                    }*/
                    AppDocumentationContent content = new AppDocumentationContent(app, path);
                    if (fileFormat.Equals(OutputFormatHelper.Word) || fileFormat.Equals(OutputFormatHelper.All))
                    {
                        //create the Word document
                        NotificationHelper.SendNotification("Creating Word documentation");
                        if (String.IsNullOrEmpty(wordTemplate) || !File.Exists(wordTemplate))
                        {
                            AppWordDocBuilder wordzip = new AppWordDocBuilder(content, null, documentDefaultChangesOnly, documentDefaults, documentSampleData);
                        }
                        else
                        {
                            AppWordDocBuilder wordzip = new AppWordDocBuilder(content, wordTemplate, documentDefaultChangesOnly, documentDefaults, documentSampleData);
                        }
                    }
                    if (fileFormat.Equals(OutputFormatHelper.Markdown) || fileFormat.Equals(OutputFormatHelper.All))
                    {
                        NotificationHelper.SendNotification("Creating Markdown documentation");
                        AppMarkdownBuilder markdownFile = new AppMarkdownBuilder(content);
                    }
                }
                DateTime endDocGeneration = DateTime.Now;
                NotificationHelper.SendNotification("AppDocumenter: Created Word documentation for " + filePath + ". A total of " + appParserFromZip.getApps().Count + " files were processed in " + (endDocGeneration - startDocGeneration).TotalSeconds + " seconds.");
                return apps;
            }
            else
            {
                NotificationHelper.SendNotification("File not found: " + filePath);
            }
            return null;
        }
    }
}