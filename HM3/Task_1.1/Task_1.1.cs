using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1._1
{
    class Task_1_1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\"LINQ Array Statistics\" by st. Dovhal Vladyslav");
            Console.WriteLine("RULE: Enter a string with a set of comma-separated integers and take results\n");

            string input;
            int[] arr;

            while (true)
            {
                Console.Write("Enter the line of numbers through the comma:\n");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    Console.WriteLine("You entered the empty line.");
                else
                {
                    var sub = input.Split(',', (char)StringSplitOptions.RemoveEmptyEntries);
                    if (sub.Where(x => x != "").All(x => int.TryParse(x, out _)))
                    {
                        Console.WriteLine("Done!\n");
                        arr = sub.Where(x => x!="").Select(x => int.Parse(x)).ToArray();
                        break;
                    }
                    else
                        Console.WriteLine("You entered incorrect data.");
                }

            }

            ArrStat(arr);

            ArrSortUnicView(arr);

            Console.ReadLine();
        }

        static void ArrStat(int[] arr)
        {
            Console.WriteLine("Minimum element:\t" + arr.Min());
            Console.WriteLine("Maximum element:\t" + arr.Max());
            Console.WriteLine("Sum of elements:\t" + arr.Sum());
            Console.WriteLine("Arithmetic mean:\t" + arr.Sum() / arr.Length);
            Console.WriteLine("Standard deviation:\t" + Deviation(arr));
        }

        static double Deviation(int[] arr)
        {
            double average = arr.Sum() / arr.Length;
            double result = Math.Sqrt(arr
                .Select(x => Math.Pow(average - x, 2))
            .Sum() / arr.Length);
            return result;
        }
        static void ArrSortUnicView(int[] arr)
        {
            IEnumerable<int> distinctSortArr = arr.OrderBy(x => x).Distinct();
            Console.WriteLine("An ascending sorted list of unique array elements: ");
            foreach (int temp in distinctSortArr)
            {
                Console.WriteLine(temp);
            }
        }
    }
}
