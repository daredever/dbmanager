using System;
using System.Threading.Tasks;
using DbManager.Domain.Services;
using DbManager.Infra.WebApi.Dto;
using DbManager.Infra.WebApi.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public sealed class GenerateScriptsController : ControllerBase
    {
        private readonly ILogger<GenerateScriptsController> _logger;
        private readonly IDbScriptsService _dbScriptsService;

        public GenerateScriptsController(IDbScriptsService dbScriptsService, ILogger<GenerateScriptsController> logger)
        {
            _dbScriptsService = dbScriptsService ?? throw new ArgumentNullException(nameof(dbScriptsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            var validationResult = tableDto.Validate();
            if (validationResult.IsValid != true)
            {
                return BadRequest(validationResult.Error);
            }

            var table = tableDto.Map();
            var script = await _dbScriptsService.GenerateCreateTableScriptAsync(table);

            return Ok(script);
        }
    }
}