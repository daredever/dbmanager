using System;
using System.Threading.Tasks;
using DbManager.Domain.Models.DefaultImpl;
using DbManager.Domain.Services;
using DbManager.Infra.WebApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerateScriptsController : ControllerBase
    {
        private readonly ILogger<GenerateScriptsController> _logger;
        private readonly IDbScriptsService _dbScriptsService;

        public GenerateScriptsController(IDbScriptsService dbScriptsService, ILogger<GenerateScriptsController> logger)
        {
            _dbScriptsService = dbScriptsService ?? throw new ArgumentNullException(nameof(dbScriptsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("createtable/{dto}")]
        public Task<string> GetCreateTableStringAsync(TableDto dto)
        {
            var table = new Table
            {
                Catalog = dto.Catalog,
                Schema = dto.Schema,
                Name = dto.Name
            };

            return _dbScriptsService.GenerateCreateTableScriptAsync(table);
        }
    }
}