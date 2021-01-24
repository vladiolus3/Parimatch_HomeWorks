using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1._2
{
    class Task_1_2
    {
        static void Main()
        {
            Console.WriteLine("\"Three sorts and one comparator\" by st. Dovhal Vladyslav\n");
            
            var lst = new List<Player>()
            { new Player(29, "Ivan", "Ivanenko", (PlayerRank)25),
            new Player(19, "Peter", "Petrenko", (PlayerRank)2),
            new Player(59, "Ivan", "Ivanov", (PlayerRank)39),
            new Player(52, "Ivan", "Snezko", (PlayerRank)21),
            new Player(34, "Alex", "Zeshko", (PlayerRank)33),
            new Player(29, "Ivan", "Ivanenko", (PlayerRank)25),
            new Player(19, "Peter", "Petrenko", (PlayerRank)2),
            new Player(34, "Vasiliy", "Sokol", (PlayerRank)29),
            new Player(31, "Alex", "Alexeenko", (PlayerRank)29)};

            lst = lst.Distinct(new EqualityComparer()).ToList();
            Console.WriteLine("Dublicates were deleted!");

            Console.WriteLine("\n\n\nSorted list by Name:");
            lst.Sort(new SortName());
            lst.ForEach(x => Console.WriteLine(x.ToStringEx()));

            Console.WriteLine("\n\n\nSorted list by Age:");
            lst.Sort(new SortAge());
            lst.ForEach(x => Console.WriteLine(x.ToStringEx()));

            Console.WriteLine("\n\n\nSorted list by Rank:");
            lst.Sort(new SortRank());
            lst.ForEach(x => Console.WriteLine(x.ToStringEx()));

            Console.ReadLine();
        }
    }
}