using System;

namespace PowerDocu.FlowDocumenter
{	
    public class ConnectionReference
    {
		public string Name;
		public string Source;
		public string ID;
		public string Connector;
		public ConnectionReference(string name, string source, string connector, string id) {
			Name = name;
			Source = source;
			Connector = connector;
			ID = id;
		}
	}
}