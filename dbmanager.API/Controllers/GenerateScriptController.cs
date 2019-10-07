using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using dbmanager.Common.Services;
using dbmanager.Common.Consts;

namespace dbmanager.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerateScriptController : ControllerBase
    {
        private readonly ILogger<GenerateScriptController> _logger;
        private readonly IGenerateScriptService _service;

        public GenerateScriptController(ILogger<GenerateScriptController> logger, IGenerateScriptService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("createtable/{catalog}/{schema}/{table}")]
        public Task<string> GetCreateTableString(string catalog, string schema, string table)
        {
            SetConnectionString();
            return _service.GetCreateTableScriptAsync(catalog, schema, table);
        }

        private void SetConnectionString()
        {
            var connectionString = HttpContext.Session.GetString(Consts.ConnectionStringKey);
            _service.SetConnectionString(connectionString);
        }
    }
}
