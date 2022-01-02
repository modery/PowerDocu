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

namespace PowerDocu.AppDocumenter
{
    class AppWordDocBuilder : WordDocBuilder
    {
        private readonly AppEntity app;
        private bool DetailedDocumentation = false;

        public AppWordDocBuilder(AppEntity appToDocument, string path, string template)
        {
            this.app = appToDocument;
            folderPath = path + CharsetHelper.GetSafeName(@"\AppDoc - " + app.Name + @"\");
            Directory.CreateDirectory(folderPath);
            do
            {
                string filename = CharsetHelper.GetSafeName(app.Name) + ((app.ID != null) ? ("(" + app.ID + ")") : "") + (DetailedDocumentation ? " detailed" : "") + ".docx";
                filename = filename.Replace(":", "-");
                filename = folderPath + filename;
                InitializeWordDocument(filename, template);
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true))
                {
                    mainPart = wordDocument.MainDocumentPart;
                    body = mainPart.Document.Body;
                    PrepareDocument(!String.IsNullOrEmpty(template));
                    addAppProperties();
                    addAppGeneralInfo();
                    addAppDataSources();
                    addAppResources();
                    addAppControlsOverview();
                    if (DetailedDocumentation) addDetailedAppControls();
                }
                DetailedDocumentation = !DetailedDocumentation;
            } while (DetailedDocumentation);
            NotificationHelper.SendNotification("Created Word documentation for " + app.Name);
        }

