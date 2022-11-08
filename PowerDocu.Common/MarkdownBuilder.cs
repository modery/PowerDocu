using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerDocu.Common
{
    public abstract class MarkdownBuilder
    {
        protected readonly Random random = new Random();
        protected Dictionary<string, string> SVGImages;

        protected string AddExpressionDetails(List<Expression> inputs)
        {
            StringBuilder tableSB = new StringBuilder("<table>");
            foreach (Expression input in inputs)
            {
                StringBuilder operandsCellSB = new StringBuilder("<td>");

                if (input.expressionOperands.Count > 1)
                {
                    StringBuilder operandsTableSB = new StringBuilder("<table>");
                    foreach (object actionInputOperand in input.expressionOperands)
                    {
                        if (actionInputOperand.GetType() == typeof(Expression))
                        {
                            operandsTableSB.Append(AddExpressionTable((Expression)actionInputOperand, false));
                        }
                        else
                        {
                            operandsTableSB.Append("<tr><td>").Append(actionInputOperand.ToString()).Append("</td></tr>");
                        }
                    }
                    operandsTableSB.Append("</table>");
                    operandsCellSB.Append(operandsTableSB).Append("</td>");
                }
                else
                {
                    if (input.expressionOperands.Count > 0)
                    {
                        if (input.expressionOperands[0]?.GetType() == typeof(Expression))
                        {
                            operandsCellSB.Append(AddExpressionTable((Expression)input.expressionOperands[0]).Append("</table>"));
                        }
                        else
                        {
                            operandsCellSB.Append(input.expressionOperands[0]?.ToString());
                        }
                    }
                    else
                    {
                        operandsCellSB.Append("");
                    }
                    operandsCellSB.Append("</td>");
                }
                tableSB.Append("<tr><td>").Append(input.expressionOperator).Append("</td>").Append(operandsCellSB).Append("</tr>");
            }
            tableSB.Append("</table>");
            return tableSB.ToString();
        }

        protected StringBuilder AddExpressionTable(Expression expression, bool createNewTable = true, bool firstColumnBold = false)
        {
            StringBuilder table = createNewTable ? new StringBuilder("<table>") : new StringBuilder();

            if (expression?.expressionOperator != null)
            {
                StringBuilder tr = new StringBuilder("<tr>");
                StringBuilder tc = new StringBuilder("<td>");

                if (firstColumnBold)
                {
                    tc.Append("<b>").Append(expression.expressionOperator).Append("</b>");
                }
                else
                {
                    tc.Append(expression.expressionOperator);
                }
                tr.Append(tc.Append("</td>"));
                tc = new StringBuilder("<td>");
                if (expression.expressionOperands.Count > 1)
                {
                    StringBuilder operandsTable = new StringBuilder("<table>");
                    foreach (var expressionOperand in expression.expressionOperands.OrderBy(o => o.ToString()).ToList())
                    {
                        if (expressionOperand.GetType().Equals(typeof(string)))
                        {
                            operandsTable.Append("<tr><td>").Append((string)expressionOperand).Append("</td></tr>");
                        }
                        else if (expressionOperand.GetType().Equals(typeof(Expression)))
                        {
                            operandsTable.Append(AddExpressionTable((Expression)expressionOperand, false));
                        }
                        else
                        {
                            operandsTable.Append("<tr><td></td></tr>");
                        }
                    }
                    tc.Append(operandsTable).Append("</table>");
                }
                else if (expression.expressionOperands.Count == 0)
                {
                    //nothing to do here
                }
                else
                {
                    object expo = expression.expressionOperands[0];
                    if (expo.GetType().Equals(typeof(string)))
                    {
                        tc.Append((expression.expressionOperands.Count == 0) ? "" : expression.expressionOperands[0]?.ToString());
                    }
                    else
                    {
                        tc.Append(AddExpressionTable((Expression)expo, true));
                    }
                }
                tr.Append(tc).Append("</td>");
                table.Append(tr.Append("</tr>"));
            }
            if (createNewTable)
            {
                table.Append("</table>");
            }
            return table;
        }
    }
}