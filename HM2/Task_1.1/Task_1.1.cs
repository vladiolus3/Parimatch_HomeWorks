using System;
using Library;

namespace Task_1._1
{
    class Task_1_1
    {
        static void Main()
        {
            Account acc1 = new Account("EUR");
            Account acc2 = new Account("USD");
            Account acc3 = new Account("UAH");
            acc1.Deposit(10, "EUR");
            acc1.Withdraw(3, "UAH");
            acc3.Deposit(121, "USD");
            try
            {
                acc2.Withdraw(5, "usd");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("ERROR!"); 
            }
            try
            {
                Account acc4 = new Account("PLN");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("ERROR!");
            }
            Console.WriteLine($"Account with currency {acc1.Currency} has {acc1.Amount} balance");
            Console.WriteLine($"Account with currency {acc2.Currency} has {acc2.Amount} balance");
            Console.WriteLine($"Account with currency {acc3.Currency} has {acc3.Amount} balance");
            Console.ReadKey();
        }
    }
}
