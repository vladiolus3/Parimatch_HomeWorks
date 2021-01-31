using System;
using System.Diagnostics;
using System.Linq;

namespace Task_1
{
    internal class Menu
    {
        private int _lnum, _hnum, _count;
        private int[] _range;
        readonly Stopwatch _stopwatch;
        public Menu()
        {
            _stopwatch = new Stopwatch();
            StartMenu();
        }

        private void StartMenu()
        {
            RangeLimits();

            int num;
            Console.WriteLine("1.\tLINQ");
            Console.WriteLine("2.\tPLINQ");

            while (true)
            {
                Console.Write("Enter the number: ");
                if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                else if (num < 1 || num > 2) Console.WriteLine("Incorrect number. Try again");
                else break;
            }
            Console.WriteLine();

            switch (num)
            {
                case 1:
                    LINQCounter();
                    break;
                case 2:
                    PLINQCounter();
                    break;
            }
            CheckResult();
        }

        private void LINQCounter()
        {

            _stopwatch.Start();
            _count = _range
                .Where(x => x > 1)
                .Where(x =>
                    Enumerable
                    .Range(2, x - 2)
                    .All(y => x % y != 0))
                .Count();
            _stopwatch.Stop();
        }

        private void PLINQCounter()
        {
            _stopwatch.Start();
            _count = _range
                .AsParallel()
                .Where(x => x > 1)
                .Where(x =>
                    Enumerable
                    .Range(2, x - 2)
                    .All(y => x % y != 0))
                .Count();
            _stopwatch.Stop();
        }

        private void CheckResult()
        {
            Console.WriteLine($"Count: {_count};\n" +
                $"Time: {_stopwatch.Elapsed.TotalSeconds}\n");
        }

        private void RangeLimits()
        {
            while (true)
            {
                Console.Write("Enter the lower limit of the range: ");
                if (!int.TryParse(Console.ReadLine(), out _lnum)) Console.WriteLine("The only numbers can be entered. Try again");
                else break;
            }
            Console.WriteLine();
            while (true)
            {
                Console.Write("Enter the upper limit of the range: ");
                if (!int.TryParse(Console.ReadLine(), out _hnum)) Console.WriteLine("The only numbers can be entered. Try again");
                else break;
            }
            Console.WriteLine();

            _range = Enumerable.Range(_lnum, _hnum - _lnum + 1).ToArray();
        }

    }
}
