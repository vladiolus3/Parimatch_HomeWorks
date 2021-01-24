using System;
using Library;

namespace Task_1._4
{
    class Task_1_4 
    {
        static void Main()
        {
            const int n = 1000000;
            Account[] accounts = new Account[n];
            for (int i = 0; i < accounts.Length; i++)
            {
                while (true)
                {
                    bool stat = false;
                    var temp = new Account("uah");
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (accounts[j].Id == temp.Id)
                            {
                                stat = true;
                                break;
                            }
                        }
                        if (stat == false)
                        {
                            accounts[i] = temp;
                            break;
                        }
                    }
                    else
                    {
                        accounts[0] = temp;
                        break;
                    }
                }
            }
            GetSortedAccountsByQuickSort(ref accounts, 0, accounts.Length - 1);
            Console.WriteLine("First ten accounts are:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(accounts[i].Id);
            }
            Console.WriteLine("Last ten accounts are:");
            for (int i = n - 10; i < n; i++)
            {
                Console.WriteLine(accounts[i].Id);
            }
            Console.ReadKey();
        }
        static void GetSortedAccountsByQuickSort(ref Account[] accounts, int left, int right)
        {
            if (left - right >= 0) return;
            int i = left;
            for (int j = left; j <= right; j++)
            {
                if (accounts[j].Id < accounts[right].Id)
                {
                    Swap(ref accounts[i], ref accounts[j]);
                    i += 1;
                }
            }
            Swap(ref accounts[i], ref accounts[right]);
            GetSortedAccountsByQuickSort(ref accounts, left, i - 1);
            GetSortedAccountsByQuickSort(ref accounts, i + 1, right);
        }
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
