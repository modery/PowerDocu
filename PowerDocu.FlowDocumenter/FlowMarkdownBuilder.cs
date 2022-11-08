using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PowerDocu.Common;
using Grynwald.MarkdownGenerator;

namespace PowerDocu.FlowDocumenter
{
    class FlowMarkdownBuilder : MarkdownBuilder
    {
        private readonly MdDocument mainDocument;
        private readonly string mainDocumentFileName;
        private readonly MdDocument connectionsDocument;
        private readonly string connectionsDocumentFileName;
        private readonly MdDocument variablesDocument;
        private readonly string variablesDocumentFileName;
        private readonly MdDocument triggerActionsDocument;
        private readonly string triggerActionsFileName;
        private readonly FlowDocumentationContent content;

        public FlowMarkdownBuilder(FlowDocumentationContent contentdocumentation)
        {
            content = contentdocumentation;
            Directory.CreateDirectory(content.folderPath);
            mainDocumentFileName = ("index " + content.filename + ".md").Replace(" ", "-");
            connectionsDocumentFileName = ("connections " + content.filename + ".md").Replace(" ", "-");
            variablesDocumentFileName = ("variables " + content.filename + ".md").Replace(" ", "-");
            triggerActionsFileName = ("triggersactions " + content.filename + ".md").Replace(" ", "-");
            var set = new DocumentSet<MdDocument>();
            mainDocument = set.CreateMdDocument(mainDocumentFileName);
            connectionsDocument = set.CreateMdDocument(connectionsDocumentFileName);
            variablesDocument = set.CreateMdDocument(variablesDocumentFileName);
            triggerActionsDocument = set.CreateMdDocument(triggerActionsFileName);

            //add all the relevant content
            addFlowMetadata();
            addFlowOverview();
            addConnectionReferenceInfo();
            addTriggerInfo();
            addVariablesInfo();
            addActionInfo();
            addFlowDetails();
            set.Save(content.folderPath);
            NotificationHelper.SendNotification("Created Markdown documentation for " + content.metadata.Name);
        }

        private MdBulletList getNavigationLinks()
        {
            MdListItem[] navItems = new MdListItem[] {
                new MdListItem(new MdLinkSpan("Overview", mainDocumentFileName)),
                new MdListItem(new MdLinkSpan("Connection References",connectionsDocumentFileName)),
                new MdListItem(new MdLinkSpan("Variables", variablesDocumentFileName)),
                new MdListItem(new MdLinkSpan("Triggers & Actions", triggerActionsFileName))
                };
            return new MdBulletList(navItems);
        }

        private void addFlowMetadata()
        {
            List<MdTableRow> tableRows = new List<MdTableRow>();
            foreach (KeyValuePair<string, string> kvp in content.metadata.metadataTable)
            {
                tableRows.Add(new MdTableRow(kvp.Key, kvp.Value));
            }
            MdTable table = new MdTable(new MdTableRow(new List<string>() { "Flow Name", content.metadata.Name }), tableRows);
            // prepare the common sections for all documents
            mainDocument.Root.Add(new MdHeading(content.metadata.header, 1));
            mainDocument.Root.Add(table);
            mainDocument.Root.Add(getNavigationLinks());
            connectionsDocument.Root.Add(new MdHeading(content.metadata.header, 1));
            connectionsDocument.Root.Add(table);
            connectionsDocument.Root.Add(getNavigationLinks());
            variablesDocument.Root.Add(new MdHeading(content.metadata.header, 1));
            variablesDocument.Root.Add(table);
            variablesDocument.Root.Add(getNavigationLinks());
            triggerActionsDocument.Root.Add(new MdHeading(content.metadata.header, 1));
            triggerActionsDocument.Root.Add(table);
            triggerActionsDocument.Root.Add(getNavigationLinks());
        }

        private void addFlowOverview()
        {
            mainDocument.Root.Add(new MdHeading(content.overview.header, 2));
            mainDocument.Root.Add(new MdParagraph(new MdTextSpan(content.overview.infoText)));
            mainDocument.Root.Add(new MdParagraph(new MdImageSpan("Flow Overview Diagram", content.overview.svgFile)));
        }

