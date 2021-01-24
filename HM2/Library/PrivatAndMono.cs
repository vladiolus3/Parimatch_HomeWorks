
namespace Library
{
    public class Privet48 : Bank
    {
        public Privet48()
        {
            Name = "Privet48";
            AvailableCards = new string[] { "Gold", "Platinum" };
        }
    }
    public class Stereobank : Bank
    {
        public Stereobank()
        {
            Name = "Stereobank";
            AvailableCards = new string[] { "Black", "White", "Iron" };
        }
    }
}
