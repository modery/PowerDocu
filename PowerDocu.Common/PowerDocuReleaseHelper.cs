using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace PowerDocu.Common
{
    public static class PowerDocuReleaseHelper
    {
        public static string currentVersion = "0.9.1";
        public static string latestVersionTag;
        public static string latestVersionUrl;
        private static bool hasReleaseBeenChecked = false;

        private static async Task<bool> GetLatestPowerDocuRelease()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "Mozilla/5.0 (compatible; PowerDocu " + currentVersion + ")"
            );
            var result = await client.GetAsync(
                "https://api.github.com/repos/modery/powerdocu/releases/latest"
            );
            var gitHubResponseJson = await result.Content.ReadAsStringAsync();
            JObject githubResponse = JsonConvert.DeserializeObject<JObject>(gitHubResponseJson);
            githubResponse.TryGetValue(
                "tag_name",
                StringComparison.CurrentCultureIgnoreCase,
                out JToken tagName
            );
            githubResponse.TryGetValue(
                "html_url",
                StringComparison.CurrentCultureIgnoreCase,
                out JToken htmlUrl
            );
            latestVersionTag = tagName.Value<string>();
            latestVersionUrl = htmlUrl.Value<string>();
            hasReleaseBeenChecked = true;
            return true;
        }

        public static async Task<bool> HasNewerPowerDocuRelease()
        {
            if(!hasReleaseBeenChecked) await GetLatestPowerDocuRelease();
            return !latestVersionTag.Contains(currentVersion);
        }
    }
}