        private void addConnectionReferenceInfo()
        {
            connectionsDocument.Root.Add(new MdHeading(content.connectionReferences.header, 2));
            connectionsDocument.Root.Add(new MdParagraph(new MdTextSpan(content.connectionReferences.infoText)));
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in content.connectionReferences.connectionTable)
            {
                string connectorUniqueName = kvp.Key;
                ConnectorIcon connectorIcon = ConnectorHelper.getConnectorIcon(connectorUniqueName);
                connectionsDocument.Root.Add(new MdHeading((connectorIcon != null) ? connectorIcon.Name : connectorUniqueName, 3));

                List<MdTableRow> tableRows = new List<MdTableRow>();
                foreach (KeyValuePair<string, string> kvp2 in kvp.Value)
                {
                    tableRows.Add(new MdTableRow(kvp2.Key, kvp2.Value));
                }
                MdTable table = new MdTable(new MdTableRow(new MdTextSpan("Connector"), getConnectorNameAndIcon(connectorUniqueName, "https://docs.microsoft.com/connectors/" + connectorUniqueName)), tableRows); //todo:  
                connectionsDocument.Root.Add(table);
            }
        }

        private MdLinkSpan getConnectorNameAndIcon(string connectorUniqueName, string url)
        {
            ConnectorIcon connectorIcon = ConnectorHelper.getConnectorIcon(connectorUniqueName);
            if (ConnectorHelper.getConnectorIconFile(connectorUniqueName) != "")
            {
                return new MdLinkSpan(new MdCompositeSpan(
                                            new MdImageSpan(connectorUniqueName, connectorUniqueName + "32.png"),
                                            new MdTextSpan(" " + ((connectorIcon != null) ? connectorIcon.Name : connectorUniqueName))
                                        ), url);
            }
            else
            {
                return new MdLinkSpan((connectorIcon != null) ? connectorIcon.Name : connectorUniqueName, url);
            }
        }

        private void addVariablesInfo()
        {
            variablesDocument.Root.Add(new MdHeading(content.variables.header, 2));
            foreach (KeyValuePair<string, Dictionary<string, string>> kvp in content.variables.variablesTable)
            {
                variablesDocument.Root.Add(new MdHeading(kvp.Key, 3));

                List<MdTableRow> tableRows = new List<MdTableRow>();

                foreach (KeyValuePair<string, string> kvp2 in kvp.Value)
                {
                    if (!kvp2.Key.Equals("Initial Value"))
                    {
                        tableRows.Add(new MdTableRow(kvp2.Key, kvp2.Value));
                    }
                }
                MdTable table = new MdTable(new MdTableRow(new List<string>() { "Property", "Value" }), tableRows);
                variablesDocument.Root.Add(table);
                if (kvp.Value.ContainsKey("Initial Value"))
                {
                    tableRows = new List<MdTableRow>();
                    content.variables.initialValTable.TryGetValue(kvp.Key, out Dictionary<string, string> initialValues);
                    foreach (KeyValuePair<string, string> initialVal in initialValues)
                    {
                        tableRows.Add(new MdTableRow(initialVal.Key, initialVal.Value));
                    }
                    if (tableRows.Count > 0)
                    {
                        table = new MdTable(new MdTableRow(new List<string>() { "Variable Property", "Initial Value" }), tableRows);
                        variablesDocument.Root.Add(table);
                    }
                }
                content.variables.referencesTable.TryGetValue(kvp.Key, out List<ActionNode> references);
                if (references?.Count > 0)
                {
                    tableRows = new List<MdTableRow>();
                    foreach (ActionNode action in references.OrderBy(o => o.Name).ToList())
                    {
                        tableRows.Add(new MdTableRow(new MdLinkSpan(action.Name, triggerActionsFileName + getLinkFromText(action.Name))));
                    }
                    table = new MdTable(new MdTableRow(new List<string>() { "Variable Used In" }), tableRows);
                    variablesDocument.Root.Add(table);
                }
            }
        }

