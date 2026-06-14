using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CommandLine;
using PowerDocu.AppDocumenter;
using PowerDocu.Common;
using PowerDocu.SolutionDocumenter;

namespace PowerDocu.GUI
{
    internal static class PowerDocuCLI
    {
        public static async Task Run(string[] args)
        {
            try
            {
                // Redirect output based on the operating system
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    RedirectOutputToConsoleWindow(args.Length > 0);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    RedirectOutputToConsoleStream();
                }

                NotificationHelper.AddNotificationReceiver(new ConsoleNotificationReceiver());

                var options = new CommandLineOptions();

                Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(parsed => { options = parsed; });

                await CheckForLatestVersion();

                switch (options.UpdateIcons)
                {
                    case true:
                        await ConnectorHelper.UpdateConnectorIcons();
                        break;
                    case false when options.ItemsToDocument == null || !options.ItemsToDocument.Any():
                        NotificationHelper.SendNotification($"No items to generate documentation on");
                        break;
                    case false when !options.ItemsToDocument.All(itemToDocument =>
                        new List<string> { ".zip", ".msapp" }.Contains(Path.GetExtension(itemToDocument))):
                        NotificationHelper.SendNotification(
                            $"No valid file provided, valid files are either .zip or .msapp formats");
                        break;
                    case false when options.Word && !string.IsNullOrEmpty(options.WordTemplate) &&
                                    !new List<string> { ".docx", ".docm", ".dotx" }.Contains(
                                        Path.GetExtension(options.WordTemplate)):
                        NotificationHelper.SendNotification(
                            $"An invalid word document was provided as the Word Template, expected the file to be .docx, .docm or .dotx format");
                        break;
                    default:
                        GenerateDocumentation(options);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
        }

        private static void GenerateDocumentation(CommandLineOptions options)
        {
            foreach (var itemToDocument in options.ItemsToDocument!)
            {
                if (!File.Exists(itemToDocument))
                {
                    NotificationHelper.SendNotification($"{itemToDocument} not found. Skipping.");
                    break;
                }
                ConfigHelper configHelper = new ConfigHelper();
                configHelper.outputFormat = options.FileFormat;
                configHelper.documentChangesOnlyCanvasApps = options.ChangesOnly;
                configHelper.documentDefaultValuesCanvasApps = options.DefaultValues;
                configHelper.documentSampleData = options.SampleDataSources;
                configHelper.flowActionSortOrder = options.SortFlowActions;
                configHelper.wordTemplate = options.WordTemplate;
                configHelper.documentSolution = options.DocumentSolution;
                configHelper.documentFlows = options.DocumentFlows;
                configHelper.documentApps = options.DocumentApps;
                configHelper.documentAppProperties = options.DocumentAppProperties;
                configHelper.documentAppVariables = options.DocumentAppVariables;
                configHelper.documentAppDataSources = options.DocumentAppDataSources;
                configHelper.documentAppResources = options.DocumentAppResources;
                configHelper.documentAppControls = options.DocumentAppControls;
                configHelper.documentDefaultColumns = options.DocumentDefaultColumns;
                configHelper.addTableOfContents = options.AddTableOfContents;
                configHelper.showAllComponentsInGraph = options.ShowAllComponentsInGraph;
                configHelper.documentModelDrivenApps = options.DocumentModelDrivenApps;
                configHelper.documentBusinessProcessFlows = options.DocumentBusinessProcessFlows;
                configHelper.documentClassicWorkflows = options.DocumentClassicWorkflows;
                configHelper.documentDataflows = options.DocumentDataflows;
                switch (Path.GetExtension(itemToDocument))
                {
                    case ".zip":
                        SolutionDocumentationGenerator.GenerateDocumentation(itemToDocument, options.FullDocumentation, configHelper, options.OutputPath);
                        break;
                    case ".msapp":
                        AppDocumentationGenerator.GenerateDocumentation(itemToDocument, options.FullDocumentation, configHelper, options.OutputPath);
                        break;
                }
            }
        }

        private static async Task CheckForLatestVersion()
        {
            if (await PowerDocuReleaseHelper.HasNewerPowerDocuRelease())
            {
                NotificationHelper.SendNotification("A new PowerDocu release has been found: " + PowerDocuReleaseHelper.latestVersionTag);
                NotificationHelper.SendNotification("Please visit " + PowerDocuReleaseHelper.latestVersionUrl);
            }
        }

        private static void RedirectOutputToConsoleWindow(bool hasArgs)
        {
            // Redirect output to the console on Windows
            if (hasArgs)
                //attach to existing console
                AttachConsole(-1);
            else
                //create new console
                AllocConsole();
        }

        private static void RedirectOutputToConsoleStream()
        {
            // Redirect output to the console stream on Linux and macOS
            var stdout = Console.OpenStandardOutput();
            Console.SetOut(new StreamWriter(stdout));
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int pid);
    }
}