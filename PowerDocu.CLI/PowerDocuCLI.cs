using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using PowerDocu.AppDocumenter;
using PowerDocu.Common;
using PowerDocu.SolutionDocumenter;

namespace PowerDocu.CLI
{
    internal class PowerDocuCLI
    {
        static async Task Main(string[] args)
        {
            NotificationHelper.AddNotificationReceiver(new ConsoleNotificationReceiver());

            var options = new CommandLineOptions();

            Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(parsed =>
            {
                options = parsed;
            });

            switch (options.UpdateIcons)
            {
                case true:
                    await ConnectorHelper.UpdateConnectorIcons();
                    break;
                case false when !options.ItemsToDocument.Any():
                    NotificationHelper.SendNotification($"No items to generate documentation on provided");
                    break;
                case false when !options.ItemsToDocument.All(itemToDocument => new List<string> { ".zip", ".msapp" }.Contains(Path.GetExtension(itemToDocument))):
                    NotificationHelper.SendNotification($"No valid file provided, valid files are either .zip or .msapp formats");
                    break;
                case false when options.Word && !string.IsNullOrEmpty(options.WordTemplate) && !new List<string> { ".docx", ".docm", ".dtox"}.Contains(Path.GetExtension(options.WordTemplate)):
                    NotificationHelper.SendNotification($"An invalid word document was provided as the Word Template, expected the file to be .docx, .docm or .dotx format");
                    break;
                default:
                    GenerateDocumentation(options);
                    break;
            }
        }

        private static void GenerateDocumentation(CommandLineOptions options)
        {
            foreach (var itemToDocument in options.ItemsToDocument)
            {
                switch (Path.GetExtension(itemToDocument))
                {
                    case ".zip":
                        SolutionDocumentationGenerator.GenerateDocumentation(itemToDocument, options.FileFormat, options.ChangesOnly, options.DefaultValues, options.SortFlowActions, options.WordTemplate);
                        break;
                    case ".msapp":
                        AppDocumentationGenerator.GenerateDocumentation(itemToDocument, options.FileFormat,
                            options.ChangesOnly, options.DefaultValues, options.WordTemplate);
                        break;
                }
            }
        }
    }
}