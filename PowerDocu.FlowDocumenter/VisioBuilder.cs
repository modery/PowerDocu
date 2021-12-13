using System;
using System.IO;
using System.Collections.Generic;
using PowerDocu.Common;
using Rubjerg.Graphviz;
using Svg;
using System.Xml;
using System.Xml.Linq;
using System.IO.Packaging;
using System.Linq;
using System.Text;

namespace PowerDocu.FlowDocumenter
{
    public class VisioBuilder
    {
        private Dictionary<Node, SubGraph> nodeClusterRelationship;
        private Dictionary<SubGraph, SubGraph> clusterRelationship;
        private List<string> nodesInGraph;
        private readonly FlowEntity flow;
        private readonly string folderPath;
        //using this list to store the names of edges. Some edges were created twice when creating an edge to a cluster (as it creates a dummy node when pointing to a cluster, which happens multiple times instead of getting reused)
        private List<string> edges;

        public VisioBuilder(FlowEntity flowToUse, string path)
        {
            flow = flowToUse;
            folderPath = path + CharsetHelper.GetSafeName(@"\FlowDoc - " + flow.Name + @"\");
            Directory.CreateDirectory(folderPath);
            PrepareVisioDocument(folderPath + "visiotest.vsdx");
            using (Package visioPackage = OpenPackage("visiotest.vsdx", folderPath))
            {
                PackagePart document = GetPackagePart(visioPackage, "http://schemas.microsoft.com/visio/2010/relationships/document");

                PackagePart pages = GetPackagePart(visioPackage, document, "http://schemas.microsoft.com/visio/2010/relationships/pages");
                PackagePart page = GetPackagePart(visioPackage, pages, "http://schemas.microsoft.com/visio/2010/relationships/page");
                XDocument doc = GetXMLFromPart(page);

                CreateNewPage(visioPackage, pages, doc,
                new Uri(Uri.EscapeUriString($"/visio/pages/flowPage1.xml"), UriKind.Relative), page.ContentType, "http://schemas.microsoft.com/visio/2010/relationships/page", CharsetHelper.GetSafeName(flow.Name));
            }
        }


        private void PrepareVisioDocument(string filename)
        {
            File.Copy(AssemblyHelper.AssemblyDirectory + @"\Resources\VisioTemplate.vsdx", filename, true);
        }

        private void CreateNewPage(Package filePackage, PackagePart parent,
                    XDocument partXML, Uri packageLocation, string contentType,
                    string relationship, string name)
        {
            // Need to check first to see whether the part exists already.

            if (!filePackage.PartExists(packageLocation))
            {
                // Create a new blank package part at the specified URI 
                // of the specified content type.
                PackagePart newPackagePart = filePackage.CreatePart(packageLocation,
                    contentType);
                // Create a stream from the package part and save the 
                // XML document to the package part.
                using (Stream partStream = newPackagePart.GetStream(FileMode.Create,
                    FileAccess.ReadWrite))
                {
                    partXML.Save(partStream);
                }
            }
            // Add a relationship from the file package to this
            // package part. You can also create relationships
            // between an existing package part and a new part.
            var rel = parent.CreateRelationship(packageLocation,
                TargetMode.Internal,
                relationship);

            var pagesXml = GetXMLFromPart(parent);
            var templatePage = pagesXml.Root.Elements().First(ele => ele.Attribute("NameU").Value == "Page-1");
            var pageXML = new XElement(templatePage);
            pageXML.SetAttributeValue("Name", name);
            pageXML.SetAttributeValue("NameU", name);
            pageXML.SetAttributeValue("IsCustomNameU", "1");
            pageXML.SetAttributeValue("IsCustomName", "1");
            pageXML.SetAttributeValue("ID", templatePage.Parent.Elements().Count());
            XNamespace ns = pagesXml.Root.GetNamespaceOfPrefix("r");
            pageXML.Elements().First(el => el.Name.LocalName == "Rel").SetAttributeValue(ns + "id", rel.Id);
            templatePage.Parent.Add(new XElement(pageXML));
            SaveXDocumentToPart(parent, pagesXml);

        }


        /** content from https://docs.microsoft.com/en-us/office/client-developer/visio/how-to-manipulate-the-visio-file-format-programmatically#create-a-vsdx-file-and-a-new-visual-studio-solution **/
        private static Package OpenPackage(string fileName,
            string folder)
        {
            Package visioPackage = null;
            // Get a reference to the location 
            // where the Visio file is stored.

            DirectoryInfo dirInfo = new DirectoryInfo(folder);
            // Get the Visio file from the location.
            FileInfo[] fileInfos = dirInfo.GetFiles(fileName);
            if (fileInfos.Length > 0)
            {
                FileInfo fileInfo = fileInfos[0];
                string filePathName = fileInfo.FullName;
                // Open the Visio file as a package with
                // read/write file access.
                visioPackage = Package.Open(
                    filePathName,
                    FileMode.Open,
                    FileAccess.ReadWrite);
            }
            // Return the Visio file as a package.
            return visioPackage;
        }

        private static PackagePart GetPackagePart(Package filePackage, string relationship)
        {
            // Use the namespace that describes the relationship 
            // to get the relationship.
            PackageRelationship packageRel =
                filePackage.GetRelationshipsByType(relationship).FirstOrDefault();
            PackagePart part = null;
            // If the Visio file package contains this type of relationship with 
            // one of its parts, return that part.
            if (packageRel != null)
            {
                // Clean up the URI using a helper class and then get the part.
                Uri docUri = PackUriHelper.ResolvePartUri(
                    new Uri("/", UriKind.Relative), packageRel.TargetUri);
                part = filePackage.GetPart(docUri);
            }
            return part;
        }

        private static PackagePart GetPackagePart(Package filePackage, PackagePart sourcePart, string relationship)
        {
            // This gets only the first PackagePart that shares the relationship
            // with the PackagePart passed in as an argument. You can modify the code
            // here to return a different PackageRelationship from the collection.
            PackageRelationship packageRel =
                sourcePart.GetRelationshipsByType(relationship).FirstOrDefault();
            PackagePart relatedPart = null;
            if (packageRel != null)
            {
                // Use the PackUriHelper class to determine the URI of PackagePart
                // that has the specified relationship to the PackagePart passed in
                // as an argument.
                Uri partUri = PackUriHelper.ResolvePartUri(
                    sourcePart.Uri, packageRel.TargetUri);
                relatedPart = filePackage.GetPart(partUri);
            }
            return relatedPart;
        }

        private static XDocument GetXMLFromPart(PackagePart packagePart)
        {
            XDocument partXml = null;
            // Open the packagePart as a stream and then 
            // open the stream in an XDocument object.
            Stream partStream = packagePart.GetStream();
            partXml = XDocument.Load(partStream);
            return partXml;
        }

        private static IEnumerable<XElement> GetXElementsByName(
            XDocument packagePart, string elementType)
        {
            // Construct a LINQ query that selects elements by their element type.
            IEnumerable<XElement> elements =
                from element in packagePart.Descendants()
                where element.Name.LocalName == elementType
                select element;
            // Return the selected elements to the calling code.
            return elements.DefaultIfEmpty(null);
        }

        private static XElement GetXElementByAttribute(IEnumerable<XElement> elements,
            string attributeName, string attributeValue)
        {
            // Construct a LINQ query that selects elements from a group
            // of elements by the value of a specific attribute.
            IEnumerable<XElement> selectedElements =
                from el in elements
                where el.Attribute(attributeName).Value == attributeValue
                select el;
            // If there aren't any elements of the specified type
            // with the specified attribute value in the document,
            // return null to the calling code.
            return selectedElements.DefaultIfEmpty(null).FirstOrDefault();
        }


        private static void SaveXDocumentToPart(PackagePart packagePart,
            XDocument partXML)
        {

            // Create a new XmlWriterSettings object to 
            // define the characteristics for the XmlWriter
            XmlWriterSettings partWriterSettings = new XmlWriterSettings();
            partWriterSettings.Encoding = Encoding.UTF8;
            // Create a new XmlWriter and then write the XML
            // back to the document part.
            XmlWriter partWriter = XmlWriter.Create(packagePart.GetStream(),
                partWriterSettings);
            partXML.WriteTo(partWriter);
            // Flush and close the XmlWriter.
            partWriter.Flush();
            partWriter.Close();
        }

        private static void RecalcDocument(Package filePackage)
        {
            // Get the Custom File Properties part from the package and
            // and then extract the XML from it.
            PackagePart customPart = GetPackagePart(filePackage,
                "http://schemas.openxmlformats.org/officeDocument/2006/relationships/" +
                "custom-properties");
            XDocument customPartXML = GetXMLFromPart(customPart);
            // Check to see whether document recalculation has already been 
            // set for this document. If it hasn't, use the integer
            // value returned by CheckForRecalc as the property ID.
            int pidValue = CheckForRecalc(customPartXML);
            if (pidValue > -1)
            {
                XElement customPartRoot = customPartXML.Elements().ElementAt(0);
                // Two XML namespaces are needed to add XML data to this 
                // document. Here, we're using the GetNamespaceOfPrefix and 
                // GetDefaultNamespace methods to get the namespaces that 
                // we need. You can specify the exact strings for the 
                // namespaces, but that is not recommended.
                XNamespace customVTypesNS = customPartRoot.GetNamespaceOfPrefix("vt");
                XNamespace customPropsSchemaNS = customPartRoot.GetDefaultNamespace();
                // Construct the XML for the new property in the XDocument.Add method.
                // This ensures that the XNamespace objects will resolve properly, 
                // apply the correct prefix, and will not default to an empty namespace.
                customPartRoot.Add(
                    new XElement(customPropsSchemaNS + "property",
                        new XAttribute("pid", pidValue.ToString()),
                        new XAttribute("name", "RecalcDocument"),
                        new XAttribute("fmtid",
                            "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}"),
                        new XElement(customVTypesNS + "bool", "true")
                    ));
            }
            // Save the Custom Properties package part back to the package.
            SaveXDocumentToPart(customPart, customPartXML);
        }


        private static int CheckForRecalc(XDocument customPropsXDoc)
        {

            // Set the inital pidValue to -1, which is not an allowed value.
            // The calling code tests to see whether the pidValue is 
            // greater than -1.
            int pidValue = -1;
            // Get all of the property elements from the document. 
            IEnumerable<XElement> props = GetXElementsByName(
                customPropsXDoc, "property");
            // Get the RecalcDocument property from the document if it exists already.
            XElement recalcProp = GetXElementByAttribute(props,
                "name", "RecalcDocument");
            // If there is already a RecalcDocument instruction in the 
            // Custom File Properties part, then we don't need to add another one. 
            // Otherwise, we need to create a unique pid value.
            if (recalcProp != null)
            {
                return pidValue;
            }
            else
            {
                // Get all of the pid values of the property elements and then
                // convert the IEnumerable object into an array.
                IEnumerable<string> propIDs =
                    from prop in props
                    where prop.Name.LocalName == "property"
                    select prop.Attribute("pid").Value;
                string[] propIDArray = propIDs.ToArray();
                // Increment this id value until a unique value is found.
                // This starts at 2, because 0 and 1 are not valid pid values.
                int id = 2;
                while (pidValue == -1)
                {
                    if (propIDArray.Contains(id.ToString()))
                    {
                        id++;
                    }
                    else
                    {
                        pidValue = id;
                    }
                }
            }
            return pidValue;
        }


        private static void CreateNewPackagePart(Package filePackage,
            XDocument partXML, Uri packageLocation, string contentType,
            string relationship)
        {
            // Need to check first to see whether the part exists already.
            if (!filePackage.PartExists(packageLocation))
            {
                // Create a new blank package part at the specified URI 
                // of the specified content type.
                PackagePart newPackagePart = filePackage.CreatePart(packageLocation,
                    contentType);
                // Create a stream from the package part and save the 
                // XML document to the package part.
                using (Stream partStream = newPackagePart.GetStream(FileMode.Create,
                    FileAccess.ReadWrite))
                {
                    partXML.Save(partStream);
                }
            }
            // Add a relationship from the file package to this
            // package part. You can also create relationships
            // between an existing package part and a new part.
            filePackage.CreateRelationship(packageLocation,
                TargetMode.Internal,
                relationship);
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
            trigger.SetAttribute("label", CharsetHelper.GetSafeName(flow.trigger.Name));
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
            addNodesToGraph(rootGraph, rootAction, null, null, showSubactions, true);
            nodesInGraph = new List<string>();
            addEdgesToGraph(rootGraph, rootAction, trigger, null, null, showSubactions, true);
            rootGraph.ComputeLayout();

            NotificationHelper.SendNotification("Created Graph " + folderPath + generateImageFiles(rootGraph, showSubactions) + ".png");
        }

        private string generateImageFiles(RootGraph rootGraph, bool showSubactions)
        {
            //Generate image files
            string filename = "flow" + (showSubactions ? " detailed" : "");
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
                currentNode.SetAttribute("label", CharsetHelper.GetSafeName(node.Name));
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
                                addNodesToGraph(rootGraph, subaction, parentCluster, yesCluster, showSubactions, false);
                            }
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
                if (node.Subactions.Count > 0)
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
                    CreateEdge(currentNode, previousNeighbourNode, precedingNeighbour, edgeName, rootGraph);
                }
                else if (precedingNeighbour != null && !edges.Contains(edgeName))
                {
                    edgeName = "Edge " + precedingNeighbour.Name + "-" + currentNode.GetName();
                    Node precNode = rootGraph.GetNode(precedingNeighbour.Name);
                    if (precNode != null)
                    {
                        CreateEdge(currentNode, precNode, precedingNeighbour, edgeName, rootGraph);
                    }
                }

