using System;

namespace Task2
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Odds Calculator developed by st. Dovhal Vladyslav");

            string team1, team2;
            double win1, draw, win2;
       
            while (true)  //entering and checking
            {
                Console.Write("enter first team's name: ");
                if (string.IsNullOrEmpty(team1 = Console.ReadLine())) Console.WriteLine("ERROR! Try again");
                else
                {
                    Console.WriteLine("Well done!\n");
                    break;
                }
            }

            while (true)
            {
                Console.Write("enter second team's name: ");
                if (string.IsNullOrEmpty(team2 = Console.ReadLine())) Console.WriteLine("ERROR! Try again");
                else
                {
                    Console.WriteLine("Well done!\n");
                    break;
                }
            }

            while (true)
            {
                Console.Write("enter WIN1 odd: ");
                if (!double.TryParse(Console.ReadLine(), out win1)) Console.WriteLine("ERROR! Try again");
                else
                {
                    Console.WriteLine("Well done!\n");
                    break;
                }
            }

            while (true)
            {
                Console.Write("enter DRAW odd: ");
                if (!double.TryParse(Console.ReadLine(), out draw)) Console.WriteLine("ERROR! Try again");
                else
                {
                    Console.WriteLine("Well done!\n");
                    break;
                }
            }

            while (true)
            {
                Console.Write("enter WIN2 odd: ");
                if (!double.TryParse(Console.ReadLine(), out win2)) Console.WriteLine("ERROR! Try again");
                else
                {
                    Console.WriteLine("Well done!\n");
                    break;
                }
            }

            double margin = (100 - 100 / (1 / win1 + 1 / win2 + 1 / draw));   //calculation
            Console.WriteLine($"Win \"{team1.ToUpper()}\" : {Math.Round((100 / win1) - (margin / 3), 1)}%");
            Console.WriteLine($"Win \"{team2.ToUpper()}\" : {Math.Round((100 / win2) - (margin / 3), 1)}%");
            Console.WriteLine($"Draw : {Math.Round((100 / draw) - (margin / 3), 1)}%");
            Console.WriteLine($"Margin : {Math.Round(margin, 1)}%");
        }
    }
}
