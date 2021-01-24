using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._3
{
    internal class Region​ : IRegion
    {
        public string Brand { get; }

        public string Country { get; }
        public Region​(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }
        public bool Equals(Region​ x, Region​ y)
        {           
            if (x.Brand == y.Brand)
                if (x.Country == y.Country) return true;
            return false;
        }

        public int GetHashCode(Region​ x)
        {
            var Code = x.Brand.GetHashCode() ^ x.Country.GetHashCode();
            return Code;
        }
    }

    public interface IRegion
    {
        string Brand { get; }
        string Country { get; }
    }

}
