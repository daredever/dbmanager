using System;
using System.Collections.Generic;
using System.Linq;
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
    public class DatabaseInfoController : ControllerBase
    {
        private readonly ILogger<DatabaseInfoController> _logger;
        private readonly IDbSchemaService _dbSchemaService;

        public DatabaseInfoController(IDbSchemaService dbSchemaService, ILogger<DatabaseInfoController> logger)
        {
            _dbSchemaService = dbSchemaService ?? throw new ArgumentNullException(nameof(dbSchemaService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("catalogs")]
        public async Task<IEnumerable<CatalogDto>> GetCatalogsAsync()
        {
            var catalogs = await _dbSchemaService.GetCatalogsAsync();
            var catalogsDto = catalogs
                .Select(c =>
                    new CatalogDto
                    {
                        Name = c.Name
                    })
                .ToList();

            return catalogsDto;
        }

        [HttpGet("tables/{dto}")]
        public async Task<IEnumerable<TableDto>> GetTablesAsync(CatalogDto dto)
        {
            var catalog = new Catalog
            {
                Name = dto.Name
            };

            var tables = await _dbSchemaService.GetTablesAsync(catalog);
            var tablesDto = tables
                .Select(t =>
                    new TableDto
                    {
                        Catalog = t.Catalog,
                        Schema = t.Schema,
                        Name = t.Name
                    })
                .ToList();

            return tablesDto;
        }

        [HttpGet("columns/{dto}")]
        public async Task<IEnumerable<ColumnDto>> GetColumnsAsync(TableDto dto)
        {
            var table = new Table
            {
                Catalog = dto.Catalog,
                Schema = dto.Schema,
                Name = dto.Name
            };

            var columns = await _dbSchemaService.GetColumnsAsync(table);
            var columnsDto = columns
                .Select(c =>
                    new ColumnDto
                    {
                        Catalog = c.Catalog,
                        Schema = c.Schema,
                        Name = c.Name,
                        Type = c.Type,
                        IsNullable = c.IsNullable,
                        CharactersMaxLength = c.CharactersMaxLength
                    })
                .ToList();

            return columnsDto;
        }
    }
}