using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgeStats
{
    public class AgeStatsCalculator
    {
        public async Task<AgeWebData> ComputeStatsAgeOfUrls(List<Uri> urls)
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            
            List<double> ages = new List<double>();
            var httpClient = new HttpClient(handler);
            foreach (var url in urls)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url.AbsoluteUri);
                var response = await httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var cookiesConverted = cookies.GetCookies(url).Cast<Cookie>();

                    if (response.Headers.Age.HasValue)
                    {
                        ages.Add(response.Headers.Age.Value.TotalSeconds);
                    }
                }
            }
            

            AgeWebData ageWebData = new AgeWebData(urls.Select(x => x.ToString()).ToList());
            ageWebData.urls = urls.Select(x => x.ToString()).ToList();
            
            if (ages.Count() == 0)
            {
                return ageWebData;
            }
            
            double average = ages.Average();
            ageWebData.averageAge = average;
            double ecartType = Math.Sqrt(ages.Select(n => (n - average) * (n - ages.Average())).Sum() / (ages.Count() - 1));
            ageWebData.ecartTypeAge = ecartType;

            return ageWebData;
        }
    }
}
