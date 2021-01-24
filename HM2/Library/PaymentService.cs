using System;

namespace Library
{
    public class PaymentService : ISupportDeposit
    {
        public PaymentMethodBase[] AvailablePaymentMethod { private set; get; }
        private Account CreditCardLimit = new Account("UAH");
        private Account Privat24Limit = new Account("UAH");
        private Account Monobank = new Account("UAH");
        public PaymentService()
        {
            AvailablePaymentMethod = new PaymentMethodBase[] { new CreditCard(), new Privet48(), new Stereobank(), new GiftVoucher() };
        }
        public void StartDeposit(decimal amount, string currency)
        {
            int num;
            {
                Random rnd = new Random();
                if (rnd.Next(1, 101) < 3) throw new PaymentServiceException("Unknown error within the system");
            }
            Console.WriteLine("1.\tCreditCard");
            Console.WriteLine("2.\tPrivet48");
            Console.WriteLine("3.\tStereobank");
            Console.WriteLine("4.\tGiftVoucher");
            Console.Write("Enter the number: ");
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                else if (num < 1 || num > 4) Console.WriteLine("Incorrect number. Try again");
                else break;
            }
            switch (num)
            {
                case 1:
                    {
                        var prevBalance = CreditCardLimit.GetBalance("UAH");
                        CreditCardLimit.Deposit(amount, currency);
                        if (CreditCardLimit.GetBalance("UAH") - prevBalance > 3000) throw new LimitExceededException("Transaction amount is more than 3 000 UAH");
                        ((CreditCard)AvailablePaymentMethod[num - 1]).StartDeposit(amount, currency);
                    }
                    break;
                case 2:
                    {
                        Privat24Limit.Deposit(amount, currency);
                        if (Privat24Limit.GetBalance("UAH") > 10000) throw new LimitExceededException("The total amount of transactions through available cards exceeded 10 000 UAH");
                        ((Privet48)AvailablePaymentMethod[num - 1]).StartDeposit(amount, currency);
                    }
                    break;
                case 3:
                    {
                        var prevBalance = Monobank.GetBalance("UAH");
                        Monobank.Deposit(amount, currency);
                        if (Monobank.GetBalance("UAH") > 7000) throw new LimitExceededException("The total amount of transactions through available cards exceeded 7 000 UAH");
                        if (CreditCardLimit.GetBalance("UAH") - prevBalance > 3000) throw new LimitExceededException("Transaction amount is more than 3 000 UAH");
                        ((Stereobank)AvailablePaymentMethod[num - 1]).StartDeposit(amount, currency);
                    }
                    break;
                case 4:
                    ((GiftVoucher)AvailablePaymentMethod[num - 1]).StartDeposit(amount, currency);
                    break;
            }
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            int num;
            {
                Random rnd = new Random();
                if (rnd.Next(1, 101) < 3) throw new PaymentServiceException("Unknown error within the system");
            }
            Console.WriteLine("1.\tCreditCard");
            Console.WriteLine("2.\tPrivet48");
            Console.WriteLine("3.\tStereobank");
            Console.Write("Enter the number: ");
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                else if (num < 1 || num > 3) Console.WriteLine("Incorrect number. Try again");
                else break;
            }
            switch (num)
            {
                case 1:
                    {
                        var prevBalance = CreditCardLimit.GetBalance("UAH");
                        CreditCardLimit.Deposit(amount, currency);
                        if (CreditCardLimit.GetBalance("UAH") - prevBalance > 3000) throw new LimitExceededException("Transaction amount is more than 3 000 UAH");
                        ((CreditCard)AvailablePaymentMethod[num - 1]).StartWithdrawal(amount, currency);
                    }
                    break;
                case 2:
                    {
                        Privat24Limit.Deposit(amount, currency);
                        if (Privat24Limit.GetBalance("UAH") > 10000) throw new LimitExceededException("The total amount of transactions through available cards exceeded 10 000 UAH");
                        ((Privet48)AvailablePaymentMethod[num - 1]).StartWithdrawal(amount, currency);
                    }
                    break;
                case 3:
                    {
                        var prevBalance = Monobank.GetBalance("UAH");
                        Monobank.Deposit(amount, currency);
                        if (Monobank.GetBalance("UAH") > 7000) throw new LimitExceededException("The total amount of transactions through available cards exceeded 7 000 UAH");
                        if (CreditCardLimit.GetBalance("UAH") - prevBalance > 3000) throw new LimitExceededException("Transaction amount is more than 3 000 UAH");
                        ((Stereobank)AvailablePaymentMethod[num - 1]).StartWithdrawal(amount, currency);
                    }
                    break;
            }
        }
    }
}
