using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PowerDocu.Common
{
    public enum ConnectionType
    {
        Connector,
        ConnectorReference
    };
    public static class ConnectorHelper
    {
        private static readonly string folderPath = AssemblyHelper.GetExecutablePath() + @"\Resources\ConnectorIcons\";
        public static List<ConnectorIcon> connectorIcons;

        public static string getConnectorIconFile(string connectorName)
        {
            if (File.Exists(folderPath + connectorName + ".png"))
            {
                return folderPath + connectorName + ".png";
            }
            return "";
        }
        public static int numberOfConnectorIcons()
        {
            return Directory.GetFiles(folderPath, "*.png").Length;
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

        private const string connectorList = "https://powerautomate.microsoft.com/en-us/api/connectors/all/";

        public static async Task<bool> UpdateConnectorIcons()
        {
            try
            {
                NotificationHelper.SendNotification("Updating Connectors list, please wait.");
                List<ConnectorIcon> connectorIcons = new List<ConnectorIcon>();
                var client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.ParseAdd(
                    "Mozilla/5.0 (compatible; PowerDocu " + PowerDocuReleaseHelper.currentVersion.ToString() + ")"
                );
                var result = await client.GetAsync(
                    connectorList
                );
                var connectorListJson = await result.Content.ReadAsStringAsync();
                JObject connectorListObject = JsonConvert.DeserializeObject<JObject>(connectorListJson);
                connectorListObject.TryGetValue(
                    "value",
                    StringComparison.CurrentCultureIgnoreCase,
                    out JToken value
                );
                foreach (JToken connector in value.Children())
                {
                    ConnectorIcon connectorIcon = new ConnectorIcon();
                    var expressionNodes = connector.Children();
                    foreach (JProperty inputNode in expressionNodes)
                    {
                        switch (inputNode.Name)
                        {
                            case "name":
                                connectorIcon.Uniquename = inputNode.Value.ToString().Replace("shared_", "");
                                break;
                            case "properties":
                                parseConnectorProperties(inputNode.Value.Children(), connectorIcon);
                                break;
                            default:
                                break;
                        }
                    }
                    connectorIcons.Add(connectorIcon);
                    var response = await client.GetAsync(connectorIcon.Url);
                    File.WriteAllBytesAsync(folderPath + connectorIcon.Uniquename + ".png", await response.Content.ReadAsByteArrayAsync());
                }
                File.WriteAllText(folderPath + "connectors.json", JsonConvert.SerializeObject(connectorIcons));
                NotificationHelper.SendNotification($"Update complete. A total of {connectorIcons.Count} connectors were found.");
            }
            catch (Exception e)
            {
                NotificationHelper.SendNotification("An error occured while trying to update the connector list:");
                NotificationHelper.SendNotification(e.ToString());
            }
            return true;
        }

        private static void parseConnectorProperties(JEnumerable<JToken> properties, ConnectorIcon connectorIcon)
        {
            foreach (JProperty property in properties)
            {
                switch (property.Name)
                {
                    case "displayName":
                        connectorIcon.Name = property.Value.ToString();
                        break;
                    case "iconUri":
                        connectorIcon.Url = property.Value.ToString();
                        break;
                    default:
                        break;
                }
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