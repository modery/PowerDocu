using System;
using System.Collections.Generic;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{
    public class Trigger
    {
        public string Name;
        public string Type;
        public string Connector;
        public string Description;
        public List<Expression> Inputs = new List<Expression>();
        public Dictionary<string, string> Recurrence = new Dictionary<string, string>();

        public Trigger(string name)
        {
            Name = name;
        }
    }
}