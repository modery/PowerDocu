using System;
using System.Collections.Generic;
using PowerDocu.Common;

namespace PowerDocu.AppDocumenter
{
    public class AppEntity
    {
        public string ID;
        public string Name;

        public List<string> variables = new List<string>();
        public List<ControlEntity> controls = new List<ControlEntity>();
        public List<Expression> properties = new List<Expression>();

        public AppEntity()
        {
        }
    }
}