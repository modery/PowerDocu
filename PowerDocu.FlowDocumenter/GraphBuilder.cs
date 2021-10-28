using System;
using System.IO;
using System.Collections.Generic;
using PowerDocu.Common;
using Rubjerg.Graphviz;
using Svg;
using System.Xml;

namespace PowerDocu.FlowDocumenter
{
    class GraphBuilder
    {

        Dictionary<Node, SubGraph> nodeClusterRelationship = new Dictionary<Node, SubGraph>();
        Dictionary<SubGraph, SubGraph> clusterRelationship = new Dictionary<SubGraph, SubGraph>();
        FlowEntity flow;
        private string folderPath;
        //using this list to store the names of edges. Some edges were created twice when creating an edge to a cluster (as it creates a dummy node when pointing to a cluster, which happens multiple times instead of getting reused)
        private List<string> edges;

        public GraphBuilder(FlowEntity flowToUse, string path)
        {
            flow = flowToUse;
            folderPath = path + CharsetHelper.GetSafeName(@"\Flow Documentation - " + flow.Name + @"\");
            Directory.CreateDirectory(folderPath);
        }

        public string buildTopLevelGraph()
        {
            return buildGraph(false);
        }

        public string buildDetailedGraph()
        {
            return buildGraph(true);
        }

        private string buildGraph(bool showSubactions)
        {
            edges = new List<string>();
            RootGraph rootGraph = RootGraph.CreateNew(CharsetHelper.GetSafeName(flow.Name), GraphType.Directed);
            Graph.IntroduceAttribute(rootGraph, "compound", "true");
            ActionNode rootAction = flow.actions.getRootNode();

            Node trigger = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(flow.trigger.Name));
            trigger.SafeSetAttribute("color", "green", "");
            trigger.SafeSetAttribute("label", CharsetHelper.GetSafeName(flow.trigger.Name), "");
            if (!String.IsNullOrEmpty(flow.trigger.Connector))
            {
                string connectorIcon = ConnectorHelper.getConnectorIconFile(flow.trigger.Connector);

                if (!String.IsNullOrEmpty(connectorIcon))
                {
                    string connectorIcon32Path = folderPath + Path.GetFileNameWithoutExtension(connectorIcon) + "32.png";
                    ImageHelper.ConvertImageTo32(connectorIcon, connectorIcon32Path);
                    //path to image is absolute here, as GraphViz wasn't able to render it properly if relative. Will be replaced in the SVG just before the PNG gets generated
                    trigger.SetAttributeHtml("label", "<table border=\"0\"><tr><td><img src=\"" + connectorIcon32Path + "\" /></td><td>" + CharsetHelper.GetSafeName(flow.trigger.Name) + "</td></tr></table>");
                }
            }
            addNodesToGraph(rootGraph, rootAction, trigger, null, null, showSubactions, true);
            rootGraph.ComputeLayout();

            //Generate image files
            string filename = flow.ID + (showSubactions ? " detailed" : "");
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

            return folderPath + filename + ".png";
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
        void addNodesToGraph(RootGraph rootGraph, ActionNode node, Node previousNeighbourNode, SubGraph parentCluster, SubGraph currentCluster, bool showSubactions, bool isTopLevel)
        {
            SubGraph cluster = null;
            SubGraph yesCluster = null;
            SubGraph noCluster = null;
            string edgeName;
            //adding the current item as a new node
            Node currentNode = rootGraph.GetOrAddNode(CharsetHelper.GetSafeName(node.Name));
            currentNode.SafeSetAttribute("shape", "record", "");
            currentNode.SafeSetAttribute("color", "blue", "");
            currentNode.SafeSetAttribute("style", "filled", "");
            currentNode.SafeSetAttribute("fillcolor", "white", "");
            //setting the label here again with the name is required to make the connector icon code below work properly
            currentNode.SafeSetAttribute("label", CharsetHelper.GetSafeName(node.Name), "");
            if (!String.IsNullOrEmpty(node.Connection))
            {
                string connectorIcon = ConnectorHelper.getConnectorIconFile(node.Connection);

                if (!String.IsNullOrEmpty(connectorIcon))
                {
                    string connectorIcon32Path = folderPath + Path.GetFileNameWithoutExtension(connectorIcon) + "32.png";
                    ImageHelper.ConvertImageTo32(connectorIcon, connectorIcon32Path);
                    //path to image is absolute here, as GraphViz wasn't able to render it properly if relative. Will be replaced in the SVG just before the PNG gets generated
                    currentNode.SetAttributeHtml("label", "<table border=\"0\"><tr><td><img src=\"" + connectorIcon32Path + "\" /></td><td>" + CharsetHelper.GetSafeName(node.Name) + "</td></tr></table>");
                }
            }

            //might not have subactions for yes/no? to check!
            //if there are actions inside (likely only for Control), let's create a container
            //How about a SCOPE? TODO
            if (node.Subactions.Count > 0)
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

                //TODO: what if there are no subactions but only elseactions?
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
                            addNodesToGraph(rootGraph, subaction, currentNode, parentCluster, yesCluster, showSubactions, false);

                        }
                        else
                        {
                            addNodesToGraph(rootGraph, subaction, currentNode, null, cluster, showSubactions, false);
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
                        addNodesToGraph(rootGraph, subaction, currentNode, parentCluster, noCluster, showSubactions, false);
                    }
                }
            }
            else if (currentCluster != null)
            {
                currentCluster.AddExisting(currentNode);
                if (!nodeClusterRelationship.ContainsKey(currentNode))
                    nodeClusterRelationship.Add(currentNode, currentCluster);

            }

