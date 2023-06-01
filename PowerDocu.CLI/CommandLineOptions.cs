using System.Collections.Generic;
using System.Linq.Expressions;
using CommandLine;
using PowerDocu.Common;
using PowerDocu.FlowDocumenter;

namespace PowerDocu.CLI
{
    public class CommandLineOptions
    {
        [Option('p', "itemsToDocument", HelpText = "Semi colon delimited list of paths of items to document", Required = false, Separator = ';')]
        public IEnumerable<string> ItemsToDocument { get; set; }
        [Option('m', "markDown", HelpText = "Format document as Markdown", Required = false, Default = false)]
        public bool Markdown { get; set; } = false;
        [Option('w', "word", HelpText = "Format document as Markdown", Required = false, Default = true)]
        public bool Word { get; set; } = true;
        [Option('c', "changesOnly", HelpText = "Document changes only or all properties",
            Required = false, Default = false)]
        public bool ChangesOnly { get; set; } = false;
        [Option('d', "defaultValues", HelpText = "Document Canvas App Default values", Required = false, Default = true)]
        public bool DefaultValues { get; set; } = true;
        [Option('s', "sortFlowsByName", HelpText = "Sort flows by name", Required = false, Default = false)]
        public bool SortFlowsByName { get; set; } = false;
        [Option('i', "updateIcons", HelpText = "Update existing set of connector icons", Required = false, Default = false)]
        public bool UpdateIcons { get; set; } = false;
        [Option('t', "wordTemplate", HelpText = "Path to a word template to use when generating a word document", Required = false)]
        public string WordTemplate { get; set; }

        internal string FileFormat => this switch
        {
            { Word: true, Markdown: true } => OutputFormatHelper.All,
            { Word: false, Markdown: true } => OutputFormatHelper.Markdown,
            { Word: true, Markdown: false } => OutputFormatHelper.Word,
            _ => OutputFormatHelper.Word
        };

        internal string SortFlowActions => this switch
        {
            { SortFlowsByName: true} => FlowActionSortOrderHelper.ByName,
            { SortFlowsByName: false } => FlowActionSortOrderHelper.ByOrderOfAppearance,
            _ => FlowActionSortOrderHelper.ByOrderOfAppearance
        };
    }
}
