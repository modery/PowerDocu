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
                MainDocumentPart mainPart = wordDocument.MainDocumentPart;
                Body body = mainPart.Document.Body;
                PrepareDocument(mainPart, !String.IsNullOrEmpty(template));
                addAppProperties(body);
                addAppDataSources(body);
                addAppControls(body);
            }
            NotificationHelper.SendNotification("Created Word documentation for " + app.Name);
        }

        private void addAppProperties(Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Power App Documentation"));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Documentation generated at"), new Text(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString())));
            foreach (Expression property in app.properties)
            {
                AddExpressionTable(property, table);
            }
            body.Append(table);
            body.AppendChild(new Paragraph(new Run(new Break())));
        }

        private void addAppControls(Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Controls"));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            foreach (ControlEntity control in app.Controls)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(control.Name));
                ApplyStyleToParagraph("Heading2", para);
                body.AppendChild(new Paragraph(new Run()));
                Table table = CreateTable();
                //this should be in its own recursive method
                table.Append(CreateMergedRow(new Text("Control Rules"), 2, WordDocBuilder.cellHeaderBackground));
                foreach (Rule rule in control.Rules)
                {
                    table.Append(CreateRow(new Text(rule.Property), new Text(rule.InvariantScript)));
                }
                foreach (ControlEntity childControl in control.Children)
                {
                    Table childtable = CreateTable();
                    childtable.Append(CreateMergedRow(new Text("Child Controls"), 2, WordDocBuilder.cellHeaderBackground));
                    foreach (Expression expression in childControl.Properties)
                    {
                        AddExpressionTable(expression, childtable);
                    }
                    table.Append(CreateRow(new Text("childcontrol"), childtable));
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


        private void addAppDataSources(Body body)
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("DataSources"));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            foreach (DataSource datasource in app.DataSources)
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text(datasource.Name));
                ApplyStyleToParagraph("Heading2", para);
                body.AppendChild(new Paragraph(new Run()));
                Table table = CreateTable();
                //this should be in its own recursive method
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
    }
}