        private void addTriggerInfo()
        {
            triggerActionsDocument.Root.Add(new MdHeading(content.trigger.header, 2));
            List<MdTableRow> tableRows = new List<MdTableRow>();
            foreach (KeyValuePair<string, string> kvp in content.trigger.triggerTable)
            {
                if (kvp.Value == "mergedrow")
                {
                    tableRows.Add(new MdTableRow(new MdCompositeSpan(kvp.Key)));
                }
                else
                {
                    tableRows.Add(new MdTableRow(kvp.Key, kvp.Value));
                }
            }
            MdTable table = new MdTable(new MdTableRow(new List<string>() { "Property", "Value" }), tableRows);
            triggerActionsDocument.Root.Add(table);
            if (content.trigger.inputs?.Count > 0)
            {
                triggerActionsDocument.Root.Add(new MdHeading(content.trigger.inputsHeader, 3));
                triggerActionsDocument.Root.Add(new MdParagraph(new MdRawMarkdownSpan(AddExpressionDetails(content.trigger.inputs))));
            }
            if (content.trigger.triggerProperties?.Count > 0)
            {
                triggerActionsDocument.Root.Add(new MdHeading("Other Trigger Properties", 3));
                triggerActionsDocument.Root.Add(new MdParagraph(new MdRawMarkdownSpan(AddExpressionDetails(content.trigger.triggerProperties))));
            }
        }

