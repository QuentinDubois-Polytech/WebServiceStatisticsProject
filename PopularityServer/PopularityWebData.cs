using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopularityStats
{
    public class PopularityWebData
    {
        public PopularityWebData(List<string> urls)
        {
            this.urls = urls;
        }

        public List<string> urls { get; set; }

        // Need to change
        public List<ServerContainerValue> ServersStats { get; set; }
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
