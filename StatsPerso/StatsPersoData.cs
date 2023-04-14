using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatsPerso
{
    public class StatsPersoData
    {
            public StatsPersoData() { 
                urls = new List<string>();
        
               }
            public StatsPersoData(List<string> urls)
            {
                this.urls = urls;
            }

            public List<string> urls { get; set; }

            public double nbCookiesSetAverage { get; set; }

            public double nbSecondsFromModificationAverage { get; set; }

            public double IsAltSvcAverage { get; set; }
    }
}
