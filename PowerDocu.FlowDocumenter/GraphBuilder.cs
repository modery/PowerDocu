using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Rubjerg.Graphviz;
using System.Xml;

namespace PowerDocu.FlowDocumenter
{
    public class GraphBuilder
    {
        private Dictionary<Node, SubGraph> nodeClusterRelationship;
        private Dictionary<SubGraph, SubGraph> clusterRelationship;
        private List<string> nodesInGraph;
        private readonly FlowEntity flow;
        private readonly string folderPath;
        //using this list to store the names of edges. Some edges were created twice when creating an edge to a cluster (as it creates a dummy node when pointing to a cluster, which happens multiple times instead of getting reused)
        private List<string> edges;

        public GraphBuilder(FlowEntity flowToUse, string path)
        {
            flow = flowToUse;
            folderPath = path + CharsetHelper.GetSafeName(@"\FlowDoc " + flow.Name + @"\");
            Directory.CreateDirectory(folderPath);
        }

        public void buildTopLevelGraph()
        {
            buildGraph(false);
        }

        public void buildDetailedGraph()
        {
            buildGraph(true);
        }

        private void buildGraph(bool showSubactions)
        {
            edges = new List<string>();
            nodesInGraph = new List<string>();
            nodeClusterRelationship = new Dictionary<Node, SubGraph>();
            clusterRelationship = new Dictionary<SubGraph, SubGraph>();
            RootGraph rootGraph = RootGraph.CreateNew(GraphType.Directed, CharsetHelper.GetSafeName(flow.Name));
            Graph.IntroduceAttribute(rootGraph, "compound", "true");
            Graph.IntroduceAttribute(rootGraph, "fontname", "helvetica");
            Node.IntroduceAttribute(rootGraph, "shape", "");
            Node.IntroduceAttribute(rootGraph, "color", "");
            Node.IntroduceAttribute(rootGraph, "style", "");
            Node.IntroduceAttribute(rootGraph, "fillcolor", "");
            Node.IntroduceAttribute(rootGraph, "label", "");
            Node.IntroduceAttribute(rootGraph, "fontname", "helvetica");
            Edge.IntroduceAttribute(rootGraph, "label", "");
            List<ActionNode> rootActions = flow.actions.getRootNodes();

            Node trigger = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(flow.trigger.Name));
            trigger.SetAttribute("color", GraphColours.GetColourForAction("Trigger"));
            trigger.SetAttribute("fillcolor", GraphColours.GetFillColourForAction("Trigger"));
            trigger.SetAttribute("style", "filled");
            if (!String.IsNullOrEmpty(flow.trigger.Description))
            {
                string html = "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(flow.trigger.Name) + "</td></tr>";
                html += "<tr><td><FONT POINT-SIZE=\"10\">(" + generateMultiLineText(System.Web.HttpUtility.HtmlEncode(flow.trigger.Description)) + ")</FONT></td></tr></table>";
                trigger.SetAttributeHtml("label", html);
            }
            else
            {
                trigger.SetAttribute("label", CharsetHelper.GetSafeName(flow.trigger.Name));
            }
            if (!String.IsNullOrEmpty(flow.trigger.Connector))
            {
                string connectorIcon = ConnectorHelper.getConnectorIconFile(flow.trigger.Connector);

                if (!String.IsNullOrEmpty(connectorIcon))
                {
                    string connectorIcon32Path = folderPath + Path.GetFileNameWithoutExtension(connectorIcon) + "32.png";
                    ImageHelper.ConvertImageTo32(connectorIcon, connectorIcon32Path);
                    //path to image is absolute here, as GraphViz wasn't able to render it properly if relative. Will be replaced in the SVG just before the PNG gets generated
                    string html = "<table border=\"0\"><tr><td><img src=\"" + connectorIcon32Path + "\" /></td><td>" + CharsetHelper.GetSafeName(flow.trigger.Name) + "</td></tr>";
                    if (!String.IsNullOrEmpty(flow.trigger.Description))
                    {
                        html += "<tr><td></td><td><FONT POINT-SIZE=\"10\">" + generateMultiLineText(System.Web.HttpUtility.HtmlEncode(flow.trigger.Description)) + "</FONT></td></tr>";
                    }
                    html += "</table>";
                    trigger.SetAttributeHtml("label", html);
                }
            }
            foreach (ActionNode rootAction in rootActions)
            {
                addNodesToGraph(rootGraph, rootAction, null, null, showSubactions, true);
                nodesInGraph = new List<string>();
                addEdgesToGraph(rootGraph, rootAction, trigger, null, null, showSubactions, true);
            }
            rootGraph.CreateLayout();
            NotificationHelper.SendNotification("  - Created Graph " + folderPath + generateImageFiles(rootGraph, showSubactions) + ".png");
        }

