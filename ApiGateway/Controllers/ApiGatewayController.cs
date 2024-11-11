using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

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
        [HttpGet("account/{region}/{server}/{gameName}/{tag}")]
        public async Task<IActionResult> GetProfile(string region, string server, string gameName, string tag)
        {
            try
            {
                // Log the start of request processing
                _logger.LogInformation("Starting request processing for {GameName}/{Tag} in region {Region}/{Server}", gameName, tag, region, server);

                var client = _httpClientFactory.CreateClient();

                // Request to storageservice to retrieve profile data
                _logger.LogInformation("Sending request to storageservice to retrieve profile data...");
                var data = await client.GetStringAsync($"http://storageservice:8080/Champion/GetProfileData/{gameName}/{tag}");

                if (string.IsNullOrEmpty(data))
                {
                    // If data is not found in storageservice, request it from riotservice
                    _logger.LogInformation("Data not found in storageservice. Requesting from riotservice...");

                    data = await client.GetStringAsync($"http://riotservice:8080/Account/{region}/{server}/{gameName}/{tag}");
                    _logger.LogInformation("Data received from riotservice: {Data}", data);

                    // Prepare data for storage and send it to storageservice for insertion
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    _logger.LogInformation("Sending data to storageservice for insertion...");
                    await client.PostAsync("http://storageservice:8080/Champion/InsertProfileData", content);
                }

                // Log the retrieved profile data
                _logger.LogInformation("Profile data: {Data}", data);

                var profile = JsonConvert.DeserializeObject<Profile>(data);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                // Log an error if something goes wrong during the request
                _logger.LogError(ex, "Error processing request");
                return BadRequest(ex.Message);
            }
        }


    }
}
