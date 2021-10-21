using System;
using System.Collections.Generic;
using System.Text;

namespace PowerDocu.FlowDocumenter
{
    public class ActionNode
    {
        public string Name;
        public string Expression;
        public string Type;
        public List<ActionInput> actionInputs = new List<ActionInput>();
        public List<ActionNode> Neighbours = new List<ActionNode>();
        public List<ActionNode> Subactions = new List<ActionNode>();
        public List<ActionNode> Elseactions = new List<ActionNode>();

        public ActionNode(string name)
        {
            this.Name = name;
        }

        public bool hasSubactions()
        {
            return Subactions.Count > 0;
        }
        public bool AddNeighbour(ActionNode neighbour)
        {
            if (Neighbours.Contains(neighbour))
            {
                return false;
            }
            else
            {
                Neighbours.Add(neighbour);
                return true;
            }
        }
        public bool AddSubaction(ActionNode subaction)
        {
            if (Subactions.Contains(subaction))
            {
                return false;
            }
            else
            {
                Subactions.Add(subaction);
                return true;
            }
        }
        public bool AddElseaction(ActionNode elseaction)
        {
            if (Elseactions.Contains(elseaction))
            {
                return false;
            }
            else
            {
                Elseactions.Add(elseaction);
                return true;
            }
        }
        public override string ToString()
        {
            //TODO add subsactions?
            StringBuilder nodeString = new StringBuilder();
            nodeString.Append("[Node Value: " + Name + " with Neighbours");
            foreach (var item in Neighbours)
            {
                nodeString.Append(" -> " + item.Name);
            }
            nodeString.Append(" ]");
            if (hasSubactions())
            {
                nodeString.Append("  -[");
                foreach (var item in Subactions)
                {
                    nodeString.Append(" -> " + item.Name);
                }
                nodeString.Append("]");
            }
            return nodeString.ToString();
        }
    }


    public class ActionGraph
    {

        List<ActionNode> myActionNodes = new List<ActionNode>();
        ActionNode rootNode = null;

        public ActionGraph()
        {

        }

        public int Count
        {
            get
            {
                return myActionNodes.Count;
            }
        }
        public IList<ActionNode> ActionNodes
        {
            get
            {
                return myActionNodes.AsReadOnly();
            }
        }
        public bool AddNode(string value)
        {
            if (Find(value) != null)
            {
                return false;
            }
            else
            {
                myActionNodes.Add(new ActionNode(value));
                return true;
            }
        }

        public bool hasRoot()
        {
            return rootNode != null;
        }

        public bool AddEdge(ActionNode gn1, ActionNode gn2)
        {
            if (gn1 == null && gn2 == null)
            {
                return false;
            }
            else if (gn1.Neighbours.Contains(gn2))
            {
                return false;
            }
            else
            {
                gn1.AddNeighbour(gn2);
                return true;
            }
        }

        ActionNode Find(string value)
        {
            foreach (ActionNode item in myActionNodes)
            {
                if (item.Name.Equals(value))
                {
                    return item;
                }
            }
            return null;
        }

        public ActionNode FindOrCreate(string value)
        {
            ActionNode item = Find(value);
            if (item == null)
            {
                item = new ActionNode(value);
                myActionNodes.Add(item);
            }
            return item;
        }

        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                nodeString.Append(myActionNodes[i].ToString());
                if (i < Count - 1)
                {
                    nodeString.Append("\n");
                }
            }
            return nodeString.ToString();
        }

        //What if we have 2 preceding nodes? To review at some point
        public ActionNode getPrecedingNeighbour(ActionNode currentNode)
        {
            foreach (ActionNode node in myActionNodes)
            {
                if (node.Neighbours.Contains(currentNode))
                {
                    return node;
                }
            }
            return null;
        }

        public ActionNode getRootNode()
        {
            return rootNode;
        }

        public void setRootNode(ActionNode root)
        {
            rootNode = root;
        }
    }

    public class ActionInput
    {
        public string Name;
        public string Value;

        public ActionInput(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
