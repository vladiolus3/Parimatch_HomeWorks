using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._3
{
    internal class RegionSettings : IRegionSettings
    {
        public string WebSite { get; }
        public RegionSettings(string webSite)
        {
            WebSite = webSite;
        }

    }

    public interface IRegionSettings 
    { 
        string WebSite { get; } 
    }
}
