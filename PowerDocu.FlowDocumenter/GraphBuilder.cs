using System;
using System.IO;
using System.Collections.Generic;
using Rubjerg.Graphviz;
using Svg;

namespace PowerDocu.FlowDocumenter
{
    class GraphBuilder
    {

        FlowEntity flow;
        private string folderPath;
        //using this list to store the names of edges. Some edges were created twice when creating an edge to a cluster (as it creates a dummy node when pointing to a cluster, which happens multiple times instead of getting reused)
        private List<string> edges;

        public GraphBuilder(FlowEntity flowToUse, string path)
        {
            flow = flowToUse;
            folderPath = @"\Flow Documentation - " + flow.Name + @"\";
            folderPath = folderPath.Replace(":", "-");
            folderPath = path + folderPath;
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
            RootGraph rootGraph = RootGraph.CreateNew(flow.Name, GraphType.Directed);
            Graph.IntroduceAttribute(rootGraph, "compound", "true");
            ActionNode rootAction = flow.actions.getRootNode();

            Node trigger = rootGraph.GetOrAddNode(flow.trigger.Name);
            trigger.SafeSetAttribute("color", "green", "");

            addNodesToGraph(rootGraph, rootAction, trigger, null, null, showSubactions, true);
            rootGraph.ComputeLayout();

            //Generate image files
            string filename = flow.ID + (showSubactions ? " detailed" : "");
            // can't save directly as PNG (limitation of the .Net Wrapper), saving as SVG is the only option 
            rootGraph.ToSvgFile(folderPath + filename + ".svg");
            //rootGraph.ToDotFile(folderPath + filename+".dot");
            // converting SVG to PNG
            var svgDocument = SvgDocument.Open(folderPath + filename + ".svg");
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
          */
        void addNodesToGraph(RootGraph rootGraph, ActionNode node, Node previousNeighbourNode, SubGraph parentCluster, SubGraph currentCluster, bool showSubactions, bool isTopLevel)
        {
            SubGraph cluster = null;
            SubGraph yesCluster = null;
            SubGraph noCluster = null;
            string edgeName;
            //adding the current item as a new node
            Node currentNode = rootGraph.GetOrAddNode(node.Name);
            currentNode.SafeSetAttribute("shape", "record", "");
            currentNode.SafeSetAttribute("color", "blue", "");
            currentNode.SafeSetAttribute("style", "filled", "");
            currentNode.SafeSetAttribute("fillcolor", "white", "");
            //TODO image attribute not working yet
            //currentNode.SafeSetAttribute("image", "office365outlook.png", "office365outlook.png");
            //currentNode.SafeSetAttribute("imagescale", "height", "");


            //might not have subactions for yes/no? to check!
            //if there are actions inside (likely only for Control), let's create a container
            //How about a SCOPE? TODO
            if (node.hasSubactions())
            {
                // if there are subactions, then we need to create a cluster for the current node and its child nodes
                // if we are inside a cluster, then we create the new cluster as a child
                if (currentCluster != null)
                {
                    // Console.WriteLine("1New cluster_" + node.Name +" under "+currentCluster.GetName());
                    cluster = currentCluster.GetOrAddSubgraph("cluster_" + node.Name);
                }
                else
                {
                    if (parentCluster == null)
                    {
                        //create the new cluster inside the rootgraph itself
                        cluster = rootGraph.GetOrAddSubgraph("cluster_" + node.Name);
                        //Console.WriteLine("2New cluster_" + node.Name +" under "+rootGraph.GetName());
                    }
                    else
                    {
                        //create the new cluster inside the parent cluster
                        cluster = parentCluster.GetOrAddSubgraph("cluster_" + node.Name);
                        //Console.WriteLine("3New cluster_" + node.Name +" under "+parentCluster.GetName());
                    }
                }
                cluster.SafeSetAttribute("style", "filled", "");
                cluster.SafeSetAttribute("fillcolor", "grey90", "");
                cluster.AddExisting(currentNode);
                //TODO: what if there are no subactions but only elseactions?
                if (showSubactions)
                {
                    foreach (ActionNode subaction in node.Subactions)
                    {
                        //Console.WriteLine("Let's look at subaction "+subaction.Name);
                        //connect the subactions to the current node inside the cluster
                        if (node.Elseactions.Count > 0)
                        {
                            yesCluster = cluster.GetOrAddSubgraph("cluster_yes" + node.Name);
                            //Console.WriteLine("4New cluster_yes" + node.Name +" under "+cluster.GetName());
                            yesCluster.SafeSetAttribute("style", "filled", "");
                            yesCluster.SafeSetAttribute("fillcolor", "lightgreen", "");
                            addNodesToGraph(rootGraph, subaction, currentNode, parentCluster, yesCluster, showSubactions, false);

                        }
                        else
                        {
                            // Console.WriteLine("preparing to add  subaction to current cluster" + cluster.GetName());
                            addNodesToGraph(rootGraph, subaction, currentNode, null, cluster, showSubactions, false);
                        }
                    }
                    foreach (ActionNode subaction in node.Elseactions)
                    {
                        //connect the subactions to the current node inside the cluster                         
                        noCluster = cluster.GetOrAddSubgraph("cluster_no" + node.Name);
                        // Console.WriteLine("5New cluster_no" + node.Name +" under "+cluster.GetName());
                        noCluster.SafeSetAttribute("style", "filled", "");
                        noCluster.SafeSetAttribute("fillcolor", "lightcoral", "");
                        addNodesToGraph(rootGraph, subaction, currentNode, parentCluster, noCluster, showSubactions, false);
                    }
                }
            }
            else if (currentCluster != null)
            {
                currentCluster.AddExisting(currentNode);

            }

            // now that the node is created, we connect it accordingly
            edgeName = "NN " + previousNeighbourNode.GetName() + "-" + currentNode.GetName();
            ActionNode preceedingNeighbour = flow.actions.getPrecedingNeighbour(node);
            //Only connect if there is no previous node or if the 'parent' is the previous node)
            if (preceedingNeighbour == null || previousNeighbourNode.GetName().Equals(preceedingNeighbour.Name))
            {
                Edge edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
            }


            //TODO: working with clusters doesn't deliver expected results yet. Code below kept for the moment, either to be recreated or updated in a later version
            /*
            if (parentCluster == null)
            {
                if (cluster == null)
                {
                    if (yesCluster == null && noCluster == null)
                    {
                        //connect the nodes directly!
                        if (currentCluster == null)
                        {
                            edgeName = "NN "+previousNeighbourNode.GetName()+"-"+currentNode.GetName();
                            if(!edges.Contains(edgeName)) {
                                Edge edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                                edges.Add(edgeName);
                            }
                        }
                        else
                        {
                            //TODO: not always right
                            edgeName = "NC1 "+previousNeighbourNode.GetName()+"-"+currentCluster.GetName();
                            if(!edges.Contains(edgeName)) {
                                Edge edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentCluster, false, edgeName);
                                edges.Add(edgeName);
                            }
                        }
                    }
                    else
                    {
                        edgeName = "NN "+previousNeighbourNode.GetName()+"-"+currentNode.GetName();
                        if(!edges.Contains(edgeName)) {
                            Edge edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, (yesCluster != null) ? yesCluster : noCluster, false, edgeName);
                            edges.Add(edgeName);
                        }
                    }
                }
                else
                {
                    //connect the parent to the cluster
                    //NOTE: this doesn't quite work properly yet? if it's inside
                    edgeName = "NC2 "+previousNeighbourNode.GetName()+"-"+cluster.GetName();
                    if(!edges.Contains(edgeName)) {
                        ActionNode preceedingNeighbour = flow.actions.getPrecedingNeighbour(node);
                        //Only connect if there is no previous node or if the 'parent' is the previous node
                        if(preceedingNeighbour == null || preceedingNeighbour.Name == previousNeighbourNode.GetName()) {
                            Edge edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, cluster, false, edgeName);
                            edges.Add(edgeName);
                        }
                    }
                }
            }
            else
            {
                if (cluster == null)
                {
                    //connect the parent cluster to the node
                    edgeName = "CN "+parentCluster.GetName()+"-"+currentNode.GetName();
                    if(!edges.Contains(edgeName)) {
                        Edge edgeAB = rootGraph.GetOrAddEdge(parentCluster, currentNode, false, edgeName);
                        edges.Add(edgeName);
                    }
                }
                else
                {
                    //Connect the 2 clusters!
                    edgeName = "CC "+parentCluster.GetName()+"-"+cluster.GetName();
                    if(!edges.Contains(edgeName)) {
                        Edge edgeAB = rootGraph.GetOrAddEdge(parentCluster, cluster, false, edgeName);
                        edges.Add(edgeName);
                    }
                    
                }
               
            }*/
            foreach (ActionNode neighbour in node.Neighbours)
            {
                addNodesToGraph(rootGraph, neighbour, currentNode, isTopLevel ? null : cluster, currentCluster, showSubactions, isTopLevel);
            }
        }

    }
}