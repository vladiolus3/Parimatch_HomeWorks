using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace Task_2._1
{
    internal class Menu
    {

        protected static List<Product> List;
        protected static List<Tag> ListTag;
        protected static List<Balance> ListBalance;

        public Menu()
        {
            List = new List<Product>();
            ListTag = new List<Tag>();
            ListBalance = new List<Balance>();
        }

        public void Start()
        {
            while (true)
            {
                FileReading();
                int num;
                Console.WriteLine("1.\tExit");
                Console.WriteLine("2.\tProducts");
                Console.WriteLine("3.\tInvetory");
                Console.WriteLine("4.\tInfo");

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
                        Exit();
                        break;
                    case 2:
                        ProductMenu();
                        break;
                    case 3:
                        InventMenu();
                        break;
                    case 4:
                        Console.WriteLine("\"ERP Reports Bot\" by st. Dovhal Vladyslav");
                        Console.WriteLine("RULE: The user, through the bot menu system, requests various reports on the balances in warehouses. " +
                            "The bot has a certain functionality: to get some kind of report, you need to enter a number that corresponds to this report in the menu.\n");
                        break;
                }
            }
        }

        private void FileReading()
        {

            string path;
            path = "Files\\Products.csv";
            using (StreamReader srProd = new StreamReader(path, System.Text.Encoding.Default))
            {
                string[] strline = File.ReadAllLines(path);

                List = strline.Skip(1)
                    .Select(x => NewProductInit(x))
                    .ToList();
            }

            path = "Files\\Tags.csv";
            using (StreamReader srProd = new StreamReader(path, System.Text.Encoding.Default))
            {
                string[] strline = File.ReadAllLines(path);

                ListTag = strline.Skip(1)
                    .Select(x => NewTagInit(x))
                    .ToList();
            }

            path = "Files\\Inventory.csv";
            using (StreamReader srProd = new StreamReader(path, System.Text.Encoding.Default))
            {
                string[] strline = File.ReadAllLines(path);

                ListBalance = strline.Skip(1)
                    .Select(x => NewBalInit(x))
                    .ToList();
            }
        }

        private void Exit()
        {
            Console.Write("See you soon! Press any key and the program will exit.");
            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }

        private void ProductMenu() => new ProductMenu(List, ListTag);

        private void InventMenu() => new InventMenu(List, ListTag, ListBalance);

        private static Product NewProductInit(string str)
        {
            var StrSplit = str.Trim('\"').Split(';');
            return new Product(StrSplit[0].ToLower(), StrSplit[1].ToLower(), StrSplit[2].ToLower(), StrSplit[3].ToLower());
        }

        private static Tag NewTagInit(string str)
        {
            var StrSplit = str.Trim('\"').Split(';');
            return new Tag(StrSplit[0].ToLower(), StrSplit[1].ToLower());
        }

        private static Balance NewBalInit(string str)
        {
            var StrSplit = str.Trim('\"').Split(';');
            return new Balance(StrSplit[0].ToLower(), StrSplit[1].ToLower(), int.Parse(StrSplit[2]));
        }

    }
}
