using Microsoft.AspNetCore.Mvc;

namespace ASP.Net_Core_WebAPI_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : Controller
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("Dogs house service. Version 1.0.1");
        }
    }
}
