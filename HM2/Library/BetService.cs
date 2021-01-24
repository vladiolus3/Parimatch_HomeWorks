using System;

namespace Library
{
    public class BetService
    {
        private decimal Odd;
        public BetService()
        {
            Random rnd = new Random();
            Odd = rnd.Next(101, 2501) / 100;
        }
        public float GetOdds()
        {
            Random rnd = new Random();
            Odd = Math.Round((decimal)rnd.Next(101, 2501) / 100, 2);
            return (float)Odd;
        }
        public bool IsWon()
        {
            Random rnd = new Random();
            if (rnd.Next(1, 101) < Math.Round(100 / Odd, 2)) return true;
            else return false;
        }
        public decimal Bet(decimal amount)
        {
            if (IsWon()) return amount * Odd;
            else return 0;
        }
    }
}
