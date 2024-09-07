using Microsoft.AspNetCore.Mvc;

namespace StorageService.Controllers
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
