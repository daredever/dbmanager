using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dbmanager.Common.Services;

namespace dbmanager.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly IHttpContextService _httpContextService;

        public UserDataController(ILogger<UserDataController> logger, IHttpContextService httpContextService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextService = httpContextService ?? throw new ArgumentNullException(nameof(httpContextService));
        }

        [HttpPost("connectionstring")]
        public IActionResult SetConnectionString([FromForm] string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string not specified");
            }

            _httpContextService.DBConnectionString = connectionString;

            return Ok();
        }

        [HttpGet("connectionstring")]
        public string GetConnectionString()
        {
            return _httpContextService.DBConnectionString;
        }
    }
}