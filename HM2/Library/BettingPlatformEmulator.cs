using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Library
{
    public class BettingPlatformEmulator
    {
        public List<Player> Players;
        public Player ActivePlayer;
        public Account Account { private set; get; }
        public BetService BetService;
        public PaymentService PaymentService;
        public BettingPlatformEmulator()
        {
            Players = new List<Player>();
            Account = new Account("usd");
            BetService = new BetService();
            PaymentService = new PaymentService();
        }
        public void Start()
        {
            while (true)
            {
                int num;
                if (ActivePlayer == null)
                {
                    Console.WriteLine("1.\tRegister");
                    Console.WriteLine("2.\tLogin");
                    Console.WriteLine("3.\tStop");
                }
                else
                {
                    Console.WriteLine("1.\tDeposit");
                    Console.WriteLine("2.\tWithdraw");
                    Console.WriteLine("3.\tGetOdds");
                    Console.WriteLine("4.\tBet");
                    Console.WriteLine("5.\tLogout");
                }
                Console.Write("Enter the number: ");
                while (true)
                {
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if ((num < 1 || num > 5) || (ActivePlayer == null && (num == 5 || num == 4))) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                if (ActivePlayer == null)
                    switch (num)
                    {
                        case 1:
                            Register();
                            break;
                        case 2:
                            Login();
                            break;
                        case 3:
                            Exit();
                            break;
                    }
                else
                    switch (num)
                    {
                        case 1:
                            Deposit();
                            break;
                        case 2:
                            Withdraw();
                            break;
                        case 3:
                            GetOdds();
                            break;
                        case 4:
                            Bet();
                            break;
                        case 5:
                            Logout();
                            break;
                    }

            }
        }
        private void Exit()
        {
            Logout();
            Console.WriteLine("See you soon!");
            Process.GetCurrentProcess().Kill();
        }
        private void Register()
        {
            string currency;
            Console.WriteLine("Enter your name, please");
            IsLineEmpty(out string firstName);
            Console.WriteLine("Enter your Last name, please");
            IsLineEmpty(out string lastName);
            Console.WriteLine("Enter your login, please");
            IsLineEmpty(out string email);
            Console.WriteLine("Enter your password, please");
            IsLineEmpty(out string password);
            Console.WriteLine("Enter your currency, please");
            currency = Console.ReadLine();
            while (currency.ToUpper() != "EUR" && currency.ToUpper() != "USD" && currency.ToUpper() != "UAH")
            {
                Console.WriteLine("We do not support this currency. Try again");
                currency = Console.ReadLine();
            }
            while (true)
            {
                bool stat = false;
                ActivePlayer = new Player(firstName, lastName, email, password, currency);
                if (Players.Count > 0)
                {
                    foreach (Player temp in Players)
                    {
                        if (ActivePlayer.Id == temp.Id)
                        {
                            stat = true;
                            break;
                        }
                    }
                    if (ActivePlayer.Id == Account.Id) stat = true;
                    if (stat == false)
                    {
                        Players.Add(ActivePlayer);
                        ActivePlayer = null;
                        break;
                    }
                }
                else
                {
                    Players.Add(ActivePlayer);
                    ActivePlayer = null;
                    break;
                }
            }//reg original player;     
            Console.WriteLine("Done!\n");
        }
        private void Login()
        {
            bool stat = false;
            Console.WriteLine("Enter your login, please");
            IsLineEmpty(out string email);
            Console.WriteLine("Enter your password, please");
            IsLineEmpty(out string password);
            foreach (Player temp in Players)
            {
                if (temp.Email == email)
                {
                    if (temp.IsPasswordValid(password))
                    {
                        ActivePlayer = temp;
                        stat = true;
                    }
                    break;
                }
            }
            if (stat == false) Console.WriteLine("Incorrect login or password. Try again\n");
            else Console.WriteLine("Done!\n");
        }
        private void Logout()
        {
            if (ActivePlayer != null)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    if (Players[i].Id == ActivePlayer.Id)
                    {
                        Players[i] = ActivePlayer;
                        ActivePlayer = null;
                        break;
                    }
                }
            }
            Console.WriteLine();
        }
        private void Deposit()
        {
            string currency;
            decimal amount;
            Console.WriteLine("Enter the currency, please");
            currency = Console.ReadLine();
            while (currency.ToUpper() != "EUR" && currency.ToUpper() != "USD" && currency.ToUpper() != "UAH")
            {
                Console.WriteLine("We do not support this currency. Try again");
                currency = Console.ReadLine();
            }
            Console.WriteLine("Enter the amount, please");
            while (true)
            {
                if (!decimal.TryParse(Console.ReadLine(), out amount)) Console.WriteLine("The only numbers can be entered. Try again");
                else if (amount <= 0) Console.WriteLine("Incorrect amount. Try again");
                else break;
            }
            Console.WriteLine();
            try
            {
                PaymentService.StartDeposit(amount, currency);
            }
            catch (LimitExceededException)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount\n");
                return;
            }
            catch (PaymentServiceException)
            {
                Console.WriteLine("Something went wrong. Try again later...\n");
                return;
            }
            ActivePlayer.Deposit(amount, currency);
            Account.Deposit(amount, currency);
            Console.WriteLine("Your balance will increase soon\n");
        }
        private void Withdraw()
        {
            string currency;
            decimal amount;
            Console.WriteLine("Enter the currency, please");
            currency = Console.ReadLine();
            while (currency.ToUpper() != "EUR" && currency.ToUpper() != "USD" && currency.ToUpper() != "UAH")
            {
                Console.WriteLine("We do not support this currency. Try again");
                currency = Console.ReadLine();
            }
            Console.WriteLine("Enter the amount, please");
            while (true)
            {
                if (!decimal.TryParse(Console.ReadLine(), out amount)) Console.WriteLine("The only numbers can be entered. Try again");
                else if (amount <= 0) Console.WriteLine("Incorrect amount. Try again");
                else break;
            }
            try
            {
                Player temp = ActivePlayer;
                temp.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is insufficient funds on your account\n");
                return;
            }
            try
            {
                Account temp = Account;
                temp.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is some problem on the platform side. Please try it later\n");
                return;
            }
            Console.WriteLine();
            try
            {
                PaymentService.StartWithdrawal(amount, currency);
            }
            catch (LimitExceededException)
            {
                Console.WriteLine("Please, try to make a transaction with lower amount\n");
                return;
            }
            catch (PaymentServiceException)
            {
                Console.WriteLine("Something went wrong. Try again later...\n");
                return;
            }
            ActivePlayer.Withdraw(amount, currency);
            Account.Withdraw(amount, currency);
            Console.WriteLine("Done!\n");
        }
        private void GetOdds() => Console.Write("Current odd: " + BetService.GetOdds() + "\n\n");
        private void Bet()
        {
            string currency;
            decimal amount;
            Console.WriteLine("Enter the currency, please");
            currency = Console.ReadLine();
            while (currency.ToUpper() != "EUR" && currency.ToUpper() != "USD" && currency.ToUpper() != "UAH")
            {
                Console.WriteLine("We do not support this currency. Try again");
                currency = Console.ReadLine();
            }
            Console.WriteLine("Enter the amount, please");
            while (true)
            {
                if (!decimal.TryParse(Console.ReadLine(), out amount)) Console.WriteLine("The only numbers can be entered. Try again");
                else if (amount <= 0) Console.WriteLine("Incorrect amount. Try again");
                else break;
            }
            try
            {
                ActivePlayer.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is insufficient funds on your account\n");
                return;
            }
            var sum = BetService.Bet(amount);
            ActivePlayer.Deposit(sum, currency);
            if (sum == 0) Console.WriteLine("You lose\n");
            else Console.WriteLine($"You win. Your gain {sum - amount}\n");
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