                foreach (ActionNode neighbour in node.Neighbours)
                {
                    addEdgesToGraph(rootGraph, neighbour, currentNode, isTopLevel ? null : cluster, currentCluster, showSubactions, isTopLevel);
                }
            }
        }

        private void CreateEdge(Node currentNode, Node previousNeighbourNode, ActionNode precedingNeighbour, string edgeName, RootGraph rootGraph)
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
                        if (curClusterParent == prevCluster)
                        {
                            edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                            edgeAB.SetLogicalHead(curCluster);
                        }
                        else
                        {
                            if (curClusterParent == null || previousNeighbourNode.GetName().Equals(precedingNeighbour?.Name))
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
                                SubGraph curClusterParentParent = clusterRelationship[curClusterParent];
                                while (curClusterParentParent != prevCluster)
                                {
                                    curClusterParent = curClusterParentParent;
                                    curClusterParentParent = clusterRelationship[curClusterParent];
                                }
                                edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                                edgeAB.SetLogicalHead(curClusterParent);
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
                if (curCluster != null)
                {
                    //update 5
                    edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                    edgeAB.SetLogicalHead(curCluster);
                }
                else
                {
                    edgeAB = rootGraph.GetOrAddEdge(previousNeighbourNode, currentNode, edgeName);
                }
            }
            edges.Add(edgeName);
        }
    }
}