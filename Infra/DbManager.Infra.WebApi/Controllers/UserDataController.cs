using System;
using DbManager.Domain.Diagnostics.Logging;
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
        private readonly INullableLogger _logger;
        private readonly IUserContextService _userContextService;

        public UserDataController(IUserContextService userContextService, ILogger<UserDataController> logger)
        {
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
            _logger = logger.Wrap() ?? throw new ArgumentNullException(nameof(logger));
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
            using var loggingScope = _logger.BeginScope("[Setting connection string]");

            try
            {
                _logger.Info?.Log($"Starts processing.");

                _logger.Trace?.Log($"Validation of '{nameof(connectionString)}'.");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    var errorMessage = "Connection string is not specified";
                    _logger.Error?.Log($"Validation error: '{errorMessage}'.");
                    return ValidationProblem(errorMessage);
                }

                _logger.Trace?.Log($"Set connection string for user context.");
                _userContextService.DbConnectionString = connectionString;

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.Error?.Log($"Failed, reason: '{ex}'.");
                return Problem(ex.Message);
            }
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
            using var loggingScope = _logger.BeginScope("[Getting connection string]");

            try
            {
                _logger.Info?.Log($"Starts processing.");

                _logger.Trace?.Log($"Get connection string from user context.");
                var dbConnectionString = _userContextService.DbConnectionString;

                _logger.Debug?.Log($"Result '{dbConnectionString}'.");
                return Ok(dbConnectionString);
            }
            catch (Exception ex)
            {
                _logger.Error?.Log($"Failed, reason: '{ex}'.");
                return Problem(ex.Message);
            }
        }
    }
}