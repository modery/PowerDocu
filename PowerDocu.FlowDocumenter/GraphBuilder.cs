using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;
using Rubjerg.Graphviz;
using Svg;
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
            folderPath = path + CharsetHelper.GetSafeName(@"\FlowDoc - " + flow.Name + @"\");
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
            RootGraph rootGraph = RootGraph.CreateNew(CharsetHelper.GetSafeName(flow.Name), GraphType.Directed);
            Graph.IntroduceAttribute(rootGraph, "compound", "true");
            Node.IntroduceAttribute(rootGraph, "shape", "");
            Node.IntroduceAttribute(rootGraph, "color", "");
            Node.IntroduceAttribute(rootGraph, "style", "");
            Node.IntroduceAttribute(rootGraph, "fillcolor", "");
            Node.IntroduceAttribute(rootGraph, "label", "");
            ActionNode rootAction = flow.actions.getRootNode();

            Node trigger = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(flow.trigger.Name));
            trigger.SetAttribute("color", "green");
            if (!String.IsNullOrEmpty(flow.trigger.Description))
            {
                string html = "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(flow.trigger.Name) + "</td></tr>";
                html += "<tr><td><FONT POINT-SIZE=\"10\">(" + flow.trigger.Description + ")</FONT></td></tr></table>";
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
                        html += "<tr><td></td><td><FONT POINT-SIZE=\"10\">" + flow.trigger.Description + "</FONT></td></tr>";
                    }
                    html += "</table>";
                    trigger.SetAttributeHtml("label", html);
                }
            }
            addNodesToGraph(rootGraph, rootAction, null, null, showSubactions, true);
            nodesInGraph = new List<string>();
            addEdgesToGraph(rootGraph, rootAction, trigger, null, null, showSubactions, true);
            rootGraph.ComputeLayout();

            NotificationHelper.SendNotification("  - Created Graph " + folderPath + generateImageFiles(rootGraph, showSubactions) + ".png");
        }

        private string generateImageFiles(RootGraph rootGraph, bool showSubactions)
        {
            //Generate image files
            string filename = "flow" + (showSubactions ? "-detailed" : "");
            // can't save directly as PNG (limitation of the .Net Wrapper), saving as SVG is the only option 
            rootGraph.ToSvgFile(folderPath + filename + ".svg");
            //rootGraph.ToDotFile(folderPath + filename+".dot");

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
            // converting SVG to PNG
            var svgDocument = SvgDocument.Open(folderPath + filename + ".svg");
            //generating the PNG from the SVG
            using (var bitmap = svgDocument.Draw())
            {
                bitmap?.Save(folderPath + filename + ".png");
            }
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
                currentNode.SetAttribute("shape", "record");
                currentNode.SetAttribute("color", "blue");
                currentNode.SetAttribute("style", "filled");
                currentNode.SetAttribute("fillcolor", "white");
                //setting the label here again with the name is required to make the connector icon code below work properly
                if (!String.IsNullOrEmpty(node.Description))
                {
                    string html = "<table border=\"0\"><tr><td>" + CharsetHelper.GetSafeName(node.Name) + "</td></tr>";
                    html += "<tr><td><font point-size=\"10\">" + node.Description + "</font></td></tr></table>";
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
                            html += "<tr><td></td><td><font point-size=\"10\">" + node.Description + "</font></td></tr>";
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
                    cluster.SafeSetAttribute("fillcolor", "grey90", "");
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
                                yesCluster.SafeSetAttribute("fillcolor", "lightgreen", "");
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
                            noCluster.SafeSetAttribute("fillcolor", "lightcoral", "");
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
                //get the preceding neighbour, which is on the same hierarchy level
                ActionNode precedingNeighbour = flow.actions.getPrecedingNeighbour(node);
                //Only connect if there is no preceding node (meaning it's the first node in the current level) or if the 'parent' is the previous node), and if there's no existing edge (to avoid duplicates)
                if ((precedingNeighbour == null || previousNeighbourNode.GetName().Equals(precedingNeighbour.Name)) && !edges.Contains(edgeName))
                {
                    CreateEdge(currentNode, previousNeighbourNode, node, precedingNeighbour, edgeName, rootGraph);
                }
                else if (precedingNeighbour != null && !edges.Contains(edgeName))
                {
                    edgeName = "Edge " + precedingNeighbour.Name + "-" + currentNode.GetName();
                    Node precNode = rootGraph.GetNode(CharsetHelper.GetSafeName(precedingNeighbour.Name));
                    if (precNode != null)
                    {
                        CreateEdge(currentNode, precNode, node, precedingNeighbour, edgeName, rootGraph);
                    }
                }

                foreach (ActionNode neighbour in node.Neighbours)
                {
                    addEdgesToGraph(rootGraph, neighbour, currentNode, isTopLevel ? null : cluster, currentCluster, showSubactions, isTopLevel);
                }
            }
        }

        private void CreateEdge(Node currentNode, Node previousNeighbourNode, ActionNode currentActionNode, ActionNode precedingNeighbour, string edgeName, RootGraph rootGraph)
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
            edges.Add(edgeName);
        }
    }
}