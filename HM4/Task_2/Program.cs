using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static async Task GetResponse()
        {
            var uri = new Uri("https://bank.gov.ua");
            var httpClient = new HttpClient
            {
                BaseAddress = uri,
                Timeout = TimeSpan.FromSeconds(10)
            };

            var response = await httpClient.GetAsync("/NBUStatService/v1/statdirectory/exchange?json");
            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync();
            File.WriteAllText("cache.json", body);
        }

        static void Main()
        {
            Console.WriteLine("Online Converter by st. Dovhal Vladyslav\n");

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-en");
            string initialCurrency, finalCurrency;
            decimal amount;

            #region 
            Console.Write("Enter the initial currency, please\t");
            initialCurrency = Console.ReadLine().Trim(' ');
            while (string.IsNullOrWhiteSpace(initialCurrency) || initialCurrency.Length != 3)
            {
                Console.Write("Input currency is not correct. Try again\t");
                initialCurrency = Console.ReadLine().Trim(' ');
            }
            Console.WriteLine();

            Console.Write("Enter the final currency, please\t");
            finalCurrency = Console.ReadLine().Trim(' '); ;
            while (string.IsNullOrWhiteSpace(finalCurrency) || finalCurrency.Length != 3)
            {
                Console.Write("Output currency is not correct. Try again\t");
                finalCurrency = Console.ReadLine().Trim(' ');
            }
            Console.WriteLine();

            Console.Write("Enter the amount, please\t");
            while (true)
            {
                if (!decimal.TryParse(Console.ReadLine().Trim(' '), out amount)) Console.Write("The only numbers can be entered. Try again\t");
                else if (amount <= 0) Console.Write("Amount must be more than zero. Try again\t");
                else break;
            }
            Console.WriteLine();
            #endregion
            //data inputs

            try
            {
                GetResponse().Wait();
            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Error: failed to update the file with currencies.\n\n");

            }
            //update cache        
            initialCurrency = initialCurrency.ToUpper();
            finalCurrency = finalCurrency.ToUpper();
            var json = JsonConvert.DeserializeObject<Currency[]>(File.ReadAllText("cache.json"));

            if (initialCurrency == "UAH")
            {
                Console.WriteLine("{0} {1} x {2} = {3} {4} (from {5})"
                    , Math.Round(amount, 4)
                    , initialCurrency
                    , Math.Round(1 / json.Where(x => x.Cc == finalCurrency).Select(x => x.Rate).Sum(), 4)
                    , Math.Round(amount / json.Where(x => x.Cc == finalCurrency).Select(x => x.Rate).Sum(), 4)
                    , finalCurrency
                    , json.Where(x => x.Cc == finalCurrency).Select(x => x.ExchangeDate).ToArray().First()
                    );
            }
            else if (finalCurrency == "UAH")
            {
                Console.WriteLine("{0} {1} x {2} = {3} {4} (from {5})"
                    , Math.Round(amount, 4)
                    , initialCurrency
                    , Math.Round(json.Where(x => x.Cc == initialCurrency).Select(x => x.Rate).Sum(), 4)
                    , Math.Round(amount * json.Where(x => x.Cc == initialCurrency).Select(x => x.Rate).Sum(), 4)
                    , finalCurrency
                    , json.Where(x => x.Cc == initialCurrency).Select(x => x.ExchangeDate).ToArray().First()
                    );
            }
            else if (!json.Any(x => x.Cc == initialCurrency))
                Console.WriteLine("Error: the database does not contain your initial currency.\n\n");
            else if (!json.Any(x => x.Cc == finalCurrency))
                Console.WriteLine("Error: the database does not contain your final currency.\n\n");
            else
            {
                decimal convertToFinal, convertToUah = amount * json.Where(x => x.Cc == initialCurrency).Select(x => x.Rate).Sum();
                convertToFinal = convertToUah / json.Where(x => x.Cc == finalCurrency).Select(x => x.Rate).Sum();

                Console.WriteLine("{0} {1} x {2} = {3} {4} (from {5})"
                   , Math.Round(amount, 4)
                   , initialCurrency
                   , Math.Round(json.Where(x => x.Cc == initialCurrency).Select(x => x.Rate).Sum() / json.Where(x => x.Cc == finalCurrency).Select(x => x.Rate).Sum(), 4)
                   , Math.Round(convertToFinal, 4)
                   , finalCurrency
                   , json.Where(x => x.Cc == finalCurrency).Select(x => x.ExchangeDate.ToString()).ToArray().First()
                   );
            }

            Console.Write("Tap to exit");
            Console.ReadLine();
        }
    }
}
