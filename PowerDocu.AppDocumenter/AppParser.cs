using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using PowerDocu.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

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
                parseAppResources(filename);
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
                        currentApp.Properties.Add(Expression.parseExpressions(prop));
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
            //TODO: only checks for a variable, but doesn't cater for scenarios where there are multiple Set/UpdateContext/etc. calls.
            //Reference: https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/working-with-variables#types-of-variables
            //check for Global Variables
            Match match;

            // variable assignment: Set( <ident>, <expr> )
            if ((match = Regex.Match(code, @"\s*Set\(\s*(?<ident>\w+)\s*,\s*(?<expr>.*)\)\s*")).Success)
            {
                currentApp.GlobalVariables.Add(match.Groups["ident"].Value);
            }
            //check for Context Variables
            if (code.Contains("UpdateContext("))
            {
                //will not yet work for all examples
                string sub1 = code.Substring(code.IndexOf("UpdateContext(") + 14);
                string var = sub1.Substring(0, sub1.IndexOf(",")).Trim();
                currentApp.ContextVariables.Add(var);
            }
            if (code.Contains("Navigate("))
            {
                // As an optional third argument, pass a record that contains the context-variable name as a column name and the new value for the context variable.
                /*
                string sub1 = code.Substring(code.IndexOf("Navigate(") + 9);
                string navigateString = sub1.Substring(0, sub1.IndexOf(")")).Trim();
                if (navigateString.IndexOf("(") == navigateString.LastIndexOf("("))
                {
                    //TODO this won't work for:
                    // Navigate(Screen2,ScreenTransition.Fade,{ ID: 12 , Shade: Color.Blue } )
                    string sub2 = navigateString.Substring(navigateString.LastIndexOf(","));
                    string ContextDetailsString = sub2.Substring(0, sub2.LastIndexOf(")"));
                    //currentApp.ContextVariables.Add(var);
                }
                else
                {
                    //TODO: need to think about how to properly parse this. If we reach this area, the manual parsing above didn't work, and we don't have the full Navigate(x,x,y) function
                }
                //NotificationHelper.SendNotification("Navigate: " + var);
                */
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

        private void parseAppResources(string appArchive)
        {
            ZipArchiveEntry dataSourceFile = ZipHelper.getFileFromZip(appArchive, "References\\Resources.json");
            using (StreamReader reader = new StreamReader(dataSourceFile.Open()))
            {
                string appJSON = reader.ReadToEnd();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                var _jsonSerializer = JsonSerializer.Create(settings);
                dynamic resourceDefinition = JsonConvert.DeserializeObject<JObject>(appJSON, settings).ToObject(typeof(object), _jsonSerializer);
                foreach (JToken resource in resourceDefinition.Resources.Children())
                {
                    Resource res = new Resource();
                    foreach (JProperty prop in resource.Children())
                    {
                        switch (prop.Name)
                        {
                            case "Name":
                                res.Name = prop.Value.ToString();
                                break;
                            case "Content":
                                res.Content = prop.Value.ToString();
                                break;
                            case "ResourceKind":
                                res.ResourceKind = prop.Value.ToString();
                                break;
                            default:
                                res.Properties.Add(Expression.parseExpressions(prop));
                                break;
                        }
                    }
                    currentApp.Resources.Add(res);
                }
            }
        }

        public List<AppEntity> getApps()
        {
            return apps;
        }
    }
}