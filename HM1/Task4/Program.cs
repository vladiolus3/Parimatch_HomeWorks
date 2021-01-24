using System;

namespace Task4
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Finding prime numbers developed by st. Dovhal Vladyslav");
            int border1, border2;
            while (true)
            {
                Console.Write("enter fisrt border: ");
                if (!int.TryParse(Console.ReadLine(), out border1)) Console.WriteLine("ERROR! Try again");
                else break;
            }
            while (true)
            {
                Console.Write("enter second border: ");
                if (!int.TryParse(Console.ReadLine(), out border2)) Console.WriteLine("ERROR! Try again");
                else break;
            }
            for (int i = border1; i <= border2; i++)
            {
                if (i == 1) continue;
                bool IsSimple = true;
                for (int j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        IsSimple = false;
                        break;
                    } 
                }
                if (IsSimple == true) Console.Write(i + " ");
            }
        }
    }
}
