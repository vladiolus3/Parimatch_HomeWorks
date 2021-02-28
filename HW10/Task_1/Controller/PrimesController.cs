using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task_1.Controller
{
    [ApiController]
    [Route("primes")]
    public class PrimesController : ControllerBase
    {
        private readonly ILogger<Startup> _logger;

        public PrimesController(ILogger<Startup> logger)
        {
            _logger = logger;
        }

        [HttpGet("{number:int}")]
        public async Task<IActionResult> IsPrime([FromRoute] int number)
        {
            _logger.LogInformation("The number is checked for simplicity");
            if (number > 1 && await PrimeNumberFinder.IsPrime(number))
            {
                _logger.LogInformation($"The number {number} is prime");
                return Ok();
            }

            _logger.LogInformation($"The number {number} is not prime");
            return NotFound($"The number {number} is not prime");
        }

        [HttpGet]
        public async Task<IActionResult> PrimeRange([FromQuery] int from, [FromQuery] int to)
        {
            _logger.LogInformation("The range is checked for simplicity");
            if (!HttpContext.Request.Query.ContainsKey("from") ||
                    !HttpContext.Request.Query.ContainsKey("to") ||
                     !int.TryParse(HttpContext.Request.Query["from"].FirstOrDefault(), out _) ||
                     !int.TryParse(HttpContext.Request.Query["to"].FirstOrDefault(), out _)
            )
            {
                _logger.LogInformation("Not enough correct parameters");
                return BadRequest("Not enough correct parameters");
            }

            var json = JsonSerializer.Serialize(await PrimeNumberFinder.PrimeRange(from, to));
            _logger.LogInformation("Search for prime numbers in the range is over");
            return Content(json);
        }
    }
}
