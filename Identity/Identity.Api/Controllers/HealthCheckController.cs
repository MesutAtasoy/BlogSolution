using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("[Controller]")]
    public class HealthCheckController : Controller
    {
        public HealthCheckController()
        {
        }

        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok("Identity Api alive and reachable");
        }
    }
}