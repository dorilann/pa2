using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherServiceTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("Chishinau")]
        public async Task<IActionResult> GetWeather()
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"https://api.open-meteo.com/v1/forecast?latitude=47.0056&longitude=28.8575&hourly=temperature_2m,wind_speed_80m";
            var response = await client.GetStringAsync(url);
            return Ok(response);
        }
    }

}