            // now that the node is created, we connect it accordingly
            edgeName = "Edge " + previousNeighbourNode.GetName() + "-" + currentNode.GetName();
            ActionNode preceedingNeighbour = flow.actions.getPrecedingNeighbour(node);
            //Only connect if there is no previous node or if the 'parent' is the previous node), and if there's no existing edge (to avoid duplicates)
            if ((preceedingNeighbour == null || previousNeighbourNode.GetName().Equals(preceedingNeighbour.Name)) && !edges.Contains(edgeName))
            {
                Edge edgeAB;
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
                            if (curClusterParent == prevCluster)
                            {
                                edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, curCluster, false, edgeName);
                            }
                            else
                            {
                                if (curClusterParent == null || previousNeighbourNode.GetName().Equals(preceedingNeighbour?.Name))
                                {
                                    if (prevClusterParent == curCluster)
                                    {
                                        edgeAB = rootGraph.GetOrAddEdge(prevCluster, currentNode, false, edgeName);
                                    }
                                    else
                                    {
                                        edgeAB = rootGraph.GetOrAddEdge(prevCluster, curCluster, false, edgeName);
                                    }
                                }
                                else
                                {
                                    SubGraph curClusterParentParent = clusterRelationship[curClusterParent];
                                    while (curClusterParentParent != prevCluster)
                                    {
                                        curClusterParent = curClusterParentParent;
                                        curClusterParentParent = clusterRelationship[curClusterParent];
                                    }
                                    edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, curClusterParent, false, edgeName);
                                }
                            }
                        }
                        else
                        {
                            edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                        }
                    }
                    else
                    {
                        edgeAB = rootGraph.GetOrAddEdge(prevCluster, currentNode, false, edgeName);
                    }
                }
                else
                {
                    if (curCluster != null)
                    {
                        edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, curCluster, false, edgeName);
                    }
                    else
                    {
                        edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                    }
                }
                edges.Add(edgeName);
            }

            foreach (ActionNode neighbour in node.Neighbours)
            {
                addNodesToGraph(rootGraph, neighbour, currentNode, isTopLevel ? null : cluster, currentCluster, showSubactions, isTopLevel);
            }
        }

    }
}