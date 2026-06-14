using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Rubjerg.Graphviz;

namespace PowerDocu.SolutionDocumenter
{
    public class SolutionComponentGraphBuilder
    {
        private static readonly Dictionary<string, (string FillColor, string FontColor, string Shape)> NodeStyles =
            new Dictionary<string, (string, string, string)>(StringComparer.OrdinalIgnoreCase)
            {
                { "Agent",             ("#6C3483", "#ffffff", "hexagon") },
                { "Flow",              ("#2874A6", "#ffffff", "rectangle") },
                { "Canvas App",        ("#1D8348", "#ffffff", "rectangle") },
                { "Model-Driven App",  ("#117A65", "#ffffff", "rectangle") },
                { "Business Process Flow", ("#AF601A", "#ffffff", "octagon") },
                { "Desktop Flow",      ("#2E86C1", "#ffffff", "rectangle") },
                { "Dataflow",          ("#1A5276", "#ffffff", "rectangle") },
                { "Table",             ("#B9770E", "#ffffff", "cylinder") },
                { "Connector",         ("#7D3C98", "#ffffff", "diamond") },
                { "Data Source",       ("#7D3C98", "#ffffff", "diamond") },
                { "AI Model",          ("#C0392B", "#ffffff", "ellipse") },
                { "Environment Variable", ("#5B6770", "#ffffff", "rectangle") },
                { "Security Role",     ("#5B6770", "#ffffff", "rectangle") },
                { "Option Set",        ("#5B6770", "#ffffff", "rectangle") },
            };

