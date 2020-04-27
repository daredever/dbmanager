using System;
using DbManager.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    internal sealed class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserContextService _userContextService;

        public UserDataController(ILogger<UserDataController> logger, IUserContextService userContextService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        [HttpPost("connectionstring")]
        public ActionResult SetConnectionString([FromForm] string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string not specified");
            }

            _userContextService.DbConnectionString = connectionString;

            return Ok();
        }

        [HttpGet("connectionstring")]
        public ActionResult<string> GetConnectionString()
        {
            return _userContextService.DbConnectionString;
        }
    }
}