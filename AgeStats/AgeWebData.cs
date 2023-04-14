using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeStats
{
    public class AgeWebData
    {
        public List<string> urls { get; set; }
        public double averageAge { get; set; }
        public double ecartTypeAge { get; set; }
        
        public AgeWebData()
        {
            urls = new List<string>();
            averageAge = 0;
            ecartTypeAge = 0;
        }
        public AgeWebData(List<string> urls)
        {
            this.urls = urls;
            averageAge = 0;
            ecartTypeAge = 0;
        }
        
        
    }
}
