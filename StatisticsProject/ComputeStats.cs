using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using StatisticsProject.data;

namespace StatisticsProject
{
    internal class ComputeStats
    {
        public async Task<StatsRes> ComputeStatsOfUrls(List<Uri> urls)
        {
            var webSitesStats = new List<WebSiteStats>();

            var cookies = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            var httpClient = new HttpClient(handler);
            

            // Could be optimzed with stream
            var statsOnWebSites = new List<WebSiteStats>();
            foreach (var url in urls)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url.AbsoluteUri);
                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var cookiesConverted = cookies.GetCookies(url).Cast<Cookie>();
                    statsOnWebSites.Add(retrieveWebSiteStats(request, response, cookiesConverted));
                }
            }

            return computeInternalStats(statsOnWebSites);
        }

        private WebSiteStats retrieveWebSiteStats(HttpRequestMessage request, HttpResponseMessage response,
            IEnumerable<Cookie> cookies)
        {
            // Console.WriteLine("retrieve stat 1");
            var url = request.RequestUri.Host;
            // Console.WriteLine("retrieve stat 2");
            var serverName = response.Headers.Server.ToString();
            // Console.WriteLine("retrieve stat 3");
            var nbCookiesSet = cookies.Count();

            int lastModified;
            if (response.Content.Headers.LastModified.HasValue)
            {
                lastModified = (int)(DateTime.Now - response.Content.Headers.LastModified.Value.DateTime).TotalSeconds;
            }
            else
            {
                lastModified = WebSiteStats.NOT_EXIST;
            }

            var isAltSvc = response.Headers.TryGetValues("Alt-Svc", out var altSvcValues);

            // Console.WriteLine("retrieve stat 4");
            var webSiteStats = new WebSiteStats(url);
            // Console.WriteLine($"serverName {serverName}");
            webSiteStats.Server = serverName;
            webSiteStats.nbSecondsFromModification = lastModified;
            webSiteStats.nbCookiesSet = nbCookiesSet;
            webSiteStats.isAltSvc = isAltSvc;

            // Console.WriteLine("retrieve stat 5");
            return webSiteStats;
        }

        private StatsRes computeInternalStats(List<WebSiteStats> stats)
        {
            var statsRes = new StatsRes(stats.Select(x => x.Host).ToList());
            statsRes.IsAltSvcAverage = stats.Select(x => x.isAltSvc ? 1 : 0).Average();
            statsRes.nbCookiesSetAverage = stats.Select(x => x.nbCookiesSet).Average();
            statsRes.nbSecondsFromModificationAverage = stats.Select(x => x.nbSecondsFromModification)
                .Where(x => x != WebSiteStats.NOT_EXIST).Average();

            var servers = new List<ServerContainerValue>();
            char[] splitter = { '/' };
            foreach (var serverValue in stats.Select(x => x.Server).Where(x => !x.Equals("")))
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

            statsRes.ServersStats = servers;
            
            return statsRes;
        }
    }
}