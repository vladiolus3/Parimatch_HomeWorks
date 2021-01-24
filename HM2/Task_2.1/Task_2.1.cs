using System;
using System.Threading;
using Library;

namespace Task_2._1
{
    class Task_2_1
    {
        static void Main()
        {
            var bser = new BetService();
            for (int i = 0; i < 10; i++)
            {
                float odd = bser.GetOdds();
                Thread.Sleep(15);
                Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {bser.Bet(100)}");
            }
            Console.WriteLine();
            int count = 0;
            while (count != 3)
            {
                float odd = bser.GetOdds();
                Thread.Sleep(15);
                while (odd <= 12)
                {
                    odd = bser.GetOdds();
                    Thread.Sleep(15);
                }
                Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {bser.Bet(100)}");
                count++;
            }
            decimal amount = 10000;
            Console.WriteLine();
            while (amount > 0 && amount < 150000)
            {
                float odd = bser.GetOdds();
                Thread.Sleep(15);
                while (odd > 3)
                {
                    odd = bser.GetOdds();
                    Thread.Sleep(15);
                }
                decimal bet = bser.Bet(1000);
                amount -= 1000;
                amount += bet;
                Console.WriteLine($"I’ve bet 1000 USD with the odd {odd} and I’ve earned {bet}");
            }
            amount = amount < 0 ? 0 : amount; 
            Console.WriteLine($"Game over. My balance is {amount}");
            Console.ReadKey();
        }
    }
}
