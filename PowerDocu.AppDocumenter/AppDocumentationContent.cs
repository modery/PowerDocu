using System.Collections.Generic;
using System.IO;
using System.Linq;
using PowerDocu.Common;

namespace PowerDocu.AppDocumenter
{
    class AppDocumentationContent
    {
        public string folderPath, filename;
        public string ID;
        public string Name;
        public AppProperties appProperties;
        public AppResources appResources;
        public AppDataSources appDataSources;
        public AppVariablesInfo appVariablesInfo;
        public AppControls appControls;
        public Dictionary<string, MemoryStream> ResourceStreams;
        // App Properties that are colours
        public readonly string[] ColourProperties = new string[] { "BorderColor", "Color", "DisabledBorderColor", "DisabledColor", "DisabledFill", "DisabledSectionColor",
                    "DisabledSelectionFill", "Fill", "FocusedBorderColor", "HoverBorderColor", "HoverColor", "HoverFill", "PressedBorderColor","PressedColor",
                    "PressedFill", "SelectionColor", "SelectionFill" };

        public AppDocumentationContent(AppEntity app, string path)
        {
            NotificationHelper.SendNotification("Preparing documentation content for " + app.Name);
            folderPath = path + CharsetHelper.GetSafeName(@"\AppDoc - " + app.Name + @"\");
            Directory.CreateDirectory(folderPath);
            filename = CharsetHelper.GetSafeName(app.Name) + ((app.ID != null) ? ("(" + app.ID + ")") : "");
            ResourceStreams = app.ResourceStreams;
            ID = app.ID;
            Name = app.Name;
            appProperties = new AppProperties(app);
            appVariablesInfo = new AppVariablesInfo(app);
            appDataSources = new AppDataSources(app);
            appResources = new AppResources(app);
            appControls = new AppControls(app);
        }
    }

    public class AppVariablesInfo
    {
        public string header = "Variables & Collections";
        public string headerGlobalVariables = "Global Variables";
        public string headerContextVariables = "Context Variables";
        public string headerCollections = "Collections";
        public string infoText = "";
        public Dictionary<string, List<ControlPropertyReference>> variableCollectionControlReferences;
        public HashSet<string> globalVariables, contextVariables, collections;
        public AppVariablesInfo(AppEntity app)
        {
            infoText = $"There are {app.GlobalVariables.Count} Global Variables, {app.ContextVariables.Count} Context Variables and {app.Collections.Count} Collections.";
            variableCollectionControlReferences = app.VariableCollectionControlReferences;
            globalVariables = app.GlobalVariables.OrderBy(o => o).ToHashSet();
            contextVariables = app.ContextVariables.OrderBy(o => o).ToHashSet();
            collections = app.Collections.OrderBy(o => o).ToHashSet();
        }
    }

    public class AppDataSources
    {
        public string header = "DataSources";
        public string infoText = "";
        public List<DataSource> dataSources;
        public AppDataSources(AppEntity app)
        {
            infoText = $"A total of {app.DataSources.Count} DataSources are located in the app:";
            dataSources = app.DataSources.OrderBy(o => o.Name).ToList();
        }
    }

    public class AppResources
    {
        public string header = "Resources";
        public string infoText = "";
        public List<Resource> resources;

        public AppResources(AppEntity app)
        {
            infoText = $"A total of {app.Resources.Count} Resources are located in the app:";
            resources = app.Resources;
        }
    }

    public class AppProperties
    {
        public string header = "";
        public string appLogo;
        public string appBackgroundColour;
        public string headerAppProperties = "App Properties";
        public string headerAppStatistics = "App Statistics";
        public string headerAppPreviewFlags = "App Preview Flags";
        public string headerDocumentationGenerated = "Documentation generated at";
        public List<Expression> appProperties;
        public Expression appPreviewsFlagProperty;
        public string[] propertiesToSkip = new string[] { "AppPreviewFlagsMap", "ControlCount" };
        public string[] OverviewProperties = new string[] {"AppCreationSource", "AppDescription", "AppName", "BackgroundColor", "DocumentAppType", "DocumentLayoutHeight", "DocumentLayoutOrientation",
                    "DocumentLayoutWidth", "IconColor", "IconName", "Id", "LastSavedDateTimeUTC", "LogoFileName", "Name"};
        public Dictionary<string, string> statisticsTable = new Dictionary<string, string>();
        public AppProperties(AppEntity app)
        {
            header = "Power App Documentation - " + app.Name;
            Expression appLogoExp = app.Properties.Find(o => o.expressionOperator == "LogoFileName");
            appLogo = appLogoExp?.expressionOperands[0].ToString();
            Expression bgColour = app.Properties.Find(o => o.expressionOperator == "BackgroundColor");
            appBackgroundColour = bgColour?.expressionOperands[0].ToString();
            statisticsTable.Add("Screens", "" + app.Controls.Where(o => o.Type == "screen").ToList().Count);
            List<ControlEntity> allControls = new List<ControlEntity>();
            foreach (ControlEntity control in app.Controls)
            {
                allControls.Add(control);
                allControls.AddRange(AppDocumentationHelper.getAllChildControls(control));
            }
            statisticsTable.Add("Controls (excluding Screens)", "" + (allControls.Count - app.Controls.Where(o => o.Type == "screen").ToList().Count));
            statisticsTable.Add("Variables", "" + (app.GlobalVariables.Count + app.ContextVariables.Count));
            statisticsTable.Add("Collections", "" + app.Collections.Count);
            statisticsTable.Add("Data Sources", "" + app.DataSources.Count);
            statisticsTable.Add("Resources", "" + app.Resources.Count);
            appProperties = app.Properties.OrderBy(o => o.expressionOperator).ToList();
            appPreviewsFlagProperty = app.Properties.Find(o => o.expressionOperator == "AppPreviewFlagsMap");
        }
    }

    public class AppControls
    {
        public string headerOverview = "Controls Overview";
        public string headerDetails = "Detailed Controls";
        public string headerScreenNavigation = "Screen Navigation";
        public string infoTextScreens = "";
        public string infoTextControls = "";
        public string infoTextScreenNavigation = "The following diagram shows the navigation between the different screens.";
        public string imageScreenNavigation = "ScreenNavigation";
        public List<ControlEntity> controls;
        public List<ControlEntity> allControls = new List<ControlEntity>();
        public string[] controlPropertiesToSkip = new string[] { "X", "Y", "ZIndex"};
        public AppControls(AppEntity app)
        {
            controls = app.Controls.OrderBy(o => o.Name).ToList();
            foreach (ControlEntity control in controls)
            {
                allControls.Add(control);
                allControls.AddRange(ControlEntity.getAllChildControls(control));
            }
            infoTextScreens = $"A total of {controls.Where(o => o.Type == "screen").ToList().Count} Screens are located in the app.";
            infoTextControls = $"A total of {allControls.Count} Controls are located in the app.";
        }
    }

    public static class AppDocumentationHelper
    {
        public static List<ControlEntity> getAllChildControls(ControlEntity control)
        {
            List<ControlEntity> childControls = new List<ControlEntity>();
            foreach (ControlEntity childControl in control.Children)
            {
                childControls.Add(childControl);
                childControls.AddRange(getAllChildControls(childControl));
            }
            return childControls;
        }
    }
}