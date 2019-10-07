using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using dbmanager.Common.Consts;

namespace dbmanager.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDataController : ControllerBase
    {
        private readonly ILogger<UserDataController> _logger;

        public UserDataController(ILogger<UserDataController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("connectionstring")]
        public IActionResult SetConnectionString([FromForm] string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                return Ok("Connection string not specified");
            }

            HttpContext.Session.SetString(Consts.ConnectionStringKey, connectionString);

            return Ok();
        }
    }
}
