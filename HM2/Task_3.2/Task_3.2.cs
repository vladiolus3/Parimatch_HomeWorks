using System;
using Library;

namespace Task_3._2
{
    class Task_3_2
    {
        static void Main()
        {
            var paybase = new PaymentService();
            paybase.StartDeposit(50, "usd");
            paybase.StartWithdrawal(50, "usd");
            paybase.StartWithdrawal(50, "usd");
            paybase.StartDeposit(50, "usd");
            paybase.StartWithdrawal(50, "usd");
            paybase.StartDeposit(50, "usd");
            paybase.StartDeposit(500, "usd");
            paybase.StartDeposit(500, "usd");
        }
    }
}
