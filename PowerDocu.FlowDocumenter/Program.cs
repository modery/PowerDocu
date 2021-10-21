using System;

namespace PowerDocu.FlowDocumenter
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length==0 || args.Length > 1) {
                Console.WriteLine("Please provide an exported Flow as parameter. For example:");
                Console.WriteLine("  flowdocumenter.exe ExportedFlow.zip");
                
            } else {
                FlowParser flowParserFromZip = new FlowParser(args[0]);
                foreach(FlowEntity flow in flowParserFromZip.getFlows()) {
                    GraphBuilder gbzip = new GraphBuilder(flow);
                    gbzip.buildTopLevelGraph();
                    gbzip.buildDetailedGraph();
                    WordDocBuilder wordzip = new WordDocBuilder(flow);
                }
            }
        }
        
    }
}
