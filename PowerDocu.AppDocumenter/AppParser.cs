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
            if (filename.EndsWith(".zip"))
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                {
                    List<ZipArchiveEntry> definitions = ZipHelper.getFilesInPathFromZip(stream, "", ".msapp");
                    packageType = PackageType.SolutionPackage;
                    foreach (ZipArchiveEntry definition in definitions)
                    {
                        string tempFile = Path.GetDirectoryName(filename) + @"\" + definition.Name;
                        definition.ExtractToFile(tempFile);
                        NotificationHelper.SendNotification("Processing app " + definition.FullName);
                        using (FileStream appDefinition = new FileStream(tempFile, FileMode.Open))
                        {
                            {
                                AppEntity app = new AppEntity();
                                currentApp = app;
                                parseAppProperties(appDefinition);
                                parseAppControls(appDefinition);
                                parseAppDataSources(appDefinition);
                                parseAppResources(appDefinition);
                                apps.Add(app);
                            }
                        }
                        File.Delete(tempFile);
                    }
                }
            }
            else if (filename.EndsWith(".msapp"))
            {
                NotificationHelper.SendNotification("Processing app " + filename);
                packageType = PackageType.AppPackage;
                AppEntity app = new AppEntity();
                currentApp = app;
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                {
                    parseAppProperties(stream);
                    parseAppControls(stream);
                    parseAppDataSources(stream);
                    parseAppResources(stream);
                    apps.Add(app);
                }
            }
            else
            {
                NotificationHelper.SendNotification("Invalid file " + filename);
            }
        }

        private void parseAppProperties(Stream appArchive)
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

        private void parseAppControls(Stream appArchive)
        {
            List<ZipArchiveEntry> controlFiles = ZipHelper.getFilesInPathFromZip(appArchive, "Controls", ".json");
            //parse the controls. each controlFile represents a screen
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
            foreach (ControlEntity control in currentApp.Controls)
            {
                CheckVariableUsage(control);
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
                            ControlEntity childControLEntity = parseControl(child.Children().ToList());
                            controlEntity.Children.Add(childControLEntity);
                            childControLEntity.Parent = controlEntity;
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
                    ControlEntity child = parseControl(((JObject)property).Children().ToList());
                    controlEntity.Children.Add(child);
                    child.Parent = controlEntity;
                }
            }
            controlEntity.Type = controlEntity.Properties.Where(e => e.expressionOperator == "Template")?.First().expressionOperands.Cast<Expression>().First(eo => eo.expressionOperator == "Name").expressionOperands[0].ToString();
            return controlEntity;
        }

        private void CheckVariableUsage(ControlEntity control)
        {
            foreach (Rule rule in control.Rules)
            {
                foreach (var globalVar in currentApp.GlobalVariables)
                {
                    if (rule.InvariantScript.Contains(globalVar))
                    {
                        addVariableControlMapping(globalVar, control, rule.Property);
                    }
                }
                foreach (var collection in currentApp.Collections)
                {
                    if (rule.InvariantScript.Contains(collection))
                    {
                        addVariableControlMapping(collection, control, rule.Property);
                    }
                }
            }
            foreach (ControlEntity child in control.Children)
            {
                CheckVariableUsage(child);
            }
            // TODO also check  Properties ?
        }

        private void addVariableControlMapping(string globalVar, ControlEntity control, string property)
        {
            if (currentApp.VariableCollectionControlReferences.ContainsKey(globalVar))
            {
                currentApp.VariableCollectionControlReferences[globalVar].Add(new ControlPropertyReference() { Control = control, RuleProperty = property });
            }
            else
            {
                List<ControlPropertyReference> list = new List<ControlPropertyReference>();
                list.Add(new ControlPropertyReference() { Control = control, RuleProperty = property });
                currentApp.VariableCollectionControlReferences.Add(globalVar, list);
            }
        }

        private void CheckForVariables(string input)
        {
            //Reference: https://docs.microsoft.com/en-us/powerapps/maker/canvas-apps/working-with-variables#types-of-variables

            string code = input.Replace("\n", "").Replace("\r", "");
            MatchCollection matches;
            //check for Global Variables            
            if ((matches = Regex.Matches(code, @"\s*Set\(\s*(?<ident>\w+)\s*,")).Count > 0)
            {
                foreach (Match match in matches)
                {
                    currentApp.GlobalVariables.Add(match.Groups["ident"].Value);
                }
            }
            //check for Context Variables

            //Note: this is way more complex than the others. Not in use for the moment until an acceptable solution has been found
            if (code.Contains("UpdateContext("))
            {
                // Using http://regex.inginf.units.it/ , which helped to generate some of the more complex regexes. Partial success so far
                //This captures the first instance
                //(?<=UpdateContext\(\{)\w+

                // (?<=UpdateContext\(\{)\w+|(?<=\d[^_]\w[^_],)\w+|[r-t]\w\d\w\d\w+|(?<=,)\w\w[A-Za-z]\w+(?=:\w+,)

                // 37 samples. 33 correct, 4 incomplete, 0 wrong
                // (?<=UpdateContext\(\{)\w+|(?<=[^a-i]\w[^_][^k-p][^r-v],)(?<=[^a-i][^_],)\w\w\w\w+|(?<=,)([UpdateContext]?\w[UpdateContext])+(?=:)

                //46 samples. 32 correct, 12 incomplete, 2 wrong
                // (?<=UpdateContext\(\{)\w+|(?<=\d[^}][^}][^_],)\w+|(?<=,)\w\w\w\w+(?=:\w+,)|(?<=,)(?:\w(?:\w\w)+(?=:))+(?=:"[A-Za-z])

                /* if ((matches = Regex.Matches(code.Replace(" ", ""), @"(?<=UpdateContext\(\{)\w+|(?<=,)[A-Za-z]+\w+|(?<=,)\w+(?=:\d+}\))|(?<=[^a-e][^}]\d[^_],)[A-Za-z]+(?=:)")).Count > 0)
                 {
                     foreach (Match match in matches)
                     {
                         NotificationHelper.SendNotification("UC2 :" + match.Groups[0].Value);
                         currentApp.ContextVariables.Add(match.Groups[0].Value);
                     }
                 }*/
            }
            if (code.Contains("Navigate("))
            {
                // As an optional third argument, pass a record that contains the context-variable name as a column name and the new value for the context variable.
                // more complex to parse, to be implemented later
            }
            //check for Collections
            if ((matches = Regex.Matches(code, @"\s*Collect\(\s*(?<ident>\w+)\s*,\s*")).Count > 0)
            {
                foreach (Match match in matches)
                {
                    currentApp.Collections.Add(match.Groups["ident"].Value);
                }
            }

            if ((matches = Regex.Matches(code, @"\s*ClearCollect\(\s*(?<ident>\w+)\s*,\s*")).Count > 0)
            {
                foreach (Match match in matches)
                {
                    currentApp.Collections.Add(match.Groups["ident"].Value);
                }
            }
        }

        private void parseAppDataSources(Stream appArchive)
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

        private void parseAppResources(Stream appArchive)
        {
            string[] ResourceExtensions = new string[] { "jpg", "jpeg", "gif", "png", "bmp", "tif", "tiff", "svg" };
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
                    string pathToResource = "";
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
                            case "Path":
                                pathToResource = prop.Value.ToString();
                                break;
                            default:
                                res.Properties.Add(Expression.parseExpressions(prop));
                                break;
                        }
                    }
                    currentApp.Resources.Add(res);
                    if (res.ResourceKind == "LocalFile")
                    {
                        string extension = pathToResource.Substring(pathToResource.LastIndexOf('.') + 1).ToLower();
                        if (ResourceExtensions.Contains(extension))
                        {
                            ZipArchiveEntry resourceFile = ZipHelper.getFileFromZip(appArchive, pathToResource);
                            MemoryStream ms = new MemoryStream();
                            resourceFile.Open().CopyTo(ms);
                            currentApp.ResourceStreams.Add(res.Name, ms);
                        }
                    }
                }
            }
        }

        public List<AppEntity> getApps()
        {
            return apps;
        }
    }
}