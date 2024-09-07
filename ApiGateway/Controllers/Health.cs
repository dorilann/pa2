using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
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
