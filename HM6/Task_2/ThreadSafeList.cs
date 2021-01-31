using System.Collections.Generic;
using System.Threading;

namespace Task_2
{
    internal static class ThreadSafeList
    {
        private static int maxRangeLimit;
        private static readonly object objLock = new object();
        private static readonly List<int> listPrime = new List<int>();

        internal static List<int> PrimeThreadLock(this Settings[] st)
        {
            foreach (Settings settings in st)
            {
                Thread t = new Thread(() => PrimeNumbers(settings));
                lock (objLock)
                {
                    t.Start();
                }
                t.Join();
            }
            return listPrime;
        }

        private static void PrimeNumbers(Settings settings)
        {
            int primesFrom;
            if (maxRangeLimit == settings.PrimesFrom)
                primesFrom = maxRangeLimit + 1;
            else
            if (maxRangeLimit > settings.PrimesFrom)
                primesFrom = maxRangeLimit;
            else
                primesFrom = settings.PrimesFrom;

            for (int i = primesFrom; i <= settings.PrimesTo; i++)
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

            if (settings.PrimesTo > maxRangeLimit) maxRangeLimit = settings.PrimesTo;

        }
    }
}
