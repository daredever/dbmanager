using System;
using System.Collections.Generic;
using System.Linq;
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
    [Produces("application/json")]
    [Route("api/[controller]")]
    public sealed class DatabaseInfoController : ControllerBase
    {
        private readonly INullableLogger _logger;
        private readonly IDbSchemaService _dbSchemaService;

        public DatabaseInfoController(IDbSchemaService dbSchemaService, ILogger<DatabaseInfoController> logger)
        {
            _dbSchemaService = dbSchemaService ?? throw new ArgumentNullException(nameof(dbSchemaService));
            _logger = logger.Wrap() ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets catalogs for database instance.
        /// </summary>
        /// <returns>Catalogs</returns>
        [HttpGet("catalogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CatalogDto>>> GetCatalogsAsync()
        {
            using var loggingScope = _logger.BeginScope("[Getting all catalogs for db]");

            try
            {
                _logger.Info?.Log($"Starts processing.");

                _logger.Trace?.Log($"Get catalogs from db instance.");
                var catalogs = await _dbSchemaService.GetCatalogsAsync();

                _logger.Trace?.Log($"Mapping from model to dto.");
                var result = catalogs.Select(catalog => catalog.Map()).ToList();
                
                _logger.Debug?.Log($"Result count: '{result.Count}'.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error?.Log($"Failed, reason: '{ex}'.");
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Gets tables for specified catalog.
        /// </summary>
        /// <param name="catalogDto">Catalog</param>
        /// <returns>Tables</returns> 
        [HttpGet("tables")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetTablesAsync([FromQuery] CatalogDto catalogDto)
        {
            using var loggingScope = _logger.BeginScope("[Getting table for catalog]");

            try
            {
                _logger.Info?.Log($"Starts processing.");

                _logger.Trace?.Log($"Validation of '{nameof(catalogDto)}'.");
                var validationResult = catalogDto.Validate();
                if (validationResult.IsValid != true)
                {
                    _logger.Error?.Log($"Validation error: '{validationResult.Error}'.");
                    return ValidationProblem(validationResult.Error);
                }

                _logger.Trace?.Log($"Mapping from dto to model.");
                var catalog = catalogDto.Map();
                
                _logger.Trace?.Log($"Get tables from db instance.");
                var tables = await _dbSchemaService.GetTablesAsync(catalog);

                _logger.Trace?.Log($"Mapping from model to dto.");
                var result = tables.Select(table => table.Map()).ToList();
                
                _logger.Debug?.Log($"Result count: '{result.Count}'.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error?.Log($"Failed, reason: '{ex}'.");
                return Problem(ex.Message);
            }
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
            using var loggingScope = _logger.BeginScope("Getting columns for table]");

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
                
                _logger.Trace?.Log($"Get columns from db instance.");
                var columns = await _dbSchemaService.GetColumnsAsync(table);

                _logger.Trace?.Log($"Mapping from model to dto.");
                var result = columns.Select(column => column.Map()).ToList();
                
                _logger.Debug?.Log($"Result count: '{result.Count}'.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.Error?.Log($"Failed, reason: '{ex}'.");
                return Problem(ex.Message);
            }
        }
    }
}