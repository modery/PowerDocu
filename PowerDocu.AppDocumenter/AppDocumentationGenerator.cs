using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Rubjerg.Graphviz;
using Svg;

namespace PowerDocu.AppDocumenter
{
    public static class AppDocumentationGenerator
    {
        public static void GenerateDocumentation(string filePath, string fileFormat, string wordTemplate = null)
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
                    string folderPath = path + CharsetHelper.GetSafeName(@"\AppDoc - " + app.Name + @"\");
                    Directory.CreateDirectory(folderPath);
                    //build the graph showing the navigations between the different screens
                    RootGraph rootGraph = RootGraph.CreateNew(CharsetHelper.GetSafeName(app.Name), GraphType.Directed);
                    Graph.IntroduceAttribute(rootGraph, "compound", "true");
                    Node.IntroduceAttribute(rootGraph, "shape", "rectangle");
                    Node.IntroduceAttribute(rootGraph, "color", "");
                    Node.IntroduceAttribute(rootGraph, "style", "");
                    Node.IntroduceAttribute(rootGraph, "fillcolor", "");
                    Node.IntroduceAttribute(rootGraph, "label", "");
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
                    rootGraph.ComputeLayout();
                    rootGraph.ToSvgFile(folderPath + "ScreenNavigation.svg");
                    var svgDocument = SvgDocument.Open(folderPath + "ScreenNavigation.svg");
                    //generating the PNG from the SVG
                    using (var bitmap = svgDocument.Draw())
                    {
                        bitmap?.Save(folderPath + "ScreenNavigation.png");
                    }
                    AppDocumentationContent content = new AppDocumentationContent(app, path);
                    if (fileFormat.Equals(OutputFormatHelper.Word) || fileFormat.Equals(OutputFormatHelper.All))
                    {
                        //create the Word document
                        NotificationHelper.SendNotification("Creating Word documentation");
                        if (String.IsNullOrEmpty(wordTemplate) || !File.Exists(wordTemplate))
                        {
                            AppWordDocBuilder wordzip = new AppWordDocBuilder(content, null);
                        }
                        else
                        {
                            AppWordDocBuilder wordzip = new AppWordDocBuilder(content, wordTemplate);
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
            }
            else
            {
                NotificationHelper.SendNotification("File not found: " + filePath);
            }
        }
    }
}