using System;

namespace Task1
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Math Expression Solver developed by st. Dovhal Vladyslav");
            Console.WriteLine("Expression: y = ((e^A + 4*lg(C)) * |arctg(D)| / √B) + 5 / sin(A)");
            Console.WriteLine("Where B = 2002, C = 4, D = 8 and A is entered by you:\n");
            double a, result;
            const double b = 2002, c = 4, d = 8;
            while(true)
            {
                Console.Write("enter num. A: ");
                if (!double.TryParse(Console.ReadLine(), out a)) Console.WriteLine("ERROR! Try again");
                else break;
            }
            result = (Math.Exp(a) + 4 * Math.Log10(c)) / Math.Sqrt(b) * Math.Abs(Math.Atan(d)) + 5 / (Math.Sin(a));
            Console.WriteLine("Result: {0}", result);
        }
    }
}
