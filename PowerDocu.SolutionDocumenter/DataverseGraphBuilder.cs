using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Rubjerg.Graphviz;
using Svg;

namespace PowerDocu.SolutionDocumenter
{
    public class DataverseGraphBuilder
    {
        private readonly SolutionDocumentationContent content;
        private readonly Dictionary<string, string> entityColors = new Dictionary<string, string>();
        public DataverseGraphBuilder(SolutionDocumentationContent contentToUse)
        {
            content = contentToUse;
            Directory.CreateDirectory(content.folderPath);
            buildGraph();
        }

        private void buildGraph()
        {
            RootGraph rootGraph = RootGraph.CreateNew(CharsetHelper.GetSafeName(content.solution.UniqueName), GraphType.Undirected);
            Graph.IntroduceAttribute(rootGraph, "compound", "true");
            Graph.IntroduceAttribute(rootGraph, "color", "#000090");
            Graph.IntroduceAttribute(rootGraph, "style", "filled");
            Graph.IntroduceAttribute(rootGraph, "fillcolor", "white");
            Graph.IntroduceAttribute(rootGraph, "label", " ");
            Graph.IntroduceAttribute(rootGraph, "rankdir", "LR");
            Graph.IntroduceAttribute(rootGraph, "fontname", "helvetica");
            Graph.IntroduceAttribute(rootGraph, "penwidth", "1");
            Node.IntroduceAttribute(rootGraph, "shape", "rectangle");
            Node.IntroduceAttribute(rootGraph, "color", "#000090");
            Node.IntroduceAttribute(rootGraph, "style", "filled");
            Node.IntroduceAttribute(rootGraph, "fillcolor", "white");
            Node.IntroduceAttribute(rootGraph, "label", "");
            Node.IntroduceAttribute(rootGraph, "fontname", "helvetica");
            Node.IntroduceAttribute(rootGraph, "fontcolor", "#ffffff");
            Node.IntroduceAttribute(rootGraph, "penwidth", "1");
            Edge.IntroduceAttribute(rootGraph, "color", "#000090");
            Edge.IntroduceAttribute(rootGraph, "penwidth", "1");

            List<TableEntity> tableEntities = content.solution.Customizations.getEntities();
            //create containers for all tables that are being looked up
            //

            foreach (TableEntity tableEntity in tableEntities.Where(o => o.containsNonDefaultLookupColumns()))
            {
                SubGraph currentTableGraph = rootGraph.GetOrAddSubgraph(CharsetHelper.GetSafeName("cluster_" + tableEntity.getName()));
                currentTableGraph.SetAttribute("label", tableEntity.getLocalizedName() + " (" + tableEntity.getName() + ")");
                currentTableGraph.SetAttribute("color", "#7070E0");
                foreach (ColumnEntity lookupColumn in tableEntity.GetColumns().Where(o => o.getDataType().Equals("Lookup")).ToList())
                {
                    TableEntity lookupTableEntity = tableEntities.Find(o => o.getName().ToLower().Equals(lookupColumn.getLogicalName()));
                    if (lookupTableEntity != null)
                    {
                        string nodeEdgeColor = getHexColor(lookupColumn.getLogicalName());
                        SubGraph lookupTableGraph = rootGraph.GetOrAddSubgraph(CharsetHelper.GetSafeName("cluster_" + lookupColumn.getLogicalName()));
                        lookupTableGraph.SetAttribute("label", lookupTableEntity.getLocalizedName() + " (" + lookupTableEntity.getName() + ")");
                        Node primaryColumnNode = lookupTableGraph.GetOrAddNode(CharsetHelper.GetSafeName(lookupTableEntity.getName() + "-" + lookupTableEntity.getPrimaryColumn()));
                        primaryColumnNode.SetAttribute("label", CharsetHelper.GetSafeName(lookupTableEntity.getPrimaryColumn() + " (Key)"));
                        primaryColumnNode.SetAttribute("fillcolor", nodeEdgeColor);
                        Node lookupColumnNode = currentTableGraph.GetOrAddNode(CharsetHelper.GetSafeName(tableEntity.getName() + "-" + lookupColumn.getDisplayName()));
                        lookupColumnNode.SetAttribute("label", CharsetHelper.GetSafeName(lookupColumn.getDisplayName()));
                        lookupColumnNode.SetAttribute("fillcolor", nodeEdgeColor);
                        Edge edge = rootGraph.GetOrAddEdge(lookupColumnNode, primaryColumnNode, "Lookup " + tableEntity.getLocalizedName() + " - " + lookupColumn.getDisplayName() + " - " + lookupColumn.getLogicalName());
                        edge.SetAttribute("color", nodeEdgeColor);
                        edge.SetAttribute("penwidth", "3");
                    }
                }
            }
            rootGraph.ComputeLayout(LayoutEngines.Dot);
            generateImageFiles(rootGraph);
        }

        private string generateImageFiles(RootGraph rootGraph)
        {
            //Generate image files
            // can't save directly as PNG (limitation of the .Net Wrapper), saving as SVG is the only option 
            rootGraph.ToSvgFile(content.folderPath + "dataverse.svg");
            // converting SVG to PNG
            var svgDocument = SvgDocument.Open(content.folderPath + "dataverse.svg");
            //generating the PNG from the SVG
            using (var bitmap = svgDocument.Draw())
            {
                bitmap?.Save(content.folderPath + "dataverse.png");
            }
            return "dataverse";
        }

        private string getHexColor(string lookupColumnName)
        {
            entityColors.TryGetValue(lookupColumnName, out string colour);
            if(String.IsNullOrEmpty(colour)) {
                string[] colors = new string[] { "#d35400", "#008000", "#3455DB", "#9400d3", "#939393", "#b8806b", "#D35400", "#008b8b", "#B50000", "#1460aa", "#8b008b", "#696969", "#634806", "#870c25"};
                colour = colors[entityColors.Count % colors.Length];
                entityColors.Add(lookupColumnName, colour);
            }
            return colour;
        }
    }
}