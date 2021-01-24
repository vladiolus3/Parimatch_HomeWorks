using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class CreditCard : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public CreditCard()
        {
            Name = "CreditCard";
        }
        public void StartDeposit(decimal amount, string currency)
        {
            Console.WriteLine("Enter your credit card number without spaces: ");
            IsLineEmpty(out string creditnum);
            while (true)
            {
                if (creditnum.Length != 16 || (creditnum[0] != '5' && creditnum[0] != '4') || long.TryParse(creditnum, out long creditnumint) == false)
                {
                    Console.WriteLine("Incorrect syntax. Try again");
                    creditnum = Console.ReadLine();
                }
                else break;
            }
            Console.WriteLine("Enter your Expiry Date in the format \"**/**\": ");
            IsLineEmpty(out string exdate);
            int[] exdateint = new int[2];
            while (true)
            {
                if (exdate.Length != 5 || exdate.Contains("/") == false)
                {
                    Console.WriteLine("Incorrect syntax. Try again");
                    exdate = Console.ReadLine();
                }
                else
                {
                    string[] sub = exdate.Split('/');
                    for (int i = 0; i < 2; i++)
                    {
                        if (sub[i].Length != 2 || int.TryParse(sub[i], out exdateint[i]) == false)
                        {
                            Console.WriteLine("Incorrect syntax. Try again");
                            exdate = Console.ReadLine();
                            continue;
                        }
                    }
                    break;
                }
            }
            Console.WriteLine("Enter your CVV: ");
            IsLineEmpty(out string cvv);
            while (true)
            {
                if (cvv.Length != 3 || int.TryParse(cvv, out int cvvint) == false)
                {
                    Console.WriteLine("Incorrect syntax. Try again");
                    cvv = Console.ReadLine();
                }
                else break;
            }
        }
        public void StartWithdrawal(decimal amount, string currency)
        {
            Console.WriteLine("Enter your credit card number without spaces: ");
            IsLineEmpty(out string creditnum);
            while (true)
            {
                if (creditnum.Length != 16 || (creditnum[0] != '5' && creditnum[0] != '4') || long.TryParse(creditnum, out long creditnumint) == false)
                {
                    Console.WriteLine("Incorrect syntax. Try again");
                    creditnum = Console.ReadLine();
                }
                else break;
            }
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
