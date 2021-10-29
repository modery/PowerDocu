using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PowerDocu.Common
{

    public enum ConnectionType
    {
        Connector,
        ConnectorReference
    };
    public class ConnectorHelper
    {
        private static string folderPath = AssemblyHelper.AssemblyDirectory + @"\Resources\ConnectorIcons\";
        public static List<ConnectorIcon> connectorIcons;

        public static string getConnectorIconFile(string connectorName)
        {
            if (File.Exists(folderPath + connectorName + ".png"))
            {
                return folderPath + connectorName + ".png";
            }
            return "";
        }

        public static ConnectorIcon getConnectorIcon(string uniqueName)
        {
            loadConnectorIcons();
            return connectorIcons.Find(x => x.Uniquename == uniqueName);
        }

        private static void loadConnectorIcons()
        {
            if (connectorIcons == null)
            {
                String JSONtxt = File.ReadAllText(folderPath + "connectors.json");
                connectorIcons = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ConnectorIcon>>(JSONtxt);
            }
        }
    }


    public class ConnectorIcon
    {
        public string Name;
        public string Uniquename;
        public string Url;
    }
}