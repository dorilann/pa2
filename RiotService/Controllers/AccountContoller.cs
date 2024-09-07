using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RiotService.Models.Account;
using System;

namespace RiotService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AccountController(ILogger<AccountController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;

            var token = Environment.GetEnvironmentVariable("X-RIOT-TOKEN")
                           ?? _configuration["X-Riot-Token"];

            _httpClient.DefaultRequestHeaders.Add("X-Riot-Token", token);
        }


        /// <summary>
        /// Получает информацию об аккаунте по PUUID.
        /// </summary>
        /// <param name="region">Регион Riot Games API (например, "europe", "americas")</param>
        /// <param name="puuid">Идентификатор PUUID игрока.</param>
        /// <returns>Информация об аккаунте в JSON формате.</returns>
        [HttpGet("{region}/{puuid}",Name = "GetAccountByPuuId")]
        public async Task<IActionResult> GetByPuuId(string region, string puuid)
        {
            var response = await _httpClient.GetAsync($"https://{region}.api.riotgames.com/riot/account/v1/accounts/by-puuid/{puuid}");
            var body = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<AccountModel>(body);
            return Ok(person);
        }

        /// <summary>
        /// Получает информацию о Riot ID.
        /// </summary>
        /// <param name="region">Регион Riot Games API (например, "europe", "americas")</param>
        /// <param name="gameName">Имя игрока в игре</param>
        /// <param name="tagLine">Тег игрока</param>
        /// <returns>Информация об аккаунте в JSON формате</returns>
        [HttpGet("{region}/{gameName}/{tagLine}",Name = "GetByRiotId")]
        public async Task<IActionResult> GetByRiotId(string region,string gameName,string tagLine)
        {
            var response = await _httpClient.GetAsync($"https://{region}.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}");
            var body = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<AccountModel>(body);
            return Ok(person);
        }
    }
}
