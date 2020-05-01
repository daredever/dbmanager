using Microsoft.AspNetCore.Mvc;

namespace DbManager.Infra.WebApi.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public sealed class DefaultController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return Redirect("/swagger/");
        }
    }
}