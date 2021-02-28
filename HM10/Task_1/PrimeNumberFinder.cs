using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task_1
{
    public static class PrimeNumberFinder
    {
        public static async Task<bool> IsPrime(int number)
        {
            bool isPrime = true;

            await Task.Run(() =>
            {
                for (int i = 2; i < number; i++)
                {
                    if (number % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
            });

            return isPrime;
        }

        public static async Task<IEnumerable<int>> PrimeRange(int primesFrom, int primesTo)
        {
            var listPrime = new List<int>();

            await Task.Run(() =>
            {
                for (int i = primesFrom; i <= primesTo; i++)
                {
                    if (i < 2) continue;
                    bool isPrime = true;
                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }

                    }
                    if (isPrime == true) listPrime.Add(i);
                }
            });

            return listPrime;
        }
    }
}