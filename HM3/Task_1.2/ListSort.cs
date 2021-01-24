using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._2
{
    class SortAge : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (x.Age.CompareTo(y.Age) != 0) return x.Age.CompareTo(y.Age);
            else
                return 0;
        }
    }

    class SortName : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            string xFullName = x.FirstName + x.LastName, yFullName = y.FirstName + y.LastName;
            if (xFullName.Length.CompareTo(yFullName.Length) > 0) return 1;
            else
                if (xFullName.Length.CompareTo(yFullName.Length) < 0) return -1;
            else return 0;
        }
    }

    class SortRank : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (x.Rank.CompareTo(y.Rank) != 0) return x.Rank.CompareTo(y.Rank);
            else
                return 0;
        }
    }

    class EqualityComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player x, Player y)
        {
            string xFullName = x.FirstName + x.LastName, yFullName = y.FirstName + y.LastName;
            if (x.Age == y.Age)
                if (xFullName == yFullName)
                    if (x.Rank == y.Rank) return true;
            return false;
        }

        public int GetHashCode(Player obj)
        {
            var Code = obj.Age.GetHashCode() + obj.Rank.GetHashCode() + (obj.FirstName + obj.LastName).GetHashCode();
            return Code;
        }
    }
}
