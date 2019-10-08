using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;
using dbmanager.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using dbmanager.Common.Consts;

namespace dbmanager.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseInfoController : ControllerBase
    {
        private readonly ILogger<DatabaseInfoController> _logger;
        private readonly IDbInfoService _service;

        public DatabaseInfoController(ILogger<DatabaseInfoController> logger, IDbInfoService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("catalogs")]
        public Task<IEnumerable<Catalog>> GetCatalogsAsync()
        {
            SetConnectionString();
            return _service.GetCatalogsAsync();
        }

        [HttpGet]
        [Route("tables/{catalog}")]
        public Task<IEnumerable<Table>> GetTablesAsync(string catalog)
        {
            SetConnectionString();
            return _service.GetTablesAsync(catalog);
        }

        [HttpGet]
        [Route("columns/{catalog}/{schema}/{table}")]
        public Task<IEnumerable<Column>> GetColumnsAsync(string catalog, string schema, string table)
        {
            SetConnectionString();
            return _service.GetColumnsAsync(catalog, schema, table);
        }

        private void SetConnectionString()
        {
            var connectionString = HttpContext.Session.GetString(Consts.ConnectionStringKey);
            _service.SetConnectionString(connectionString);
        }
    }
}
