using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._2
{
    public class Player : IPlayer
    {
        public int Age { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public PlayerRank Rank { get; }
        public Player(int age, string fname, string lname, PlayerRank rank)
        {
            Age = age;
            FirstName = fname;
            LastName = lname;
            Rank = rank;
        }
    }

    public interface IPlayer
    {
        int Age { get; }
        string FirstName { get; }
        string LastName { get; }
        PlayerRank Rank { get; }
        
    }

    public enum PlayerRank
    {
        Private = 2,
        Lieutenant = 21,
        Captain = 25,
        Major = 29,
        Colonel = 33,
        General = 39,
    }

    public static class Extended
    {
        public static string ToStringEx(this Player player) => ($"[{ player.Age},\t\t\"{player.FirstName}\",\t\t\"{player.LastName}\",\t\t{player.Rank}]");
    }

}
