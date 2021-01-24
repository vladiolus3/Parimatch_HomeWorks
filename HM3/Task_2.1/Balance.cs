
namespace Task_2._1
{
    class Balance
    {
        internal string Id { get; }
        internal int Count { get; }
        internal string Location { get; }

        internal Balance(string id, string location, int count)
        {
            Id = id;
            Location = location;
            Count = count;
        }
    }
}
