using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Task_1.Contracts;
using Task_1.Models;
using Task_1.Options;

namespace Task_1.Clients
{
    /// <summary>
    /// NBU currency rates provider.
    /// For more details, please visit
    /// https://bank.gov.ua/ua/open-data/api-dev
    /// </summary>
    public class NbuClient : IRatesProviderClient
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Constructor.
        /// </summary>
        public NbuClient(HttpClient client, IOptions<NbuClientOptions> options)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            
            // Use IOptions.Value
            var optionsValue = options?.Value;
            if (optionsValue == null || !optionsValue.IsValid)
                throw new ArgumentOutOfRangeException(nameof(options), "Options are invalid");
            
            _client.BaseAddress = new Uri(optionsValue.BaseAddress);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CurrencyRate>> GetRatesAsync()
        {
            using var response = await _client.GetAsync(@"NBUStatService/v1/statdirectory/exchange?json");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStreamAsync();
            var content = await JsonSerializer.DeserializeAsync<NbuRate[]>(json);
            
            return content.Select(nbuRate => new CurrencyRate
            {
                Currency = nbuRate.Currency?.ToUpperInvariant(),
                Rate = nbuRate.Rate
            });
        }
    }
}
