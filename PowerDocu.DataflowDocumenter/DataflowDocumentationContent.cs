using System.IO;
using PowerDocu.Common;

namespace PowerDocu.DataflowDocumenter
{
    public class DataflowDocumentationContent
    {
        public string folderPath, filename;
        public DataflowEntity dataflow;
        public DocumentationContext context;

        public string headerOverview = "Overview";
        public string headerMetadata = "Metadata";
        public string headerQueries = "Queries";
        public string headerMCode = "Power Query M Code";
        public string headerConnectionOverrides = "Connection Overrides";
        public string headerRefreshSettings = "Refresh Settings";
        public string headerSettings = "Settings";
        public string headerDocumentationGenerated = "Documentation generated at";

        public DataflowDocumentationContent(DataflowEntity dataflow, string path, DocumentationContext context)
        {
            NotificationHelper.SendNotification("Preparing documentation content for Dataflow: " + dataflow.GetDisplayName());
            this.dataflow = dataflow;
            this.context = context;
            folderPath = path + CharsetHelper.GetSafeName(@"\DataflowDoc " + dataflow.GetDisplayName() + @"\");
            Directory.CreateDirectory(folderPath);
            filename = CharsetHelper.GetSafeName(dataflow.GetDisplayName());
        }
    }
}
