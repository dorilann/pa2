using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Health : ControllerBase
    {
        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok("Is Alive");
        }
    }
}