        private string generateImageFiles(RootGraph rootGraph, bool showSubactions)
        {
            //Generate image files
            string filename = "flow" + (showSubactions ? "-detailed" : "");
            rootGraph.ToPngFile(folderPath + filename + ".png");
            rootGraph.ToSvgFile(folderPath + filename + ".svg");

            //updating the SVG, embedding any images as base64 content so that they are shown in the Word output
            XmlDocument xmlDoc = new XmlDocument
            {
                XmlResolver = null
            };
            xmlDoc.Load(folderPath + filename + ".svg");
            XmlNodeList elemList = xmlDoc.GetElementsByTagName("image");
            foreach (XmlNode xn in elemList)
            {
                xn.Attributes["xlink:href"].Value = "data:image/png;base64," + ImageHelper.GetBase64(xn.Attributes["xlink:href"].Value);
            }
            xmlDoc.Save(folderPath + filename + ".svg");
            //the following code is no longer required, as saving directly to PNG is now possible through GraphViz. Keeping it in case it is required in the future
            /*
            // converting SVG to PNG
            var svgDocument = SvgDocument.Open(folderPath + filename + ".svg");
            //generating the PNG from the SVG
            using (var bitmap = svgDocument.Draw())
            {
                bitmap?.Save(folderPath + filename + ".png");
            }
            */
            return filename;
        }

