using System;
using Library;

namespace Task_1._3
{
    class Task_1_3
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
            Console.Write("Enter id: ");
            GetAccount(accounts, int.Parse(Console.ReadLine()));
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

        static void GetAccount(Account[] accounts, int id)
        {
            int l = 0, r = accounts.Length - 1, m, count = 0, ind = 0;
            bool stat = false;

            while (true)
            {
                if (r - l == 1)
                {
                    if (accounts[l].Id == id)
                    {
                        ind = l;
                        stat = true;
                        break;
                    }
                    if (accounts[r].Id == id)
                    {
                        ind = r;
                        stat = true;
                        break;
                    }
                    break;
                }
                m = l + (r - l) / 2;
                if (accounts[m].Id == id)
                {
                    ind = m;
                    stat = true;
                    break;
                }
                else if (accounts[m].Id < id) l = m;
                else if (accounts[m].Id > id) r = m;
                count++;
            }

            if (stat == true) Console.WriteLine($"{id} was found at index {ind} by {count} tries");
            else Console.WriteLine($"There is no account {id} in the list");
        }
    }
}
