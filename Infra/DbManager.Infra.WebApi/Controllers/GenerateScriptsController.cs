using System;
using System.Threading.Tasks;
using DbManager.Domain.Services;
using DbManager.Infra.WebApi.Dto;
using DbManager.Infra.WebApi.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    internal sealed class GenerateScriptsController : ControllerBase
    {
        private readonly ILogger<GenerateScriptsController> _logger;
        private readonly IDbScriptsService _dbScriptsService;

        public GenerateScriptsController(IDbScriptsService dbScriptsService, ILogger<GenerateScriptsController> logger)
        {
            _dbScriptsService = dbScriptsService ?? throw new ArgumentNullException(nameof(dbScriptsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("createtable/{dto}")]
        public async Task<ActionResult<string>> GetCreateTableStringAsync(TableDto dto)
        {
            var validationResult = dto.Validate();
            if (validationResult.IsValid != true)
            {
                return BadRequest(validationResult.Error);
            }

            var table = dto.Map();

            return await _dbScriptsService.GenerateCreateTableScriptAsync(table);
        }
    }
}