        private static readonly Dictionary<string, string> EdgeColors =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "uses as tool",    "#2874A6" },
                { "uses connector",  "#7D3C98" },
                { "invokes agent",   "#C0392B" },
                { "knowledge source","#B9770E" },
                { "data source",     "#1D8348" },
                { "uses table",      "#B9770E" },
                { "navigates to",    "#B9770E" },
                { "lookup",          "#D35400" },
                { "many-to-many",    "#D35400" },
                { "uses AI model",   "#C0392B" },
                { "primary entity",  "#AF601A" },
                { "stage entity",    "#AF601A" },
                { "uses env variable","#5B6770" },
                { "calls flow",      "#2874A6" },
                { "loads into",      "#1A5276" },
            };

        // Component types to include in the graph. Add new types here as needed.
        private static readonly HashSet<string> AllowedComponentTypes =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "Agent",
                "AI Model",
                "Canvas App",
                "Flow",
                "Model-Driven App",
                "Business Process Flow",
                "Desktop Flow",
                "Dataflow",
                "Table"
                /* [potential future additions]
                "Connector",
                "Data Source",
                "Environment Variable",
                "Security Role",
                "Option Set"    */
            };

        private readonly SolutionDocumentationContent content;
        private readonly string outputPath;
        private readonly bool showAllComponents;

        public SolutionComponentGraphBuilder(SolutionDocumentationContent content, string outputPath, bool showAllComponents)
        {
            this.content = content;
            this.outputPath = outputPath;
            this.showAllComponents = showAllComponents;
        }

        public void Build()
        {
            var (relationships, allComponents) = SolutionRelationshipAnalyzer.Analyze(content);

            if (relationships.Count == 0 && !showAllComponents)
                return;

            RootGraph rootGraph = RootGraph.CreateNew(GraphType.Directed, "SolutionComponentRelationships");

            // Graph attributes
            Graph.IntroduceAttribute(rootGraph, "compound", "true");
            Graph.IntroduceAttribute(rootGraph, "rankdir", "LR");
            Graph.IntroduceAttribute(rootGraph, "fontname", "helvetica");
            Graph.IntroduceAttribute(rootGraph, "label", "Solution Component Relationships");
            Graph.IntroduceAttribute(rootGraph, "labelloc", "t");
            Graph.IntroduceAttribute(rootGraph, "fontsize", "18");
            Graph.IntroduceAttribute(rootGraph, "style", "");
            Graph.IntroduceAttribute(rootGraph, "color", "");
            Graph.IntroduceAttribute(rootGraph, "fillcolor", "");
            Graph.IntroduceAttribute(rootGraph, "penwidth", "1");

            // Node defaults
            Node.IntroduceAttribute(rootGraph, "shape", "rectangle");
            Node.IntroduceAttribute(rootGraph, "style", "filled");
            Node.IntroduceAttribute(rootGraph, "fillcolor", "#5B6770");
            Node.IntroduceAttribute(rootGraph, "fontcolor", "#ffffff");
            Node.IntroduceAttribute(rootGraph, "fontname", "helvetica");
            Node.IntroduceAttribute(rootGraph, "fontsize", "11");
            Node.IntroduceAttribute(rootGraph, "color", "#333333");
            Node.IntroduceAttribute(rootGraph, "label", "");
            Node.IntroduceAttribute(rootGraph, "penwidth", "1");

            // Edge defaults
            Edge.IntroduceAttribute(rootGraph, "color", "#666666");
            Edge.IntroduceAttribute(rootGraph, "fontname", "helvetica");
            Edge.IntroduceAttribute(rootGraph, "fontsize", "9");
            Edge.IntroduceAttribute(rootGraph, "penwidth", "1.5");
            Edge.IntroduceAttribute(rootGraph, "label", "");

            // Determine which components appear in edges (only from relationships where both ends are allowed)
            var connectedComponents = new HashSet<SolutionComponentNode>();
            foreach (var rel in relationships)
            {
                if (!AllowedComponentTypes.Contains(rel.SourceType) || !AllowedComponentTypes.Contains(rel.TargetType))
                    continue;
                connectedComponents.Add(new SolutionComponentNode(rel.SourceType, rel.SourceName));
                connectedComponents.Add(new SolutionComponentNode(rel.TargetType, rel.TargetName));
            }

            // Group components by type for clustering
            var componentsByType = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            // Add connected components (always), filtered to allowed types
            foreach (var comp in connectedComponents)
            {
                if (!AllowedComponentTypes.Contains(comp.Type))
                    continue;
                if (!componentsByType.ContainsKey(comp.Type))
                    componentsByType[comp.Type] = new List<string>();
                if (!componentsByType[comp.Type].Contains(comp.Name))
                    componentsByType[comp.Type].Add(comp.Name);
            }

            // Add all components if requested, filtered to allowed types
            if (showAllComponents)
            {
                foreach (var comp in allComponents)
                {
                    if (!AllowedComponentTypes.Contains(comp.Type))
                        continue;
                    if (!componentsByType.ContainsKey(comp.Type))
                        componentsByType[comp.Type] = new List<string>();
                    if (!componentsByType[comp.Type].Contains(comp.Name))
                        componentsByType[comp.Type].Add(comp.Name);
                }
            }

            // Create nodes for each component (no clustering by type)
            var nodeMap = new Dictionary<string, Node>();
            foreach (var kvp in componentsByType.OrderBy(k => k.Key, StringComparer.OrdinalIgnoreCase))
            {
                string componentType = kvp.Key;
                var names = kvp.Value;

                var (fillColor, fontColor, shape) = GetNodeStyle(componentType);

                foreach (string name in names.OrderBy(n => n, StringComparer.OrdinalIgnoreCase))
                {
                    string nodeId = CharsetHelper.GetSafeName(componentType + "::" + name);
                    Node node = rootGraph.GetOrAddNode(nodeId);
                    node.SetAttribute("label", name);
                    node.SetAttribute("fillcolor", fillColor);
                    node.SetAttribute("fontcolor", fontColor);
                    node.SetAttribute("shape", shape);
                    nodeMap[componentType + "::" + name] = node;
                }
            }

            // Build a legend showing only the component types actually used
            var usedTypes = componentsByType.Keys.OrderBy(k => k, StringComparer.OrdinalIgnoreCase).ToList();
            if (usedTypes.Count > 0)
            {
                SubGraph legend = rootGraph.GetOrAddSubgraph("cluster_legend");
                legend.SetAttribute("label", "Legend");
                legend.SetAttribute("style", "rounded");
                legend.SetAttribute("color", "#999999");
                legend.SetAttribute("fontname", "helvetica");
                legend.SetAttribute("fontsize", "12");

                Node prevLegendNode = null;
                foreach (string typeName in usedTypes)
                {
                    var (fillColor, fontColor, shape) = GetNodeStyle(typeName);
                    string legendNodeId = CharsetHelper.GetSafeName("legend_" + typeName);
                    Node legendNode = legend.GetOrAddNode(legendNodeId);
                    legendNode.SetAttribute("label", typeName);
                    legendNode.SetAttribute("fillcolor", fillColor);
                    legendNode.SetAttribute("fontcolor", fontColor);
                    legendNode.SetAttribute("shape", shape);
                    legendNode.SetAttribute("fontsize", "9");

                    // Chain legend nodes with invisible edges to keep them in order
                    if (prevLegendNode != null)
                    {
                        Edge invisEdge = rootGraph.GetOrAddEdge(prevLegendNode, legendNode,
                            CharsetHelper.GetSafeName("legend_edge_" + typeName));
                        invisEdge.SetAttribute("color", "transparent");
                        invisEdge.SetAttribute("label", "");
                    }
                    prevLegendNode = legendNode;
                }
            }

            // Deduplicate edges (skip edges involving excluded component types)
            var edgeSet = new HashSet<string>();
            foreach (var rel in relationships)
            {
                if (!AllowedComponentTypes.Contains(rel.SourceType) || !AllowedComponentTypes.Contains(rel.TargetType))
                    continue;

                string edgeKey = rel.SourceType + "::" + rel.SourceName + " -> " + rel.TargetType + "::" + rel.TargetName + " [" + rel.RelationshipLabel + "]";
                if (!edgeSet.Add(edgeKey))
                    continue;

                string sourceKey = rel.SourceType + "::" + rel.SourceName;
                string targetKey = rel.TargetType + "::" + rel.TargetName;

                if (!nodeMap.ContainsKey(sourceKey) || !nodeMap.ContainsKey(targetKey))
                    continue;

                string edgeColor = GetEdgeColor(rel.RelationshipLabel);
                Edge edge = rootGraph.GetOrAddEdge(nodeMap[sourceKey], nodeMap[targetKey],
                    CharsetHelper.GetSafeName(edgeKey));
                edge.SetAttribute("color", edgeColor);
                edge.SetAttribute("label", rel.RelationshipLabel);
                edge.SetAttribute("fontcolor", edgeColor);
            }

            rootGraph.ComputeLayout(LayoutEngines.Dot);

            Directory.CreateDirectory(outputPath);
            string svgPath = Path.Combine(outputPath, "solution-components.svg");
            string pngPath = Path.Combine(outputPath, "solution-components.png");
            rootGraph.ToSvgFile(svgPath);
            rootGraph.ToPngFile(pngPath);
        }

        private static (string FillColor, string FontColor, string Shape) GetNodeStyle(string componentType)
        {
            if (NodeStyles.TryGetValue(componentType, out var style))
                return style;
            return ("#5B6770", "#ffffff", "rectangle");
        }

        private static string GetEdgeColor(string relationshipLabel)
        {
            if (EdgeColors.TryGetValue(relationshipLabel, out string color))
                return color;
            return "#666666";
        }
    }
}