        private void addAppProperties()
        {
            string[] propertiesToSkip = new string[] { "AppPreviewFlagsMap", "ControlCount" };
            string[] OverviewProperties = new string[] {"AppCreationSource", "AppDescription", "AppName", "BackgroundColor", "DocumentAppType", "DocumentLayoutHeight", "DocumentLayoutOrientation",
                    "DocumentLayoutWidth", "IconColor", "IconName", "Id", "LastSavedDateTimeUTC", "LogoFileName", "Name"};
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Power App Documentation - " + app.Name));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("App Name"), new Text(app.Name)));
            Expression appLogo = app.Properties.FirstOrDefault(o => o.expressionOperator == "LogoFileName");
            if (appLogo != null)
            {
                //TODO display logo here
                //table.Append(CreateRow(new Text("App Logo"), ));
            }
            table.Append(CreateRow(new Text("Documentation generated at"), new Text(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString())));
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("App Properties"));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run()));
            table = CreateTable();
            foreach (Expression property in app.Properties.OrderBy(o => o.expressionOperator).ToList())
            {
                if (!propertiesToSkip.Contains(property.expressionOperator) && (OverviewProperties.Contains(property.expressionOperator) || DetailedDocumentation))
                {
                    AddExpressionTable(property, table, 1, false, true);
                }
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
            if (DetailedDocumentation)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text("App Preview Flags"));
                ApplyStyleToParagraph("Heading2", para);
                body.AppendChild(new Paragraph(new Run()));
                table = CreateTable();
                Expression appPreviewsFlagProperty = app.Properties.First(o => o.expressionOperator == "AppPreviewFlagsMap");
                if (appPreviewsFlagProperty != null)
                {
                    foreach (Expression flagProp in appPreviewsFlagProperty.expressionOperands)
                    {
                        AddExpressionTable(flagProp, table, 1, false, true);
                    }
                }
                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
        }

        private void addAppGeneralInfo()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Variables & Collections"));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run(new Text($"There are {app.GlobalVariables.Count} Variables and {app.Collections.Count} Collections."))));
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("Variables"));
            ApplyStyleToParagraph("Heading3", para);
            Table table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Variable Name"), new Text("Used In")));
            foreach (string var in app.GlobalVariables.OrderBy(o => o).ToHashSet())
            {
                Table varReferenceTable = CreateTable();
                List<ControlPropertyReference> references = app.VariableCollectionControlReferences[var];
                if (references != null)
                {
                    varReferenceTable.Append(CreateHeaderRow(new Text("Control"), new Text("Property")));
                    foreach (ControlPropertyReference reference in references.OrderBy(o => o.Control.Name).ThenBy(o => o.RuleProperty))
                    {
                        varReferenceTable.Append(CreateRow(new Text(reference.Control.Name), new Text(reference.RuleProperty)));
                    }
                }
                table.Append(CreateRow(new Text(var), varReferenceTable));
            }
            foreach (string var in app.ContextVariables.OrderBy(o => o).ToHashSet())
            {
                table.Append(CreateRow(new Text(var), new Text("Context Variable")));
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("Collections"));
            ApplyStyleToParagraph("Heading3", para);
            table = CreateTable();
            table.Append(CreateHeaderRow(new Text("Collection Name"), new Text("Used In")));
            foreach (string coll in app.Collections.OrderBy(o => o).ToHashSet())
            {
                Table collReferenceTable = CreateTable();
                List<ControlPropertyReference> references = app.VariableCollectionControlReferences[coll];
                if (references != null)
                {
                    collReferenceTable.Append(CreateHeaderRow(new Text("Control"), new Text("Property")));
                    foreach (ControlPropertyReference reference in references.OrderBy(o => o.Control.Name).ThenBy(o => o.RuleProperty))
                    {
                        collReferenceTable.Append(CreateRow(new Text(reference.Control.Name), new Text(reference.RuleProperty)));
                    }
                }
                table.Append(CreateRow(new Text(coll), collReferenceTable));
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addAppControlsOverview()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Controls Overview"));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run(new Text($"A total of {app.Controls.Where(o => o.Type == "screen").ToList().Count} Screens are located in the app:"))));
            foreach (ControlEntity control in app.Controls.OrderBy(o => o.Name).ToList())
            {
                if (control.Type != "appinfo")
                {
                    para = body.AppendChild(new Paragraph());
                    run = para.AppendChild(new Run());
                    run.AppendChild(new Text("Screen: " + control.Name));
                    ApplyStyleToParagraph("Heading3", para);
                    body.AppendChild(CreateControlTable(control));
                    body.AppendChild(new Paragraph(new Run(new Break())));
                }
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private Table CreateControlTable(ControlEntity control, BorderValues borderType = BorderValues.Single)
        {
            Table table = CreateTable();
            table.GetFirstChild<TableProperties>().TableBorders = new TableBorders(
                    SetDefaultTableBorderStyle(new TopBorder(), borderType),
                    SetDefaultTableBorderStyle(new LeftBorder(), borderType),
                    SetDefaultTableBorderStyle(new BottomBorder(), borderType),
                    SetDefaultTableBorderStyle(new RightBorder(), borderType),
                    SetDefaultTableBorderStyle(new InsideHorizontalBorder(), BorderValues.None),
                    SetDefaultTableBorderStyle(new InsideVerticalBorder(), BorderValues.None)
                );
            table.Append(CreateRow(InsertSvgImage(mainPart, AppControlIcons.GetControlIcon(control.Type), 32, 32), new Text(control.Name + " [" + control.Type + "]")));
            foreach (ControlEntity child in control.Children.OrderBy(o => o.Name).ToList())
            {
                table.Append(CreateRow(new Text(""), CreateControlTable(child, BorderValues.None)));
            }
            return table;
        }

        private void addDetailedAppControls()
        {
            string[] ColourProperties = new string[] { "BorderColor", "Color", "DisabledBorderColor", "DisabledColor", "DisabledFill", "DisabledSectionColor",
                    "DisabledSelectionFill", "Fill", "FocusedBorderColor", "HoverBorderColor", "HoverColor", "HoverFill", "PressedBorderColor","PressedColor",
                    "PressedFill", "SelectionColor", "SelectionFill" };

            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Detailed Controls"));
            ApplyStyleToParagraph("Heading2", para);

            List<ControlEntity> allControls = new List<ControlEntity>();
            foreach (ControlEntity control in app.Controls)
            {
                allControls.Add(control);
                allControls.AddRange(getAllChildControls(control));
            }
            body.AppendChild(new Paragraph(new Run(new Text($"A total of {allControls.Count} Controls are located in the app:"))));
            foreach (ControlEntity control in allControls.OrderBy(o => o.Name).ToList())
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(control.Name));
                ApplyStyleToParagraph("Heading3", para);
                body.AppendChild(new Paragraph(new Run()));
                Table table = CreateTable();
                Table typeTable = CreateTable(BorderValues.None);
                typeTable.Append(CreateRow(InsertSvgImage(mainPart, AppControlIcons.GetControlIcon(control.Type), 16, 16), new Text(control.Type)));
                table.Append(CreateRow(new Text("Type"), typeTable));
                string category = "";
                foreach (Rule rule in control.Rules.OrderBy(o => o.Category).ThenBy(o => o.Property).ToList())
                {
                    if (!ColourProperties.Contains(rule.Property))
                    {
                        if (rule.Category != category)
                        {
                            category = rule.Category;
                            table.Append(CreateMergedRow(new Text(category), 2, WordDocBuilder.cellHeaderBackground));
                        }
                        if (rule.InvariantScript.StartsWith("RGBA("))
                        {
                            Table colorTable = CreateTable(BorderValues.None);
                            colorTable.Append(CreateRow(new Text(rule.InvariantScript)));
                            string colour = ColourHelper.ColorToHex(ColourHelper.ParseColor(rule.InvariantScript.Substring(0, rule.InvariantScript.IndexOf(')') + 1)));
                            colorTable.Append(CreateMergedRow(new Text(""), 1, colour));
                            table.Append(CreateRow(new Text(rule.Property), colorTable));
                        }
                        else
                        {
                            table.Append(CreateRow(new Text(rule.Property), new Text(rule.InvariantScript)));
                        }
                    }
                }
                table.Append(CreateMergedRow(new Text("Color Properties"), 2, WordDocBuilder.cellHeaderBackground));
                foreach (string property in ColourProperties)
                {
                    Rule rule = control.Rules.FirstOrDefault(o => o.Property == property);
                    if (rule != null)
                    {
                        if (rule.InvariantScript.StartsWith("RGBA("))
                        {
                            Table colorTable = CreateTable(BorderValues.None);
                            colorTable.Append(CreateRow(new Text(rule.InvariantScript)));
                            string colour = ColourHelper.ColorToHex(ColourHelper.ParseColor(rule.InvariantScript.Substring(0, rule.InvariantScript.IndexOf(')') + 1)));
                            colorTable.Append(CreateMergedRow(new Text(""), 1, colour));
                            table.Append(CreateRow(new Text(rule.Property), colorTable));
                        }
                        else
                        {
                            table.Append(CreateRow(new Text(rule.Property), new Text(rule.InvariantScript)));
                        }
                    }
                }
                foreach (ControlEntity childControl in control.Children)
                {
                    /*Table childtable = CreateTable();
                    childtable.Append(CreateMergedRow(new Text("Child Controls"), 2, WordDocBuilder.cellHeaderBackground));
                    foreach (Expression expression in childControl.Properties)
                    {
                        AddExpressionTable(expression, childtable);
                    }
                    table.Append(CreateRow(new Text("childcontrol"), childtable));
                    */
                }
                /* //Other properties are likely not needed for documentation, still keeping this code in case we want to show them at some point
                table.Append(CreateMergedRow(new Text("Properties"), 2, WordDocBuilder.cellHeaderBackground));
                foreach (Expression expression in control.Properties)
                {
                    AddExpressionTable(expression, table);
                }*/

                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private List<ControlEntity> getAllChildControls(ControlEntity control)
        {
            List<ControlEntity> childControls = new List<ControlEntity>();
            foreach (ControlEntity childControl in control.Children)
            {
                childControls.Add(childControl);
                childControls.AddRange(getAllChildControls(childControl));
            }
            return childControls;
        }

        private void addAppDataSources()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("DataSources"));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run(new Text($"A total of {app.DataSources.Count} DataSources are located in the app:"))));
            foreach (DataSource datasource in app.DataSources.OrderBy(o => o.Name))
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(datasource.Name));
                ApplyStyleToParagraph("Heading3", para);
                body.AppendChild(new Paragraph(new Run()));
                Table table = CreateTable();
                table.Append(CreateRow(new Text("Name"), new Text(datasource.Name)));
                table.Append(CreateRow(new Text("Type"), new Text(datasource.Type)));
                if (DetailedDocumentation)
                {
                    table.Append(CreateMergedRow(new Text("DataSource Properties"), 2, WordDocBuilder.cellHeaderBackground));
                    foreach (Expression expression in datasource.Properties.OrderBy(o => o.expressionOperator))
                    {
                        AddExpressionTable(expression, table);
                    }
                }
                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addAppResources()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Resources"));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run(new Text($"A total of {app.Resources.Count} Resources are located in the app:"))));
            foreach (Resource resource in app.Resources)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(resource.Name));
                ApplyStyleToParagraph("Heading3", para);
                body.AppendChild(new Paragraph(new Run()));
                Table table = CreateTable();
                table.Append(CreateRow(new Text("Name"), new Text(resource.Name)));
                table.Append(CreateRow(new Text("Content"), new Text(resource.Content)));
                table.Append(CreateRow(new Text("Resource Kind"), new Text(resource.ResourceKind)));
                if (resource.ResourceKind == "LocalFile")
                {
                    MemoryStream resourceStream;
                    if (app.ResourceStreams.TryGetValue(resource.Name, out resourceStream))
                    {
                        Drawing icon = null;
                        Expression fileName = resource.Properties.First(o => o.expressionOperator == "FileName");
                        if (fileName.expressionOperands.First().ToString().ToLower().EndsWith("svg"))
                        {
                            string svg = Encoding.Default.GetString(resourceStream.ToArray());
                            icon = InsertSvgImage(mainPart, svg, 400, 400);
                        }
                        else
                        {
                            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                            int imageWidth, imageHeight;
                            using (var image = Image.FromStream(resourceStream, false, false))
                            {
                                imageWidth = image.Width;
                                imageHeight = image.Height;
                            }
                            resourceStream.Position = 0;
                            imagePart.FeedData(resourceStream);
                            int usedWidth = (imageWidth > 400) ? 400 : imageWidth;
                            icon = InsertImage(mainPart.GetIdOfPart(imagePart), usedWidth, (int)(usedWidth * imageHeight / imageWidth));
                        }
                        table.Append(CreateRow(new Text("Resource Preview"), icon));
                    }
                }
                if (DetailedDocumentation)
                {
                    table.Append(CreateMergedRow(new Text("Resource Properties"), 2, WordDocBuilder.cellHeaderBackground));
                    foreach (Expression expression in resource.Properties.OrderBy(o => o.expressionOperator))
                    {
                        AddExpressionTable(expression, table);
                    }
                }
                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }
    }
}