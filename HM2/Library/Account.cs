using System;

namespace Library
{
    public class Account
    {
        public readonly int Id;
        public readonly string Currency;
        public decimal Amount { private set; get; }
        public Account(string currency)
        {
            var rnd = new Random();
            Id = rnd.Next(100000, 100000000);
            if (currency.ToUpper() != "EUR" && currency.ToUpper() != "USD" && currency.ToUpper() != "UAH") throw new NotSupportedException();
            Currency = currency.ToUpper();
            Amount = 0;
        }
        public void Deposit(decimal amount, string currency) => Amount += GetBalance(amount, currency);
        public void Withdraw(decimal amount, string currency)
        {
            if (GetBalance(amount, currency) > Amount) throw new InvalidOperationException();
            Amount -= GetBalance(amount, currency);
        }
        public decimal GetBalance(string currency)
        {
            switch (Currency)
            {
                case "EUR":
                    switch (currency)
                    {
                        case "EUR": return Amount;
                        case "USD": return Amount * 1.19m;
                        case "UAH": return Amount * 33.63m;
                        default: throw new NotSupportedException();
                    }
                case "USD":
                    switch (currency)
                    {
                        case "EUR": return Amount * 0.8403m;
                        case "USD": return Amount;
                        case "UAH": return Amount * 28.36m;
                        default: throw new NotSupportedException();
                    }
                case "UAH":
                    switch (currency)
                    {
                        case "EUR": return Amount * 0.0297m;
                        case "USD": return Amount * 0.0352m;
                        case "UAH": return Amount;
                        default: throw new NotSupportedException();
                    }
            }
            throw new NotSupportedException();
        }
        private decimal GetBalance(decimal amount, string currency)
        {
            switch (currency.ToUpper())
            {
                case "EUR":
                    switch (Currency)
                    {
                        case "EUR": return amount;
                        case "USD": return amount * 1.19m;
                        case "UAH": return amount * 33.63m;
                        default: throw new NotSupportedException();
                    }
                case "USD":
                    switch (Currency)
                    {
                        case "EUR": return amount * 0.8403m;
                        case "USD": return amount;
                        case "UAH": return amount * 28.36m;
                        default: throw new NotSupportedException();
                    }
                case "UAH":
                    switch (Currency)
                    {
                        case "EUR": return amount * 0.0297m;
                        case "USD": return amount * 0.0352m;
                        case "UAH": return amount;
                        default: throw new NotSupportedException();
                    }
            }
            throw new NotSupportedException();
        }
    }
}
