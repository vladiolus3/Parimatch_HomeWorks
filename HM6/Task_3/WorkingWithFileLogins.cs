using System;
using System.Collections.Concurrent;
using System.IO;
using System.Runtime.Serialization;

namespace Task_3
{
    internal class WorkingWithFileLogins
    {
        private readonly static string path = "logins.csv";
        private readonly ConcurrentDictionary<string, string> conDictionary = new ConcurrentDictionary<string, string>();
        public WorkingWithFileLogins(ConcurrentDictionary<string, string> conDict)
        {
            conDictionary = conDict;
            OpenAndFilling();
            try
            { Deserialize(); }
            catch (Exception)
            {
                throw new SerializationException();
            }
        }

        private void OpenAndFilling()
        {
            if (!File.Exists(path))
                File.WriteAllText(path, LoginsGenerator());
        }

        private void Deserialize()
        {
            var fileAllLine = File.ReadAllLines(path);
            for (int i = 0; i < fileAllLine.Length; i++)
            {
                var line = fileAllLine[i].Split(';');
                conDictionary.TryAdd(line[0], line[1].Trim('\n'));
            }
        }

        private string LoginsGenerator()
        {
            string logins = default;

            for (int i = 0; i < new Random().Next(200, 1000); i++)
            {
                string loginKey, loginValue;

                loginKey = Guid.NewGuid().ToString() + ';';
                loginValue = Guid.NewGuid().ToString() + '\n';
                logins += loginKey + loginValue;
            }

            return logins;
        }
    }
}
