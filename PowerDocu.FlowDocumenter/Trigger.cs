using System;
using System.Collections.Generic;

namespace PowerDocu.FlowDocumenter
{

    public class Trigger
    {
        public string Name;
        public string Type;
        public string Connector;
        public string Description;
        public Dictionary<string, string> Recurrence = new Dictionary<string, string>();
        public Dictionary<string, string> Inputs = new Dictionary<string, string>();

        public Trigger(string name)
        {
            Name = name;
        }
    }
}