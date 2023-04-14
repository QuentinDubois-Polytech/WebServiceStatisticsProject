using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace StatsPerso
{
    public class StatsPersoCalculator
    {
        public async Task<StatsPersoData> ComputeStatsPersoOfUrls(List<Uri> urls)
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            List<HttpResponseMessage> responseMessages = new List<HttpResponseMessage>();
            var httpClient = new HttpClient(handler);
            int nbIsAltSvc = 0;
            int nbCookiesSet = 0;

            int nbSecondsFromModification = 0;
            int nbLastModified = 0;

            foreach (var url in urls)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url.AbsoluteUri);
                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var cookiesConverted = cookies.GetCookies(url).Cast<Cookie>();

                    if (response.Content.Headers.LastModified.HasValue)
                    {

                        nbLastModified++;
                        nbSecondsFromModification += response.Content.Headers.LastModified.Value.Second;
                    }
                    if (response.Headers.TryGetValues("Alt-Svc", out var altSvcValues)) nbIsAltSvc++;
                    nbCookiesSet += cookiesConverted.Count();
                }
            }

            int nbUrls = urls.Count();
            
            StatsPersoData statsPerso = new StatsPersoData(urls.Select(x => x.ToString()).ToList());
            statsPerso.IsAltSvcAverage = (double) nbIsAltSvc / nbUrls;

            if (nbLastModified != 0)
            {
                statsPerso.nbSecondsFromModificationAverage = (double) nbSecondsFromModification / nbLastModified;
            }
            else
            {
                statsPerso.nbSecondsFromModificationAverage = -1;
            }
            
            statsPerso.nbCookiesSetAverage = (double)  nbCookiesSet / nbUrls;

            return statsPerso;
        }
    }
}
