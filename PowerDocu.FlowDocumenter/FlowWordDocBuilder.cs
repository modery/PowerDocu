using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using A14 = DocumentFormat.OpenXml.Office2010.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{
    class FlowWordDocBuilder : WordDocBuilder
    {
        private readonly FlowEntity flow;

        public FlowWordDocBuilder(FlowEntity flowToDocument, string path, string template)
        {
            this.flow = flowToDocument;
            folderPath = path + CharsetHelper.GetSafeName(@"\FlowDoc - " + flow.Name + @"\");
            Directory.CreateDirectory(folderPath);
            string filename = CharsetHelper.GetSafeName(flow.Name) + ((flow.ID != null) ? ("(" + flow.ID + ")") : "") + ".docx";
            filename = filename.Replace(":", "-");
            filename = folderPath + filename;
            InitializeWordDocument(filename, template);
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true))
            {
                mainPart = wordDocument.MainDocumentPart;
                body = mainPart.Document.Body;
                PrepareDocument(!String.IsNullOrEmpty(template));
                //add all the relevant content
                addFlowMetadata();
                addFlowOverview(wordDocument);
                addConnectionReferenceInfo();
                addVariablesInfo();
                addTriggerInfo();
                addActionInfo();
                addFlowDetails(wordDocument);
            }
            NotificationHelper.SendNotification("Created Word documentation for " + flow.Name);
        }

        private void addConnectionReferenceInfo()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Connections"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text($"There are a total of {flow.connectionReferences.Count} connections used in this Flow:"));
            foreach (ConnectionReference cRef in flow.connectionReferences)
            {
                string connectorUniqueName = cRef.Name;
                ConnectorIcon connectorIcon = ConnectorHelper.getConnectorIcon(connectorUniqueName);
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text((connectorIcon != null) ? connectorIcon.Name : connectorUniqueName));
                ApplyStyleToParagraph("Heading3", para);

                var rel = mainPart.AddHyperlinkRelationship(new Uri("https://docs.microsoft.com/connectors/" + connectorUniqueName), true);
                Table table = CreateTable();
                table.Append(CreateRow(new Text("Connector"),
                                appendConnectorNameAndIcon(connectorUniqueName, mainPart, rel)));
                table.Append(CreateRow(new Text("Connection Type"), new Text(cRef.Type.ToString())));
                if (cRef.Type == ConnectionType.ConnectorReference)
                {
                    if (!String.IsNullOrEmpty(cRef.ConnectionReferenceLogicalName))
                        table.Append(CreateRow(new Text("Connection Reference Name"), new Text(cRef.ConnectionReferenceLogicalName)));
                }
                if (!String.IsNullOrEmpty(cRef.ID))
                {
                    table.Append(CreateRow(new Text("ID"), new Text(cRef.ID)));
                }
                if (!String.IsNullOrEmpty(cRef.Source))
                {
                    table.Append(CreateRow(new Text("Source"), new Text(cRef.Source)));
                }
                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private OpenXmlElement appendConnectorNameAndIcon(string connectorUniqueName, MainDocumentPart mainPart, HyperlinkRelationship rel)
        {
            ConnectorIcon connectorIcon = ConnectorHelper.getConnectorIcon(connectorUniqueName);
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
            int imageWidth, imageHeight;
            if (ConnectorHelper.getConnectorIconFile(connectorUniqueName) != "")
            {
                using (FileStream stream = new FileStream(ConnectorHelper.getConnectorIconFile(connectorUniqueName), FileMode.Open))
                {
                    using (var image = Image.FromStream(stream, false, false))
                    {
                        imageWidth = image.Width;
                        imageHeight = image.Height;
                    }
                    stream.Position = 0;
                    imagePart.FeedData(stream);
                }
                Drawing icon = InsertImage(mainPart.GetIdOfPart(imagePart), 32, 32);
                Run run = new Run(new RunProperties(
                    new DocumentFormat.OpenXml.Wordprocessing.Color { Val = "0563C1", ThemeColor = ThemeColorValues.Hyperlink }),
                                            new Text((connectorIcon != null) ? connectorIcon.Name : connectorUniqueName));
                Table iconTable = CreateTable(BorderValues.None);
                iconTable.Append(CreateRow(icon, new Hyperlink(run) { History = OnOffValue.FromBoolean(true), Id = rel.Id }));
                return iconTable;
            }
            else
            {
                return new Hyperlink(new Run(new RunProperties(
                    new DocumentFormat.OpenXml.Wordprocessing.Color { Val = "0563C1", ThemeColor = ThemeColorValues.Hyperlink }),
                                             new Text((connectorIcon != null) ? connectorIcon.Name : connectorUniqueName)))
                { History = OnOffValue.FromBoolean(true), Id = rel.Id };
            }
        }

        private void addFlowMetadata()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Flow Documentation - " + flow.Name));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Flow Name"), new Text(flow.Name)));
            if (!String.IsNullOrEmpty(flow.ID))
            {
                table.Append(CreateRow(new Text("Flow ID"), new Text(flow.ID)));
            }
            table.Append(CreateRow(new Text("Documentation generated at"), new Text(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString())));
            table.Append(CreateRow(new Text("Number of Variables"), new Text("" + flow.actions.ActionNodes.Count(o => o.Type == "InitializeVariable"))));
            table.Append(CreateRow(new Text("Number of Actions"), new Text("" + flow.actions.ActionNodes.Count)));
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addVariablesInfo()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Variables"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            List<ActionNode> variablesNodes = flow.actions.ActionNodes.Where(o => o.Type == "InitializeVariable").OrderBy(o => o.Name).ToList();
            List<ActionNode> modifyVariablesNodes = flow.actions.ActionNodes.Where(o => o.Type == "SetVariable" || o.Type == "IncrementVariable").ToList();
            foreach (ActionNode node in variablesNodes)
            {
                foreach (Expression exp in node.actionInputs)
                {
                    if (exp.expressionOperator == "variables")
                    {
                        string vname = "";
                        string vtype = "";
                        OpenXmlElement vval = null;
                        foreach (Expression expO in exp.expressionOperands)
                        {
                            if (expO.expressionOperator == "name")
                            {
                                vname = expO.expressionOperands[0].ToString();
                            }
                            if (expO.expressionOperator == "type")
                            {
                                vtype = expO.expressionOperands[0].ToString();
                            }
                            if (expO.expressionOperator == "value")
                            {
                                if (expO.expressionOperands.Count == 1)
                                {
                                    vval = new Text(expO.expressionOperands[0].ToString());
                                }
                                else
                                {
                                    vval = CreateTable(BorderValues.Single, 0.8);
                                    foreach (var eop in expO.expressionOperands)
                                    {
                                        if (eop.GetType() == typeof(string))
                                        {
                                            vval = new Text(expO.expressionOperands.ToString());
                                        }
                                        else
                                        {
                                            vval.Append(CreateRow(new Text(((Expression)eop).expressionOperator), new Text(((Expression)eop).expressionOperands[0].ToString())));
                                        }
                                    }
                                }
                            }
                        }
                        List<ActionNode> referencedInNodes = new List<ActionNode>
                        {
                            node
                        };
                        foreach (ActionNode actionNode in modifyVariablesNodes)
                        {
                            foreach (Expression expO in actionNode.actionInputs)
                            {
                                if (expO.expressionOperator == "name")
                                {
                                    if (expO.expressionOperands[0].ToString().Equals(vname))
                                    {
                                        referencedInNodes.Add(actionNode);
                                    }
                                }
                            }
                        }
                        foreach (ActionNode actionNode in flow.actions.ActionNodes)
                        {
                            if (actionNode.actionExpression?.ToString().Contains($"@variables('{vname}')") == true || actionNode.Expression?.Contains($"@variables('{vname}')") == true || actionNode.actionInput?.ToString().Contains($"@variables('{vname}')") == true)
                            {
                                referencedInNodes.Add(actionNode);
                            }
                            else
                            {
                                foreach (Expression actionInput in actionNode.actionInputs)
                                {
                                    if (actionInput.ToString().Contains($"@variables('{vname}')"))
                                    {
                                        referencedInNodes.Add(actionNode);
                                    }
                                }
                            }
                        }
                        para = body.AppendChild(new Paragraph());
                        run = para.AppendChild(new Run());
                        run.AppendChild(new Text(vname));
                        string bookmarkID = (new Random()).Next(100000, 999999).ToString();
                        BookmarkStart start = new BookmarkStart() { Name = CreateMD5Hash(vname), Id = bookmarkID };
                        BookmarkEnd end = new BookmarkEnd() { Id = bookmarkID };
                        para.Append(start, end);
                        ApplyStyleToParagraph("Heading3", para);
                        Table table = CreateTable();
                        table.Append(CreateRow(new Text("Name"), new Text(vname)));
                        table.Append(CreateRow(new Text("Type"), new Text(vtype)));
                        table.Append(CreateRow(new Text("Initial Value"), (vval == null) ? new Text("") : vval));
                        var tr = new TableRow();
                        var tc = CreateTableCell();
                        run = new Run(new Text("Used in these Actions"));
                        RunProperties runProperties = new RunProperties();
                        runProperties.Append(new Bold());
                        run.RunProperties = runProperties;
                        tc.Append(new Paragraph(run));
                        tr.Append(tc);
                        tc = CreateTableCell();
                        foreach (ActionNode action in referencedInNodes.OrderBy(o => o.Name).ToList())
                        {
                            //adding a link to the action's section in the Word doc
                            tc.Append(new Paragraph(new Hyperlink(new Run(new Text(action.Name)))
                            {
                                Anchor = action.Name,
                                DocLocation = ""
                            }));
                        }
                        tr.Append(tc);
                        table.Append(tr);
                        body.Append(table);
                        body.AppendChild(new Paragraph(new Run(new Break())));
                    }
                }
            }
        }

        private void addTriggerInfo()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Trigger"));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Name"), new Text(flow.trigger.Name)));
            table.Append(CreateRow(new Text("Type"), new Text(flow.trigger.Type)));
            if (!String.IsNullOrEmpty(flow.trigger.Connector))
            {
                table.Append(CreateRow(new Text("Connector"), new Text(flow.trigger.Connector)));
            }
            if (!String.IsNullOrEmpty(flow.trigger.Description))
            {
                table.Append(CreateRow(new Text("Description"), new Text(flow.trigger.Description)));
            }
            if (flow.trigger.Recurrence.Count > 0)
            {
                table.Append(CreateMergedRow(new Text("Recurrence Details"), 2, cellHeaderBackground));
                foreach (KeyValuePair<string, string> properties in flow.trigger.Recurrence)
                {
                    table.Append(CreateRow(new Text(properties.Key),
                                                    new Text(properties.Value)));
                }
            }
            if (flow.trigger.Inputs.Count > 0)
            {
                AddExpressionDetails(table, flow.trigger.Inputs, "Inputs Details");
            }
            if (flow.trigger.TriggerProperties.Count > 0)
            {
                AddExpressionDetails(table, flow.trigger.TriggerProperties, "Other Trigger Properties");
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addFlowOverview(WordprocessingDocument wordDoc)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Flow Overview"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("The following chart shows the top level layout of the Flow. For a detailed view, please visit the section called Detailed Flow Diagram"));
            //we generated a png and a svg file. We use both: SVG as the default, PNG as the fallback for older clients that can't display SVG
            ImagePart imagePart = wordDoc.MainDocumentPart.AddImagePart(ImagePartType.Png);
            int imageWidth, imageHeight;
            using (FileStream stream = new FileStream(folderPath + "flow.png", FileMode.Open))
            {
                using (var image = Image.FromStream(stream, false, false))
                {
                    imageWidth = image.Width;
                    imageHeight = image.Height;
                }
                stream.Position = 0;
                imagePart.FeedData(stream);
            }
            ImagePart svgPart = wordDoc.MainDocumentPart.AddNewPart<ImagePart>("image/svg+xml", "rId" + (new Random()).Next(100000, 999999));
            using (FileStream stream = new FileStream(folderPath + "flow.svg", FileMode.Open))
            {
                svgPart.FeedData(stream);
            }
            body.AppendChild(new Paragraph(new Run(
                InsertSvgImage(wordDoc.MainDocumentPart.GetIdOfPart(svgPart), wordDoc.MainDocumentPart.GetIdOfPart(imagePart), imageWidth, imageHeight)
            )));
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addActionInfo()
        {
            List<ActionNode> actionNodesList = flow.actions.ActionNodes.OrderBy(o => o.Name).ToList();
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Actions"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text($"There are a total of {actionNodesList.Count} actions used in this Flow:"));
            foreach (ActionNode action in actionNodesList)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(action.Name));
                string bookmarkID = (new Random()).Next(100000, 999999).ToString();
                BookmarkStart start = new BookmarkStart() { Name = CreateMD5Hash(action.Name), Id = bookmarkID };
                BookmarkEnd end = new BookmarkEnd() { Id = bookmarkID };
                para.Append(start, end);
                ApplyStyleToParagraph("Heading3", para);

                Table actionDetailsTable = CreateTable();
                actionDetailsTable.Append(CreateRow(new Text("Name"), new Text(action.Name)));
                actionDetailsTable.Append(CreateRow(new Text("Type"), new Text(action.Type)));
                if (!String.IsNullOrEmpty(action.Description))
                {
                    actionDetailsTable.Append(CreateRow(new Text("Description"), new Text(action.Description)));
                }
                if (!String.IsNullOrEmpty(action.Connection))
                {
                    var rel = mainPart.AddHyperlinkRelationship(new Uri("https://docs.microsoft.com/connectors/" + action.Connection), true);
                    actionDetailsTable.Append(CreateRow(new Text("Connection"),
                                                appendConnectorNameAndIcon(action.Connection, mainPart, rel)));
                }

                //TODO provide more details, such as information about subaction, subsequent actions, switch actions, ...
                if (action.actionExpression != null || !String.IsNullOrEmpty(action.Expression))
                {
                    actionDetailsTable.Append(CreateRow(new Text("Expression"), (action.actionExpression != null) ? AddExpressionTable(action.actionExpression) : new Text(action.Expression)));
                }
                if (action.actionInputs.Count > 0 || !String.IsNullOrEmpty(action.Inputs))
                {
                    actionDetailsTable.Append(CreateMergedRow(new Text("Inputs"), 2, cellHeaderBackground));
                    if (action.actionInputs.Count > 0)
                    {
                        foreach (Expression actionInput in action.actionInputs)
                        {
                            TableCell operandsCell = CreateTableCell();
                            if (actionInput.expressionOperands.Count > 1)
                            {
                                Table operandsTable = CreateTable(BorderValues.Single, 0.8);
                                foreach (object actionInputOperand in actionInput.expressionOperands)
                                {
                                    if (actionInputOperand.GetType() == typeof(Expression))
                                    {
                                        AddExpressionTable((Expression)actionInputOperand, operandsTable, 0.8);
                                    }
                                    else
                                    {
                                        operandsTable.Append(CreateRow(new Text(actionInputOperand.ToString())));
                                    }
                                }
                                operandsCell.Append(operandsTable, new Paragraph());
                            }
                            else
                            {
                                if (actionInput.expressionOperands.Count == 0)
                                {
                                    operandsCell.Append(new Paragraph(new Run(new Text(""))));
                                }
                                else
                                {
                                    if (actionInput.expressionOperands[0]?.GetType() == typeof(Expression))
                                    {
                                        operandsCell.Append(AddExpressionTable((Expression)actionInput.expressionOperands[0]), new Paragraph());
                                    }
                                    else
                                    {
                                        operandsCell.Append(new Paragraph(new Run(new Text(actionInput.expressionOperands[0]?.ToString()))));
                                    }
                                }
                            }
                            actionDetailsTable.Append(CreateRow(new Text(actionInput.expressionOperator), operandsCell));
                        }
                    }
                    if (!String.IsNullOrEmpty(action.Inputs))
                    {
                        actionDetailsTable.Append(CreateRow(new Text("Value"), new Text(action.Inputs)));
                    }
                }

                if (action.Subactions.Count > 0 || action.Elseactions.Count > 0)
                {
                    if (action.Subactions.Count > 0)
                    {
                        var tr = new TableRow();
                        var tc = CreateTableCell();
                        run = new Run(new Text(action.Type == "Switch" ? "Switch Actions" : "Subactions"));
                        RunProperties runProperties = new RunProperties();
                        runProperties.Append(new Bold());
                        run.RunProperties = runProperties;
                        tc.Append(new Paragraph(run));
                        tr.Append(tc);
                        tc = CreateTableCell();
                        if (action.Type == "Switch")
                        {
                            Table switchTable = CreateTable();
                            switchTable.Append(CreateHeaderRow(new Text("Case Values"), new Text("Action")));
                            foreach (ActionNode subaction in action.Subactions)
                            {
                                if (action.switchRelationship.TryGetValue(subaction, out string switchValue))
                                {
                                    switchTable.Append(CreateRow(new Text(switchValue), new Paragraph(new Hyperlink(new Run(new Text(subaction.Name)))
                                    {
                                        Anchor = subaction.Name,
                                        DocLocation = ""
                                    })));
                                }
                            }
                            tc.Append(switchTable, new Paragraph());
                        }
                        else
                        {
                            foreach (ActionNode subaction in action.Subactions)
                            {
                                //adding a link to the subaction's section in the Word doc
                                tc.Append(new Paragraph(new Hyperlink(new Run(new Text(subaction.Name)))
                                {
                                    Anchor = subaction.Name,
                                    DocLocation = ""
                                }));
                                if (action.switchRelationship.TryGetValue(subaction, out string switchValue))
                                {
                                    tc.Append(new Paragraph(new Run(new Text("Switch: " + switchValue))));
                                }
                            }
                        }
                        tr.Append(tc);
                        actionDetailsTable.Append(tr);
                    }
                    if (action.Elseactions.Count > 0)
                    {
                        var tr = new TableRow();
                        var tc = CreateTableCell();
                        run = new Run(new Text("Elseactions"));
                        RunProperties runProperties = new RunProperties();
                        runProperties.Append(new Bold());
                        run.RunProperties = runProperties;
                        tc.Append(new Paragraph(run));
                        tr.Append(tc);
                        tc = CreateTableCell();
                        foreach (ActionNode elseaction in action.Elseactions)
                        {
                            //adding a link to the elseaction's section in the Word doc
                            tc.Append(new Paragraph(new Hyperlink(new Run(new Text(elseaction.Name)))
                            {
                                Anchor = elseaction.Name,
                                DocLocation = ""
                            }));
                        }
                        tr.Append(tc);
                        actionDetailsTable.Append(tr);
                    }
                }
                if (action.Neighbours.Count > 0)
                {
                    var tr = new TableRow();
                    var tc = CreateTableCell();
                    run = new Run(new Text("Next Action(s) Conditions"));
                    RunProperties runProperties = new RunProperties();
                    runProperties.Append(new Bold());
                    run.RunProperties = runProperties;
                    tc.Append(new Paragraph(run));
                    tr.Append(tc);
                    tc = CreateTableCell();
                    foreach (ActionNode nextAction in action.Neighbours)
                    {
                        string[] raConditions = action.nodeRunAfterConditions[nextAction];
                        //adding a link to the next action's section in the Word doc
                        tc.Append(new Paragraph(new Hyperlink(new Run(new Text(nextAction.Name)))
                        {
                            Anchor = nextAction.Name,
                            DocLocation = ""
                        }, new Run(new Text(" [" + string.Join(", ", raConditions) + "]") { Space = SpaceProcessingModeValues.Preserve })));
                    }
                    tr.Append(tc);
                    actionDetailsTable.Append(tr);
                }
                body.Append(actionDetailsTable);

                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addFlowDetails(WordprocessingDocument wordDoc)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Detailed Flow Diagram"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("The following chart shows the detailed layout of the Flow"));

            //We add both the SVG and the PNG here. Modern clients (Office 2016 onwards?) can display the SVG. Others should use PNG as fallback
            ImagePart imagePart = wordDoc.MainDocumentPart.AddImagePart(ImagePartType.Png);
            int imageWidth, imageHeight;
            using (FileStream stream = new FileStream(folderPath + "flow detailed.png", FileMode.Open))
            {
                using (var image = Image.FromStream(stream, false, false))
                {
                    imageWidth = image.Width;
                    imageHeight = image.Height;
                }
                stream.Position = 0;
                imagePart.FeedData(stream);
            }
            ImagePart svgPart = wordDoc.MainDocumentPart.AddNewPart<ImagePart>("image/svg+xml", "rId" + (new Random()).Next(100000, 999999));
            using (FileStream stream = new FileStream(folderPath + "flow detailed.svg", FileMode.Open))
            {
                svgPart.FeedData(stream);
            }
            body.AppendChild(new Paragraph(new Run(
                InsertSvgImage(wordDoc.MainDocumentPart.GetIdOfPart(svgPart), wordDoc.MainDocumentPart.GetIdOfPart(imagePart), imageWidth, imageHeight)
            )));
            body.AppendChild(new Paragraph(new Run(new Break())));
        }
    }
}