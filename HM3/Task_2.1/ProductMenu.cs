using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2._1
{
    internal class ProductMenu
    {
        private static List<Product> List;
        private static List<Tag> ListTag;
        public ProductMenu(List<Product> list, List<Tag> tags)
        {
            List = list;
            ListTag = tags;
            InitMenu();
        }

        private static void InitMenu()
        {
            while (true)
            {
                int num;
                Console.WriteLine("1.\tBack to main menu");
                Console.WriteLine("2.\tSearch product");
                Console.WriteLine("3.\tList of all products - ascending price");
                Console.WriteLine("4.\tList of all products - descending price");

                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 1 || num > 4) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                Console.WriteLine();
                switch (num)
                {
                    case 1:
                        return;
                    case 2:
                        SearchProd();
                        break;
                    case 3:
                        AscendList();
                        break;
                    case 4:
                        DescendList();
                        break;
                }
            }
        }

        private static void SearchProd()
        {
            Console.Write("Enter something: ");
            IsLineEmpty(out string param);
            param = param.ToLower();

            var unicalID = new List<string>();

            Console.WriteLine("Match with item ID:");
            if (List.Any(x => x.Id.Equals(param)))
            {
                List.Where(x => x.Id.Equals(param))
                   .ToList()
                   .ForEach(x => AddUnicalID(ref unicalID, x, ListTag));

            }
            else Console.WriteLine("No matches!");

            Console.WriteLine();

            Console.WriteLine("Occurrence of a string in the name of a model or brand:");
            if (List.Any(x => x.Brand.Contains(param) || x.Model.Contains(param)))
            {
                List.Where(x => x.Brand.Contains(param) || x.Model.Contains(param))
                   .Where(x => !unicalID.Contains(x.Id))
                   .ToList()
                   .ForEach(x => AddUnicalID(ref unicalID, x, ListTag));
            }
            else Console.WriteLine("No matches!");

            Console.WriteLine();

            Console.WriteLine("Occurrence or match on one of the product tags:");
            if (ListTag.Any(x => x.Value.Contains(param) || x.Value.Equals(param)))
            {
                ListTag.Where(x => x.Value.Contains(param) || x.Value.Equals(param))
                     .Where(x => !unicalID.Contains(x.Id))
                     .ToList()
                     .ForEach(x => Console.WriteLine(x.ToStringEx(List, ListTag))); 
            }
            else Console.WriteLine("No matches!");

            Console.WriteLine();
        }

        private static void AscendList()
        {
            if (List.Count == 0) Console.WriteLine("The List is empty!");
            else
            {
                List.OrderBy(x => int.Parse(x.Cost))
                     .ToList()
                     .ForEach(x => Console.WriteLine(x.ToStringEx(ListTag)));
            }
            Console.WriteLine();
        }

        private static void DescendList()
        {
            if (List.Count == 0) Console.WriteLine("The List is empty!");
            else
            {
                List.OrderByDescending(x => int.Parse(x.Cost))
                     .ToList()
                     .ForEach(x => Console.WriteLine(x.ToStringEx(ListTag)));
            }
            Console.WriteLine();
        }

        private static void AddUnicalID(ref List<string> unicalID, Product x, List<Tag> ListTag)
        {
            Console.WriteLine(x.ToStringEx(ListTag));
            unicalID.Add(x.Id);
        }

        private static void IsLineEmpty(out string temp)
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
