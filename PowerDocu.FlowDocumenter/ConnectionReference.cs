using System;
using PowerDocu.Common;

namespace PowerDocu.FlowDocumenter
{

    public class ConnectionReference
    {
        public string Name;
        public string Source;
        public string ID;
        public string Connector;
        public string ConnectionReferenceLogicalName;
        public ConnectionType Type;

        public ConnectionReference()
        {
        }
    }
}