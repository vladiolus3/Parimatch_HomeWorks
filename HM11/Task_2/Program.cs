using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        static async Task Main()
        {
            var path = "address.json";
            if (!File.Exists(path))
            {
                Console.WriteLine("Not found correct base address");
                return;
            }

            string url;
            try
            {
                var text = File.ReadAllText(path);
                url = JsonSerializer.Deserialize<string>(text);
            }
            catch
            {
                Console.WriteLine("Cou1d not read the contents of the file");
                return;
            }

            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    using var client = new HttpClient()
                    {
                        BaseAddress = new Uri(url)
                    };
                    await TestAuth(client);
                    await TestRatesSame(client);
                    await TestRatesCorrect(client);
                    await TestRatesIncorrect(client);
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("Failed to create request");
                }
            }
            else
            {
                Console.WriteLine("Found url is null");
            }

            Console.ReadKey();
        }

        static async Task TestAuth(HttpClient client)
        {
            Console.WriteLine("POST http://localhost:5000/Auth/reg");
            var json = JsonSerializer.Serialize(new AuthPair
            {
                Login = "login",
                Password = "password"
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/Auth/reg", content);

            var text = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(text);

            Console.WriteLine("Received Body:\t" + data["Message"]);
            Console.WriteLine("Expected Body:\tThe method or operation is not implemented.");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.OK);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.OK && data["Message"].ToString() == "The method or operation is not implemented.");
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestRatesSame(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/Rates/uah/uah?amount=5");
            var response = await client.GetAsync("/Rates/uah/uah?amount=5");
            var text = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Received Body:\t" + JsonSerializer.Deserialize<int>(text));
            Console.WriteLine("Expected Body:\t5");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.OK);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.OK && JsonSerializer.Deserialize<int>(text) == 5);
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestRatesCorrect(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/Rates/eur/uah?amount=5");
            var response = await client.GetAsync("/Rates/eur/uah?amount=5");
            var text = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Received Body:\t" + JsonSerializer.Deserialize<double>(text));
            Console.WriteLine("Expected Body:\t165.375");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.OK);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.OK && JsonSerializer.Deserialize<double>(text) == 165.375);
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestRatesIncorrect(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/Rates/eus/uah?amount=5");
            var response = await client.GetAsync("/Rates/eus/uah?amount=5");
            var text = await response.Content.ReadAsStringAsync();

            Console.WriteLine("Received Body:\t" + text);
            Console.WriteLine("Expected Body:\tInvalid currency code");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.BadRequest);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.BadRequest && text == "Invalid currency code");
            Console.WriteLine("Test finished!\n\n");
        }
    }
}