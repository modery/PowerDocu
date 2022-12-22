using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{
    class FlowWordDocBuilder : WordDocBuilder
    {
        private readonly FlowDocumentationContent content;

        public FlowWordDocBuilder(FlowDocumentationContent contentDocumentation, string template)
        {
            this.content = contentDocumentation;
            Directory.CreateDirectory(content.folderPath);
            string filename = InitializeWordDocument(content.folderPath + content.filename, template);
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
            NotificationHelper.SendNotification("Created Word documentation for " + content.metadata.Name);
        }

        private void addConnectionReferenceInfo()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(content.connectionReferences.header));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text(content.connectionReferences.infoText));
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in content.connectionReferences.connectionTable)
            {
                string connectorUniqueName = kvp.Key;
                ConnectorIcon connectorIcon = ConnectorHelper.getConnectorIcon(connectorUniqueName);
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text((connectorIcon != null) ? connectorIcon.Name : connectorUniqueName));
                ApplyStyleToParagraph("Heading3", para);

                var rel = mainPart.AddHyperlinkRelationship(new Uri("https://docs.microsoft.com/connectors/" + connectorUniqueName), true);
                Table table = CreateTable();
                table.Append(CreateRow(new Text("Connector"),
                                appendConnectorNameAndIcon(connectorUniqueName, mainPart, rel)));
                foreach (KeyValuePair<string, string> kvp2 in kvp.Value)
                {
                    table.Append(CreateRow(new Text(kvp2.Key), new Text(kvp2.Value)));
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
            run.AppendChild(new Text(content.metadata.header));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            foreach (KeyValuePair<string, string> kvp in content.metadata.metadataTable)
            {
                table.Append(CreateRow(new Text(kvp.Key), new Text(kvp.Value)));
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addVariablesInfo()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(content.variables.header));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            para = body.AppendChild(new Paragraph());

            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in content.variables.variablesTable)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(kvp.Key));
                string bookmarkID = (new Random()).Next(100000, 999999).ToString();
                BookmarkStart start = new BookmarkStart() { Name = CreateMD5Hash(kvp.Key), Id = bookmarkID };
                BookmarkEnd end = new BookmarkEnd() { Id = bookmarkID };
                para.Append(start, end);
                ApplyStyleToParagraph("Heading3", para);

                Table table = CreateTable();
                foreach (KeyValuePair<string, string> kvp2 in kvp.Value)
                {
                    if (kvp2.Key.Equals("Initial Value"))
                    {
                        Table initialValTable = CreateTable(BorderValues.Single, 0.8);
                        content.variables.initialValTable.TryGetValue(kvp.Key, out Dictionary<string, string> initialValues);
                        foreach (KeyValuePair<string, string> initialVal in initialValues)
                        {
                            initialValTable.Append(CreateRow(new Text(initialVal.Key), new Text(initialVal.Value)));
                        }
                        table.Append(CreateRow(new Text("Initial Value"), initialValTable));
                    }
                    else
                    {
                        table.Append(CreateRow(new Text(kvp2.Key), new Text(kvp2.Value)));
                    }
                }
                content.variables.referencesTable.TryGetValue(kvp.Key, out List<ActionNode> references);
                if (references?.Count > 0)
                {
                    var tr = new TableRow();
                    var tc = CreateTableCell();
                    run = new Run(new Text("Used in these Actions"));
                    RunProperties runProperties = new RunProperties();
                    runProperties.Append(new Bold());
                    run.RunProperties = runProperties;
                    tc.Append(new Paragraph(run));
                    tr.Append(tc);
                    tc = CreateTableCell();
                    foreach (ActionNode action in references.OrderBy(o => o.Name).ToList())
                    {
                        tc.Append(new Paragraph(new Hyperlink(new Run(new Text(action.Name)))
                        {
                            Anchor = CreateMD5Hash(action.Name),
                            DocLocation = ""
                        }));
                    }
                    tr.Append(tc);
                    table.Append(tr);
                }
                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
        }

        private void addTriggerInfo()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(content.trigger.header));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            foreach (KeyValuePair<string, string> kvp in content.trigger.triggerTable)
            {
                if (kvp.Value == "mergedrow")
                {
                    table.Append(CreateMergedRow(new Text(kvp.Key), 2, cellHeaderBackground));
                }
                else
                {
                    table.Append(CreateRow(new Text(kvp.Key), new Text(kvp.Value)));
                }
            }

            if (content.trigger.inputs?.Count > 0)
            {
                AddExpressionDetails(table, content.trigger.inputs, "Inputs Details");
            }
            if (content.trigger.triggerProperties?.Count > 0)
            {
                AddExpressionDetails(table, content.trigger.triggerProperties, "Other Trigger Properties");
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
            using (FileStream stream = new FileStream(content.folderPath + "flow.png", FileMode.Open))
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
            using (FileStream stream = new FileStream(content.folderPath + "flow.svg", FileMode.Open))
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
            List<ActionNode> actionNodesList = content.actions.actionNodesList;
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
                    actionDetailsTable.Append(CreateRow(new Text("Description / Note"), new Text(action.Description)));
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
                            //special handling
                            if (action.Type.Equals("ParseJson") && actionInput.expressionOperator.Equals("schema"))
                            {
                                operandsCell.Append(new Paragraph(CreateRunWithLinebreaks(actionInput.expressionOperands[0]?.ToString())));
                                    //new Run(new Text(actionInput.expressionOperands[0]?.ToString()))));
                            }
                            else
                            {
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
                                        Anchor = CreateMD5Hash(subaction.Name),
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
                                    Anchor = CreateMD5Hash(subaction.Name),
                                    DocLocation = ""
                                }));
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
                                Anchor = CreateMD5Hash(elseaction.Name),
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
                            Anchor = CreateMD5Hash(nextAction.Name),
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
            using (FileStream stream = new FileStream(content.folderPath + content.details.imageFileName + ".png", FileMode.Open))
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
            using (FileStream stream = new FileStream(content.folderPath + content.details.imageFileName + ".svg", FileMode.Open))
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