using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
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
        private FlowEntity flow;

        private string folderPath;

        public WordDocBuilder(FlowEntity flowToDocument)
        {
            this.flow = flowToDocument;
            folderPath = "Flow Documentation - " + flow.Name + @"\";
            folderPath = folderPath.Replace(":", "-");
            System.IO.Directory.CreateDirectory(folderPath);
            string filename = folderPath + flow.Name + " (" + flow.ID + ").docx";
            filename = filename.Replace(":", "-");
            Console.WriteLine(filename);
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

                //add all the relevant content
                addFlowMetadata(body);
                addFlowOverview(body, wordDocument);
                addConnectionReferenceInfo(mainPart, body);
                addTriggerInfo(body);
                addActionInfo(body, wordDocument);
                addFlowDetails(body, wordDocument);
            }
            Console.WriteLine("Created Word documentation for " + flow.Name);
        }


        private void addConnectionReferenceInfo(MainDocumentPart mainPart, Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Connection References"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("The following connection references are used in this Flow:"));
            foreach (ConnectionReference cRef in flow.connectionReferences)
            {
                string connectorUniqueName = cRef.Connector.Replace("/providers/Microsoft.PowerApps/apis/shared_", "");
                ConnectorIcon connectorIcon = ConnectorHelper.getConnectorIcon(connectorUniqueName);
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(connectorIcon.Name));
                ApplyStyleToParagraph("Heading3", para);

                Table table = CreateTable(body);
                var tr = new TableRow();
                var tc = new TableCell();
                tc.Append(new Paragraph(new Run(new Text("Connector"))));
                tr.Append(tc);
                tc = new TableCell();
                var rel = mainPart.AddHyperlinkRelationship(new Uri("https://docs.microsoft.com/connectors/" + connectorUniqueName), true);
                //todo put into its own function?
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
                    run = new Run(new RunProperties(
                        new DocumentFormat.OpenXml.Wordprocessing.Color { ThemeColor = ThemeColorValues.Hyperlink }),
                                                icon, new Break(), new Text(connectorIcon.Name));
                }
                else
                {
                    run = new Run(new RunProperties(
                        new DocumentFormat.OpenXml.Wordprocessing.Color { ThemeColor = ThemeColorValues.Hyperlink }),
                                                new Text(connectorIcon.Name));
                }

                tc.Append(new Paragraph(new Hyperlink(run)
                { History = OnOffValue.FromBoolean(true), Id = rel.Id }));


                // this can be used to link within doc
                /*
					tc.Append(new Paragraph(new Hyperlink(new Run(new Text("text")))
										{
											Anchor = "anchor"",
											DocLocation = "https://docs.microsoft.com/connectors/"+cRef.Connector.Replace("/providers/Microsoft.PowerApps/apis/shared_", "")
										}));
										*/


                tr.Append(tc);
                table.Append(tr);
                tr = new TableRow();
                tc = new TableCell();
                tc.Append(new Paragraph(new Run(new Text("ID"))));
                tr.Append(tc);
                tc = new TableCell();
                tc.Append(new Paragraph(new Run(new Text(cRef.ID))));
                tr.Append(tc);
                table.Append(tr);
                tr = new TableRow();
                tc = new TableCell();
                tc.Append(new Paragraph(new Run(new Text("Source"))));
                tr.Append(tc);
                tc = new TableCell();
                tc.Append(new Paragraph(new Run(new Text(cRef.Source))));
                tr.Append(tc);
                table.Append(tr);
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Break());
            }

            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Break());

        }

        private void addFlowMetadata(Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Flow Documentation"));
            ApplyStyleToParagraph("Heading1", para);

            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());

            AddTable(body, new string[,]
                { { "Flow Name", flow.Name },
                { "Flow ID", flow.ID }},
                false
            );
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Break());
        }

        private void addTriggerInfo(Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Trigger"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            AddTable(body, new string[,]
                { { "Name", flow.trigger.Name },
                  { "Type", flow.trigger.Type },
                  { "Description", flow.trigger.Description }
                }
            );
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Break());
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
            using (FileStream stream = new FileStream(folderPath + flow.ID + ".png", FileMode.Open))
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
            using (FileStream stream = new FileStream(folderPath + flow.ID + ".svg", FileMode.Open))
            {
                svgPart.FeedData(stream);
            }
            body.AppendChild(new Paragraph(new Run(
                InsertSvgImage(wordDoc.MainDocumentPart.GetIdOfPart(svgPart), wordDoc.MainDocumentPart.GetIdOfPart(imagePart), imageWidth, imageHeight)
            )));
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Break());
        }

        private void addActionInfo(Body body, WordprocessingDocument wordDoc)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Actions"));
            ApplyStyleToParagraph("Heading2", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("The following actions are used in this Flow:"));
            foreach (ActionNode action in flow.actions.ActionNodes)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(action.Name));
                ApplyStyleToParagraph("Heading3", para);
                Table actionDetailsTable = AddTable(body, new string[,]
                                                { {"Name", action.Name },
                                                    {"Type", action.Type }, 
													//{"Details", action.ToString() },   //TODO provide more details, such as information about subaction, subsequent actions?
													{"Expression", action.Expression }
                                                },
                                                false
                                            );
                if (action.actionInputs.Count > 0)
                {
                    var tr = new TableRow();
                    var tc = new TableCell();
                    RunProperties run1Properties = new RunProperties();
                    run1Properties.Append(new Bold());
                    run = new Run(new Text("Inputs"));
                    run.RunProperties = run1Properties;
                    tc.Append(new Paragraph(run));
                    tc.TableCellProperties = new TableCellProperties();
                    tc.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Restart };
                    tr.Append(tc);
                    var tc2 = new TableCell();
                    tc2.TableCellProperties = new TableCellProperties();
                    tc2.TableCellProperties.HorizontalMerge = new HorizontalMerge { Val = MergedCellValues.Continue };
                    tc2.Append(new Paragraph());
                    tr.Append(tc2);
                    actionDetailsTable.Append(tr);

                    foreach (ActionInput actionInput in action.actionInputs)
                    {
                        tr = new TableRow();
                        tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(actionInput.Name))));
                        tr.Append(tc);
                        tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(actionInput.Value))));
                        tr.Append(tc);
                        actionDetailsTable.Append(tr);
                    }
                }

                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Break());
            }

            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Break());
        }

        private Drawing InsertImage(string relationshipId, int imageWidth, int imageHeight)
        {
            int maxImageWidth = PageWidth - PageMarginRight - PageMarginLeft;
            int maxImageHeight = PageHeight - PageMarginTop - PageMarginBottom;
            //image too wide for a page?
            if (maxImageWidth / DocumentSizePerPixel < imageWidth)
            {
                imageHeight = (int)(imageHeight * ((maxImageWidth / DocumentSizePerPixel) / imageWidth));
                imageWidth = (int)Math.Round(maxImageWidth / DocumentSizePerPixel);
            }
            //image too high for a page?
            if (maxImageHeight / DocumentSizePerPixel < imageHeight)
            {
                imageWidth = (int)(imageWidth * ((maxImageHeight / DocumentSizePerPixel) / imageHeight));
                imageHeight = (int)Math.Round(maxImageHeight / DocumentSizePerPixel);
            }
            Int64Value width = imageWidth * 9525;
            Int64Value height = imageHeight * 9525;

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
                            Id = (UInt32Value)1U,
                            Name = "Picture 1"
                        },
                        new DW.NonVisualGraphicFrameDrawingProperties(
                            new A.GraphicFrameLocks() { NoChangeAspect = true }),
                        new A.Graphic(
                            new A.GraphicData(
                                new PIC.Picture(
                                    new PIC.NonVisualPictureProperties(
                                        new PIC.NonVisualDrawingProperties()
                                        {
                                            Id = (UInt32Value)0U,
                                            Name = "New Bitmap Image.png"
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
                        EditId = "50D07946"
                    });

            return element;
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
            using (FileStream stream = new FileStream(folderPath + flow.ID + " detailed.png", FileMode.Open))
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
            using (FileStream stream = new FileStream(folderPath + flow.ID + " detailed.svg", FileMode.Open))
            {
                svgPart.FeedData(stream);
            }
            body.AppendChild(new Paragraph(new Run(
                InsertSvgImage(wordDoc.MainDocumentPart.GetIdOfPart(svgPart), wordDoc.MainDocumentPart.GetIdOfPart(imagePart), imageWidth, imageHeight)
            )));
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Break());
        }


        private Drawing InsertSvgImage(string svgRelationshipId, string imgRelationshipId, int imageWidth, int imageHeight)
        {
            int maxImageWidth = PageWidth - PageMarginRight - PageMarginLeft;
            int maxImageHeight = PageHeight - PageMarginTop - PageMarginBottom;
            //image too wide for a page?
            if (maxImageWidth / DocumentSizePerPixel < imageWidth)
            {
                imageHeight = (int)(imageHeight * ((maxImageWidth / DocumentSizePerPixel) / imageWidth));
                imageWidth = (int)Math.Round(maxImageWidth / DocumentSizePerPixel);
            }
            //image too high for a page?
            if (maxImageHeight / DocumentSizePerPixel < imageHeight)
            {
                imageWidth = (int)(imageWidth * ((maxImageHeight / DocumentSizePerPixel) / imageHeight));
                imageHeight = (int)Math.Round(maxImageHeight / DocumentSizePerPixel);
            }
            Int64Value width = imageWidth * 9525;
            Int64Value height = imageHeight * 9525;

            A.BlipExtension svgelement = new A.BlipExtension();
            svgelement.Uri = "{96DAC541-7B7A-43D3-8B79-37D633B846F1}";
            svgelement.InnerXml = "<asvg:svgBlip xmlns:asvg=\"http://schemas.microsoft.com/office/drawing/2016/SVG/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" r:embed=\"" + svgRelationshipId + "\"/>";

            A14.UseLocalDpi useLocalDpi1 = new A14.UseLocalDpi() { Val = false };
            useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");
            A.BlipExtension blipExtension1 = new A.BlipExtension();
            blipExtension1.Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}";
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
                            Id = (UInt32Value)1U,
                            Name = "Picture 1"
                        },
                        new DW.NonVisualGraphicFrameDrawingProperties(
                            new A.GraphicFrameLocks() { NoChangeAspect = true }),
                        new A.Graphic(
                            new A.GraphicData(
                                new PIC.Picture(
                                    new PIC.NonVisualPictureProperties(
                                        new PIC.NonVisualDrawingProperties()
                                        {
                                            Id = (UInt32Value)0U,
                                            Name = "New Bitmap Image.png"
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
                        EditId = "50D07946"
                    });

            return element;
        }

        /* used to add the styles (mainly heading1, heading2, etc.) from styles.xml to the document */
        private StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            var part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            var root = new Styles();
            root.Save(part);
            FileStream stylesTemplate = new FileStream("Resources\\styles.xml", FileMode.Open, FileAccess.Read);
            part.FeedData(stylesTemplate);
            return part;
        }


        /* helper class to add the givens style to the provided paragraph */
        private void ApplyStyleToParagraph(string styleid, Paragraph p)
        {
            // If the paragraph has no ParagraphProperties object, create one.
            if (p.Elements<ParagraphProperties>().Count() == 0)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            // Get the paragraph properties element of the paragraph.
            ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();

            // Set the style of the paragraph.
            pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleid };
        }


        // Take the data from a two-dimensional array and build a table at the 
        // end of the supplied document.
        private Table AddTable(Body body, string[,] data, bool autoSize = false)
        {

            Table table = CreateTable(body);

            for (var i = 0; i <= data.GetUpperBound(0); i++)
            {
                var tr = new TableRow();
                for (var j = 0; j <= data.GetUpperBound(1); j++)
                {
                    var tc = new TableCell();
                    tc.Append(new Paragraph(new Run(new Text(data[i, j]))));

                    if (autoSize)
                    {
                        // Assume you want columns that are automatically sized.
                        tc.Append(new TableCellProperties(
                            new TableCellWidth { Type = TableWidthUnitValues.Auto }));
                    }
                    tr.Append(tc);
                }
                table.Append(tr);
            }
            return table;
        }

        private Table CreateTable(Body body)
        {
            Table table = new Table();

            TableProperties props = new TableProperties(
                new TableBorders(
                new TopBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Dotted),
                    Size = 12
                },
                new BottomBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Dotted),
                    Size = 12
                },
                new LeftBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Dotted),
                    Size = 12
                },
                new RightBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Dotted),
                    Size = 12
                },
                new InsideHorizontalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Dotted),
                    Size = 12
                },
                new InsideVerticalBorder
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Dotted),
                    Size = 12
                }));

            table.AppendChild<TableProperties>(props);

            body.Append(table);
            return table;
        }

    }
}