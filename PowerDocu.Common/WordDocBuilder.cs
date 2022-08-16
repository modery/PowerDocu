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

namespace PowerDocu.Common
{
    public abstract class WordDocBuilder
    {
        // Define Constants for Page Width and Page Margin
        //using A4 by default
        protected const int PageWidth = 11906;
        protected const int PageHeight = 16838;
        protected const int PageMarginLeft = 1000;
        protected const int PageMarginRight = 1000;
        protected const int PageMarginTop = 1000;
        protected const int PageMarginBottom = 1000;
        protected const double DocumentSizePerPixel = 15;
        protected const double EmuPerPixel = 9525;
        protected int maxImageWidth = PageWidth - PageMarginRight - PageMarginLeft;
        protected int maxImageHeight = PageHeight - PageMarginTop - PageMarginBottom;
        protected const string cellHeaderBackground = "E5E5FF";
        protected readonly Random random = new Random();
        protected string folderPath;
        protected MainDocumentPart mainPart;
        protected Body body;
        protected Dictionary<string, string> SVGImages;

        public HashSet<int> UsedRandomNumbers = new HashSet<int>();

        protected void InitializeWordDocument(string filename, string template)
        {
            SVGImages = new Dictionary<string, string>();
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

        protected TableRow CreateHeaderRow(params OpenXmlElement[] cellValues)
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

        protected TableCell CreateTableCell()
        {
            TableCell tc = new TableCell();
            TableCellProperties tableCellProperties = new TableCellProperties();
            TableCellWidth tableCellWidth = new TableCellWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
            tableCellProperties.Append(tableCellWidth);
            tc.Append(tableCellProperties);
            return tc;
        }

        protected void AddExpressionDetails(Table table, List<Expression> inputs, string header)
        {
            table.Append(CreateMergedRow(new Text(header), 2, cellHeaderBackground));
            foreach (Expression input in inputs)
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
                    if (input.expressionOperands.Count > 0)
                    {
                        if (input.expressionOperands[0]?.GetType() == typeof(Expression))
                        {
                            operandsCell.Append(AddExpressionTable((Expression)input.expressionOperands[0]), new Paragraph());
                        }
                        else
                        {
                            operandsCell.Append(new Paragraph(new Run(new Text(input.expressionOperands[0]?.ToString()))));
                        }
                    }
                    else
                    {
                        operandsCell.Append(new Paragraph(new Run(new Text(""))));
                    }
                }
                table.Append(CreateRow(new Text(input.expressionOperator), operandsCell));
            }
        }

        protected Drawing InsertImage(string relationshipId, int imageWidth, int imageHeight)
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
            int randomId = GetRandomNumber();
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

        protected Drawing InsertSvgImage(string svgRelationshipId, string imgRelationshipId, int imageWidth, int imageHeight)
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
            int randomId = GetRandomNumber();

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

