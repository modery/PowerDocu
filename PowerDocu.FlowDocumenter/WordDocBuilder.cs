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
    class WordDocBuilder
    {
        // Define Constants for Page Width and Page Margin
        //using A4 by default
        private const int PageWidth = 11906;
        private const int PageHeight = 16838;
        private const int PageMarginLeft = 1000;
        private const int PageMarginRight = 1000;
        private const int PageMarginTop = 1000;
        private const int PageMarginBottom = 1000;
        private const double DocumentSizePerPixel = 15;
        private const double EmuPerPixel = 9525;
        private readonly int maxImageWidth = PageWidth - PageMarginRight - PageMarginLeft;
        private readonly int maxImageHeight = PageHeight - PageMarginTop - PageMarginBottom;
        private const string cellHeaderBackground = "E5E5FF";
        private readonly FlowEntity flow;
        private readonly Random random = new Random();

        private readonly string folderPath;

        public WordDocBuilder(FlowEntity flowToDocument, string path, string template)
        {
            this.flow = flowToDocument;
            folderPath = path + CharsetHelper.GetSafeName(@"\FlowDoc - " + flow.Name + @"\");
            Directory.CreateDirectory(folderPath);
            string filename = CharsetHelper.GetSafeName(flow.Name) + ((flow.ID != null) ? ("(" + flow.ID + ")") : "") + ".docx";
            filename = filename.Replace(":", "-");
            filename = folderPath + filename;
            PrepareWordDocument(filename, template);
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true))
            {
                MainDocumentPart mainPart = wordDocument.MainDocumentPart;
                Body body = mainPart.Document.Body;
                AddNameSpaces(mainPart.Document);
                if (!String.IsNullOrEmpty(template))
                {
                    // Set Page Size and Page Margin so that we can place the image as desired.
                    var sectionProperties = body.GetFirstChild<SectionProperties>();
                    // pageSize contains Width and Height properties
                    var pageSize = sectionProperties.GetFirstChild<PageSize>();
                    // this contains information about surrounding margins
                    var pageMargin = sectionProperties.GetFirstChild<PageMargin>();
                    maxImageWidth = (int)(pageSize.Width.Value - pageMargin.Right.Value - pageMargin.Left.Value);
                    maxImageHeight = (int)(pageSize.Height.Value - pageMargin.Top.Value - pageMargin.Bottom.Value);
                }
                //add all the relevant content
                addFlowMetadata(body);
                addFlowOverview(body, wordDocument);
                addConnectionReferenceInfo(mainPart, body);
                addVariablesInfo(body);
                addTriggerInfo(body);
                addActionInfo(body, mainPart);
                addFlowDetails(body, wordDocument);
            }
            NotificationHelper.SendNotification("Created Word documentation for " + flow.Name);
        }

        private void PrepareWordDocument(string filename, string template)
        {
            //create a new document if no template is provided
            if (String.IsNullOrEmpty(template))
            {
                using (WordprocessingDocument wordDocument =
                WordprocessingDocument.Create(filename, WordprocessingDocumentType.Document))
                {
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                    // Create the document structure and add some text.
                    mainPart.Document = new Document();
                    AddStylesPartToPackage(wordDocument);
                    Body body = mainPart.Document.AppendChild(new Body());

                    // Set Page Size and Page Margin so that we can place the image as desired.
                    // Available Width = PageWidth - PageMarginLeft - PageMarginRight (= 17000 - 1000 - 1000 = 15000 for default values)
                    var sectionProperties = new SectionProperties();
                    sectionProperties.AppendChild(new PageSize { Width = PageWidth, Height = PageHeight });
                    sectionProperties.AppendChild(new PageMargin { Left = PageMarginLeft, Bottom = PageMarginBottom, Top = PageMarginTop, Right = PageMarginRight });
                    body.AppendChild(sectionProperties);
                }
            }
            else
            {
                //Create a copy of the Word template that will be used to add the documentation
                File.Copy(template, filename, true);
            }
        }

        private void addConnectionReferenceInfo(MainDocumentPart mainPart, Body body)
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

        private void addFlowMetadata(Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Flow Documentation"));
            ApplyStyleToParagraph("Heading1", para);

            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Flow Name"), new Text(flow.Name)));
            if (!String.IsNullOrEmpty(flow.ID))
            {
                table.Append(CreateRow(new Text("Flow ID"), new Text(flow.ID)));
            }
            table.Append(CreateRow(new Text("Documentation generated at"), new Text(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString())));
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addVariablesInfo(Body body)
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
                        List<ActionNode> referencedInNodes = new List<ActionNode>();
                        referencedInNodes.Add(node);
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

        private TableRow CreateHeaderRow(params OpenXmlElement[] cellValues)
        {
            TableRow tr = new TableRow();
            foreach (var cellValue in cellValues)
            {
                TableCell tc = CreateTableCell();
                var run = new Run(cellValue);
                RunProperties runProperties = new RunProperties();
                runProperties.Append(new Bold());
                run.RunProperties = runProperties;
                tc.Append(new Paragraph(run));
                var shading = new Shading()
                {
                    Color = "auto",
                    Fill = cellHeaderBackground,
                    Val = ShadingPatternValues.Clear
                };

                tc.TableCellProperties.Append(shading);
                tr.Append(tc);
            }
            return tr;
        }

        private TableCell CreateTableCell()
        {
            TableCell tc = new TableCell();
            TableCellProperties tableCellProperties = new TableCellProperties();
            TableCellWidth tableCellWidth = new TableCellWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
            tableCellProperties.Append(tableCellWidth);
            tc.Append(tableCellProperties);
            return tc;
        }

        private void addTriggerInfo(Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Trigger"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
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
                table.Append(CreateMergedRow(new Text("Inputs Details"), 2, cellHeaderBackground));
                foreach (Expression input in flow.trigger.Inputs)
                {
                    TableCell operandsCell = CreateTableCell();

                    Table operandsTable = CreateTable(BorderValues.Single, 0.8);
                    if (input.expressionOperands.Count > 1)
                    {
                        foreach (object actionInputOperand in input.expressionOperands)
                        {
                            if (actionInputOperand.GetType() == typeof(Expression))
                            {
                                AddExpressionTable((Expression)actionInputOperand, operandsTable);
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
                        operandsCell.Append(new Paragraph(new Run(new Text(input.expressionOperands[0]?.ToString()))));
                    }
                    table.Append(CreateRow(new Text(input.expressionOperator), operandsCell));
                }
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addFlowOverview(Body body, WordprocessingDocument wordDoc)
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

        private void addActionInfo(Body body, MainDocumentPart mainPart)
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
                                operandsCell.Append(new Paragraph(new Run(new Text(actionInput.expressionOperands[0]?.ToString()))));
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
                        run = new Run(new Text("Subactions"));
                        RunProperties runProperties = new RunProperties();
                        runProperties.Append(new Bold());
                        run.RunProperties = runProperties;
                        tc.Append(new Paragraph(run));
                        tr.Append(tc);
                        tc = CreateTableCell();
                        foreach (ActionNode subaction in action.Subactions)
                        {
                            //adding a link to the subaction's section in the Word doc
                            tc.Append(new Paragraph(new Hyperlink(new Run(new Text(subaction.Name)))
                            {
                                Anchor = subaction.Name,
                                DocLocation = ""
                            }));
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
                body.Append(actionDetailsTable);

                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private Drawing InsertImage(string relationshipId, int imageWidth, int imageHeight)
        {
            //image too wide for a page?
            if (maxImageWidth / DocumentSizePerPixel < imageWidth)
            {
                imageHeight = (int)(imageHeight * (maxImageWidth / DocumentSizePerPixel / imageWidth));
                imageWidth = (int)Math.Round(maxImageWidth / DocumentSizePerPixel);
            }
            //image too high for a page?
            if (maxImageHeight / DocumentSizePerPixel < imageHeight)
            {
                imageWidth = (int)(imageWidth * (maxImageHeight / DocumentSizePerPixel / imageHeight));
                imageHeight = (int)Math.Round(maxImageHeight / DocumentSizePerPixel);
            }
            Int64Value width = imageWidth * 9525;
            Int64Value height = imageHeight * 9525;
            string randomHex = GetRandomHexNumber(8);
            int randomId = (new Random()).Next(100000, 999999);
            return new Drawing(
                    new DW.Inline(
                        new DW.Extent() { Cx = width, Cy = height },
                        new DW.EffectExtent()
                        {
                            LeftEdge = 0L,
                            TopEdge = 0L,
                            RightEdge = 0L,
                            BottomEdge = 0L
                        },
                        new DW.DocProperties()
                        {
                            Id = (uint)randomId,
                            Name = "Picture " + randomId
                        },
                        new DW.NonVisualGraphicFrameDrawingProperties(
                            new A.GraphicFrameLocks() { NoChangeAspect = true }),
                        new A.Graphic(
                            new A.GraphicData(
                                new PIC.Picture(
                                    new PIC.NonVisualPictureProperties(
                                        new PIC.NonVisualDrawingProperties()
                                        {
                                            Id = (uint)randomId + 1,
                                            Name = "New Bitmap Image" + (randomId + 1) + ".png"
                                        },
                                        new PIC.NonVisualPictureDrawingProperties()),
                                    new PIC.BlipFill(
                                        new A.Blip(
                                            new A.BlipExtensionList(
                                                new A.BlipExtension()
                                                {
                                                    Uri =
                                                        "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                })
                                        )
                                        {
                                            Embed = relationshipId,
                                            CompressionState =
                                            A.BlipCompressionValues.Print
                                        },
                                        new A.Stretch(
                                            new A.FillRectangle())),
                                    new PIC.ShapeProperties(
                                        new A.Transform2D(
                                            new A.Offset() { X = 0L, Y = 0L },
                                            new A.Extents() { Cx = width, Cy = height }),
                                        new A.PresetGeometry(
                                            new A.AdjustValueList()
                                        )
                                        { Preset = A.ShapeTypeValues.Rectangle }))
                            )
                            { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                    )
                    {
                        DistanceFromTop = (UInt32Value)0U,
                        DistanceFromBottom = (UInt32Value)0U,
                        DistanceFromLeft = (UInt32Value)0U,
                        DistanceFromRight = (UInt32Value)0U,
                        AnchorId = randomHex,
                        EditId = randomHex
                    });
        }

        private void addFlowDetails(Body body, WordprocessingDocument wordDoc)
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

        private Drawing InsertSvgImage(string svgRelationshipId, string imgRelationshipId, int imageWidth, int imageHeight)
        {
            //image too wide for a page?
            if (maxImageWidth / DocumentSizePerPixel < imageWidth)
            {
                imageHeight = (int)(imageHeight * (maxImageWidth / DocumentSizePerPixel / imageWidth));
                imageWidth = (int)Math.Round(maxImageWidth / DocumentSizePerPixel);
            }
            //image too high for a page?
            if (maxImageHeight / DocumentSizePerPixel < imageHeight)
            {
                imageWidth = (int)(imageWidth * (maxImageHeight / DocumentSizePerPixel / imageHeight));
                imageHeight = (int)Math.Round(maxImageHeight / DocumentSizePerPixel);
            }
            Int64Value width = imageWidth * 9525;
            Int64Value height = imageHeight * 9525;
            string randomHex = GetRandomHexNumber(8);
            int randomId = (new Random()).Next(100000, 999999);

            A.BlipExtension svgelement = new A.BlipExtension
            {
                Uri = "{96DAC541-7B7A-43D3-8B79-37D633B846F1}",
                InnerXml = "<asvg:svgBlip xmlns:asvg=\"http://schemas.microsoft.com/office/drawing/2016/SVG/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" r:embed=\"" + svgRelationshipId + "\"/>"
            };

            A14.UseLocalDpi useLocalDpi1 = new A14.UseLocalDpi() { Val = false };
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");
            A.BlipExtension blipExtension1 = new A.BlipExtension
            {
                Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
            };
            blipExtension1.Append(useLocalDpi1);

            var element =
                new Drawing(
                    new DW.Inline(
                        new DW.Extent() { Cx = width, Cy = height },
                        new DW.EffectExtent()
                        {
                            LeftEdge = 0L,
                            TopEdge = 0L,
                            RightEdge = 0L,
                            BottomEdge = 0L
                        },
                        new DW.DocProperties()
                        {
                            Id = (uint)randomId,
                            Name = "Picture " + randomId
                        },
                        new DW.NonVisualGraphicFrameDrawingProperties(
                            new A.GraphicFrameLocks() { NoChangeAspect = true }),
                        new A.Graphic(
                            new A.GraphicData(
                                new PIC.Picture(
                                    new PIC.NonVisualPictureProperties(
                                        new PIC.NonVisualDrawingProperties()
                                        {
                                            Id = (uint)randomId + 1,
                                            Name = "New Bitmap Image" + (randomId + 1) + ".png"
                                        },
                                        new PIC.NonVisualPictureDrawingProperties()),
                                    new PIC.BlipFill(
                                        new A.Blip(
                                            new A.BlipExtensionList(
                                                    blipExtension1,
                                                    svgelement
                                                )
                                        )
                                        {
                                            Embed = imgRelationshipId,
                                            CompressionState =
                                            A.BlipCompressionValues.Print
                                        },
                                        new A.Stretch(
                                            new A.FillRectangle())),
                                    new PIC.ShapeProperties(
                                        new A.Transform2D(
                                            new A.Offset() { X = 0L, Y = 0L },
                                            new A.Extents() { Cx = width, Cy = height }),
                                        new A.PresetGeometry(
                                            new A.AdjustValueList()
                                        )
                                        { Preset = A.ShapeTypeValues.Rectangle }))
                            )
                            { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                    )
                    {
                        DistanceFromTop = (UInt32Value)0U,
                        DistanceFromBottom = (UInt32Value)0U,
                        DistanceFromLeft = (UInt32Value)0U,
                        DistanceFromRight = (UInt32Value)0U,
                        AnchorId = randomHex,
                        EditId = randomHex
                    });

            return element;
        }

        /* used to add the styles (mainly heading1, heading2, etc.) from styles.xml to the document */
        private StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            var part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            var root = new Styles();
            root.Save(part);
            FileStream stylesTemplate = new FileStream(AssemblyHelper.AssemblyDirectory + @"\Resources\styles.xml", FileMode.Open, FileAccess.Read);
            part.FeedData(stylesTemplate);
            return part;
        }

        /* helper class to add the givens style to the provided paragraph */
        private void ApplyStyleToParagraph(string styleid, Paragraph p)
        {
            // If the paragraph has no ParagraphProperties object, create one.
            if (!p.Elements<ParagraphProperties>().Any())
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            // Get the paragraph properties element of the paragraph.
            ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();

            // Set the style of the paragraph.
            pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleid };
        }

        private Table CreateTable(BorderValues borderType = BorderValues.Single, double factor = 1)
        {
            Table table = new Table();
            TableProperties props = new TableProperties(
                new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct },
                new TableBorders(
                    SetDefaultTableBorderStyle(new TopBorder(), borderType),
                    SetDefaultTableBorderStyle(new LeftBorder(), borderType),
                    SetDefaultTableBorderStyle(new BottomBorder(), borderType),
                    SetDefaultTableBorderStyle(new RightBorder(), borderType),
                    SetDefaultTableBorderStyle(new InsideHorizontalBorder(), borderType),
                    SetDefaultTableBorderStyle(new InsideVerticalBorder(), borderType)
                )
                );
            table.AppendChild<TableProperties>(props);
            table.AppendChild(new TableGrid(new GridColumn() { Width = Math.Round(1822 * factor).ToString() }, new GridColumn() { Width = Math.Round(8300 * factor).ToString() }));
            return table;
        }

        private BorderType SetDefaultTableBorderStyle(BorderType border, BorderValues borderType)
        {
            border.Val = new EnumValue<BorderValues>(borderType);
            border.Size = 12;
            border.Color = "A6A6A6";
            border.Space = 0;
            return border;
        }

        private TableRow CreateRow(params OpenXmlElement[] cellValues)
        {
            TableRow tr = new TableRow();
            bool isFirstCell = true;
            foreach (var cellValue in cellValues)
            {
                if (cellValue.GetType() == typeof(TableCell))
                {
                    tr.Append(cellValue);
                }
                else
                {
                    TableCell tc = CreateTableCell();
                    RunProperties runProperties = new RunProperties();
                    if (isFirstCell && cellValues.Length > 1)
                    {
                        runProperties.Append(new Bold());
                        isFirstCell = false;
                        //if it's the first cell and the content is of type Drawing (an icon!), then we use a reduced width
                        string cellWidth = (cellValue.GetType() == typeof(Drawing)) ? "100" : "900";
                        tc.TableCellProperties.TableCellWidth = new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = cellWidth };
                    }
                    //if we are inserting a table, we do so directly, but also need to add an empty paragraph right after it
                    if (cellValue.GetType() == typeof(Table))
                    {
                        tc.Append(cellValue, new Paragraph());
                    }
                    //hyperlinks get added within a paragraph
                    else if (cellValue.GetType() == typeof(Hyperlink))
                    {
                        tc.Append(new Paragraph(cellValue));
                    }
                    //paragraphs get added directly
                    else if (cellValue.GetType() == typeof(Paragraph))
                    {
                        tc.Append(cellValue);
                    }
                    else
                    {
                        tc.Append(new Paragraph(new Run(cellValue)
                        {
                            RunProperties = runProperties
                        }));
                    }
                    tr.Append(tc);
                }
            }
            return tr;
        }

        private Table AddExpressionTable(Expression expression, Table table = null, double factor = 1)
        {
            if (table == null)
                table = CreateTable(BorderValues.Single, factor);
            if (expression?.expressionOperator != null)
            {
                var tr = new TableRow();
                var tc = CreateTableCell();
                var shading = new Shading()
                {
                    Color = "auto",
                    Fill = "E5FFE5",
                    Val = ShadingPatternValues.Clear
                };

                tc.TableCellProperties.Append(shading);
                tc.TableCellProperties.TableCellWidth = new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = "700" };
                tc.Append(new Paragraph(new Run(new Text(expression.expressionOperator))));
                tr.Append(tc);
                tc = CreateTableCell();
                if (expression.expressionOperands.Count > 1)
                {
                    Table operandsTable = CreateTable(BorderValues.Single, factor * factor);
                    foreach (var expressionOperand in expression.expressionOperands)
                    {
                        if (expressionOperand.GetType().Equals(typeof(string)))
                        {
                            operandsTable.Append(CreateRow(new Text((string)expressionOperand)));
                        }
                        else if (expressionOperand.GetType().Equals(typeof(Expression)))
                        {
                            AddExpressionTable((Expression)expressionOperand, operandsTable, factor * factor);
                        }
                        else
                        {
                            operandsTable.Append(CreateRow(new Text("")));
                        }
                    }
                    tc.Append(operandsTable, new Paragraph());
                }
                else if (expression.expressionOperands.Count == 0)
                {
                    tc.Append(new Paragraph(new Run(new Text(""))));
                }
                else
                {
                    object expo = expression.expressionOperands[0];
                    if (expo.GetType().Equals(typeof(string)))
                    {
                        tc.Append(new Paragraph(new Run(new Text((expression.expressionOperands.Count == 0) ? "" : expression.expressionOperands[0]?.ToString()))));
                    }
                    else
                    {
                        tc.Append(AddExpressionTable((Expression)expo, null, factor * factor), new Paragraph());
                    }
                }
                tr.Append(tc);
                table.Append(tr);
            }
            return table;
        }

        private TableRow CreateMergedRow(OpenXmlElement cellValue, int colSpan, string colour)
        {
            TableRow tr = new TableRow();
            var tc = CreateTableCell();
            RunProperties run1Properties = new RunProperties();
            run1Properties.Append(new Bold());
            var run = new Run(cellValue)
            {
                RunProperties = run1Properties
            };
            tc.Append(new Paragraph(run));
            tc.TableCellProperties.GridSpan = new GridSpan() { Val = colSpan };
            var shading = new Shading()
            {
                Color = "auto",
                Fill = colour,
                Val = ShadingPatternValues.Clear
            };

            tc.TableCellProperties.Append(shading);
            tr.Append(tc);

            return tr;
        }

        private void AddNameSpaces(Document document)
        {
            document.AddNamespaceDeclaration("wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
            document.AddNamespaceDeclaration("cx", "http://schemas.microsoft.com/office/drawing/2014/chartex");
            document.AddNamespaceDeclaration("cx1", "http://schemas.microsoft.com/office/drawing/2015/9/8/chartex");
            document.AddNamespaceDeclaration("cx2", "http://schemas.microsoft.com/office/drawing/2015/10/21/chartex");
            document.AddNamespaceDeclaration("cx3", "http://schemas.microsoft.com/office/drawing/2016/5/9/chartex");
            document.AddNamespaceDeclaration("cx4", "http://schemas.microsoft.com/office/drawing/2016/5/10/chartex");
            document.AddNamespaceDeclaration("cx5", "http://schemas.microsoft.com/office/drawing/2016/5/11/chartex");
            document.AddNamespaceDeclaration("cx6", "http://schemas.microsoft.com/office/drawing/2016/5/12/chartex");
            document.AddNamespaceDeclaration("cx7", "http://schemas.microsoft.com/office/drawing/2016/5/13/chartex");
            document.AddNamespaceDeclaration("cx8", "http://schemas.microsoft.com/office/drawing/2016/5/14/chartex");
            document.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            document.AddNamespaceDeclaration("aink", "http://schemas.microsoft.com/office/drawing/2016/ink");
            document.AddNamespaceDeclaration("am3d", "http://schemas.microsoft.com/office/drawing/2017/model3d");
            document.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            document.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            document.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            document.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            document.AddNamespaceDeclaration("wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
            document.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            document.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            //document.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            document.AddNamespaceDeclaration("w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            document.AddNamespaceDeclaration("w15", "http://schemas.microsoft.com/office/word/2012/wordml");
            document.AddNamespaceDeclaration("w16cex", "http://schemas.microsoft.com/office/word/2018/wordml/cex");
            document.AddNamespaceDeclaration("w16cid", "http://schemas.microsoft.com/office/word/2016/wordml/cid");
            document.AddNamespaceDeclaration("w16", "http://schemas.microsoft.com/office/word/2018/wordml");
            document.AddNamespaceDeclaration("w16sdtdh", "http://schemas.microsoft.com/office/word/2020/wordml/sdtdatahash");
            document.AddNamespaceDeclaration("w16se", "http://schemas.microsoft.com/office/word/2015/wordml/symex");
            document.AddNamespaceDeclaration("wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
            document.AddNamespaceDeclaration("wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
            document.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
            document.AddNamespaceDeclaration("wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");
        }

        public string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }


        public string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}