using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using PowerDocu.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace PowerDocu.FlowDocumenter
{
    class FlowParser
    {
        public enum PackageType
        {
            FlowPackage,
            LogicAppsTemplate,
            SolutionPackage
        }
        private dynamic flowDefinition;
        private readonly List<FlowEntity> flows = new List<FlowEntity>();
        public PackageType packageType;

        public FlowParser(string filename)
        {
            NotificationHelper.SendNotification(" - Processing " + filename);
            if (filename.EndsWith("zip", StringComparison.OrdinalIgnoreCase))
            {
                using (FileStream stream = new FileStream(filename, FileMode.Open))
                {
                    List<ZipArchiveEntry> flowDefinitions = ZipHelper.getWorkflowFilesFromZip(stream);
                    //Getting any potential app definitions as well, so that we can define if the package is a simple Flow (only 1 FLow inside) or a Solution
                    List<ZipArchiveEntry> appDefinitions = ZipHelper.getFilesInPathFromZip(stream, "", ".msapp");
                    packageType = (flowDefinitions.Count == 1 && appDefinitions.Count == 0) ? PackageType.FlowPackage : PackageType.SolutionPackage;
                    foreach (ZipArchiveEntry definition in flowDefinitions)
                    {
                        using (StreamReader reader = new StreamReader(definition.Open()))
                        {
                            NotificationHelper.SendNotification("  - Processing workflow definition " + definition.FullName);
                            string definitionContent = reader.ReadToEnd();
                            FlowEntity flow = parseFlow(definitionContent);
                            if (String.IsNullOrEmpty(flow.Name))
                            {
                                flow.Name = definition.Name.Replace(".json", "");
                            }
                            flows.Add(flow);
                        }
                    }
                }
            }
            else if (filename.EndsWith("json", StringComparison.OrdinalIgnoreCase))
            {
                packageType = PackageType.LogicAppsTemplate;
                //TODO currently working with definition.json files, but need to consider logic app templates as a next step
                // not parsing at the moment until it's been updated to work with Logic Apps       
                /*FlowEntity flow = parseFlow(File.ReadAllText(filename));
                if (String.IsNullOrEmpty(flow.Name))
                {
                    flow.Name = filename.Replace(".json", "");
                }
                flows.Add(flow);*/
            }
            else
            {
                NotificationHelper.SendNotification("Invalid file " + filename);
            }
        }

        /**
		  * This function takes a Flow's JSON definition and parses it for further processing
		  */
        private FlowEntity parseFlow(string flowJSON)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
            var _jsonSerializer = JsonSerializer.Create(settings);
            flowDefinition = JsonConvert.DeserializeObject<JObject>(flowJSON, settings).ToObject(typeof(object), _jsonSerializer);
            FlowEntity flow = new FlowEntity();
            parseMetadata(flow);
            parseTrigger(flow);
            parseActions(flow, flowDefinition.properties.definition.actions.Children(), null);
            parseConnectionReferences(flow);
            return flow;
        }

        /**
		  *
		  */
        private void parseMetadata(FlowEntity flow)
        {
            flow.ID = flowDefinition.name;
            flow.Name = flowDefinition.properties.displayName;
        }

        /**
		  *
		  */
        private void parseTrigger(FlowEntity flow)
        {
            JProperty trigger = (JProperty)((JObject)flowDefinition.properties.definition.triggers).First;

            flow.addTrigger(trigger.Name);
            JObject triggerDetails = (JObject)trigger.Value;
            foreach (JProperty property in triggerDetails.Children())
            {
                switch (property.Name)
                {
                    case "description":
                        flow.trigger.Description = property.Value.ToString();
                        break;

                    case "type":
                        flow.trigger.Type = property.Value.ToString();
                        break;

                    case "recurrence":
                        foreach (JProperty recurrenceitem in property.Value.Children())
                        {
                            flow.trigger.Recurrence.Add(recurrenceitem.Name, recurrenceitem.Value.ToString());
                        }
                        break;

                    case "inputs":
                        JObject inputs = (JObject)property.Value;
                        parseInputObject(inputs.Children(), flow.trigger.Inputs, ref flow.trigger.Connector);
                        break;
                    default:
                        flow.trigger.TriggerProperties.Add(Expression.parseExpressions(property));
                        break;
                }
            }
        }

        private string extractConnectorName(string connectionstring)
        {
            if (connectionstring.StartsWith("@"))
            {
                return connectionstring.Replace("@parameters('$connections')['shared_", "").Replace("']['connectionId']", "");
            }
            else
            {
                return connectionstring.Replace("/providers/Microsoft.PowerApps/apis/shared_", "").Split("_")[0];
            }
        }

        /**
		  *
		  */
        private void parseConnectionReferences(FlowEntity flow)
        {
            var connectionReferences = flowDefinition.properties.connectionReferences.Children();
            foreach (JProperty connRef in connectionReferences)
            {
                JObject cRefDetails = (JObject)connRef.Value;
                //if it's a connection reference
                /* Example:
                {"shared_commondataserviceforapps": {
                    "api": {
                        "name": "shared_commondataserviceforapps"
                    },
                    "connection": {
                        "connectionReferenceLogicalName": "admin_CoECoreDataverse"
                    },
                    "runtimeSource": "embedded"
                }}*/
                if (cRefDetails["api"] != null)
                {
                    flow.connectionReferences.Add(new ConnectionReference()
                    {
                        Name = extractConnectorName(cRefDetails["api"]["name"].ToString()),
                        Source = cRefDetails["runtimeSource"].ToString(),
                        ConnectionReferenceLogicalName = cRefDetails["connection"]?["connectionReferenceLogicalName"]?.ToString(),
                        Type = ConnectionType.ConnectorReference
                    });
                }
                //if it's a connector
                /* Example:
                "shared_office365": {
                    "connectionName": "b612ac9b883942f8bb974a9581ed70c1",
                    "source": "Embedded",
                    "id": "/providers/Microsoft.PowerApps/apis/shared_office365",
                    "tier": "NotSpecified"
                }*/
                if (cRefDetails["connectionName"] != null)
                {
                    flow.connectionReferences.Add(new ConnectionReference()
                    {
                        Name = extractConnectorName(cRefDetails["id"].ToString()),
                        Source = cRefDetails["source"].ToString(),
                        ID = connRef.Name,
                        Type = ConnectionType.Connector
                    });
                }
            }
        }

        /**
		  * actions - list of JSON actions
		  * parentAction - the parent action of the current list of actions
		  * isElseActions - bool that defines if the set of actions is in a Else area (Or a "No" side of a Yes/No decision)
          * switchValue - value of a Switch condition
		  */
        private void parseActions(FlowEntity flow, JEnumerable<JToken> actions, ActionNode parentAction, bool isElseActions = false, string switchValue = null)
        {
            foreach (JProperty action in actions)
            {
                JObject actionDetails = (JObject)action.Value;
                ActionNode aNode = flow.actions.FindOrCreate(action.Name);
                foreach (JProperty property in actionDetails.Children())
                {
                    switch (property.Name)
                    {
                        case "expression":
                            //NOTE: sometimes JObject, sometimes JValue
                            if (property.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
                            {
                                aNode.Expression = property.Value?.ToString();
                            }
                            else if (property.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
                            {
                                aNode.Expression = property.Value?.ToString();
                                var expressionNodes = property.Value.Children();
                                foreach (JProperty inputNode in expressionNodes)
                                {
                                    aNode.actionExpression = Expression.parseExpressions(inputNode);
                                }
                            }
                            break;
                        case "inputs":
                            if (property.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
                            {
                                aNode.Inputs = property.Value?.ToString();
                            }
                            else if (property.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
                            {
                                var inputNodes = property.Value.Children();
                                parseInputObject(inputNodes, aNode.actionInputs, ref aNode.Connection);
                            }
                            break;
                        case "actions":
                            parseActions(flow, property.Value.Children(), aNode);
                            break;
                        case "cases":
                            //TODO special parsing required
                            foreach (JProperty switchCase in property.Value.Children())
                            {
                                parseActions(flow, switchCase.Value["actions"].Children(), aNode, false, switchCase.Value["case"].ToString());
                            }
                            break;
                        case "default":
                            parseActions(flow, property.Value["actions"].Children(), aNode, false, "default");
                            break;
                        case "runAfter":
                            //TODO: runfter can be based on different conditions, such as succeeded, failed?, others. Review if current effort is enough, or if more things need to be done
                            if (!property.Value.HasValues && parentAction == null && !flow.actions.hasRoot())
                            {
                                //root node does not run after another node and is not inside
                                flow.actions.setRootNode(aNode);
                            }
                            else
                            {
                                //not root, let's connect the runafter node and this one by adding an edge
                                var runAfterNodes = property.Value.Children();
                                foreach (JProperty raNode in runAfterNodes)
                                {
                                    ActionNode runAfterNode = flow.actions.FindOrCreate(raNode.Name);
                                    //array can contain Failed, Succeeded, Skipped, TimedOut
                                    string[] raConditionsArray = raNode.Children().FirstOrDefault()?.ToObject<string[]>();
                                    flow.actions.AddEdge(runAfterNode, aNode, raConditionsArray);
                                }
                            }
                            break;
                        case "else":
                            JObject elseActions = (JObject)property.Value["actions"];
                            if (elseActions != null)
                            {
                                parseActions(flow, elseActions.Children(), aNode, true);
                            }
                            break;
                        case "type":
                            aNode.Type = property.Value.ToString();
                            break;
                        case "description":
                            aNode.Description = property.Value.ToString();
                            break;
                        case "foreach":
                            //TODO
                            //{"foreach": "@outputs('List_Environment_Capacity_information')?['body/value']"}
                            //{"foreach": "@body('Get_calendar_view_of_events_(V2)')?['value']"}
                            //{"foreach": "@items('Apply_to_each_Environment')?['properties/capacity']"}
                            //{"foreach": "@variables('Apps_Editted')"}
                            break;
                        case "runtimeConfiguration":
                            //TODO
                            // {"runtimeConfiguration": {"paginationPolicy": {"minimumItemCount": 100000}}}
                            break;
                        case "kind":
                            //TODO
                            //{"kind": "GetPastTime"}
                            //{"kind": "PowerApp"}
                            //{"kind": "Http"}
                            break;
                        case "metadata":
                            //TODO
                            break;
                        case "limit":
                            //TODO
                            // {"limit": {"count": 240,"timeout": "P10D"}}
                            //{"limit": {"count": 60,"timeout": "PT1H"}}
                            break;
                        case "operationOptions":
                            //TODO
                            //{"operationOptions": "DisableAsyncPattern"}
                            break;
                        case "trackedProperties":
                            //TODO
                            //{"trackedProperties": {"12": 12,"23": 23}}
                            break;
                        default:
                            break;
                    }
                }

                if (parentAction != null)
                {
                    if (isElseActions)
                    {
                        parentAction.AddElseaction(aNode);
                    }
                    else
                    {
                        parentAction.AddSubaction(aNode);
                    }
                    if (switchValue != null)
                    {
                        parentAction.switchRelationship.Add(aNode, switchValue);
                    }
                }
            }
        }

        private void parseInputObject(JEnumerable<JToken> inputNodes, List<Expression> inputList, ref string conn)
        {
            foreach (JProperty inputNode in inputNodes)
            {
                inputList.Add(Expression.parseExpressions(inputNode));
                //If the node's name = host then there are details about the connection used inside
                if (inputNode.Name == "host")
                {
                    //this is not a nice way, but works so far
                    //exported Flow
                    JToken connectionToken = (JToken)inputNode.Value["connection"]?["name"];
                    if (connectionToken == null)
                    {
                        //seen as part of a solution
                        connectionToken = (JToken)inputNode.Value["apiId"];
                    }
                    if (connectionToken != null)
                    {
                        conn = extractConnectorName(connectionToken.ToString());
                    }
                }
            }
        }

        public List<FlowEntity> getFlows()
        {
            return flows;
        }
    }
}