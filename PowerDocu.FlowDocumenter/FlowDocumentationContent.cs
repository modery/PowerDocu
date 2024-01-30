using System;
using System.Collections.Generic;
using System.Linq;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{
    class FlowDocumentationContent
    {
        public string folderPath, filename;
        public FlowMetadata metadata;
        public FlowOverview overview;
        public FlowConnectionReferences connectionReferences;
        public FlowTrigger trigger;
        public FlowVariables variables;
        public FlowDetails details;
        public FlowActions actions;

        public FlowDocumentationContent(FlowEntity flow, string path, FlowActionSortOrder sortOrder = FlowActionSortOrder.SortByName)
        {
            NotificationHelper.SendNotification("Preparing documentation content for " + flow.Name);
            folderPath = path + CharsetHelper.GetSafeName(@"\FlowDoc " + flow.Name + @"\");
            filename = CharsetHelper.GetSafeName(flow.Name) + ((flow.ID != null) ? ("(" + flow.ID + ")") : "");
            metadata = new FlowMetadata(flow);
            overview = new FlowOverview();
            connectionReferences = new FlowConnectionReferences(flow);
            trigger = new FlowTrigger(flow);
            variables = new FlowVariables(flow);
            details = new FlowDetails();
            actions = new FlowActions(flow, sortOrder);
        }
    }

    public enum FlowActionSortOrder
    {
        SortByOrder,
        SortByName
    }

    public class FlowMetadata
    {
        public string Name;
        public string ID;
        public string header;
        public Dictionary<string, string> metadataTable;

        public FlowMetadata(FlowEntity flow)
        {
            ID = flow.ID;
            Name = flow.Name;
            header = "Flow Documentation - " + Name;
            metadataTable = new Dictionary<string, string>
            {
                { "Flow Name", flow.Name }
            };
            if (!String.IsNullOrEmpty(flow.ID))
            {
                metadataTable.Add("Flow ID", flow.ID);
            }
            metadataTable.Add("Documentation generated at", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString());
            metadataTable.Add("Number of Variables", "" + flow.actions.ActionNodes.Count(o => o.Type == "InitializeVariable"));
            metadataTable.Add("Number of Actions", "" + flow.actions.ActionNodes.Count);
        }
    }

    public class FlowOverview
    {
        public string header;
        public string infoText;
        public string pngFile = "flow.png";
        public string svgFile = "flow.svg";

        public FlowOverview()
        {
            header = "Flow Overview";
            infoText = "The following chart shows the top level layout of the Flow. For a detailed view, please visit the section called Detailed Flow Diagram";
        }
    }

    public class FlowConnectionReferences
    {
        public string header;
        public string infoText;
        public Dictionary<string, Dictionary<string, string>> connectionTable;
        public FlowConnectionReferences(FlowEntity flow)
        {
            header = "Connections";
            infoText = $"There are a total of {flow.connectionReferences.Count} connections used in this Flow:";
            connectionTable = new Dictionary<string, Dictionary<string, string>>();
            foreach (ConnectionReference cRef in flow.connectionReferences)
            {
                string connectorUniqueName = cRef.Name;
                Dictionary<string, string> connectionDetailsTable = new Dictionary<string, string>
                {
                    { "Connection Type", cRef.Type.ToString() }
                };
                if (cRef.Type == ConnectionType.ConnectorReference)
                {
                    if (!String.IsNullOrEmpty(cRef.ConnectionReferenceLogicalName))
                        connectionDetailsTable.Add("Connection Reference Name", cRef.ConnectionReferenceLogicalName);
                }
                if (!String.IsNullOrEmpty(cRef.ID))
                {
                    connectionDetailsTable.Add("ID", cRef.ID);
                }
                if (!String.IsNullOrEmpty(cRef.Source))
                {
                    connectionDetailsTable.Add("Source", cRef.Source);
                }
                connectionTable.TryAdd(connectorUniqueName, connectionDetailsTable);
            }
        }
    }

    public class FlowTrigger
    {
        public string header;
        public string infoText;
        public Dictionary<string, string> triggerTable;
        public List<Expression> inputs;
        public string inputsHeader = "Inputs Details";
        public List<Expression> triggerProperties;
        public string triggerPropertiesHeader = "Other Trigger Properties";
        public FlowTrigger(FlowEntity flow)
        {
            header = "Trigger";

            triggerTable = new Dictionary<string, string>
            {
                { "Name", flow.trigger.Name },
                { "Type", flow.trigger.Type }
            };
            if (!String.IsNullOrEmpty(flow.trigger.Connector))
            {
                triggerTable.Add("Connector", flow.trigger.Connector);
            }
            //Description = a Note added
            if (!String.IsNullOrEmpty(flow.trigger.Description))
            {
                triggerTable.Add("Description / Note", flow.trigger.Description);
            }
            if (flow.trigger.Recurrence.Count > 0)
            {
                triggerTable.Add("Recurrence Details", "mergedrow");
                foreach (KeyValuePair<string, string> properties in flow.trigger.Recurrence)
                {
                    triggerTable.Add(properties.Key, properties.Value);
                }
            }
            if (flow.trigger.Inputs.Count > 0)
            {
                inputs = flow.trigger.Inputs;
            }
            if (flow.trigger.TriggerProperties.Count > 0)
            {
                triggerProperties = flow.trigger.TriggerProperties;
            }
        }
    }

    public class FlowVariables
    {
        public string header = "Variables";
        public Dictionary<string, Dictionary<string, string>> variablesTable;
        public Dictionary<string, List<ActionNode>> referencesTable;
        public Dictionary<string, Dictionary<string, string>> initialValTable;
        public FlowVariables(FlowEntity flow)
        {
            variablesTable = new Dictionary<string, Dictionary<string, string>>();
            referencesTable = new Dictionary<string, List<ActionNode>>();
            initialValTable = new Dictionary<string, Dictionary<string, string>>();
            List<ActionNode> variablesNodes = flow.actions.ActionNodes.Where(o => o.Type == "InitializeVariable").OrderBy(o => o.Name).ToList();
            List<ActionNode> modifyVariablesNodes = flow.actions.ActionNodes.Where(o => o.Type == "SetVariable" || o.Type == "IncrementVariable").ToList();
            foreach (ActionNode node in variablesNodes)
            {
                foreach (Expression exp in node.actionInputs)
                {
                    if (exp.expressionOperator == "variables")
                    {
                        Dictionary<string, string> variableValueTable = new Dictionary<string, string>();
                        getVariableDetails(exp.expressionOperands, variableValueTable, out string vname, out string vtype);
                        List<ActionNode> referencedInNodes = new List<ActionNode>
                        {
                            node
                        };
                        foreach (ActionNode actionNode in modifyVariablesNodes)
                        {
                            foreach (Expression expO in actionNode.actionInputs)
                            {
                                if (expO.expressionOperator == "name")
                                {
                                    if (expO.expressionOperands[0].ToString().Equals(vname))
                                    {
                                        referencedInNodes.Add(actionNode);
                                    }
                                }
                            }
                        }
                        foreach (ActionNode actionNode in flow.actions.ActionNodes)
                        {
                            if (actionNode.actionExpression?.ToString().Contains($"@variables('{vname}')") == true
                                || actionNode.Expression?.Contains($"@variables('{vname}')") == true
                                || actionNode.actionInput?.ToString().Contains($"@variables('{vname}')") == true)
                            {
                                referencedInNodes.Add(actionNode);
                            }
                            else
                            {
                                foreach (Expression actionInput in actionNode.actionInputs)
                                {
                                    if (actionInput.ToString().Contains($"@variables('{vname}')"))
                                    {
                                        referencedInNodes.Add(actionNode);
                                    }
                                }
                            }
                        }

                        Dictionary<string, string> variableDetailsTable = new Dictionary<string, string>
                        {
                            { "Name", vname },
                            {"Type",vtype},
                            {"Initial Value", ""}
                        };
                        variablesTable.Add(vname, variableDetailsTable);
                        referencesTable.Add(vname, referencedInNodes);
                        initialValTable.Add(vname, variableValueTable);
                    }
                }
            }
        }

        private void getVariableDetails(List<object> expressionOperands, Dictionary<string, string> variableValueTable, out string vname, out string vtype)
        {
            vname = "";
            vtype = "";
            foreach (object obj in expressionOperands)
            {
                if (obj.GetType().Equals(typeof(Expression)))
                {
                    Expression expO = (Expression)obj;
                    if (expO.expressionOperator == "name")
                    {
                        vname = expO.expressionOperands[0].ToString();
                    }
                    if (expO.expressionOperator == "type")
                    {
                        vtype = expO.expressionOperands[0].ToString();
                    }
                    if (expO.expressionOperator == "value")
                    {
                        if (expO.expressionOperands.Count == 1 && !expO.expressionOperands.GetType().Equals(typeof(List<object>)))
                        {
                            variableValueTable.Add(expO.expressionOperands[0].ToString(), "");
                        }
                        else
                        {
                            foreach (var eop in expO.expressionOperands)
                            {
                                if (eop.GetType() == typeof(string))
                                {
                                    variableValueTable.Add(eop.ToString(), "");
                                }
                                else if (eop.GetType() == typeof(Expression))
                                {
                                    if (!variableValueTable.ContainsKey(((Expression)eop).expressionOperator))
                                    {
                                        variableValueTable.Add(((Expression)eop).expressionOperator,
                                                                (((Expression)eop).expressionOperands.Count > 0) ? ((Expression)eop).expressionOperands[0].ToString() : "");
                                    }
                                    else
                                    {
                                        //Console.WriteLine("Duplicate key: " + ((Expression)eop).expressionOperator);
                                    }
                                }
                                else if (eop.GetType() == typeof(List<object>))
                                {
                                    variableValueTable.Add(Expression.createStringFromExpressionList((List<object>)eop), "");
                                }
                            }
                        }
                    }
                }
                else if (obj.GetType().Equals(typeof(List<object>)))
                {
                    List<object> topList = (List<object>)obj;
                    List<object> innerList = (List<object>)topList[0];
                    getVariableDetails(innerList, variableValueTable, out vname, out vtype);
                }
            }
        }
    }

    public class FlowDetails
    {
        public string header = "Detailed Flow Diagram";
        public string infoText = "The following chart shows the detailed layout of the Flow";
        public string imageFileName = "flow-detailed";
    }

    public class FlowActions
    {
        public string header = "Actions";
        public string infoText = "";
        public Dictionary<string, Dictionary<string, string>> actionsTable;
        public List<ActionNode> actionNodesList;
        public FlowActions(FlowEntity flow, FlowActionSortOrder sortOrder)
        {
            actionsTable = new Dictionary<string, Dictionary<string, string>>();
            if (sortOrder == FlowActionSortOrder.SortByOrder)
            {
                actionNodesList = flow.actions.ActionNodes.OrderBy(o => o.Order).ToList();
            }
            if (sortOrder == FlowActionSortOrder.SortByName)
            {
                actionNodesList = flow.actions.ActionNodes.OrderBy(o => o.Name).ToList();
            }
            infoText = $"There are a total of {actionNodesList.Count} actions used in this Flow:";
        }
    }
}