        /**
          * rootGraph - the RootGraph under which most new nodes get created
            node - the ActionNode to be processed now
            previousNeighbourNode - the Node that comes before the current node
            parentCluster -
            currentCluster -
            showSubactions - bool that controls if subactions are shown (showing all detail). If false, only the top level actions are shown
            isTopLevel - bool
          */
        private void addNodesToGraph(RootGraph rootGraph, ActionNode node, SubGraph parentCluster, SubGraph currentCluster, bool showSubactions, bool isTopLevel)
        {
            //we need to process each node only once
            if (!nodesInGraph.Contains(CharsetHelper.GetSafeName(node.Name)))
            {
                nodesInGraph.Add(CharsetHelper.GetSafeName(node.Name));
                SubGraph cluster = null;
                SubGraph yesCluster = null;
                SubGraph noCluster = null;
                //adding the current item as a new node
                Node currentNode = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(node.Name));
                currentNode.SetAttribute("shape", "box");
                currentNode.SetAttribute("margin", "0");
                currentNode.SetAttribute("color", GraphColours.GetColourForAction(node.Type));
                currentNode.SetAttribute("style", "filled");
                currentNode.SetAttribute("fillcolor", GraphColours.GetFillColourForAction(node.Type));
                //setting the label here again with the name is required to make the connector icon code below work properly
                if (!String.IsNullOrEmpty(node.Description))
                {
                    string html = "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(node.Name) + "</td></tr>";
                    html += "<tr><td><font point-size=\"10\">" + generateMultiLineText(System.Web.HttpUtility.HtmlEncode(node.Description)) + "</font></td></tr></table>";
                    currentNode.SetAttributeHtml("label", html);
                }
                else if (node.Type == "Switch")
                {
                    string html = "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(node.Name) + "</td></tr>";
                    html += "<tr><td><FONT POINT-SIZE=\"10\">(" + generateMultiLineText(System.Web.HttpUtility.HtmlEncode(node.Expression)) + ")</FONT></td></tr></table>";
                    currentNode.SetAttributeHtml("label", html);
                }
                else
                {
                    currentNode.SetAttribute("label", CharsetHelper.GetSafeName(node.Name));
                }
                if (!String.IsNullOrEmpty(node.Connection))
                {
                    string connectorIcon = ConnectorHelper.getConnectorIconFile(node.Connection);

                    if (!String.IsNullOrEmpty(connectorIcon))
                    {
                        string connectorIcon32Path = folderPath + Path.GetFileNameWithoutExtension(connectorIcon) + "32.png";
                        ImageHelper.ConvertImageTo32(connectorIcon, connectorIcon32Path);
                        //path to image is absolute here, as GraphViz wasn't able to render it properly if relative. Will be replaced in the SVG just before the PNG gets generated
                        string html = "<table border=\"0\"><tr><td><img src=\"" + connectorIcon32Path + "\" /></td><td>" + CharsetHelper.GetSafeName(node.Name) + "</td></tr>";
                        if (!String.IsNullOrEmpty(node.Description))
                        {
                            html += "<tr><td></td><td><font point-size=\"10\">" + generateMultiLineText(System.Web.HttpUtility.HtmlEncode(node.Description)) + "</font></td></tr>";
                        }
                        html += "</table>";
                        currentNode.SetAttributeHtml("label", html);
                    }
                }

                //might not have subactions for yes/no? to check!
                //if there are actions inside (likely only for Control), let's create a container
                //How about a SCOPE? TODO
                if (node.Subactions.Count + node.Elseactions.Count > 0)
                {
                    // if there are subactions, then we need to create a cluster for the current node and its child nodes
                    // if we are inside a cluster, then we create the new cluster as a child
                    if (currentCluster != null)
                    {
                        cluster = currentCluster.GetOrAddSubgraph("cluster_" + CharsetHelper.GetSafeName(node.Name));
                        if (!clusterRelationship.ContainsKey(cluster))
                            clusterRelationship.Add(cluster, currentCluster);
                    }
                    else
                    {
                        if (parentCluster == null)
                        {
                            //create the new cluster inside the rootgraph itself
                            cluster = rootGraph.GetOrAddSubgraph("cluster_" + CharsetHelper.GetSafeName(node.Name));
                        }
                        else
                        {
                            //create the new cluster inside the parent cluster
                            cluster = parentCluster.GetOrAddSubgraph("cluster_" + CharsetHelper.GetSafeName(node.Name));
                            if (!clusterRelationship.ContainsKey(cluster))
                                clusterRelationship.Add(cluster, parentCluster);
                        }
                    }
                    cluster.SafeSetAttribute("style", "filled", "");
                    cluster.SafeSetAttribute("fillcolor", GraphColours.GetFillColourForAction("Cluster"), "");
                    cluster.SafeSetAttribute("color", GraphColours.GetColourForAction("Cluster"), "");
                    cluster.AddExisting(currentNode);
                    if (!nodeClusterRelationship.ContainsKey(currentNode))
                        nodeClusterRelationship.Add(currentNode, cluster);

                    if (showSubactions)
                    {
                        foreach (ActionNode subaction in node.Subactions)
                        {
                            //connect the subactions to the current node inside the cluster
                            if (node.Elseactions.Count > 0)
                            {
                                yesCluster = cluster.GetOrAddSubgraph("cluster_yes" + CharsetHelper.GetSafeName(node.Name));
                                if (!clusterRelationship.ContainsKey(yesCluster))
                                    clusterRelationship.Add(yesCluster, cluster);
                                yesCluster.SafeSetAttribute("style", "filled", "");
                                yesCluster.SafeSetAttribute("fillcolor", GraphColours.GetFillColourForAction("YesCluster"), "");
                                yesCluster.SafeSetAttribute("color", GraphColours.GetColourForAction("YesCluster"), "");
                                addNodesToGraph(rootGraph, subaction, parentCluster, yesCluster, showSubactions, false);
                            }
                            //todo if there are no elseactions, there is no cluster and it doesn't get coloured. But of we add it, the CoE flows throw errors
                            /*else if (node.Type.Equals("If"))
                            {
                                //todo there are no elseactions, but we should still create a new cluster for the yes branch (and we don't show the no branch)
                                yesCluster = cluster.GetOrAddSubgraph("cluster_yes" + CharsetHelper.GetSafeName(node.Name));
                                //Console.WriteLine("Cluster5: " + yesCluster.GetName() + " - " + cluster.GetName());
                                if (!clusterRelationship.ContainsKey(yesCluster))
                                    clusterRelationship.Add(yesCluster, cluster);
                                yesCluster.SafeSetAttribute("style", "filled", "");
                                yesCluster.SafeSetAttribute("fillcolor", "lightgreen", "");
                                //next line is likely breaking stuff
                                addNodesToGraph(rootGraph, subaction, cluster, yesCluster, showSubactions, false);
                            }*/
                            else
                            {
                                addNodesToGraph(rootGraph, subaction, null, cluster, showSubactions, false);
                            }
                        }
                        foreach (ActionNode subaction in node.Elseactions)
                        {
                            //connect the subactions to the current node inside the cluster                         
                            noCluster = cluster.GetOrAddSubgraph("cluster_no" + CharsetHelper.GetSafeName(node.Name));
                            if (!clusterRelationship.ContainsKey(noCluster))
                                clusterRelationship.Add(noCluster, cluster);
                            noCluster.SafeSetAttribute("style", "filled", "");
                            noCluster.SafeSetAttribute("fillcolor", GraphColours.GetFillColourForAction("NoCluster"), "");
                            noCluster.SafeSetAttribute("color", GraphColours.GetColourForAction("NoCluster"), "");
                            addNodesToGraph(rootGraph, subaction, parentCluster, noCluster, showSubactions, false);
                        }
                    }
                }
                else if (currentCluster != null)
                {
                    currentCluster.AddExisting(currentNode);
                    if (!nodeClusterRelationship.ContainsKey(currentNode))
                        nodeClusterRelationship.Add(currentNode, currentCluster);
                }

                foreach (ActionNode neighbour in node.Neighbours)
                {
                    addNodesToGraph(rootGraph, neighbour, isTopLevel ? null : cluster, currentCluster, showSubactions, isTopLevel);
                }
            }
        }

