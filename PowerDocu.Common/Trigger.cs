using System;
using System.Collections.Generic;

namespace PowerDocu.Common
{
    public class Trigger
    {
        public string Name;
        public string Type;
        public string Connector;
        public string Description;
        public List<Expression> Inputs = new List<Expression>();
        public Dictionary<string, string> Recurrence = new Dictionary<string, string>();
        public List<Expression> TriggerProperties = new List<Expression>();

        public Trigger(string name)
        {
            Name = name;
        }
    }
}