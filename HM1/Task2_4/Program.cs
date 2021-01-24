using System;

namespace Task2_4
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("\"Guess Number\" developed by st. Dovhal Vladyslav");
            Console.WriteLine("  First, enter the upper limit of the number range. The computer will set a number in this range.\n" +
                "Your task will be to guess this number, getting prompts \" > \" or \" < \".\n" +
                "Based on the count of incorrect attempts, you will receive points from 1 to 100.");
            int max = 1, guess, resp = 0, degree = 0, mistake = 0;
            double mark = 0;
            Random rnd = new Random();
            bool exit = false;
            DateTime start = DateTime.Now;

        limit0:
            while (true)
            {
                Console.Write("\nenter a Max Value from 0 to 1.000.000: ");
                var input = Console.ReadLine().ToLower();
                if (input == "exit")
                {
                    exit = true;
                    break;
                }
                if (!int.TryParse(input, out max) || max < 0 || max > 1000000) Console.WriteLine("ERROR! Try again");
                else break;
            }

            if (exit == true)
            {
                Console.WriteLine("See you soon!");
                Show(mark, mistake, DateTime.Now - start);
                return;
            }
            if (max == 0)
            {
                Console.WriteLine("The is only 1 number in your range. Maybe you will choose your limit?");
                goto limit0;
            }

            guess = rnd.Next(0, max);

            int j = 1;
            while (j < max + 1)
            {
                j *= 2;
                degree++;
            }
            if (Math.Abs(max + 1 - Math.Pow(2, degree)) > Math.Abs(max + 1 - Math.Pow(2, degree - 1))) degree--;

            while (resp != guess)
            {
                while (true)
                {
                    Console.Write("\ntry to guess a number from 1 to {0}: ", max);
                    var input = Console.ReadLine().ToLower();
                    if (input == "exit")
                    {
                        exit = true;
                        break;
                    }
                    if (!int.TryParse(input, out resp) || resp < 1 || resp > max) Console.WriteLine("ERROR! Try again");
                    else break;
                }
                if (exit == true)
                {
                    Console.WriteLine("See you soon!");
                    Show(mark, mistake, DateTime.Now - start);
                    return;
                }
                if (resp == guess)
                {
                    Console.WriteLine("Congratulate! You guess a number!");
                    mistake++;
                    break;
                }
                switch (resp > guess)
                {
                    case true:
                        Console.WriteLine("Your response > guessed number!");
                        mistake++;
                        break;
                    case false:
                        Console.WriteLine("Your response < guessed number!");
                        mistake++;
                        break;
                }

            }

            if (mistake - 1 >= degree) mark = 1;
            else mark = Math.Ceiling((double)100 * (degree - (mistake - 1)) / degree);
            Show(mark, mistake, DateTime.Now - start);
        }

        static void Show(double mark, int mistake, TimeSpan time)
        {
            Console.WriteLine("Mark:\t{0}", mark);
            Console.WriteLine("Number of attempts:\t{0}", mistake);
            Console.WriteLine("Game duration:\t{0}", time);
        }
    }
}
