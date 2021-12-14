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
        public List<ControlEntity> Controls = new List<ControlEntity>();
        public List<Expression> properties = new List<Expression>();
        public List<DataSource> DataSources = new List<DataSource>();
        public AppEntity()
        {
        }
    }

    public class DataSource
    {
        public string Name;
        public string Type;
        public List<Expression> Properties = new List<Expression>();
    }
}