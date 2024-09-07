using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiGatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ILogger<ApiGatewayController> _logger;

        public ApiGatewayController(ILogger<ApiGatewayController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("weather/{city,date}")]
        public async Task<IActionResult> GetWeather(string city,string date)
        {
            var client = _httpClientFactory.CreateClient();
            var hasAnyData = await client.GetStringAsync($"http://storageservice/api/history/{date}");
            var response = await client.GetStringAsync($"http://weatherservice/api/weather/{city}");
            return Ok(response);
        }

    }
}
