using System;
using DbManager.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;
        private readonly IUserContextService _userContextService;

        public UserDataController(ILogger<UserDataController> logger, IUserContextService userContextService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        /// <summary>
        /// Sets db connection string.
        /// </summary>
        /// <param name="connectionString">Db connection string</param>
        [HttpPost("connectionstring")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult SetConnectionString([FromForm] string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return BadRequest("Connection string not specified");
            }

            _userContextService.DbConnectionString = connectionString;

            return Accepted();
        }

        /// <summary>
        /// Gets db connection string.
        /// </summary>
        /// <returns>Connection string</returns>
        [HttpGet("connectionstring")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> GetConnectionString()
        {
            return Ok(_userContextService.DbConnectionString);
        }
    }
}