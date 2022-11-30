using System;
using System.IO;
using PowerDocu.Common;

namespace PowerDocu.AppDocumenter
{
    class AppDocumenter
    {
        static void Main(string[] args)
        {
            NotificationHelper.AddNotificationReceiver(new ConsoleNotificationReceiver());
            if (args.Length == 0 || args.Length > 2)
            {
                NotificationHelper.SendNotification("Please provide an exported Power App package as parameter (mandatory), and optionally a Word document to use as template. For example:");
                NotificationHelper.SendNotification("  powerdocu.appdocumenter.exe ExportedApp.msapp");
                NotificationHelper.SendNotification("  powerdocu.appdocumenter.exe ExportedApp.msapp WordTemplate.docx");
            }
            else
            {
                if (args.Length == 1)
                    AppDocumentationGenerator.GenerateDocumentation(args[0], "All");
                if (args.Length == 2)
                    AppDocumentationGenerator.GenerateDocumentation(args[0], "All", args[1]);
            }
        }
    }
}
