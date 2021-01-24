using System;
using Library;

namespace Task_1._5
{
    class Task_1_5
    {
        static void Main()
        {
            Player plr1 = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "USD");
            Console.WriteLine($"Login with login {plr1.Email} and password {plr1.Password} is {plr1.IsPasswordValid(plr1.Password)}");
            Console.WriteLine($"Login with login {plr1.Email} and password {"GEROI_4E4HU"} is {plr1.IsPasswordValid("GEROI_4E4HU")}");
            plr1.Deposit(100, "usd");
            plr1.Withdraw(50, "eur");
            try
            {
                plr1.Withdraw(1000, "usd");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("ERROR!");
            }
            try
            {
                Player plr2 = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "PLN");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("ERROR!");
            }
            Console.ReadKey();
        }

    }
}
