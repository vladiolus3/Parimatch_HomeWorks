using System;

namespace Task2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0) Solution(false);
            else Solution(true);
        }

        static void Solution(bool _dialogmode)
        {
            if (_dialogmode == true) Console.WriteLine("Array Statistics developed by st. Dovhal Vladyslav");
            double[] arr;
            double min = double.MaxValue, max = 0, sum = 0, average, deviation = 0;
            if (_dialogmode == true)
            {
                int n;
                while (true)
                {
                    Console.Write("enter the dimension of the array: ");
                    if (!int.TryParse(Console.ReadLine(), out n)) Console.WriteLine("ERROR! Try again");
                    else break;
                }
                double[] temp = new double[n];
                for (int i = 0; i < n; i++)
                {
                    while (true)
                    {
                        Console.Write($"enter the {i + 1} array element: ");
                        if (!double.TryParse(Console.ReadLine(), out temp[i])) Console.WriteLine("ERROR! Try again");
                        else break;
                    }
                }
                arr = temp;
            }
            else
            {
                string input = Console.ReadLine().ToLower();
                string[] subs = input.Split(' ');
                double[] temp = new double[subs.Length];
                int i = 0;
                while (i < subs.Length)
                {
                    if (!double.TryParse(subs[i], out temp[i]))
                    {
                        Console.WriteLine(-1);
                        return;
                    }
                    i++;
                }
                arr = temp;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max) max = arr[i];
                if (arr[i] < min) min = arr[i];
                sum += arr[i];

            }
            average = sum / arr.Length;
            for (int i = 0; i < arr.Length; i++) deviation += Math.Pow((arr[i] - average), 2) / arr.Length;
            deviation = Math.Sqrt(deviation);
            if (_dialogmode == true) Console.Write("Minimum element:\t");
            Console.WriteLine(min);
            if (_dialogmode == true) Console.Write("Maximum element:\t");
            Console.WriteLine(max);
            if (_dialogmode == true) Console.Write("Sum of elements:\t");
            Console.WriteLine(sum);
            if (_dialogmode == true) Console.Write("Average:\t");
            Console.WriteLine(average);
            if (_dialogmode == true) Console.Write("Standard deviation:\t");
            Console.WriteLine(deviation);
            ShellSort(arr.Length, arr);
            if (_dialogmode == true) Console.Write("Sorted array:");
            Console.WriteLine();
                foreach (double temp in arr)
            {
                Console.Write($"{temp}\t");
            }
        }


        static void ShellSort(int n, double[] mass)
        {
            int i, j, step;
            double tmp;
            for (step = n / 2; step > 0; step /= 2)
                for (i = step; i < n; i++)
                {
                    tmp = mass[i];
                    for (j = i; j >= step; j -= step)
                    {
                        if (tmp < mass[j - step])
                            mass[j] = mass[j - step];
                        else
                            break;
                    }
                    mass[j] = tmp;
                }
        }
    }
}
