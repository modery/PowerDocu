using System;
using System.Collections.Generic;
using PowerDocu.Common;

namespace PowerDocu.AppDocumenter
{
    public class ControlEntity
    {
        public string ID;
        public string Name;
        public string Type;
        public List<Expression> Properties = new List<Expression>();
        public List<ControlEntity> Children = new List<ControlEntity>();
        public List<Rule> Rules = new List<Rule>();
        public ControlEntity Parent;
        public ControlEntity()
        {
        }
    }

    public class Rule
    {
        public string Property;
        public string Category;
        public string InvariantScript;
        public string RuleProviderType;
    }
}