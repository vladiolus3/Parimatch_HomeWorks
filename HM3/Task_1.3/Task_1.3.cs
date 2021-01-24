using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._3
{
    class Task_1_3
    {
        static void Main()
        {
            Console.WriteLine("\"Key dictionary\" by st. Dovhal Vladyslav");
            Console.WriteLine("RULE: You fill out the dictionary. First, you need to specify the number of items in it. " +
               "After that, enter each element, and if the key is duplicated, you will have to enter the key again, but differently.\n");

            var dctn = new Dictionary<Region​, RegionSettings>();
            int n;

            while (true)
            {
                Console.Write("Enter the count of elements in Dictionary: ");
                if (int.TryParse(Console.ReadLine(), out n))
                {
                    if (n >= 0)
                    {
                        Console.WriteLine("Done!\n");
                        break;
                    }
                }
                Console.WriteLine("You entered incorrect data.");
            }

            for (int i = 0; i < n; i++)
            {
            tryagain:
                Console.Write("Please, enter the Brand: ");
                IsLineEmpty(out string brand);
                Console.Write("Please, enter the Country: ");
                IsLineEmpty(out string country);
                Console.Write("Please, enter the WebSite: ");
                IsLineEmpty(out string website);
                Region reg = new Region(brand, country);
                RegionSettings rset = new RegionSettings(website);

                try
                {
                    DictionaryAdd(ref dctn, reg, rset);
                }
                catch (ExceptionDictionaryAdd)
                {
                    while (true)
                    {
                        Console.WriteLine("Do you want to try add again?\n" +
                       "0\t—\t No;\n" +
                       "1\t—\t Yes;\n");
                        if (int.TryParse(Console.ReadLine(), out int act))
                        {
                            if (act == 1) goto tryagain;
                            else if (act == 0) goto exit;
                        }
                        Console.WriteLine("You entered incorrect data.");
                    }
                }
            }

            exit:
            Console.WriteLine("Your Dictionary:");
            dctn.ToList().ForEach(x => Console.WriteLine($"[{x.Key.Brand},\t{x.Key.Country}]\t=\t[{x.Value.WebSite}]"));

            Console.WriteLine("\nComplete!");
            Console.ReadLine();
        }

        internal static void DictionaryAdd(ref Dictionary<Region​, RegionSettings> dctn, Region reg, RegionSettings rset)
        {
            if (dctn.Any(x => x.Key.GetHashCode(x.Key) == reg.GetHashCode(reg) && x.Key.Equals(x.Key, reg)))      //(dctn.Any(x => x.Key.GetHashCode() == reg.GetHashCode() && x.Key.Equals(reg)))
            {
                Console.WriteLine("Error. The key is already in the dictionary. Try again\n");
                throw new ExceptionDictionaryAdd();
            }
            else dctn.Add(reg, rset);
            Console.WriteLine("Done! Element was added.\n");
        }

        internal class ExceptionDictionaryAdd : Exception
        {
            public ExceptionDictionaryAdd()
            {

            }
        }

        internal static void IsLineEmpty(out string temp)
        {
            temp = Console.ReadLine();
            while (string.IsNullOrEmpty(temp))
            {
                Console.WriteLine("Line is not can be empty. Try again");
                temp = Console.ReadLine();
            }
        }
    }
}
