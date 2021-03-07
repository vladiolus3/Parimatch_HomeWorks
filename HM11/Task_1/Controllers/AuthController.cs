using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Task_1.Models;

namespace Task_1.Controllers
{
    /// <summary>
    /// Authorization controller for user registration  
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
       
#pragma warning disable 1591
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }
#pragma warning restore 1591

        /// <summary>
        /// Async method for registration of user
        /// </summary>
        /// <param name="pair"></param>
        [HttpPost("reg")]
        [ProducesResponseType(200)]
        public async Task Register([FromBody] AuthPair pair)
        {
            _logger.LogInformation($"Attempt to register user <{pair.Login}>");
            throw new NotImplementedException();
        }
    }
}
