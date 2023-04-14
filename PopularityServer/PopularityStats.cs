using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PopularityStats
{
    public class PopularityStatsCalculator
    {
        public async Task<PopularityWebData> ComputeStatsPopularityOfUrls(List<Uri> urls)
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            List<string> popularity = new List<string>();
            var httpClient = new HttpClient(handler);
            foreach (var url in urls)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url.AbsoluteUri);
                var response = await httpClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var cookiesConverted = cookies.GetCookies(url).Cast<Cookie>();
                    popularity.Add(response.Headers.Server.ToString()); ;
                }
            }

            PopularityWebData webData = new PopularityWebData(urls.Select(x => x.ToString()).ToList());

            var servers = new List<ServerContainerValue>();
            char[] splitter = { '/' };
            foreach (var serverValue in popularity.Where(x => !x.Equals("")))
            {
                var serverName = serverValue.Split(splitter)[0].ToLower();
                if (serverName.Length > 0)
                {
                    serverName = serverName.Trim().ToLower();
                    var findOne = false;
                    foreach (var s in servers)
                        if (s.Name.Equals(serverName))
                        {
                            s.Value++;
                            findOne = true;
                        }

                    if (!findOne) servers.Add(new ServerContainerValue(serverName, 1));
                }
            }

            webData.ServersStats = servers;
            return webData;
        }
    }
}
