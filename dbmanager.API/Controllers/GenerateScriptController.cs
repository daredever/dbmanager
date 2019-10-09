using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using dbmanager.Common.Services;

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

        [HttpGet("createtable/{catalog}/{schema}/{table}")]
        public Task<string> GetCreateTableStringAsync(string catalog, string schema, string table)
        {
            return _service.GetCreateTableScriptAsync(catalog, schema, table);
        }
    }
}
