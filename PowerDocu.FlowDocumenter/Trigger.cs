using System;

namespace PowerDocu.FlowDocumenter
{

    public class Trigger
    {
        public string Name;
        public string Type;
        public string Description;

        public Trigger(string name)
        {
            Name = name;
        }
    }
}