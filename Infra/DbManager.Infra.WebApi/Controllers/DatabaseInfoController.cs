using System;
using System.Collections.Generic;
using System.Linq;
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
    public sealed class DatabaseInfoController : ControllerBase
    {
        private readonly ILogger<DatabaseInfoController> _logger;
        private readonly IDbSchemaService _dbSchemaService;

        public DatabaseInfoController(IDbSchemaService dbSchemaService, ILogger<DatabaseInfoController> logger)
        {
            _dbSchemaService = dbSchemaService ?? throw new ArgumentNullException(nameof(dbSchemaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets catalogs for database instance.
        /// </summary>
        /// <returns>Catalogs</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>
        [HttpGet("catalogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogsAsync()
        {
            var catalogs = await _dbSchemaService.GetCatalogsAsync();

            return Ok(catalogs.Select(catalog => catalog.Map()).ToList());
        }

        /// <summary>
        /// Gets tables for specified catalog.
        /// </summary>
        /// <param name="catalogDto">Catalog</param>
        /// <returns>Tables</returns>
        /// <response code="200"></response>
        /// <response code="400"></response>  
        [HttpGet("tables")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTablesAsync([FromQuery] CatalogDto catalogDto)
        {
            var validationResult = catalogDto.Validate();
            if (validationResult.IsValid != true)
            {
                return BadRequest(validationResult.Error);
            }

            var catalog = catalogDto.Map();
            var tables = await _dbSchemaService.GetTablesAsync(catalog);

            return Ok(tables.Select(table => table.Map()).ToList());
        }

        /// <summary>
        /// Gets columns for specified table.
        /// </summary>
        /// <param name="tableDto">Table</param>
        /// <returns>Column</returns>
        [HttpGet("columns")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ColumnDto>>> GetColumnsAsync([FromQuery] TableDto tableDto)
        {
            var validationResult = tableDto.Validate();
            if (validationResult.IsValid != true)
            {
                return BadRequest(validationResult.Error);
            }

            var table = tableDto.Map();
            var columns = await _dbSchemaService.GetColumnsAsync(table);

            return Ok(columns.Select(column => column.Map()).ToList());
        }
    }
}