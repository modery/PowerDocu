using System.Collections.Generic;
using CommandLine;
using PowerDocu.Common;

#nullable enable
namespace PowerDocu.GUI
{
    public class CommandLineOptions
    {
        [Option('p', "itemsToDocument", HelpText = "Semi colon delimited list of paths of items to document", Required = false, Separator = ';')]
        public IEnumerable<string>? ItemsToDocument { get; set; }
        [Option('o', "outputPath", HelpText = "Destination to write documentation to. Will default to path of item if blank", Required = false)]
        public string? OutputPath { get; set; }
        [Option('m', "markDown", HelpText = "Format document as Markdown", Required = false)]
        public bool Markdown { get; set; }
        [Option('w', "word", HelpText = "Format document as Word", Required = false)]
        public bool Word { get; set; }
        [Option('f', "fullDocumentation", HelpText = "Document changes only or all properties", Required = false)]
        public bool FullDocumentation { get; set; }
        [Option('c', "changesOnly", HelpText = "Create full set of documentation (true) or images only (false)", Required = false)]
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

        internal string FileFormat => this switch
        {
            { Word: true, Markdown: true } => OutputFormatHelper.All,
            { Word: false, Markdown: true } => OutputFormatHelper.Markdown,
            { Word: true, Markdown: false } => OutputFormatHelper.Word,
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
