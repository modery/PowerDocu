using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace PowerDocu.Common
{
    public class Expression
    {
        public string expressionOperator;
        public List<object> expressionOperands = new List<object>();

        public Expression()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(expressionOperator).Append(": ");
            sb.Append("\n");
            foreach (object eo in expressionOperands)
            {
                sb.Append(eo.ToString()).Append(", ");
            }

            sb.Append("\n");

            return sb.ToString();
        }

        public static Expression parseExpressions(JProperty jsonExpression)
        {
            Expression expression = new Expression
            {
                expressionOperator = jsonExpression.Name
            };
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
    }
}