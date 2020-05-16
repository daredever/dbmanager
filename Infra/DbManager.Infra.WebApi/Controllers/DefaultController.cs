using System;
using DbManager.Domain.Diagnostics.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public sealed class DefaultController : ControllerBase
    {
        private readonly INullableLogger _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger.Wrap() ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            _logger.Trace?.Log("Redirect to swagger UI.");
            return Redirect("/swagger/");
        }
    }
}