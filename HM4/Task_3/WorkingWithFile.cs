using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Task_3
{
    internal class WorkingWithFile
    {
        internal string Json { get; }
        internal WorkingWithFile()
        {
            using (File.Open("notes.json", FileMode.OpenOrCreate)) { }
            Json = TakeFromFile();
        }
        private string TakeFromFile() => File.ReadAllText("notes.json");
        internal void PushToFile(List<Note> lst) => File.WriteAllText("notes.json", JsonSerializer.Serialize(lst));
    }
}
