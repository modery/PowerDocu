using System;
using System.Collections.Generic;

namespace PowerDocu.FlowDocumenter
{
    public class FlowEntity
    {
        public string ID;
        public string Name;
        public Trigger trigger;
        public ActionGraph actions = new ActionGraph();
        public List<ConnectionReference> connectionReferences = new List<ConnectionReference>();

        public FlowEntity()
        {
        }

        public void addTrigger(string name)
        {
            this.trigger = new Trigger(name);
        }

        public override string ToString()
        {
            return "Flow " + Name + " (" + ID + ")";
        }
    }
}