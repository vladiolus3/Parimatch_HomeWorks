using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings range;
            var result = new Result();
            var listPrime = new List<int>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {

                var fileSettings = File.ReadAllText("settings.json");
                range = JsonSerializer.Deserialize<Settings>(fileSettings);

                for (int i = range.PrimesFrom; i <= range.PrimesTo; i++)
                {
                    if (i < 2) continue;
                    bool isPrime = true;
                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime == true) listPrime.Add(i);
                }

            }
            catch (Exception ex)
            {

                result.Error = ex.Message;
                result.Success = false;

            }
            finally
            {

                result.Primes = listPrime.Count == 0 ? null : listPrime.ToArray();
                stopWatch.Stop();
                var dur = stopWatch.Elapsed;
                result.Duration = dur.ToString();

            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            File.WriteAllText("result.json", JsonSerializer.Serialize(result, options));

        }
    }
}
