using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2._1
{
    class Product
    {
        internal string Id { get; }
        internal string Brand { get; }
        internal string Model { get; }
        internal string Cost { get; }
        

        internal Product(string id, string brand, string model, string cost)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Cost = cost;           
        }

    }

    class FileException : Exception
    {
        public FileException()
        {

        }
    }
}
