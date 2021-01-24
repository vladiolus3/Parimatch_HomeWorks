using System;

namespace Task3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Adder developed by st. Dovhal Vladyslav");
            Console.WriteLine("Expression: Sum(i = 1 to infinity) = 1 / i / (i + 1)");
            Console.WriteLine("Where EPSILON = 1 / 2002\n");
            double epsilon = (double)1 / 2002;
            double i = 0, result = 0, temp;
            do
            {
                i++;
                temp = 1 / (i * (i + 1));
                result += temp;
            }
            while (temp >= epsilon);
            Console.WriteLine("Result: {0}", result);
        }
    }
}
