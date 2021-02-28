using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Task_1
{
    [ApiController]
    [Route("")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<Startup> _logger;

        public StatusController(ILogger<Startup> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetBaseInfo()
        {
            _logger.LogInformation("Main page is got");
            return Content("\"Prime numbers\" by st. Dovhal Vladyslav");
        }
    }
}