        private void addActionInfo()
        {
            List<ActionNode> actionNodesList = content.actions.actionNodesList;
            triggerActionsDocument.Root.Add(new MdHeading(content.actions.header, 2));
            triggerActionsDocument.Root.Add(new MdParagraph(new MdTextSpan(content.actions.infoText)));

            foreach (ActionNode action in actionNodesList)
            {
                triggerActionsDocument.Root.Add(new MdHeading(action.Name, 3));
                List<MdTableRow> tableRows = new List<MdTableRow>
                {
                    new MdTableRow("Name", action.Name),
                    new MdTableRow("Type", action.Type)
                };
                if (!String.IsNullOrEmpty(action.Description))
                {
                    tableRows.Add(new MdTableRow("Description / Note", action.Description));
                }
                if (!String.IsNullOrEmpty(action.Connection))
                {
                    tableRows.Add(new MdTableRow("Connection",
                                                getConnectorNameAndIcon(action.Connection, "https://docs.microsoft.com/connectors/" + action.Connection)));
                }

                //TODO provide more details, such as information about subaction, subsequent actions, switch actions, ...
                if (action.actionExpression != null || !String.IsNullOrEmpty(action.Expression))
                {
                    tableRows.Add(new MdTableRow("Expression", new MdRawMarkdownSpan((action.actionExpression != null) ? AddExpressionTable(action.actionExpression).ToString() : action.Expression)));
                }
                MdTable table = new MdTable(new MdTableRow(new List<string>() { "Property", "Value" }), tableRows);
                triggerActionsDocument.Root.Add(table);
                if (action.actionInputs.Count > 0 || !String.IsNullOrEmpty(action.Inputs))
                {
                    tableRows = new List<MdTableRow>();
                    triggerActionsDocument.Root.Add(new MdHeading("Inputs", 4));
                    if (action.actionInputs.Count > 0)
                    {
                        tableRows.Add(new MdTableRow("test", "test"));
                        foreach (Expression actionInput in action.actionInputs)
                        {
                            StringBuilder operandsCell = new StringBuilder();
                            if (actionInput.expressionOperands.Count > 1)
                            {
                                StringBuilder operandsTable = new StringBuilder("<table>");
                                foreach (object actionInputOperand in actionInput.expressionOperands)
                                {
                                    if (actionInputOperand.GetType() == typeof(Expression))
                                    {
                                        operandsTable.Append(AddExpressionTable((Expression)actionInputOperand, false));
                                    }
                                    else
                                    {
                                        operandsTable.Append("<tr><td>").Append(actionInputOperand.ToString()).Append("</td></tr>");
                                    }
                                }
                                operandsCell.Append(operandsTable.Append("</table>"));
                            }
                            else
                            {
                                if (actionInput.expressionOperands.Count == 0)
                                {
                                    operandsCell.Append("<tr><td></td></tr>");
                                }
                                else
                                {
                                    if (actionInput.expressionOperands[0]?.GetType() == typeof(Expression))
                                    {
                                        operandsCell.Append(AddExpressionTable((Expression)actionInput.expressionOperands[0]));
                                    }
                                    else
                                    {
                                        operandsCell.Append(actionInput.expressionOperands[0]?.ToString());
                                    }
                                }
                            }
                            tableRows.Add(new MdTableRow(actionInput.expressionOperator, new MdRawMarkdownSpan(operandsCell.ToString())));
                        }
                    }
                    if (!String.IsNullOrEmpty(action.Inputs))
                    {
                        tableRows.Add(new MdTableRow("Value", action.Inputs));
                    }
                    table = new MdTable(new MdTableRow(new List<string>() { "Property", "Value" }), tableRows);
                    triggerActionsDocument.Root.Add(table);
                }

                if (action.Subactions.Count > 0 || action.Elseactions.Count > 0)
                {
                    if (action.Subactions.Count > 0)
                    {
                        tableRows = new List<MdTableRow>();
                        triggerActionsDocument.Root.Add(new MdHeading(action.Type == "Switch" ? "Switch Actions" : "Subactions", 4));
                        if (action.Type == "Switch")
                        {
                            foreach (ActionNode subaction in action.Subactions)
                            {
                                if (action.switchRelationship.TryGetValue(subaction, out string switchValue))
                                {
                                    tableRows.Add(new MdTableRow(switchValue, new MdLinkSpan(subaction.Name, getLinkFromText(subaction.Name))));
                                }
                            }
                            table = new MdTable(new MdTableRow(new List<string>() { "Case Values", "Action" }), tableRows);
                            triggerActionsDocument.Root.Add(table);
                        }
                        else
                        {
                            foreach (ActionNode subaction in action.Subactions)
                            {
                                //adding a link to the subaction's section in the documentation
                                tableRows.Add(new MdTableRow(new MdLinkSpan(subaction.Name, getLinkFromText(subaction.Name))));
                            }
                            table = new MdTable(new MdTableRow(new List<string>() { "Action" }), tableRows);
                            triggerActionsDocument.Root.Add(table);
                        }
                    }
                    if (action.Elseactions.Count > 0)
                    {
                        tableRows = new List<MdTableRow>();
                        triggerActionsDocument.Root.Add(new MdHeading("Elseactions", 4));
                        foreach (ActionNode elseaction in action.Elseactions)
                        {
                            //adding a link to the elseaction's section
                            tableRows.Add(new MdTableRow(new MdLinkSpan(elseaction.Name, getLinkFromText(elseaction.Name))));
                        }
                        table = new MdTable(new MdTableRow(new List<string>() { "Elseactions" }), tableRows);
                        triggerActionsDocument.Root.Add(table);
                    }
                }
                if (action.Neighbours.Count > 0)
                {
                    triggerActionsDocument.Root.Add(new MdHeading("Next Action(s) Conditions", 4));
                    tableRows = new List<MdTableRow>();
                    foreach (ActionNode nextAction in action.Neighbours)
                    {
                        string[] raConditions = action.nodeRunAfterConditions[nextAction];
                        tableRows.Add(new MdTableRow(new MdLinkSpan(nextAction.Name + " [" + string.Join(", ", raConditions) + "]", getLinkFromText(nextAction.Name))));
                    }
                    table = new MdTable(new MdTableRow(new List<string>() { "Next Action" }), tableRows);
                    triggerActionsDocument.Root.Add(table);
                }
            }
        }

        private void addFlowDetails()
        {
            mainDocument.Root.Add(new MdHeading(content.details.header, 2));
            mainDocument.Root.Add(new MdParagraph(new MdTextSpan(content.details.infoText)));
            mainDocument.Root.Add(new MdParagraph(new MdImageSpan(content.details.header, content.details.imageFileName + ".svg")));
        }
    }
}