        protected Drawing InsertSvgImage(MainDocumentPart mainDocumentPart, string svgcontent, int imageWidth, int imageHeight)
        {
            string partId;
            string contenthash = CreateMD5Hash(svgcontent);
            if (SVGImages.ContainsKey(contenthash))
            {
                partId = SVGImages[contenthash];
            }
            else
            {
                ImagePart svgPart = mainDocumentPart.AddNewPart<ImagePart>("image/svg+xml", "rId" + (GetRandomNumber()));
                using (MemoryStream stream = new MemoryStream())
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(svgcontent);
                    writer.Flush();
                    stream.Position = 0;
                    svgPart.FeedData(stream);
                }
                partId = mainDocumentPart.GetIdOfPart(svgPart);
                SVGImages.Add(contenthash, partId);
            }
            return InsertSvgImage(partId, "", imageWidth, imageHeight);
        }

        private int GetRandomNumber()
        {
            int r;
            do
            {
                r = random.Next(100000, 999999);
            } while (UsedRandomNumbers.Contains(r));
            UsedRandomNumbers.Add(r);
            return r;
        }

        /* used to add the styles (mainly heading1, heading2, etc.) from styles.xml to the document */
        protected StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            var part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            var root = new Styles();
            root.Save(part);
            FileStream stylesTemplate = new FileStream(AssemblyHelper.GetExecutablePath() + @"\Resources\styles.xml", FileMode.Open, FileAccess.Read);
            part.FeedData(stylesTemplate);
            return part;
        }

        /* helper class to add the givens style to the provided paragraph */
        protected void ApplyStyleToParagraph(string styleid, Paragraph p)
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

        protected Table CreateTable(BorderValues borderType = BorderValues.Single, double factor = 1)
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

        protected BorderType SetDefaultTableBorderStyle(BorderType border, BorderValues borderType)
        {
            border.Val = new EnumValue<BorderValues>(borderType);
            border.Size = 12;
            border.Color = "A6A6A6";
            border.Space = 0;
            return border;
        }

        protected TableRow CreateRow(params OpenXmlElement[] cellValues)
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

        protected Table AddExpressionTable(Expression expression, Table table = null, double factor = 1, bool showShading = true, bool firstColumnBold = false)
        {
            if (table == null)
                table = CreateTable(BorderValues.Single, factor);
            if (expression?.expressionOperator != null)
            {
                var tr = new TableRow();
                var tc = CreateTableCell();
                if (showShading)
                {
                    var shading = new Shading()
                    {
                        Color = "auto",
                        Fill = "E5FFE5",
                        Val = ShadingPatternValues.Clear
                    };

                    tc.TableCellProperties.Append(shading);
                }
                tc.TableCellProperties.TableCellWidth = new TableCellWidth { Type = TableWidthUnitValues.Pct, Width = "700" };
                Paragraph para = new Paragraph();
                Run run = para.AppendChild(new Run(new Text(expression.expressionOperator)));
                if (firstColumnBold)
                {
                    RunProperties runProperties = new RunProperties();
                    runProperties.Append(new Bold());
                    run.RunProperties = runProperties;
                }
                tc.Append(para);
                tr.Append(tc);
                tc = CreateTableCell();
                if (expression.expressionOperands.Count > 1)
                {
                    Table operandsTable = CreateTable(BorderValues.Single, factor * factor);
                    foreach (var expressionOperand in expression.expressionOperands.OrderBy(o => o.ToString()).ToList())
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

        protected TableRow CreateMergedRow(OpenXmlElement cellValue, int colSpan, string colour)
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

        protected void PrepareDocument(bool templateUsed)
        {
            AddSettingsToMainDocumentPart();
            AddNameSpaces(mainPart.Document);
            if (templateUsed)
            {
                // Set Page Size and Page Margin so that we can place the image as desired.
                var sectionProperties = mainPart.Document.Body.GetFirstChild<SectionProperties>();
                // pageSize contains Width and Height properties
                var pageSize = sectionProperties.GetFirstChild<PageSize>();
                // this contains information about surrounding margins
                var pageMargin = sectionProperties.GetFirstChild<PageMargin>();
                maxImageWidth = (int)(pageSize.Width.Value - pageMargin.Right.Value - pageMargin.Left.Value);
                maxImageHeight = (int)(pageSize.Height.Value - pageMargin.Top.Value - pageMargin.Bottom.Value);
            }
        }

        private void AddSettingsToMainDocumentPart()
        {
            DocumentSettingsPart settingsPart = mainPart.AddNewPart<DocumentSettingsPart>();
            settingsPart.Settings = new Settings(
               new Compatibility(
                   //Compatibility for Office 2013 onwards, which helps with processing larger documents
                   new CompatibilitySetting()
                   {
                       Name = CompatSettingNameValues.CompatibilityMode,
                       Val = new StringValue("15"),
                       Uri = new StringValue("http://schemas.microsoft.com/office/word")
                   }
               )
            );
            settingsPart.Settings.Save();
        }

        protected void AddNameSpaces(Document document)
        {
            SafeAddNameSpaceDeclaration(document, "wpc", "http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas");
            SafeAddNameSpaceDeclaration(document, "cx", "http://schemas.microsoft.com/office/drawing/2014/chartex");
            SafeAddNameSpaceDeclaration(document, "cx1", "http://schemas.microsoft.com/office/drawing/2015/9/8/chartex");
            SafeAddNameSpaceDeclaration(document, "cx2", "http://schemas.microsoft.com/office/drawing/2015/10/21/chartex");
            SafeAddNameSpaceDeclaration(document, "cx3", "http://schemas.microsoft.com/office/drawing/2016/5/9/chartex");
            SafeAddNameSpaceDeclaration(document, "cx4", "http://schemas.microsoft.com/office/drawing/2016/5/10/chartex");
            SafeAddNameSpaceDeclaration(document, "cx5", "http://schemas.microsoft.com/office/drawing/2016/5/11/chartex");
            SafeAddNameSpaceDeclaration(document, "cx6", "http://schemas.microsoft.com/office/drawing/2016/5/12/chartex");
            SafeAddNameSpaceDeclaration(document, "cx7", "http://schemas.microsoft.com/office/drawing/2016/5/13/chartex");
            SafeAddNameSpaceDeclaration(document, "cx8", "http://schemas.microsoft.com/office/drawing/2016/5/14/chartex");
            SafeAddNameSpaceDeclaration(document, "mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            SafeAddNameSpaceDeclaration(document, "aink", "http://schemas.microsoft.com/office/drawing/2016/ink");
            SafeAddNameSpaceDeclaration(document, "am3d", "http://schemas.microsoft.com/office/drawing/2017/model3d");
            SafeAddNameSpaceDeclaration(document, "o", "urn:schemas-microsoft-com:office:office");
            SafeAddNameSpaceDeclaration(document, "r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            SafeAddNameSpaceDeclaration(document, "m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            SafeAddNameSpaceDeclaration(document, "v", "urn:schemas-microsoft-com:vml");
            SafeAddNameSpaceDeclaration(document, "wp14", "http://schemas.microsoft.com/office/word/2010/wordprocessingDrawing");
            SafeAddNameSpaceDeclaration(document, "wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            SafeAddNameSpaceDeclaration(document, "w10", "urn:schemas-microsoft-com:office:word");
            SafeAddNameSpaceDeclaration(document, "w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            SafeAddNameSpaceDeclaration(document, "w14", "http://schemas.microsoft.com/office/word/2010/wordml");
            SafeAddNameSpaceDeclaration(document, "w15", "http://schemas.microsoft.com/office/word/2012/wordml");
            SafeAddNameSpaceDeclaration(document, "w16cex", "http://schemas.microsoft.com/office/word/2018/wordml/cex");
            SafeAddNameSpaceDeclaration(document, "w16cid", "http://schemas.microsoft.com/office/word/2016/wordml/cid");
            SafeAddNameSpaceDeclaration(document, "w16", "http://schemas.microsoft.com/office/word/2018/wordml");
            SafeAddNameSpaceDeclaration(document, "w16sdtdh", "http://schemas.microsoft.com/office/word/2020/wordml/sdtdatahash");
            SafeAddNameSpaceDeclaration(document, "w16se", "http://schemas.microsoft.com/office/word/2015/wordml/symex");
            SafeAddNameSpaceDeclaration(document, "wpg", "http://schemas.microsoft.com/office/word/2010/wordprocessingGroup");
            SafeAddNameSpaceDeclaration(document, "wpi", "http://schemas.microsoft.com/office/word/2010/wordprocessingInk");
            SafeAddNameSpaceDeclaration(document, "wne", "http://schemas.microsoft.com/office/word/2006/wordml");
            SafeAddNameSpaceDeclaration(document, "wps", "http://schemas.microsoft.com/office/word/2010/wordprocessingShape");
        }

        private void SafeAddNameSpaceDeclaration(Document document, string prefix, string namespacestring)
        {
            if (document.LookupNamespace(prefix) == null)
            {
                document.AddNamespaceDeclaration(prefix, namespacestring);
            }
        }

        protected string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }

        protected string CreateMD5Hash(string input)
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