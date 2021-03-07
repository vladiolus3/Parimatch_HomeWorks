using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Task_1.Services;

namespace Task_1.Controllers
{
    /// <summary>
    /// Rates Controller for sending requests to get result of exchange 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _rates;

#pragma warning disable 1591
        public RatesController(
            IRatesService rates,
            ILogger<RatesController> logger)
        {
            _rates = rates;
            _logger = logger;
        }
#pragma warning restore 1591

        /// <summary>
        /// Method for for sending requests to get result of exchange without body
        /// </summary>
        /// <param name="srcCurrency"></param>
        /// <param name="dstCurrency"></param>
        /// <param name="amount"></param>
        /// <returns>decimal</returns>
        [HttpGet("{srcCurrency}/{dstCurrency}")]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(decimal), 200)]
        public async Task<ActionResult<decimal>> Get(string srcCurrency, string dstCurrency, decimal? amount)
        {
            var exchange =  await _rates.ExchangeAsync(srcCurrency, dstCurrency, amount ?? decimal.One);
            if (!exchange.HasValue)
            {
                _logger.LogDebug($"Can't exchange from '{srcCurrency}' to '{dstCurrency}'");
                return BadRequest("Invalid currency code");
            }
            return exchange.Value.DestinationAmount;
        }
    }
}
