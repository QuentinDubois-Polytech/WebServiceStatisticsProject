using System.Collections.Generic;

namespace StatisticsProject.data
{
    public class StatsRes
    {
        public StatsRes(List<string> urls)
        {
            this.urls = urls;
        }

        public List<string> urls { get; set; }

        // Need to change
        public List<ServerContainerValue> ServersStats { get; set; }

        public double nbCookiesSetAverage { get; set; }

        public double nbSecondsFromModificationAverage { get; set; }

        public double IsAltSvcAverage { get; set; }
    }

    public class ServerContainerValue
    {
        public ServerContainerValue(string Name, int Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public string Name { get; set; }
        public int Value { get; set; }
    }
}