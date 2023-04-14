namespace StatisticsProject.data
{
    public class WebSiteStats
    {
        public static int NOT_EXIST = -1;

        public WebSiteStats(string Host)
        {
            this.Host = Host;
        }

        public string Host { get; set; }

        public string Server { get; set; }

        public int nbCookiesSet { get; set; }

        public int nbSecondsFromModification { get; set; }

        public bool isAltSvc { get; set; }
    }
}