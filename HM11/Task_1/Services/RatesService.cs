﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Task_1.Clients;
using Task_1.Models;
using Task_1.Options;

namespace Task_1.Services
{
    /// <summary>
    /// Class for getting of result of exchange 
    /// </summary>
    public class RatesService : IRatesService
    {
        private const string RatesCacheKey = "rates";
        private static readonly TimeSpan DefaultCacheLifeTime = TimeSpan.FromHours(1d);

        private readonly IRatesProviderClient _client;
        private readonly IMemoryCache _cache;

        private readonly string _baseCurrency;
        private readonly CacheOptions _cacheOptions;

        /// <summary>
        /// RatesService constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="cache"></param>
        /// <param name="ratesOptions"></param>
        /// <param name="cacheOptions"></param>
        public RatesService(
            IRatesProviderClient client,
            IMemoryCache cache,
            IOptions<RatesOptions> ratesOptions,
            IOptions<CacheOptions> cacheOptions)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));

            var ratesOptionsValue = ratesOptions?.Value;
            if (ratesOptionsValue == null || !ratesOptionsValue.IsValid)
                throw new ArgumentOutOfRangeException(nameof(ratesOptions), "Options are invalid");
            _baseCurrency = ratesOptionsValue.BaseCurrency.ToUpperInvariant();

            _cacheOptions = cacheOptions?.Value ??
                throw new ArgumentOutOfRangeException(nameof(cacheOptions), "Options are invalid");
        }

        /// <summary>
        /// Method for checking of currencies and getting result
        /// </summary>
        /// <param name="srcCurrency"></param>
        /// <param name="destCurrency"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<ExchangeResult?> ExchangeAsync(string srcCurrency, string destCurrency, decimal amount)
        {
            var comparer = StringComparer.Ordinal;

            if (string.IsNullOrWhiteSpace(srcCurrency) ||
                string.IsNullOrWhiteSpace(destCurrency)) return null;

            srcCurrency = srcCurrency.ToUpperInvariant();
            destCurrency = destCurrency.ToUpperInvariant();

            // check case with same src. and dest. currencies
            if (comparer.Equals(srcCurrency, destCurrency))
                return new ExchangeResult(decimal.One, amount, amount);

            var rates = await GetRatesAsync();
            if (rates == null) throw new InvalidOperationException("Currency rates are invalid");

            var srcRate = comparer.Equals(srcCurrency, _baseCurrency)
                ? decimal.One
                : rates.FirstOrDefault(r => comparer.Equals(r.Currency, srcCurrency))?.Rate;

            var destRate = comparer.Equals(destCurrency, _baseCurrency)
                ? decimal.One
                : rates.FirstOrDefault(r => comparer.Equals(r.Currency, destCurrency))?.Rate;

            if (!srcRate.HasValue || !destRate.HasValue) return null;

            var rate = srcRate.Value / destRate.Value;
            return new ExchangeResult(rate, amount, rate * amount);
        }

        private async Task<CurrencyRate[]> GetRatesAsync()
        {
            return await _cache.GetOrCreateAsync(RatesCacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    _cacheOptions.RatesCacheLifeTime ?? DefaultCacheLifeTime;

                var rates = await _client.GetRatesAsync();
                return rates is CurrencyRate[] ratesArray
                    ? ratesArray
                    : rates?.ToArray();
            });
        }

        /// <summary>
        /// Remove from cache
        /// </summary>
        /// <returns></returns>
        public Task ActualizeRatesAsync()
        {
            _cache.Remove(RatesCacheKey);
            return Task.CompletedTask;
        }
    }
}
