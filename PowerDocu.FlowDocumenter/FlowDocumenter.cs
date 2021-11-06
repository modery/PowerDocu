using System;
using System.IO;

namespace PowerDocu.FlowDocumenter
{
    class FlowDocumenter
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("Please provide an exported Flow package as parameter (mandatory), and optionally a Word document to use as template. For example:");
                Console.WriteLine("  powerdocu.flowdocumenter.exe ExportedFlow.zip");
                Console.WriteLine("  powerdocu.flowdocumenter.exe ExportedFlow.zip WordTemplate.docx");
            }
            else
            {
                if (args.Length == 1)
                    FlowDocumentationGenerator.GenerateWordDocumentation(args[0]);
                if (args.Length == 2)
                    FlowDocumentationGenerator.GenerateWordDocumentation(args[0], args[1]);
            }
        }
    }
}
