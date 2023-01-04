using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using PowerDocu.Common;

namespace PowerDocu.SolutionDocumenter
{
    class SolutionWordDocBuilder : WordDocBuilder
    {
        private readonly SolutionDocumentationContent content;

        public SolutionWordDocBuilder(SolutionDocumentationContent contentDocumentation, string template)
        {
            content = contentDocumentation;
            Directory.CreateDirectory(content.folderPath);
            string filename = InitializeWordDocument(content.folderPath + "Solution - " + content.filename, template);
            using WordprocessingDocument wordDocument = WordprocessingDocument.Open(filename, true);
            mainPart = wordDocument.MainDocumentPart;
            body = mainPart.Document.Body;
            PrepareDocument(!String.IsNullOrEmpty(template));
            addSolutionMetadata();
            addSolutionComponents();
        }

        private void addSolutionMetadata()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text(content.solution.UniqueName));
            ApplyStyleToParagraph("Heading1", para);
            body.AppendChild(new Paragraph(new Run()));
            Table table = CreateTable();
            table.Append(CreateRow(new Text("Status"), new Text(content.solution.isManaged ? "Managed" : "Unmanaged")));
            table.Append(CreateRow(new Text("Version"), new Text(content.solution.Version)));
            table.Append(CreateRow(new Text("Publisher"), new Text(content.solution.Publisher.UniqueName)));
            body.Append(table);
        }

        private Table GetPublisherInfo()
        {
            Table table = CreateTable();
            //todo implement
            return table;
        }

        private void addSolutionComponents()
        {
            Paragraph para = body.AppendChild(new Paragraph());
            Run run = para.AppendChild(new Run());
            run.AppendChild(new Text("Solution Components"));
            ApplyStyleToParagraph("Heading1", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("This solution contains the following components"));
            Table table = CreateTable();
            foreach (string componentType in content.solution.GetComponentTypes())
            {
                List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == componentType).OrderBy(c => c.DisplayName).ToList();
                table.Append(CreateHeaderRow(new Text(componentType)));
                foreach (SolutionComponent component in components)
                {
                    string name = String.IsNullOrEmpty(component.SchemaName) ? component.ID : component.SchemaName;
                    table.Append(CreateRow(new Text(name)));
                    ApplyStyleToParagraph("Heading2", para);
                }
            }
            body.Append(table);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("Solution Component Dependencies"));
            ApplyStyleToParagraph("Heading1", para);
            para = body.AppendChild(new Paragraph());
            run = para.AppendChild(new Run());
            run.AppendChild(new Text("This solution has the following dependencies"));
            foreach (string solution in content.solution.Dependencies.GroupBy(p => p.Required.Solution).Select(g => g.First()).OrderBy(t => t.Required.Solution).Select(t => t.Required.Solution).ToList())
            {
                para = body.AppendChild(new Paragraph());
                run = para.AppendChild(new Run());
                run.AppendChild(new Text("Solution: " + solution));
                ApplyStyleToParagraph("Heading2", para);
                foreach (SolutionDependency dependency in content.solution.Dependencies.Where(p => p.Required.Solution.Equals(solution)))
                {
                    table = CreateTable();
                    table.Append(CreateHeaderRow(new Text("Property"), new Text("Requirement"), new Text("Dependency")));
                    if (!String.IsNullOrEmpty(dependency.Required.DisplayName) || !String.IsNullOrEmpty(dependency.Dependent.DisplayName))
                        table.Append(CreateRow(new Text("Display Name"), new Text(dependency.Required.DisplayName), new Text(dependency.Dependent.DisplayName)));
                    if (!String.IsNullOrEmpty(dependency.Required.Type) || !String.IsNullOrEmpty(dependency.Dependent.Type))
                        table.Append(CreateRow(new Text("Type"), new Text(dependency.Required.Type), new Text(dependency.Dependent.Type)));
                    if (!String.IsNullOrEmpty(dependency.Required.SchemaName) || !String.IsNullOrEmpty(dependency.Dependent.SchemaName))
                        table.Append(CreateRow(new Text("Schema Name"), new Text(dependency.Required.SchemaName), new Text(dependency.Dependent.SchemaName)));
                    if (!String.IsNullOrEmpty(dependency.Required.Solution) || !String.IsNullOrEmpty(dependency.Dependent.Solution))
                        table.Append(CreateRow(new Text("Solution"), new Text(dependency.Required.Solution), new Text(dependency.Dependent.Solution)));
                    if (!String.IsNullOrEmpty(dependency.Required.ID) || !String.IsNullOrEmpty(dependency.Dependent.ID))
                        table.Append(CreateRow(new Text("ID"), new Text(dependency.Required.ID), new Text(dependency.Dependent.ID)));
                    if (!String.IsNullOrEmpty(dependency.Required.IdSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.IdSchemaName))
                        table.Append(CreateRow(new Text("ID Schema Name"), new Text(dependency.Required.IdSchemaName), new Text(dependency.Dependent.IdSchemaName)));
                    if (!String.IsNullOrEmpty(dependency.Required.ParentDisplayName) || !String.IsNullOrEmpty(dependency.Dependent.ParentDisplayName))
                        table.Append(CreateRow(new Text("Parent Display Name"), new Text(dependency.Required.ParentDisplayName), new Text(dependency.Dependent.ParentDisplayName)));
                    if (!String.IsNullOrEmpty(dependency.Required.ParentSchemaName) || !String.IsNullOrEmpty(dependency.Dependent.ParentSchemaName))
                        table.Append(CreateRow(new Text("Parent Schema Name"), new Text(dependency.Required.ParentSchemaName), new Text(dependency.Dependent.ParentSchemaName)));
                    body.Append(table);
                    /*List<SolutionComponent> components = content.solution.Components.Where(c => c.Type == componentType).OrderBy(c => c.DisplayName).ToList();
                    table.Append(CreateHeaderRow(new Text(componentType)));
                    foreach (SolutionComponent component in components)
                    {
                        string name = String.IsNullOrEmpty(component.SchemaName) ? component.ID : component.SchemaName;
                        table.Append(CreateRow(new Text(name)));
                        ApplyStyleToParagraph("Heading2", para);
                    }*/
                }
            }
        }
    }
}