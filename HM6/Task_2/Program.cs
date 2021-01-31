using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Task_2
{
    class Program
    {
        static void Main()
        {
            Settings[] range;
            var result = new Result();
            var listPrime = new List<int>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {

                var fileSettings = File.ReadAllText("settings.json");
                range = JsonSerializer
                    .Deserialize<Settings[]>(fileSettings)
                    .Where(x => x != null)
                    .OrderBy(x => x.PrimesFrom)
                    .ToArray();

                listPrime = range.PrimeThreadLock();

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
