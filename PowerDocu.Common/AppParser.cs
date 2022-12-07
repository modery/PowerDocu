using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace PowerDocu.Common
{
    public class AppParser
    {
        public enum PackageType
        {
            AppPackage,
            SolutionPackage
        }
        private readonly List<AppEntity> apps = new List<AppEntity>();
        private readonly AppEntity currentApp;
        public PackageType packageType;

        public AppParser(string filename)
        {
            NotificationHelper.SendNotification(" - Processing " + filename);
            if (filename.EndsWith(".zip"))
            {
                using FileStream stream = new FileStream(filename, FileMode.Open);
                List<ZipArchiveEntry> definitions = ZipHelper.getFilesInPathFromZip(stream, "", ".msapp");
                packageType = PackageType.SolutionPackage;
                foreach (ZipArchiveEntry definition in definitions)
                {
                    string tempFile = Path.GetDirectoryName(filename) + @"\" + definition.Name;
                    definition.ExtractToFile(tempFile, true);
                    NotificationHelper.SendNotification("  - Processing app " + definition.FullName);
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
            else if (filename.EndsWith(".msapp"))
            {
                NotificationHelper.SendNotification("  - Processing app " + filename);
                packageType = PackageType.AppPackage;
                AppEntity app = new AppEntity();
                currentApp = app;
                using FileStream stream = new FileStream(filename, FileMode.Open);
                parseAppProperties(stream);
                parseAppControls(stream);
                parseAppDataSources(stream);
                parseAppResources(stream);
                apps.Add(app);
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
                using StreamReader reader = new StreamReader(ZipHelper.getFileFromZip(appArchive, fileToParse).Open());
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
                    if (prop.Name.Equals("LogoFileName") && !String.IsNullOrEmpty(prop.Value.ToString()))
                    {
                        ZipArchiveEntry resourceFile = ZipHelper.getFileFromZip(appArchive, "Resources\\" + prop.Value.ToString());
                        MemoryStream ms = new MemoryStream();
                        resourceFile.Open().CopyTo(ms);
                        currentApp.ResourceStreams.Add(prop.Value.ToString(), ms);
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
                using StreamReader reader = new StreamReader(controlEntry.Open());
                string appJSON = reader.ReadToEnd();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
                var _jsonSerializer = JsonSerializer.Create(settings);
                dynamic controlsDefinition = JsonConvert.DeserializeObject<JObject>(appJSON, settings).ToObject(typeof(object), _jsonSerializer);
                currentApp.Controls.Add(parseControl(((JObject)controlsDefinition.TopParent).Children().ToList()));
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
                                        CheckForVariables(controlEntity, ruleProp.Value.ToString());
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
            //for containers, there are a few different types (Contaner, Horizontal Container, Vertical Container) which are defined by the "VariantName" property
            if (controlEntity.Type.Equals("groupContainer"))
            {
                controlEntity.Type = controlEntity.Properties.First(o => o.expressionOperator.Equals("VariantName")).expressionOperands[0].ToString();
            }
            //components can safely be identified through the Id of the template
            string controlId = controlEntity.Properties.Where(e => e.expressionOperator == "Template")?.First().expressionOperands.Cast<Expression>().First(eo => eo.expressionOperator == "Id").expressionOperands[0].ToString();
            if (controlId.Equals("http://microsoft.com/appmagic/Component"))
            {
                controlEntity.Type = "component";
            }
            return controlEntity;
        }

        private void CheckVariableUsage(ControlEntity control)
        {
            foreach (Rule rule in control.Rules)
            {
                foreach (var globalVar in currentApp.GlobalVariables)
                {
                    //if (rule.InvariantScript.Contains(globalVar))
                    if (containsVariable(rule.InvariantScript, globalVar))
                    {
                        addVariableControlMapping(globalVar, control, rule.Property);
                    }
                }
                foreach (var contextVar in currentApp.ContextVariables)
                {
                    //if (rule.InvariantScript.Contains(contextVar))
                    if (containsVariable(rule.InvariantScript, contextVar))
                    {
                        addVariableControlMapping(contextVar, control, rule.Property);
                    }
                }
                foreach (var collection in currentApp.Collections)
                {
                    // if (rule.InvariantScript.Contains(collection))
                    if (containsVariable(rule.InvariantScript, collection))
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

        private bool containsVariable(string script, string var)
        {
            char[] validAfterChars = { ' ', ',', ')', '.', '+', '-', '/', '*', '=', ':', '}', '\n', '\r', '&' };
            char[] validPreChars = { ' ', '(', '+', '-', '/', '*', '=', ',', '{', '\n', '\r', '&' };
            if (script.Contains(var))
            {
                if (script.StartsWith(var) || validPreChars.Contains(script[script.IndexOf(var) - 1]))
                {
                    if ((script.Length == (script.IndexOf(var) + var.Length)) || validAfterChars.Contains(script[script.IndexOf(var) + var.Length]))
                    {
                        return true;
                    }
                    else
                    {
                        //not in use at the moment
                    }
                }
                else
                {
                    //not in use at the moment
                }
            }
            return false;
        }

        private void addVariableControlMapping(string globalVar, ControlEntity control, string property)
        {
            if (currentApp.VariableCollectionControlReferences.ContainsKey(globalVar))
            {
                currentApp.VariableCollectionControlReferences[globalVar].Add(new ControlPropertyReference() { Control = control, RuleProperty = property });
            }
            else
            {
                List<ControlPropertyReference> list = new List<ControlPropertyReference>
                {
                    new ControlPropertyReference() { Control = control, RuleProperty = property }
                };
                currentApp.VariableCollectionControlReferences.Add(globalVar, list);
            }
        }

        private void addScreenNavigation(ControlEntity controlEntity, string destinationScreen)
        {
            if (currentApp.ScreenNavigations.ContainsKey(controlEntity))
            {
                currentApp.ScreenNavigations[controlEntity].Add(destinationScreen);
            }
            else
            {
                List<string> list = new List<string>
                {
                    destinationScreen
                };
                currentApp.ScreenNavigations.Add(controlEntity, list);
            }
        }

        private void CheckForVariables(ControlEntity controlEntity, string input)
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
            string codeWithSpacesRemoved = code.Replace(" ", "");
            if (codeWithSpacesRemoved.Contains("UpdateContext("))
            {
                List<int> indexes = findAllIndexesOf(codeWithSpacesRemoved, "UpdateContext(");
                foreach (int index in indexes)
                {
                    foreach (string var in extractContextVariableNames(codeWithSpacesRemoved[index..]))
                    {
                        currentApp.ContextVariables.Add(var);
                    }
                }
            }
            if (codeWithSpacesRemoved.Contains("Navigate("))
            {
                // As an optional third argument, pass a record that contains the context-variable name as a column name and the new value for the context variable.
                List<int> indexes = findAllIndexesOf(codeWithSpacesRemoved, "Navigate(");
                foreach (int index in indexes)
                {
                    string firstParam = "";
                    string secondParam = "";
                    string thirdParam = "";
                    string navigateString = codeWithSpacesRemoved[index..];
                    navigateString = navigateString[0..(findClosingCharacter(navigateString, '(', ')'))].Replace("Navigate(", "");

                    firstParam = extractNavigateParam(navigateString);
                    if (firstParam != navigateString)
                    {
                        string navigateStringWithoutFirstParam = navigateString[(firstParam.Length + 1)..];
                        secondParam = extractNavigateParam(navigateStringWithoutFirstParam);
                        if (secondParam != navigateStringWithoutFirstParam)
                        {
                            //there's a third parameter!
                            thirdParam = navigateStringWithoutFirstParam[(secondParam.Length + 1)..];
                            foreach (string var in extractContextVariableNames(thirdParam))
                            {
                                currentApp.ContextVariables.Add(var);
                            }
                        }
                        else
                        {
                            //only 2 parameters; nothing to do here at the moment
                        }
                    }
                    else
                    {
                        //a single parameter only; nothing to do here at the moment
                    }
                    addScreenNavigation(controlEntity, firstParam);
                }
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

        private string extractNavigateParam(string navigateString)
        {
            //at least 2 commands found. We may or may not have 3 parameters in use here
            int closingBracketIndex;
            //find the first parameter: find the first comma (potentially after brackets)
            char openingCharacter = 'C';
            if (navigateString.IndexOf("(") == -1)
            {
                if (navigateString.IndexOf("{") > -1)
                {
                    openingCharacter = '{';
                }
            }
            else
            {
                if (navigateString.IndexOf("{") > -1)
                {
                    if (navigateString.IndexOf("(") < navigateString.IndexOf("{"))
                    {
                        openingCharacter = '(';
                    }
                    else
                    {
                        openingCharacter = '{';
                    }
                }
                else
                {
                    openingCharacter = '(';
                }
            }
            if (openingCharacter == 'C' || (navigateString.IndexOf(',') < navigateString.IndexOf(openingCharacter)))
            {
                //no brackets for first parameter, so we can use it directly
                if (navigateString.IndexOf(',') > -1)
                {
                    return navigateString[0..navigateString.IndexOf(',')];
                }
                //no commas at all? return it directly
                return navigateString;
            }
            else
            {
                //brackets found. Need to find the first comma after the closing bracket
                char closingCharacter = (openingCharacter == '(') ? ')' : '}';
                closingBracketIndex = findClosingCharacter(navigateString, openingCharacter, closingCharacter);
                int commaPos = navigateString.IndexOf(',', closingBracketIndex);
                if (commaPos > -1)
                {
                    //comma after closing character found!
                    return navigateString[0..commaPos];
                }
                else
                {
                    // we may end up here if there is a lot of code but no commas after 
                    //example: "Navigate(If(2>1,Screen2,Screen2))"
                    return navigateString;
                }
            }
        }

        private List<string> extractContextVariableNames(string code)
        {
            //todo: issue: code="If(2>1,{e:12},{e:43244})";
            //code = "UpdateContext({Person:{Name:\"Milton\",Address:\"1MainSt\",Phone:123456789},Cat:{Name:\"Fluffy\",Age:11,Owner:{Name:\"Rene\",Phone:\"+6512334\"}}})";
            List<string> extractedVariables = new List<string>();
            string checkVariable = code;
            if (code.StartsWith("UpdateContext"))
            {
                string variableStart = code[code.IndexOf('{')..];
                //need to find the closing bracket for UpdateContext
                int closingBracketIndex = findClosingCharacter(variableStart, '{', '}');

                //we found the end of the current UpdateContext/Navigate. Time to extract the variable names defined here
                checkVariable = (closingBracketIndex > 0) ? variableStart[..(closingBracketIndex + 1)] : variableStart;
            }
            if (checkVariable[0] == '{' && checkVariable[checkVariable.Length - 1] == '}')
            {
                checkVariable = checkVariable[1..(checkVariable.Length - 1)];
            }

            while (checkVariable.Contains(":"))
            {
                if (checkVariable.Contains('{') || checkVariable.Contains('('))
                {
                    int firstCurlyBracketIndex = checkVariable.IndexOf('{');
                    int firstRoundBracketIndex = checkVariable.IndexOf('(');
                    int firstCommaIndex = checkVariable.IndexOf(',');
                    int closingCharacterIndex;
                    if (firstCurlyBracketIndex > -1 && ((firstRoundBracketIndex == -1) || (firstCurlyBracketIndex < firstRoundBracketIndex)))
                    {
                        if ((firstCommaIndex == -1) || (firstCommaIndex > -1 && (firstCurlyBracketIndex < firstCommaIndex)))
                        {
                            closingCharacterIndex = findClosingCharacter(checkVariable, '{', '}');
                        }
                        else
                        {
                            closingCharacterIndex = firstCommaIndex;
                        }
                    }
                    else
                    {
                        if ((firstCommaIndex == -1) || (firstCommaIndex > -1 && (firstRoundBracketIndex < firstCommaIndex)))
                        {
                            closingCharacterIndex = findClosingCharacter(checkVariable, '(', ')');
                        }
                        else
                        {
                            closingCharacterIndex = firstCommaIndex;
                        }
                    }
                    extractedVariables.Add(checkVariable[0..closingCharacterIndex].Split(":")[0].Trim().Replace("{", ""));
                    checkVariable = checkVariable[(closingCharacterIndex + ((closingCharacterIndex >= firstCommaIndex) ? 1 : 2))..];
                    if (checkVariable.Length > 0 && checkVariable[0] == ',') checkVariable = checkVariable[1..];
                }
                else
                {
                    foreach (string variableSection in checkVariable.Split(','))
                    {
                        //direct assignment of variables, they can be extracted right away
                        extractedVariables.Add(variableSection.Split(':')[0].Trim().Replace("{", ""));
                    }
                    checkVariable = "";
                }
            }
            return extractedVariables;
        }

        private int findClosingCharacter(string content, char open, char close)
        {
            bool closingBracketFound = false;
            int currentClosingBracketIndex = content.IndexOf(close);
            if (currentClosingBracketIndex != -1)
            {
                while (!closingBracketFound)
                {
                    if (findAllIndexesOf(content[0..(currentClosingBracketIndex + 1)], open.ToString()).Count == findAllIndexesOf(content[0..(currentClosingBracketIndex + 1)], close.ToString()).Count)
                    {
                        closingBracketFound = true;
                    }
                    else
                    {
                        currentClosingBracketIndex = content[(currentClosingBracketIndex + 1)..].IndexOf(close) + currentClosingBracketIndex + 1;
                    }
                }
            }
            return currentClosingBracketIndex;
        }

        private void parseAppDataSources(Stream appArchive)
        {
            ZipArchiveEntry dataSourceFile = ZipHelper.getFileFromZip(appArchive, "References\\DataSources.json");
            using StreamReader reader = new StreamReader(dataSourceFile.Open());
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

        private void parseAppResources(Stream appArchive)
        {
            string[] ResourceExtensions = new string[] { "jpg", "jpeg", "gif", "png", "bmp", "tif", "tiff", "svg" };
            ZipArchiveEntry dataSourceFile = ZipHelper.getFileFromZip(appArchive, "References\\Resources.json");
            using StreamReader reader = new StreamReader(dataSourceFile.Open());
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
                    string extension = pathToResource[(pathToResource.LastIndexOf('.') + 1)..].ToLower();
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

        public List<AppEntity> getApps()
        {
            return apps;
        }

        public List<int> findAllIndexesOf(string str, string value)
        {
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}