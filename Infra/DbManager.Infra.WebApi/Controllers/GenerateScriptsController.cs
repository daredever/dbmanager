using System;
using System.Threading.Tasks;
using DbManager.Domain.Diagnostics.Logging;
using DbManager.Domain.Services;
using DbManager.Infra.WebApi.Dto;
using DbManager.Infra.WebApi.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class GenerateScriptsController : ControllerBase
    {
        private readonly INullableLogger _logger;
        private readonly IDbScriptsService _dbScriptsService;

        public GenerateScriptsController(IDbScriptsService dbScriptsService, ILogger<GenerateScriptsController> logger)
        {
            _dbScriptsService = dbScriptsService ?? throw new ArgumentNullException(nameof(dbScriptsService));
            _logger = logger.Wrap() ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Generates create table script.
        /// </summary>
        /// <param name="tableDto">Table</param>
        /// <returns>Create table script</returns>
        [HttpPost("createtable")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GenerateCreateTableStringAsync(TableDto tableDto)
        {
            using var loggingScope = _logger.BeginScope("[Generating create table script]");

            try
            {
                _logger.Info?.Log($"Starts processing.");

                _logger.Trace?.Log($"Validation of '{nameof(tableDto)}'.");
                var validationResult = tableDto.Validate();
                if (validationResult.IsValid != true)
                {
                    _logger.Error?.Log($"Validation error: '{validationResult.Error}'.");
                    return ValidationProblem(validationResult.Error);
                }

                _logger.Trace?.Log($"Mapping from dto to model.");
                var table = tableDto.Map();

                _logger.Trace?.Log($"Starts generating create table script " +
                                   $"for table '{table.Catalog}.{table.Schema}.{table.Name}'.");
                var script = await _dbScriptsService.GenerateCreateTableScriptAsync(table);

                _logger.Debug?.Log($"Result '{script}'.");

                return Ok(script);
            }
            catch (Exception ex)
            {
                _logger.Error?.Log($"Failed, reason: '{ex}'.");
                return Problem(ex.Message);
            }
        }
    }
}