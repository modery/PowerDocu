using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using PowerDocu.Common;
using Newtonsoft.Json.Linq;

namespace PowerDocu.FlowDocumenter
{
    class FlowParser
    {
        private dynamic flowDefinition;
        private List<FlowEntity> flows = new List<FlowEntity>();

        public FlowParser(string filename)
        {
            Console.Write("Processing " + filename);

            if (filename.ToLower().EndsWith("zip"))
            {
                List<ZipArchiveEntry> definitions = ZipHelper.getFilesFromZip(filename, ZipHelper.FlowDefinitionFile);
                foreach (ZipArchiveEntry definition in definitions)
                {
                    using (StreamReader reader = new StreamReader(definition.Open()))
                    {
                        Console.WriteLine("Processing workflow definition " + definition.FullName);
                        string definitionContent = reader.ReadToEnd();
                        parseFlow(definitionContent);
                    }
                }
            }
            else if (filename.ToLower().EndsWith("json"))
            {
                //TODO currently working with definition.json files, but need to consider logic app templates
                parseFlow(File.ReadAllText(filename));
            }
            else
            {
                Console.WriteLine("Invalid file " + filename);
            }

        }

        /**
		  * This function takes a Flow's JSON definition and parses it for further processing
		  */
        private void parseFlow(string flowJSON)
        {
            flowDefinition = JObject.Parse(flowJSON);
            FlowEntity flow = new FlowEntity();
            parseMetadata(flow);
            parseTrigger(flow);
            parseActions(flow, flowDefinition.properties.definition.actions.Children(), null);
            parseConnectionReferences(flow);
            flows.Add(flow);
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
            var dtriggers = flowDefinition.properties.definition.triggers.Children();
            //only one trigger, need to revisit at some point to make it cleaner
            foreach (JProperty trigger in dtriggers)
            {
                flow.addTrigger(trigger.Name);
                JObject triggerDetails = (JObject)trigger.Value;
                flow.trigger.Description = triggerDetails["description"]?.ToString();
                flow.trigger.Type = triggerDetails["type"].ToString();

                //the following 2 IFs could be turned into their own function
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
                    foreach (JProperty property in inputs.Children())
                    {
                        flow.trigger.Recurrence.Add(property.Name, property.Value.ToString());
                    }
                }

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
                flow.connectionReferences.Add(new ConnectionReference(connRef.Name.Replace("shared_", ""), cRefDetails["source"].ToString(), cRefDetails["id"].ToString(), cRefDetails["connectionName"].ToString()));
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
                //TODO better expression parsing
                if (actionDetails["expression"] != null)
                {
                    //NOTE: sometimes JObject, sometimes JValue
                    if (((JToken)actionDetails["expression"]).GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
                    {
                        aNode.Expression = actionDetails["expression"]?.ToString();
                    }
                    else if (((JToken)actionDetails["expression"]).GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
                    {
                        //TODO better expressions parsing
                        aNode.Expression = actionDetails["expression"]?.ToString();
                        var expressionNodes = actionDetails["expression"].Children();
                        foreach (JProperty inputNode in expressionNodes)
                        {
                            //there should be only one node here, code can be improved
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
                        //TODO better expressions parsing
                        aNode.Inputs = actionDetails["inputs"]?.ToString();
                        var inputNodes = actionDetails["inputs"].Children();
                        foreach (JProperty inputNode in inputNodes)
                        {
                            //there should be only one node here, code can be improved
                            //also: preparing for input parsing similar to expressions
                            aNode.actionInputs.Add(new ActionInput(inputNode.Name, inputNode.Value.ToString()));

                        }
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

        private ActionExpression parseExpressions(JProperty expression)
        {
            ActionExpression actionExpression = new ActionExpression();
            actionExpression.expressionOperator = expression.Name.ToString();
            if (expression.Value.GetType().Equals(typeof(Newtonsoft.Json.Linq.JArray)))
            {
                JArray operands = (JArray)expression.Value;
                foreach (JToken operandExpression in operands)
                {
                    if (operandExpression.GetType().Equals(typeof(Newtonsoft.Json.Linq.JValue)))
                    {
                        actionExpression.experessionOperands.Add(operandExpression.ToString());
                    }
                    else if (operandExpression.GetType().Equals(typeof(Newtonsoft.Json.Linq.JObject)))
                    {
                        var expressionNodes = operandExpression.Children();
                        foreach (JProperty inputNode in expressionNodes)
                        {
                            actionExpression.experessionOperands.Add(parseExpressions(inputNode));
                        }
                    }
                }
            }
            return actionExpression;
        }

        public List<FlowEntity> getFlows()
        {
            return flows;
        }
    }
}