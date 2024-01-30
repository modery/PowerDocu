using System.Collections.Generic;
using CommandLine;
using PowerDocu.Common;

namespace PowerDocu.GUI
{
    public class CommandLineOptions
    {
        [Option('p', "itemsToDocument", HelpText = "Semi colon delimited list of paths of items to document", Required = false, Separator = ';')]
        public IEnumerable<string>? ItemsToDocument { get; set; }
        [Option('o', "outputPath", HelpText = "Destination to write dcoumentation to. Will default to path of item if blank", Required = false)]
        public string? OutputPath { get; set; }
        [Option('m', "markDown", HelpText = "Format document as Markdown", Required = false)]
        public bool Markdown { get; set; }
        [Option('w', "word", HelpText = "Format document as Word", Required = false)]
        public bool Word { get; set; }
        [Option('c', "changesOnly", HelpText = "Document changes only or all properties", Required = false)]
        public bool ChangesOnly { get; set; }
        [Option('d', "defaultValues", HelpText = "Document Canvas App Default values", Required = false)]
        public bool DefaultValues { get; set; }
        [Option('s', "sortFlowsByName", HelpText = "Sort flows by name", Required = false)]
        public bool SortFlowsByName { get; set; }
        [Option('i', "updateIcons", HelpText = "Update existing set of connector icons", Required = false)]
        public bool UpdateIcons { get; set; }
        [Option('t', "wordTemplate", HelpText = "Path to a word template to use when generating a word document", Required = false)]
        public string? WordTemplate { get; set; }

        internal string FileFormat => this switch
        {
            { Word: true, Markdown: true } => OutputFormatHelper.All,
            { Word: false, Markdown: true } => OutputFormatHelper.Markdown,
            { Word: true, Markdown: false } => OutputFormatHelper.Word,
            _ => OutputFormatHelper.Word
        };

        internal string SortFlowActions => this switch
        {
            { SortFlowsByName: true} => "By name",
            { SortFlowsByName: false } => "By order of appearance",
            _ => "By order of appearance"
        };
    }
}
