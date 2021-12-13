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
        private dynamic appDefinition;
        private readonly List<AppEntity> apps = new List<AppEntity>();
        public PackageType packageType;

        public AppParser(string filename)
        {
            NotificationHelper.SendNotification("Processing " + filename);
            if (filename.EndsWith("zip", StringComparison.OrdinalIgnoreCase))
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
            else if (filename.EndsWith("msapp", StringComparison.OrdinalIgnoreCase))
            {
                NotificationHelper.SendNotification("Processing app " + filename);
                packageType = PackageType.AppPackage;
                AppEntity app = new AppEntity();
                parseAppProperties(filename, app);
                parseAppControls(filename, app);
                apps.Add(app);
            }
            else
            {
                NotificationHelper.SendNotification("Invalid file " + filename);
            }
        }

        private void parseAppProperties(string appArchive, AppEntity app)
        {
            string[] filesToParse = new string[] { "Resources\\PublishInfo.json", "Header.json", "Properties.json" };
            foreach (string fileToParse in filesToParse)
            {
                using (StreamReader reader = new StreamReader(ZipHelper.getFileFromZip(appArchive, fileToParse).Open()))
                {
                    string appJSON = reader.ReadToEnd();
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                    var _jsonSerializer = JsonSerializer.Create(settings);
                    appDefinition = JsonConvert.DeserializeObject<JObject>(appJSON, settings).ToObject(typeof(object), _jsonSerializer);
                    foreach (JToken property in appDefinition.Children())
                    {
                        JProperty prop = (JProperty)property;
                        app.properties.Add(Expression.parseExpressions(prop));
                        if (prop.Name.Equals("AppName"))
                        {
                            app.Name = prop.Value.ToString();
                        }
                        if (prop.Name.Equals("ID"))
                        {
                            app.ID = prop.Value.ToString();
                        }
                    }
                }
            }
        }

        private void parseAppControls(string appArchive, AppEntity app)
        {
            List<ZipArchiveEntry> controlFiles = ZipHelper.getFilesInPathFromZip(appArchive, "Controls", ".json");
            foreach (ZipArchiveEntry controlEntry in controlFiles)
            {
                using (StreamReader reader = new StreamReader(controlEntry.Open()))
                {
                    string appJSON = reader.ReadToEnd();
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                    var _jsonSerializer = JsonSerializer.Create(settings);
                    appDefinition = JsonConvert.DeserializeObject<JObject>(appJSON, settings).ToObject(typeof(object), _jsonSerializer);
                    app.controls.Add(parseControl(((JObject)appDefinition.TopParent).Children().ToList()));
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
            //TODO delete the following line later
            if (controlEntity.Properties.Count > 0)
                controlEntity.Type = controlEntity.Properties.Where(e => e.expressionOperator == "Template")?.First().expressionOperands.Cast<Expression>().First(eo => eo.expressionOperator == "Name").expressionOperands[0].ToString();
            return controlEntity;
        }

        public List<AppEntity> getApps()
        {
            return apps;
        }
    }
}