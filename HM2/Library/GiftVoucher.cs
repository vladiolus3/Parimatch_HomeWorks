using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class GiftVoucher : PaymentMethodBase, ISupportDeposit
    {
        private readonly List<long> GiftArr = new List<long>();
        public GiftVoucher()
        {
            Name = "GiftVoucher";
        }
        public void StartDeposit(decimal amount, string currency)
        {
            if (amount != 100 && amount != 500 && amount != 1000)
            {
                Console.WriteLine("Unavailable amount. It can be only 100, 500 or 1000. Try again");
                return;
            }
            Console.WriteLine("Enter your gift card number without spaces: ");
            IsLineEmpty(out string giftnum);
            while (true)
            {
                if (giftnum.Length != 10 || long.TryParse(giftnum, out long giftnumint) == false)
                {
                    Console.WriteLine("Incorrect syntax. Try again");
                    giftnum = Console.ReadLine();
                }
                else if (GiftArr.Contains(giftnumint)) throw new LimitExceededException("No money available for this voucher");
                else
                {
                    GiftArr.Add(giftnumint);
                    break;
                }
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
