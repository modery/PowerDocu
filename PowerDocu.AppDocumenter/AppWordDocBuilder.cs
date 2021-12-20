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

        public AppWordDocBuilder(AppEntity appToDocument, string path, string template)
        {
            this.app = appToDocument;
            folderPath = path + CharsetHelper.GetSafeName(@"\AppDoc - " + app.Name + @"\");
            Directory.CreateDirectory(folderPath);
            string filename = CharsetHelper.GetSafeName(app.Name) + ((app.ID != null) ? ("(" + app.ID + ")") : "") + ".docx";
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
                addDetailedAppControls();
            }
            NotificationHelper.SendNotification("Created Word documentation for " + app.Name);
        }

        private void addAppProperties()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Power App Documentation"));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("App Name"), new Text(app.Name)));
            table.Append(CreateRow(new Text("Documentation generated at"), new Text(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString())));
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));

            run = para.AppendChild(new Run());
            run.AppendChild(new Text("App Properties"));
            ApplyStyleToParagraph("Heading2", para);
            body.AppendChild(new Paragraph(new Run()));
            table = CreateTable();
            foreach (Expression property in app.Properties)
            {
                AddExpressionTable(property, table);
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
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
            foreach (string var in app.GlobalVariables.OrderBy(o => o).ToHashSet())
            {
                table.Append(CreateRow(new Text(var), new Text("Global Variable")));
            }
            foreach (string var in app.ContextVariables)
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
            foreach (string coll in app.Collections.OrderBy(o => o).ToHashSet())
            {
                table.Append(CreateRow(new Text(coll), new Text("Collection")));
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

            List<ControlEntity> allControls = new List<ControlEntity>();
            body.AppendChild(new Paragraph(new Run(new Text($"A total of {app.Controls.Count} Screens are located in the app:"))));
            foreach (ControlEntity control in app.Controls.OrderBy(o => o.Name).ToList())
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text("Screen: " + control.Name));
                ApplyStyleToParagraph("Heading3", para);
                body.AppendChild(CreateControlTable(control));
                body.AppendChild(new Paragraph(new Run(new Break())));
            }

            foreach (ControlEntity control in allControls.OrderBy(o => o.Name).ToList())
            {
                //TODO
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
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Detailed Controls"));
            ApplyStyleToParagraph("Heading2", para);

            List<ControlEntity> allControls = new List<ControlEntity>();
            foreach (ControlEntity control in app.Controls)
            {
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
                table.Append(CreateRow(new Text("Type"), new Text(control.Type)));
                table.Append(CreateMergedRow(new Text("Control Rules"), 2, WordDocBuilder.cellHeaderBackground));
                foreach (Rule rule in control.Rules)
                {
                    table.Append(CreateRow(new Text(rule.Property), new Text(rule.InvariantScript)));
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
            foreach (DataSource datasource in app.DataSources)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(datasource.Name));
                ApplyStyleToParagraph("Heading3", para);
                body.AppendChild(new Paragraph(new Run()));
                Table table = CreateTable();
                table.Append(CreateRow(new Text("Name"), new Text(datasource.Name)));
                table.Append(CreateRow(new Text("Type"), new Text(datasource.Type)));
                table.Append(CreateMergedRow(new Text("DataSource Properties"), 2, WordDocBuilder.cellHeaderBackground));
                foreach (Expression expression in datasource.Properties)
                {
                    AddExpressionTable(expression, table);
                }

                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

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
                table.Append(CreateMergedRow(new Text("Resource Properties"), 2, WordDocBuilder.cellHeaderBackground));
                foreach (Expression expression in resource.Properties)
                {
                    AddExpressionTable(expression, table);
                }

                body.Append(table);
                body.AppendChild(new Paragraph(new Run(new Break())));
            }
            body.AppendChild(new Paragraph(new Run(new Break())));
        }
    }
}