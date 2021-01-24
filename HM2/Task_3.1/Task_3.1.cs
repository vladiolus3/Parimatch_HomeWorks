using System;
using Library;

namespace Task_3._1
{
    class Task_3_1
    {
        static void Main()
        {
            var creditCard = new CreditCard();
            var giftvoucher = new GiftVoucher();
            var privet = new Privet48();
            var stereobank = new Stereobank();
            CheckInput(out int amount);
            creditCard.StartDeposit(amount, "usd");
            CheckInput(out amount);
            creditCard.StartWithdrawal(amount, "usd");
            CheckInput(out amount);
            privet.StartDeposit(amount, "usd");
            CheckInput(out amount);
            stereobank.StartDeposit(amount, "usd");
            CheckInput(out amount);
            giftvoucher.StartDeposit(amount, "usd");
            CheckInput(out amount);
            giftvoucher.StartDeposit(amount, "usd");
            
            Console.ReadKey();
        }
        static void CheckInput(out int amount)
        {
            Console.WriteLine("Enter the amount: ");
            while (!int.TryParse(Console.ReadLine(), out amount))
                Console.WriteLine("There can only be numbers. Try again");
        }
    }
}
