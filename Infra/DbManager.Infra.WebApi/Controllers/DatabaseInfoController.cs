using System;
using System.Collections.Generic;
using System.Linq;
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
    internal sealed class DatabaseInfoController : ControllerBase
    {
        private readonly ILogger<DatabaseInfoController> _logger;
        private readonly IDbSchemaService _dbSchemaService;

        public DatabaseInfoController(IDbSchemaService dbSchemaService, ILogger<DatabaseInfoController> logger)
        {
            _dbSchemaService = dbSchemaService ?? throw new ArgumentNullException(nameof(dbSchemaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("catalogs")]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogsAsync()
        {
            var catalogs = await _dbSchemaService.GetCatalogsAsync();

            return catalogs.Select(catalog => catalog.Map()).ToList();
        }

        [HttpGet("tables/{dto}")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTablesAsync(CatalogDto dto)
        {
            var validationResult = dto.Validate();
            if (validationResult.IsValid != true)
            {
                return BadRequest(validationResult.Error);
            }

            var catalog = dto.Map();
            var tables = await _dbSchemaService.GetTablesAsync(catalog);

            return tables.Select(table => table.Map()).ToList();
        }

        [HttpGet("columns/{dto}")]
        public async Task<ActionResult<IEnumerable<ColumnDto>>> GetColumnsAsync(TableDto dto)
        {
            var validationResult = dto.Validate();
            if (validationResult.IsValid != true)
            {
                return BadRequest(validationResult.Error);
            }
            
            var table = dto.Map();
            var columns = await _dbSchemaService.GetColumnsAsync(table);

            return columns.Select(column => column.Map()).ToList();
        }
    }
}