        void addEdgesToGraph(RootGraph rootGraph, ActionNode node, Node previousNeighbourNode, SubGraph parentCluster, SubGraph currentCluster, bool showSubactions, bool isTopLevel)
        {
            //we need to process each node only once
            if (!nodesInGraph.Contains(CharsetHelper.GetSafeName(node.Name)))
            {
                nodesInGraph.Add(CharsetHelper.GetSafeName(node.Name));
                SubGraph cluster = null;
                SubGraph yesCluster = null;
                SubGraph noCluster = null;
                string edgeName;
                Node currentNode = rootGraph.GetNode(CharsetHelper.GetSafeName(node.Name));
                if (node.Subactions.Count + node.Elseactions.Count > 0)
                {
                    if (currentCluster != null)
                    {
                        cluster = currentCluster.GetSubgraph("cluster_" + CharsetHelper.GetSafeName(node.Name));
                    }
                    else
                    {
                        if (parentCluster == null)
                        {
                            cluster = rootGraph.GetSubgraph("cluster_" + CharsetHelper.GetSafeName(node.Name));
                        }
                        else
                        {
                            cluster = parentCluster.GetSubgraph("cluster_" + CharsetHelper.GetSafeName(node.Name));
                        }
                    }
                    if (showSubactions)
                    {
                        foreach (ActionNode subaction in node.Subactions)
                        {
                            //connect the subactions to the current node inside the cluster
                            if (node.Elseactions.Count > 0)
                            {
                                yesCluster = cluster.GetSubgraph("cluster_yes" + CharsetHelper.GetSafeName(node.Name));
                                addEdgesToGraph(rootGraph, subaction, currentNode, parentCluster, yesCluster, showSubactions, false);
                            }
                            else
                            {
                                addEdgesToGraph(rootGraph, subaction, currentNode, null, cluster, showSubactions, false);
                            }
                        }
                        foreach (ActionNode subaction in node.Elseactions)
                        {
                            noCluster = cluster.GetSubgraph("cluster_no" + CharsetHelper.GetSafeName(node.Name));
                            addEdgesToGraph(rootGraph, subaction, currentNode, parentCluster, noCluster, showSubactions, false);
                        }
                    }
                }

                //start connecting the nodes
                edgeName = "Edge " + previousNeighbourNode.GetName() + "-" + currentNode.GetName();
                //get the preceding neighbours 
                List<ActionNode> precedingNeighbours = flow.actions.getPrecedingNeighbours(node);
                //Only connect if there is no preceding node (meaning it's the first node in the current level) or if the 'parent' is the previous node), and if there's no existing edge (to avoid duplicates)
                if ((precedingNeighbours.Count == 0 || (precedingNeighbours.Count == 1 && precedingNeighbours.Find(o => o.Name.Equals(previousNeighbourNode.GetName())) != null)) && !edges.Contains(edgeName))
                {
                    ActionNode precedingNeighbour = precedingNeighbours.Count > 0 ? precedingNeighbours.Find(o => o.Name.Equals(previousNeighbourNode.GetName())) : null;
                    string edgeLabel = null;
                    if (node.parent?.Type.Equals("Switch") == true && node.parent.Name.Equals(previousNeighbourNode.GetName()))
                    {
                        node.parent.switchRelationship.TryGetValue(node, out edgeLabel);
                    }
                    CreateEdge(currentNode, previousNeighbourNode, node, precedingNeighbour, edgeName, rootGraph, edgeLabel);
                }
                else if (precedingNeighbours.Count > 0 && !edges.Contains(edgeName))
                {
                    foreach (ActionNode precedingNeighbour in precedingNeighbours)
                    {
                        edgeName = "Edge " + precedingNeighbour.Name + "-" + currentNode.GetName();
                        Node precNode = rootGraph.GetNode(CharsetHelper.GetSafeName(precedingNeighbour.Name));
                        if (precNode != null)
                        {
                            CreateEdge(currentNode, precNode, node, precedingNeighbour, edgeName, rootGraph);
                        }
                    }
                }
                foreach (ActionNode neighbour in node.Neighbours)
                {
                    addEdgesToGraph(rootGraph, neighbour, currentNode, isTopLevel ? null : cluster, currentCluster, showSubactions, isTopLevel);
                }
            }
        }

