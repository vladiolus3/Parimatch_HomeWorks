using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading;

namespace Task_3
{
    class Program
    {
        private static readonly ConcurrentDictionary<string, string> conDictionary = new ConcurrentDictionary<string, string>();
        private static int countSuccess, countFail;
        private static CountdownEvent countdownEvent;
        static void Main()
        {
            Console.WriteLine("Service for issuing unique logins by st. Dovhal Vladyslav\n");

            int countThread;

            while (true)
            {
                Console.Write("Enter the number of threads for load: ");
                if (!int.TryParse(Console.ReadLine(), out countThread)) Console.WriteLine("The only numbers can be entered. Try again");
                else if (countThread < 1) Console.WriteLine("Incorrect number. Try again");
                else break;
            }

            try
            {
                new WorkingWithFileLogins(conDictionary);
                countdownEvent = new CountdownEvent(conDictionary.Count); 
            }
            catch (SerializationException)
            {
                Console.WriteLine("Failed to deserialize logins file!");
                return;
            }

            //Stopwatch timer = new Stopwatch();
            //timer.Start();

            ThreadPool.SetMaxThreads(countThread, countThread);
            while (!conDictionary.IsEmpty)
            {
                if (conDictionary.IsEmpty) break;
                var key = conDictionary.Keys.ToArray().First();
                conDictionary.TryGetValue(key, out string value);
                conDictionary.TryRemove(key, out value);
                ThreadPool.QueueUserWorkItem(ThreadLoginWithIncrement, key + ';' + value);
                //for (int i = 0; i < countThread; i++)
                //{
                //    if (conDictionary.IsEmpty) break;
                //    var key = conDictionary.Keys.ToArray()[0];
                //    conDictionary.TryGetValue(key, out string value);
                //    conDictionary.TryRemove(key, out value);
                //    new Thread(() => ThreadLoginWithIncrement(key, value)).Start();
                //}
            }
           
            countdownEvent.Wait();

            //timer.Stop();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            File.WriteAllText("result.json", JsonSerializer.Serialize(new Result(countSuccess, countFail), options));

            //Console.WriteLine(timer.Elapsed.TotalSeconds);
        }

        private static void ThreadLoginWithIncrement(object state)
        {
            string login = state.ToString().Split(';').First();
            string password = state.ToString().Split(';').Last();
            if (string.IsNullOrEmpty(LoginClient.Login(login, password)))
                Interlocked.Increment(ref countFail);
            else
                Interlocked.Increment(ref countSuccess);
            countdownEvent.Signal();
        }
    }
}
