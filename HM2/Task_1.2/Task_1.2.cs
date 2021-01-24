using System;
using Library;

namespace Task_1._2
{
    class Task_1_2
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
            accounts = GetSortedAccounts(accounts);
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

        static Account[] GetSortedAccounts(Account[] accounts)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                for (int j = 0; j < accounts.Length - i - 1; j++)
                {
                    if (accounts[j + 1].Id < accounts[j].Id)
                    {
                        Account temp = accounts[j + 1];
                        accounts[j + 1] = accounts[j];
                        accounts[j] = temp;
                    }
                }

            }
            return accounts;
        }
    }
}
