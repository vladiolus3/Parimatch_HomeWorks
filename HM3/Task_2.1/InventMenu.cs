using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2._1
{
    class InventMenu
    {
        private static List<Product> List;
        private static List<Tag> ListTag;
        private static List<Balance> ListBalance;
        public InventMenu(List<Product> list, List<Tag> tags, List<Balance> balances)
        {
            List = list;
            ListTag = tags;
            ListBalance = balances;
            InitMenu();
        }

        static void InitMenu()
        {
            while (true)
            {
                int num;
                Console.WriteLine("1.\tBack to main menu");
                Console.WriteLine("2.\tUnavailable products");
                Console.WriteLine("3.\tBalances are ascending");
                Console.WriteLine("4.\tBalances are descending");
                Console.WriteLine("5.\tBalance by product`s ID");

                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 1 || num > 5) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                Console.WriteLine();
                switch (num)
                {
                    case 1:
                        return;
                    case 2:
                        UnavailProd();
                        break;
                    case 3:
                        AscendBalance();
                        break;
                    case 4:
                        DescendBalance();
                        break;
                    case 5:
                        IDBalance();
                        break;
                }
            }
        }

        private static void UnavailProd()
        {
            List.OrderBy(x => x.Id)
                .Where(y => ListBalance
                    .Where(x => x.Id.Equals(y.Id))
                    .Select(x => x.Count)
                .Sum() == 0)
                .ToList()
                .ForEach(y => Console.WriteLine(y.ToStringEx(ListTag)));
            Console.WriteLine();
        }

        private static void AscendBalance()
        {
            int temp = 0;
            List.OrderBy(y => temp = ListBalance
                    .Where(x => x.Id.Equals(y.Id))
                    .Select(x => x.Count)
                .Sum())
                .ToList()
                .ForEach(y => Console.WriteLine(y.ToStringEx(ListTag) + "\tCount: " + ListBalance
                    .Where(x => x.Id.Equals(y.Id))
                    .Select(x => x.Count)
                    .Sum()));
            Console.WriteLine();
        }

        private static void DescendBalance()
        {
            int temp = 0;
            List.OrderByDescending(y => temp = ListBalance
                    .Where(x => x.Id.Equals(y.Id))
                    .Select(x => x.Count)
                .Sum())
                .ToList()
                .ForEach(y => Console.WriteLine(y.ToStringEx(ListTag) + "\tCount: " + ListBalance
                    .Where(x => x.Id.Equals(y.Id))
                    .Select(x => x.Count)
                    .Sum()));
            Console.WriteLine();
        }

        private static void IDBalance()
        {
            Console.Write("Enter the ID: ");
            IsLineEmpty(out string param);
            param = param.ToLower();

            if (ListBalance.Any(x => x.Id.Equals(param)))
            {
                ListBalance.Where(x => x.Id.Equals(param))
                    .OrderByDescending(x => x.Count)
                    .ToList()
                    .ForEach(x => Console.WriteLine($"{x.Location} — {x.Count}"));
            }
            else Console.WriteLine("Product not found");
            Console.WriteLine();
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
