using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
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
                    await TestInfo(client);
                    await TestNumberIsPrime(client);
                    await TestNumberIsNotPrime(client);
                    await TestCorrectRange(client);
                    await TestEmptyRange(client);
                    await TestIncorrectRange(client);
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

        static async Task TestInfo(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/");
            var response = await client.GetAsync("");
            var text = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Received Body:\t" + text);
            Console.WriteLine("Expected Body:\t\"Prime numbers\" by st. Dovhal Vladyslav");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.OK);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.OK && text == "Prime numbers\" by st. Dovhal Vladyslav");
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestNumberIsPrime(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/primes/5");
            var response = await client.GetAsync("/primes/5");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.OK);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.OK);
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestNumberIsNotPrime(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/primes/4");
            var response = await client.GetAsync("/primes/4");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.NotFound);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.NotFound);
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestCorrectRange(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/primes?from=2&to=5");
            var response = await client.GetAsync("/primes?from=2&to=5");

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Received Body:\t" + body);
            Console.WriteLine("Expected Body:\t[2,3,5]");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.OK);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(body == "[2,3,5]" && response.StatusCode == HttpStatusCode.OK);
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestEmptyRange(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/primes?from=-5&to=1");
            var response = await client.GetAsync("/primes?from=-5&to=1");

            var body = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Received Body:\t" + body);
            Console.WriteLine("Expected Body:\t[]");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.OK);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(body == "[]" && response.StatusCode == HttpStatusCode.OK);
            Console.WriteLine("Test finished!\n\n");
        }

        static async Task TestIncorrectRange(HttpClient client)
        {
            Console.WriteLine("GET http://localhost:5000/primes?to=abcd");
            var response = await client.GetAsync("/primes?to=abcd");
            Console.WriteLine("Expected Status:\t" + HttpStatusCode.BadRequest);
            Console.WriteLine("Received Status:\t" + response.StatusCode);
            Debug.Assert(response.StatusCode == HttpStatusCode.BadRequest);
            Console.WriteLine("Test finished!\n\n");
        }
    }
}