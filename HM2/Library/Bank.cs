using System;

namespace Library
{
    public abstract class Bank : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public string[] AvailableCards { get; protected set; }
        public void StartDeposit(decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}");
            Console.WriteLine("Please, enter your login");
            IsLineEmpty(out string email);
            Console.WriteLine("Please, enter your password");
            IsLineEmpty(out string password);
            Console.WriteLine($"Hello Mr {email}. Pick a card to proceed the transaction");
            for (int i = 0; i < AvailableCards.Length; i++)
                Console.WriteLine($"{i}.\t{AvailableCards[i]}");
            int num;
            if (AvailableCards.Length != 0)
            {
                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 0 || num >= AvailableCards.Length) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                Console.WriteLine($"You’ve withdraw {amount} {currency} from your {AvailableCards[num]} card successfully\n");
            }
            else Console.WriteLine("You don't have any cards available\n");
        }
        public void StartWithdrawal(decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}");
            Console.WriteLine("Please, enter your login");
            IsLineEmpty(out string email);
            Console.WriteLine("Please, enter your password");
            IsLineEmpty(out string password);
            Console.WriteLine($"Hello Mr {email}. Pick a card to proceed the transaction");
            for (int i = 0; i < AvailableCards.Length; i++)
                Console.WriteLine($"{i}.\t{AvailableCards[i]}");
            int num;
            if (AvailableCards.Length != 0)
            {
                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 0 || num >= AvailableCards.Length) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                Console.WriteLine($"You’ve deposit {amount} {currency} to your {AvailableCards[num]} card successfully\n");
            }
            else Console.WriteLine("You don't have any cards available\n");
        }
        internal static void IsLineEmpty(out string temp)
        {
            temp = Console.ReadLine();
            while (string.IsNullOrEmpty(temp))
            {
                Console.WriteLine("Line is not can be empty. Try again");
                temp = Console.ReadLine();
            }
        }
    }
}
