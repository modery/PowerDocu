using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using PowerDocu.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
        private List<FlowEntity> flows = new List<FlowEntity>();
        public PackageType packageType;

        public FlowParser(string filename)
        {
            Console.WriteLine("Processing " + filename);

            if (filename.ToLower().EndsWith("zip"))
            {
                List<ZipArchiveEntry> definitions = ZipHelper.getWorkflowFilesFromZip(filename);
                packageType = (definitions.Count == 1) ? PackageType.FlowPackage : PackageType.SolutionPackage;
                foreach (ZipArchiveEntry definition in definitions)
                {
                    using (StreamReader reader = new StreamReader(definition.Open()))
                    {
                        Console.WriteLine("Processing workflow definition " + definition.FullName);
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
            else if (filename.ToLower().EndsWith("json"))
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
                Console.WriteLine("Invalid file " + filename);
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
            flow.trigger.Description = triggerDetails["description"]?.ToString();
            flow.trigger.Type = triggerDetails["type"].ToString();

            if (triggerDetails["recurrence"] != null)
            {
                JObject recurrence = (JObject)triggerDetails["recurrence"];
                foreach (JProperty property in recurrence.Children())
                {
                    flow.trigger.Recurrence.Add(property.Name, property.Value.ToString());
                }
            }
            if (triggerDetails["inputs"] != null)
            {
                JObject inputs = (JObject)triggerDetails["inputs"];
                parseInputObject(inputs.Children(), flow.trigger.Inputs, ref flow.trigger.Connector);
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
                return connectionstring.Replace("/providers/Microsoft.PowerApps/apis/shared_", "");
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
                //TODO connections work, connection references don't

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
                        Name = cRefDetails["api"]["name"].ToString().Replace("shared_", ""),
                        Source = cRefDetails["runtimeSource"].ToString(),
                        ConnectionReferenceLogicalName = cRefDetails["connection"]["connectionReferenceLogicalName"].ToString(),
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
                        Name = connRef.Name.Replace("shared_", ""),
                        Source = cRefDetails["source"].ToString(),
                        Connector = extractConnectorName(cRefDetails["id"].ToString()),
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
		  */
        private void parseActions(FlowEntity flow, JEnumerable<JToken> actions, ActionNode parentAction, bool isElseActions = false)
        {

            foreach (JProperty action in actions)
            {
                JObject actionDetails = (JObject)action.Value;
                JObject runAfter = (JObject)actionDetails["runAfter"];
                ActionNode aNode = flow.actions.FindOrCreate(action.Name);
                aNode.Type = actionDetails["type"].ToString();

                if (actionDetails["expression"] != null)
                {
                    //NOTE: sometimes JObject, sometimes JValue
                    if (((JToken)actionDetails["expression"]).GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
                    {
                        aNode.Expression = actionDetails["expression"]?.ToString();
                    }
                    else if (((JToken)actionDetails["expression"]).GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
                    {
                        aNode.Expression = actionDetails["expression"]?.ToString();
                        var expressionNodes = actionDetails["expression"].Children();
                        foreach (JProperty inputNode in expressionNodes)
                        {
                            aNode.actionExpression = parseExpressions(inputNode);
                        }
                    }
                }

                if (actionDetails["inputs"] != null)
                {
                    if (((JToken)actionDetails["inputs"]).GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
                    {
                        aNode.Inputs = actionDetails["inputs"]?.ToString();
                    }
                    else if (((JToken)actionDetails["inputs"]).GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
                    {
                        var inputNodes = actionDetails["inputs"].Children();
                        parseInputObject(inputNodes, aNode.actionInputs, ref aNode.Connection);
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
                }
                //TODO: runfter can be based on different conditions, such as succeeded, failed?, others. Review if current effort is enough, or if more things need to be done
                if (runAfter.HasValues == false && parentAction == null && !flow.actions.hasRoot())
                {
                    //root node does not run after another node and is not inside
                    flow.actions.setRootNode(aNode);
                }
                else
                {
                    //not root, let's connect the runafter node and this one by adding an edge
                    var runAfterNodes = runAfter.Children();
                    foreach (JProperty raNode in runAfterNodes)
                    {
                        ActionNode runAfterNode = flow.actions.FindOrCreate(raNode.Name);
                        flow.actions.AddEdge(runAfterNode, aNode);
                    }
                }

                //subactions
                JObject subSactions = (JObject)actionDetails["actions"];
                if (subSactions != null)
                {
                    parseActions(flow, subSactions.Children(), aNode);
                }

                //elseactions
                JObject elseActions = (JObject)((JObject)actionDetails["else"])?["actions"];
                if (elseActions != null)
                {
                    parseActions(flow, elseActions.Children(), aNode, true);
                }

                //todo: currently switch actions are subactions, but we do not have any way of determining this afterwards
                if (aNode.Type == "Switch")
                {
                    JObject switchCases = (JObject)actionDetails["cases"];
                    if (switchCases != null)
                    {
                        //TODO special parsing required
                        foreach (JProperty switchCase in switchCases.Children())
                        {
                            parseActions(flow, switchCase.Value["actions"].Children(), aNode);
                        }
                    }
                    JObject defaultCase = (JObject)actionDetails["default"];
                    if (defaultCase != null)
                    {
                        parseActions(flow, defaultCase["actions"].Children(), aNode);
                    }
                }

            }
        }

        private Expression parseExpressions(JProperty jsonExpression)
        {
            Expression expression = new Expression();
            expression.expressionOperator = jsonExpression.Name.ToString();
            if (jsonExpression.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JArray)))
            {
                JArray operands = (JArray)jsonExpression.Value;
                foreach (JToken operandExpression in operands)
                {
                    if (operandExpression.GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
                    {
                        expression.expressionOperands.Add(operandExpression.ToString());
                    }
                    else if (operandExpression.GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
                    {
                        var expressionNodes = operandExpression.Children();
                        foreach (JProperty inputNode in expressionNodes)
                        {
                            expression.expressionOperands.Add(parseExpressions(inputNode));
                        }
                    }
                }
            }
            else if (jsonExpression.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
            {
                JObject expressionObject = (JObject)jsonExpression.Value;
                var expressionNodes = expressionObject.Children();
                foreach (JProperty inputNode in expressionNodes)
                {
                    expression.expressionOperands.Add(parseExpressions(inputNode));
                }
            }
            else if (jsonExpression.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
            {
                expression.expressionOperands.Add(jsonExpression.Value.ToString());
            }
            return expression;
        }

        private void parseInputObject(JEnumerable<JToken> inputNodes, List<Expression> inputList, ref string conn)
        {
            foreach (JProperty inputNode in inputNodes)
            {
                inputList.Add(parseExpressions(inputNode));
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