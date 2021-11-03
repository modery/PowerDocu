using System;
using System.Collections.Generic;
using System.Text;

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
            sb.Append(expressionOperator + ": ");
            sb.Append("\n");
            foreach (object eo in expressionOperands)
            {
                sb.Append(eo.ToString() + ", ");
            }
            sb.Append("\n");

            return sb.ToString();
        }

    }
}