using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2._1
{
    class Task_2_1
    {
        static int Main()
        {
            Console.WriteLine("\"ERP Reports Bot\" by st. Dovhal Vladyslav");
            Console.WriteLine("RULE: The user, through the bot menu system, requests various reports on the balances in warehouses. " +
                "The bot has a certain functionality: to get some kind of report, you need to enter a number that corresponds to this report in the menu.\n");

            var Menu = new Menu();
            try
            {
                Menu.Start();
            }
            catch (Exception)
            {
                return -1;
            }

            return 0;
        }
    }
}
