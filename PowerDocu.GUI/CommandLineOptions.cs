using System.Collections.Generic;
using CommandLine;
using PowerDocu.Common;

namespace PowerDocu.GUI
{
    public class CommandLineOptions
    {
        [Option('q', "itemsToDocument", HelpText = "Semi colon delimited list of paths of items to document", Required = false, Separator = ';')]
        public IEnumerable<string>? ItemsToDocument { get; set; }
        [Option('o', "outputPath", HelpText = "Destination to write documentation to. Will default to path of item if blank", Required = false)]
        public string? OutputPath { get; set; }
        [Option('m', "markDown", HelpText = "Format document as Markdown", Required = false)]
        public bool Markdown { get; set; }
        [Option('w', "word", HelpText = "Format document as Word", Required = false)]
        public bool Word { get; set; }
        [Option('h', "html", HelpText = "Format document as HTML", Required = false)]
        public bool Html { get; set; }
        [Option('f', "fullDocumentation", HelpText = "Create full set of documentation (true) or images only (false)", Required = false)]
        public bool FullDocumentation { get; set; }
        [Option('c', "changesOnly", HelpText = "Document changes only or all properties", Required = false)]
        public bool ChangesOnly { get; set; }
        [Option('d', "defaultValues", HelpText = "Document Canvas App Default values", Required = false)]
        public bool DefaultValues { get; set; }
        [Option('e', "sampledatasources", HelpText = "Document Sample Datasources", Required = false)]
        public bool SampleDataSources { get; set; }
        [Option('s', "sortFlowsByName", HelpText = "Sort flows by name", Required = false)]
        public bool SortFlowsByName { get; set; }
        [Option('i', "updateIcons", HelpText = "Update existing set of connector icons", Required = false)]
        public bool UpdateIcons { get; set; }
        [Option('t', "wordTemplate", HelpText = "Path to a word template to use when generating a word document", Required = false)]
        public string? WordTemplate { get; set; }
        [Option('l',"documentSolution", HelpText = "Document the solution", Required = false, Default = true)]
        public bool DocumentSolution { get; set; }
        [Option('p', "documentFlows", HelpText = "Document flows", Required = false, Default = true)]
        public bool DocumentFlows { get; set; }
        [Option('a', "documentApps", HelpText = "Document apps", Required = false, Default = true)]
        public bool DocumentApps { get; set; }
        [Option('b', "documentAppProperties", HelpText = "Document app properties", Required = false, Default = true)]
        public bool DocumentAppProperties { get; set; }
        [Option('v', "documentAppVariables", HelpText = "Document app variables", Required = false, Default = true)]
        public bool DocumentAppVariables { get; set; }
        [Option('x', "documentAppDataSources", HelpText = "Document app data sources", Required = false, Default = true)]
        public bool DocumentAppDataSources { get; set; }
        [Option('r', "documentAppResources", HelpText = "Document app resources", Required = false, Default = true)]
        public bool DocumentAppResources { get; set; }
        [Option('g', "documentAppControls", HelpText = "Document app controls", Required = false, Default = true)]
        public bool DocumentAppControls { get; set; }
        [Option('j', "documentDefaultColumns", HelpText = "Document default Dataverse table columns", Required = false, Default = false)]
        public bool DocumentDefaultColumns { get; set; }
        [Option('n', "addTableOfContents", HelpText = "Add a Table of Contents to generated Word documents", Required = false, Default = false)]
        public bool AddTableOfContents { get; set; }
        [Option("showAllComponentsInGraph", HelpText = "Show all solution components in the relationship graph, including isolated ones", Required = false, Default = true)]
        public bool ShowAllComponentsInGraph { get; set; }
        [Option('k', "documentModelDrivenApps", HelpText = "Document Model-Driven Apps", Required = false, Default = true)]
        public bool DocumentModelDrivenApps { get; set; }
        [Option('u', "documentBusinessProcessFlows", HelpText = "Document Business Process Flows", Required = false, Default = true)]
        public bool DocumentBusinessProcessFlows { get; set; }
        [Option('y', "documentClassicWorkflows", HelpText = "Document Classic Workflows", Required = false, Default = true)]
        public bool DocumentClassicWorkflows { get; set; }
        [Option("documentDataflows", HelpText = "Document Dataflows", Required = false, Default = true)]
        public bool DocumentDataflows { get; set; }

        internal string FileFormat => this switch
        {
            { Word: true, Markdown: true, Html: true } => OutputFormatHelper.All,
            { Word: false, Markdown: true, Html: false } => OutputFormatHelper.Markdown,
            { Word: false, Markdown: false, Html: true } => OutputFormatHelper.Html,
            { Word: true, Markdown: false, Html: false } => OutputFormatHelper.Word,
            _ => OutputFormatHelper.Word
        };

        internal string SortFlowActions => this switch
        {
            { SortFlowsByName: true } => "By name",
            { SortFlowsByName: false } => "By order of appearance",
            _ => "By order of appearance"
        };
    }
}