        private void CreateEdge(Node currentNode, Node previousNeighbourNode, ActionNode currentActionNode, ActionNode precedingNeighbour, string edgeName, RootGraph rootGraph, string edgeLabel = null)
        {
            Edge edgeAB = null;
            SubGraph prevCluster = (nodeClusterRelationship.ContainsKey(previousNeighbourNode)) ? nodeClusterRelationship[previousNeighbourNode] : null;
            SubGraph curCluster = (nodeClusterRelationship.ContainsKey(currentNode)) ? nodeClusterRelationship[currentNode] : null;
            if (prevCluster != null)
            {
                if (curCluster != null)
                {
                    if (prevCluster != curCluster)
                    {
                        SubGraph curClusterParent = (clusterRelationship.ContainsKey(curCluster)) ? clusterRelationship[curCluster] : null;
                        SubGraph prevClusterParent = (clusterRelationship.ContainsKey(prevCluster)) ? clusterRelationship[prevCluster] : null;
                        //connect a node inside a cluster with a subsequent cluster
                        if (curClusterParent == prevCluster)
                        {
                            edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                            edgeAB.SetLogicalHead(curCluster);
                        }
                        else
                        {
                            //connecting two top level clusters
                            if (curClusterParent == null || previousNeighbourNode.GetName().Equals(precedingNeighbour?.Name) || curCluster == prevClusterParent)
                            {
                                //adding an invisible node at the bottom of the previous cluster
                                Node invisNode = prevCluster.GetOrAddNode("invisnode" + prevCluster + ((prevClusterParent == curCluster) ? currentNode : curCluster));
                                invisNode.SafeSetAttribute("style", "invis", "");
                                invisNode.SafeSetAttribute("margin", "0", "");
                                invisNode.SafeSetAttribute("width", "0", "");
                                invisNode.SafeSetAttribute("height", "0", "");
                                invisNode.SafeSetAttribute("shape", "point", "");
                                edgeAB = rootGraph.GetOrAddEdge(invisNode, currentNode, edgeName);
                                if (prevClusterParent != curCluster)
                                {
                                    edgeAB.SetLogicalHead(curCluster);
                                }
                                edgeAB.SetLogicalTail(prevCluster);
                                //invisible node is at the bottom during the layout process because we connect all other "end nodes" in the preCluster, that is nodes that do not have any neighbour nodes, with it
                                foreach (Node clusterNode in prevCluster.Nodes())
                                {
                                    ActionNode clusterActionNode = flow.actions.Find(clusterNode.GetName());
                                    if (clusterActionNode?.Neighbours.Count + clusterActionNode?.Subactions.Count + clusterActionNode?.Elseactions.Count == 0)
                                    {
                                        //creating an invisible edge only if there are no other subsequent nodes (neighbors or subnodes)
                                        edgeAB = rootGraph.GetOrAddEdge(clusterNode, invisNode, clusterNode + "-" + invisNode);
                                        edgeAB.SafeSetAttribute("style", "invis", "");
                                    }
                                }
                            }
                            else
                            {
                                SubGraph prevClusterParentParent = null;
                                SubGraph curClusterParentParent = null;
                                if (curClusterParent != null)
                                    clusterRelationship.TryGetValue(curClusterParent, out curClusterParentParent);
                                if (prevClusterParent != null)
                                    clusterRelationship.TryGetValue(prevClusterParent, out prevClusterParentParent);
                                if (curClusterParentParent != prevClusterParentParent)
                                {
                                    while (curClusterParentParent != prevCluster)
                                    {
                                        curClusterParent = curClusterParentParent;
                                        curClusterParentParent = clusterRelationship[curClusterParent];
                                    }
                                    edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                                    edgeAB.SetLogicalHead(curClusterParent);
                                }
                                else
                                {
                                    //edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                                    //edgeAB.SetLogicalHead(curClusterParent);
                                    //adding an invisible node at the bottom of the previous cluster
                                    Node invisNode = prevCluster.GetOrAddNode("invisnode" + prevCluster + ((prevClusterParent == curCluster) ? currentNode : curCluster));
                                    invisNode.SafeSetAttribute("style", "invis", "");
                                    invisNode.SafeSetAttribute("margin", "0", "");
                                    invisNode.SafeSetAttribute("width", "0", "");
                                    invisNode.SafeSetAttribute("height", "0", "");
                                    invisNode.SafeSetAttribute("shape", "point", "");
                                    edgeAB = rootGraph.GetOrAddEdge(invisNode, currentNode, edgeName);
                                    if (prevClusterParent != curCluster)
                                    {
                                        edgeAB.SetLogicalHead(curCluster);
                                    }
                                    edgeAB.SetLogicalTail(prevCluster);
                                    //invisible node is at the bottom during the layout process because we connect all other "end nodes" in the preCluster, that is nodes that do not have any neighbour nodes, with it
                                    foreach (Node clusterNode in prevCluster.Nodes())
                                    {
                                        ActionNode clusterActionNode = flow.actions.Find(clusterNode.GetName());
                                        if (clusterActionNode?.Neighbours.Count + clusterActionNode?.Subactions.Count + clusterActionNode?.Elseactions.Count == 0)
                                        {
                                            //creating an invisible edge only if there are no other subsequent nodes (neighbors or subnodes)
                                            edgeAB = rootGraph.GetOrAddEdge(clusterNode, invisNode, clusterNode + "-" + invisNode);
                                            edgeAB.SafeSetAttribute("style", "invis", "");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //connect two nodes inside a cluster
                    else
                    {
                        edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                    }
                }
                else
                {
                    //adding an invisible node at the bottom of the cluster
                    Node invisNode = prevCluster.GetOrAddNode("invisnode" + prevCluster + currentNode);
                    invisNode.SafeSetAttribute("style", "invis", "");
                    invisNode.SafeSetAttribute("margin", "0", "");
                    invisNode.SafeSetAttribute("width", "0", "");
                    invisNode.SafeSetAttribute("height", "0", "");
                    invisNode.SafeSetAttribute("shape", "point", "");
                    edgeAB = rootGraph.GetOrAddEdge(invisNode, currentNode, edgeName);
                    edgeAB.SetLogicalTail(prevCluster);
                    //invisible node is at the bottom because we connect all other "end nodes" in the preCluster, that is nodes that do not have any neighbour nodes, with it
                    foreach (Node clusterNode in prevCluster.Nodes())
                    {
                        ActionNode clusterActionNode = flow.actions.Find(clusterNode.GetName());
                        if (clusterActionNode?.Neighbours.Count + clusterActionNode?.Subactions.Count + clusterActionNode?.Elseactions.Count == 0)
                        {
                            //creating an invisible edge only if there are no other subsequent nodes (neighbors or subnodes)
                            edgeAB = rootGraph.GetOrAddEdge(clusterNode, invisNode, clusterNode + "-" + invisNode);
                            edgeAB.SafeSetAttribute("style", "invis", "");
                        }
                    }
                }
            }
            else
            {
                //connect a node with a subsequently following cluster
                if (curCluster != null)
                {
                    edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                    edgeAB.SetLogicalHead(curCluster);
                }
                //directly connect two nodes that are not part of a cluster
                else
                {
                    edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                }
            }
            //additional formatting for the edge in case the "run after" property of the action does not include "Succeeded"
            if (precedingNeighbour?.nodeRunAfterConditions?.ContainsKey(currentActionNode) == true)
            {
                precedingNeighbour.nodeRunAfterConditions.TryGetValue(currentActionNode, out string[] runAfterConditions);
                if (!runAfterConditions.Contains("Succeeded"))
                {
                    edgeAB.SafeSetAttribute("style", "dotted", "");
                    edgeAB.SafeSetAttribute("color", "red", "");
                }
            }
            if (edgeLabel != null)
            {
                edgeAB.SetAttribute("label", edgeLabel);
            }
            edges.Add(edgeName);
        }

        //splits a text into multiple lines (<br/> for line breaks), with each line having a maximum of 65 characters
        private string generateMultiLineText(string text)
        {
            string[] words = text.Split(' ');
            string multiLineText = "";
            int lineLength = 0;
            for (var counter = 0; counter < words.Length; counter++)
            {
                lineLength += words[counter].Length + 1;
                if (lineLength >= 65)
                {
                    multiLineText += "<br/>";
                    lineLength = 0;
                }
                multiLineText = multiLineText + words[counter] + " ";
            }
            return multiLineText;
        }

    }

    public static class GraphColours
    {
        public static string ControlDarkColour = "#484f58";
        public static string ControlDarkFillColour = "#dedfe0";
        public static string ControlLightColour = "#486991";
        public static string ControlLightFillColour = "#e4e9ef";
        public static string ScopeColour = "#8c3900";
        public static string ScopeFillColour = "#eee1d9";
        public static string TerminateColour = "#f41700";
        public static string TerminateFillColour = "#fddcd9";
        public static string DataOperationColour = "#8c6cff";
        public static string DataOperationFillColour = "#eee9ff";
        public static string TriggerColour = "#0077ff";
        public static string TriggerFillColour = "#d9ebff";
        public static string DataverseColour = "#088142";
        public static string DataverseFillColour = "#daece3";
        public static string VariableColour = "#770bd6";
        public static string VariableFillColour = "#ebdbf9";
        public static string YesClusterFillColour = "#88da8d";//actual in studio: "#edf9ee";
        public static string YesClusterColour = "#88da8d";
        public static string NoClusterFillColour = "#fb8981";//actual in studio: "#feedec";
        public static string NoClusterColour = "#fb8981";
        public static string ClusterColour = "#000090";

        //TODO other types to add
        /*
        Response 
        Query         
        Workflow 
        OpenApiConnectionWebhook 
        Wait
        */
        public static string GetColourForAction(string actionType)
        {
            return actionType switch
            {
                "Trigger" or "Expression" => TriggerColour,
                "Compose" or "ParseJson" or "Select" or "Switch" or "Table" => DataOperationColour,
                "If" or "Switch" => ControlDarkColour,
                "Foreach" or "Until" => ControlDarkColour,
                "Scope" => ScopeColour,
                "Terminate" => TerminateColour,
                "OpenApiConnection" => DataverseColour,
                "SetVariable" or "AppendToArrayVariable" or "InitializeVariable" or "IncrementVariable" or "AppendToStringVariable" => VariableColour,
                "Cluster" => ClusterColour,
                "YesCluster" => YesClusterColour, //previously "lightgreen"
                "NoCluster" => NoClusterColour, //previously "lightcoral"
                "ApiConnection" => "blue", //todo this would require some additional checks which colour to use. For example, office365, msnweather, sharepoint, .... (property Connection of the ActionNode)
                _ => "blue",
            };
        }

        public static string GetFillColourForAction(string actionType)
        {
            string colour = actionType switch
            {
                "Trigger" or "Expression" => TriggerFillColour,
                "Compose" or "ParseJson" or "Select" or "Switch" or "Table" => DataOperationFillColour,
                "If" or "Switch" => ControlDarkFillColour,
                "Foreach" or "Until" => ControlDarkFillColour,
                "Scope" => ScopeFillColour,
                "Terminate" => TerminateFillColour,
                "OpenApiConnection" => DataverseFillColour,
                "SetVariable" or "AppendToArrayVariable" or "InitializeVariable" or "IncrementVariable" or "AppendToStringVariable" => VariableFillColour,
                "Cluster" => "#ffffff", //previously "grey90"
                "YesCluster" => YesClusterFillColour, //previously "lightgreen"
                "NoCluster" => NoClusterFillColour, //previously "lightcoral"
                "ApiConnection" => "#ffffff", //todo this would require some additional checks which colour to use. For example, office365, msnweather, sharepoint, .... (property Connection of the ActionNode)
                _ => "white",
            };
            //todo left in for debugging purposes
            /*if (colour == "white")
            {
                Console.WriteLine(actionType);
            }*/
            return colour;
        }
    }
}