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
                if (File.Exists(args[0]))
                {
                    string path = Path.GetDirectoryName(args[0]);
                    FlowParser flowParserFromZip = new FlowParser(args[0]);
                    foreach (FlowEntity flow in flowParserFromZip.getFlows())
                    {
                        GraphBuilder gbzip = new GraphBuilder(flow, path);
                        gbzip.buildTopLevelGraph();
                        gbzip.buildDetailedGraph();
                        WordDocBuilder wordzip = new WordDocBuilder(flow, path);
                    }
                }
            }
        }

    }
}
