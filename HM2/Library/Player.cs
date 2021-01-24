using System;

namespace Library
{
    public class Player
    {
        public readonly int Id;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Email;
        public readonly string Password;
        public Account Account { private set; get; }
        public Player(string firstName, string lastName, string email, string password, string currency)
        {
            var rnd = new Random();
            Id = rnd.Next(100000, 100000000);
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Account = new Account(currency);
        }
        public bool IsPasswordValid(string password)
        {
            if (Password == password) return true;
            else return false;
        }
        public void Deposit(decimal amount, string currency) => Account.Deposit(amount, currency);
        public void Withdraw(decimal amount, string currency) => Account.Withdraw(amount, currency);
    }
}
