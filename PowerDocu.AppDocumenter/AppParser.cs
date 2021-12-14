using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using PowerDocu.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace PowerDocu.AppDocumenter
{
    class AppParser
    {
        public enum PackageType
        {
            AppPackage,
            SolutionPackage
        }
        private readonly List<AppEntity> apps = new List<AppEntity>();
        private AppEntity currentApp;
        public PackageType packageType;

        public AppParser(string filename)
        {
            NotificationHelper.SendNotification("Processing " + filename);
            if (filename.EndsWith("zip"))
            {
                /*
                List<ZipArchiveEntry> definitions = ZipHelper.getWorkflowFilesFromZip(filename);
                packageType = (definitions.Count == 1) ? PackageType.FlowPackage : PackageType.SolutionPackage;
                foreach (ZipArchiveEntry definition in definitions)
                {
                    using (StreamReader reader = new StreamReader(definition.Open()))
                    {
                        NotificationHelper.SendNotification("Processing app " + definition.FullName);
                        string definitionContent = reader.ReadToEnd();
                        AppEntity flow = parseApp(definitionContent);
                        if (String.IsNullOrEmpty(flow.Name))
                        {
                            flow.Name = definition.Name.Replace(".json", "");
                        }
                        flows.Add(flow);
                    }
                }
                */
            }
            else if (filename.EndsWith("msapp"))
            {
                NotificationHelper.SendNotification("Processing app " + filename);
                packageType = PackageType.AppPackage;
                AppEntity app = new AppEntity();
                currentApp = app;
                parseAppProperties(filename);
                parseAppControls(filename);
                parseAppDataSources(filename);
                apps.Add(app);
            }
            else
            {
                NotificationHelper.SendNotification("Invalid file " + filename);
            }
        }

        private void parseAppProperties(string appArchive)
        {
            string[] filesToParse = new string[] { "Resources\\PublishInfo.json", "Header.json", "Properties.json" };
            foreach (string fileToParse in filesToParse)
            {
                using (StreamReader reader = new StreamReader(ZipHelper.getFileFromZip(appArchive, fileToParse).Open()))
                {
                    string appJSON = reader.ReadToEnd();
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                    var _jsonSerializer = JsonSerializer.Create(settings);
                    dynamic propertiesDefinition = JsonConvert.DeserializeObject<JObject>(appJSON, settings).ToObject(typeof(object), _jsonSerializer);
                    foreach (JToken property in propertiesDefinition.Children())
                    {
                        JProperty prop = (JProperty)property;
                        currentApp.properties.Add(Expression.parseExpressions(prop));
                        if (prop.Name.Equals("AppName"))
                        {
                            currentApp.Name = prop.Value.ToString();
                        }
                        if (prop.Name.Equals("ID"))
                        {
                            currentApp.ID = prop.Value.ToString();
                        }
                    }
                }
            }
        }

        private void parseAppControls(string appArchive)
        {
            List<ZipArchiveEntry> controlFiles = ZipHelper.getFilesInPathFromZip(appArchive, "Controls", ".json");
            foreach (ZipArchiveEntry controlEntry in controlFiles)
            {
                using (StreamReader reader = new StreamReader(controlEntry.Open()))
                {
                    string appJSON = reader.ReadToEnd();
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                    var _jsonSerializer = JsonSerializer.Create(settings);
                    dynamic controlsDefinition = JsonConvert.DeserializeObject<JObject>(appJSON, settings).ToObject(typeof(object), _jsonSerializer);
                    currentApp.Controls.Add(parseControl(((JObject)controlsDefinition.TopParent).Children().ToList()));
                }
            }
        }

        private ControlEntity parseControl(List<JToken> properties)
        {
            ControlEntity controlEntity = new ControlEntity();
            foreach (JToken property in properties)
            {
                if (property.GetType().Equals(typeof(JProperty)))
                {
                    JProperty prop = (JProperty)property;
                    if (prop.Name.Equals("Children"))
                    {
                        JEnumerable<JToken> children = ((JArray)prop.Value).Children();
                        foreach (JToken child in children)
                        {
                            controlEntity.Children.Add(parseControl(child.Children().ToList()));
                        }
                    }
                    else if (prop.Name.Equals("Rules"))
                    {
                        JEnumerable<JToken> children = ((JArray)prop.Value).Children();
                        foreach (JObject child in children)
                        {
                            Rule rule = new Rule();
                            foreach (JProperty ruleProp in child.Children())
                            {
                                switch (ruleProp.Name)
                                {
                                    case "Property":
                                        rule.Property = ruleProp.Value.ToString();
                                        break;
                                    case "Category":
                                        rule.Category = ruleProp.Value.ToString();
                                        break;
                                    case "RuleProviderType":
                                        rule.RuleProviderType = ruleProp.Value.ToString();
                                        break;
                                    case "InvariantScript":
                                        rule.InvariantScript = ruleProp.Value.ToString();
                                        CheckForVariables(ruleProp.Value.ToString());
                                        break;
                                }
                            }
                            controlEntity.Rules.Add(rule);
                        }
                    }
                    else
                    {
                        controlEntity.Properties.Add(Expression.parseExpressions(prop));
                        if (prop.Name.Equals("Name"))
                        {
                            controlEntity.Name = prop.Value.ToString();
                        }
                    }
                }
                else
                {
                    controlEntity.Children.Add(parseControl(((JObject)property).Children().ToList()));
                }
            }
            controlEntity.Type = controlEntity.Properties.Where(e => e.expressionOperator == "Template")?.First().expressionOperands.Cast<Expression>().First(eo => eo.expressionOperator == "Name").expressionOperands[0].ToString();
            return controlEntity;
        }

        private void CheckForVariables(string code)
        {
            //Reference: https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/working-with-variables#types-of-variables
            //check for Global Variables
            if (code.Contains("Set("))
            {
                string sub1 = code.Substring(code.IndexOf("Set(") + 4);
                string var = sub1.Substring(0, sub1.IndexOf(",")).Trim();
                currentApp.GlobalVariables.Add(var);
            }
            //check for Context Variables
            if (code.Contains("UpdateContext("))
            {
                string sub1 = code.Substring(code.IndexOf("UpdateContext(") + 14);
                string var = sub1.Substring(0, sub1.IndexOf(",")).Trim();
                currentApp.ContextVariables.Add(var);
            }
            if (code.Contains("Navigate("))
            {
                //string sub1 = code.Substring(code.IndexOf("Navigate(") + 9);
                //string var = sub1.Substring(0, sub1.IndexOf(",")).Trim();
                //NotificationHelper.SendNotification("Navigate: " + var);
            }
            //check for Collections
            if (code.Contains("Collect("))
            {
                string sub1 = code.Substring(code.IndexOf("Collect(") + 8);
                string coll = sub1.Substring(0, sub1.IndexOf(",")).Trim();
                currentApp.Collections.Add(coll);
            }
            if (code.Contains("ClearCollect("))
            {
                string sub1 = code.Substring(code.IndexOf("ClearCollect(") + 13);
                string coll = sub1.Substring(0, sub1.IndexOf(",")).Trim();
                currentApp.Collections.Add(coll);
            }
        }

        private void parseAppDataSources(string appArchive)
        {
            ZipArchiveEntry dataSourceFile = ZipHelper.getFileFromZip(appArchive, "References\\DataSources.json");
            using (StreamReader reader = new StreamReader(dataSourceFile.Open()))
            {
                string appJSON = reader.ReadToEnd();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                var _jsonSerializer = JsonSerializer.Create(settings);
                dynamic datasourceDefinition = JsonConvert.DeserializeObject<JObject>(appJSON, settings).ToObject(typeof(object), _jsonSerializer);
                foreach (JToken datasource in datasourceDefinition.DataSources.Children())
                {
                    DataSource ds = new DataSource();
                    foreach (JProperty prop in datasource.Children())
                    {
                        switch (prop.Name)
                        {
                            case "Name":
                                ds.Name = prop.Value.ToString();
                                break;
                            case "Type":
                                ds.Type = prop.Value.ToString();
                                break;
                            default:
                                ds.Properties.Add(Expression.parseExpressions(prop));
                                break;
                        }
                    }
                    currentApp.DataSources.Add(ds);
                }
            }
        }

        public List<AppEntity> getApps()
        {
            return apps;
        }
    }
}