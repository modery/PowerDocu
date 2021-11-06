using System;
using System.IO;

namespace PowerDocu.FlowDocumenter
{
    class FlowDocumenter
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 1)
            {
                Console.WriteLine("Please provide an exported Flow package as parameter. For example:");
                Console.WriteLine("  powerdocu.flowdocumenter.exe ExportedFlow.zip");
            }
            else
            {
                FlowDocumentationGenerator.GenerateWordDocumentation(args[0]);
            }
        }
    }
}
