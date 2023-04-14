using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StatisticsProject.data;
using AgeStats;
using PopularityStats;
using StatsPerso;

namespace StatisticsProject
{
    internal class Server
    {
        private AgeStatsCalculator ageStatsCalculator;
        private PopularityStatsCalculator popularityStatsCalculator;
        private StatsPersoCalculator statsPersoCalculator;

        public Server() {
            ageStatsCalculator = new AgeStatsCalculator();
            popularityStatsCalculator = new PopularityStatsCalculator();
            statsPersoCalculator = new StatsPersoCalculator();
        }

        public void Start()
        {
            if (!HttpListener.IsSupported)
            {
                return;
            }

            var listener = new HttpListener();

            // Trap Ctrl-C and exit 
            Console.CancelKeyPress += delegate
            {
                listener.Stop();
                Environment.Exit(0);
            };

            var url = "http://localhost:8080/";
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine($"Listening on {url}...");
            Task.Run(() => simpleRoutingFunction(listener));
            Console.ReadLine();
        }

        private async Task simpleRoutingFunction(HttpListener listener)
        {
            while (true)
            {
                var context = await listener.GetContextAsync();
                var request = context.Request;

                if (request.Url.LocalPath.StartsWith("/api"))
                {
                    List<Uri> urls = new List<Uri>();
                    try
                    {
                       urls = retrieveUrlsInFile();
                    } catch (Exception ex)
                    {
                        sendResponse(500, ex.Message, context);
                    }

                    try
                    {

                        switch (request.Url.LocalPath)
                        {
                            case "/api/popularityStats":
                                PopularityWebData res = await popularityStatsCalculator.ComputeStatsPopularityOfUrls(urls);
                                sendResponse(200, JsonSerializer.Serialize(res), context);
                                break;
                            case "/api/ageStats":
                                AgeWebData res2 = await ageStatsCalculator.ComputeStatsAgeOfUrls(urls);
                                sendResponse(200, JsonSerializer.Serialize(res2), context);
                                // await
                                break;
                            case "/api/persoStats":
                                StatsPersoData res3 = await statsPersoCalculator.ComputeStatsPersoOfUrls(urls);
                                sendResponse(200, JsonSerializer.Serialize(res3), context);
                                break;
                        }
                    } catch(Exception ex)
                    {
                        sendResponse(500, ex.Message, context);
                    }
                } else
                {
                    retrievePage(context);
                }
            }
        }

        /*
        private async Task callComputeStats(HttpListenerContext context)
        {
            // TODO : need to change it later
            StatsRes statsRes;
            try
            {
                statsRes = await computeStats.ComputeStatsOfUrls(urls);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error when calculating stats {e.Message}");
                responseText = $"Erreur : {e.Message}";
                sendResponse(400, responseText, context);
                return;
            }

            responseText = JsonSerializer.Serialize(statsRes);
            sendResponse(200, responseText, context);
        }
        */

        private List<Uri> retrieveUrlsInFile()
        {
            var fileName =
                @"C:\\Users\\QuentinXPS\\Tigli\\Projet\\StatisticsProject\\StatisticsProject\\ressources.txt";
            var lines = File.ReadAllLines(fileName);

            var urls = new List<Uri>();
            foreach (var line in lines)
            {
                urls.Add(new Uri(line));
            }
            return urls;
        }

        private void retrievePage(HttpListenerContext context)
        {
            string documentContents;
            var request = context.Request;
            using (var receiveStream = request.InputStream)
            {
                using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                {
                    documentContents = readStream.ReadToEnd();
                }
            }

            // TDOD : change by a environnement variable
            // string root_web_server = Environment.GetEnvironmentVariable("HTTP_ROOT");

            string responseString;
            try
            {
                responseString = new StreamReader($"www/pub/{request.Url.LocalPath}").ReadToEnd();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in reading file");
                sendResponse(404, $"Impossible to find the file : {request.Url.LocalPath}", context);
                return;
            }

            sendResponse(200, responseString, context);
        }

        private void sendResponse(int statusCode, string responseMessage, HttpListenerContext context)
        {
            // Write statusCode
            context.Response.StatusCode = statusCode;

            // Write in the http body the message
            var buffer = Encoding.UTF8.GetBytes(responseMessage);
            context.Response.ContentLength64 = buffer.Length;
            var output = context.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}