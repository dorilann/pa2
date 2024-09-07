using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using StorageService.Models.Champions;

namespace StorageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChampionController : ControllerBase
    {
        private readonly IMongoCollection<Champion> _championCollection;

        public ChampionController(IMongoDatabase database)
        {
            _championCollection = database.GetCollection<Champion>("Champions");
        }

        [HttpPost("insert")]
        public async Task<IActionResult> InsertData()
        {
            try
            {
                string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "champion.json");
                var jsonData = System.IO.File.ReadAllText(jsonFilePath);

                JObject champions = JObject.Parse(jsonData);
                var championsList = new List<Champion>();

                if (champions["data"] is null)
                    return BadRequest($"Wrong file location: {jsonFilePath}");

                foreach (var champion in champions["data"])
                {
                    var championModel = champion.First.ToObject<Champion>();
                    championsList.Add(championModel);
                }
                await _championCollection.InsertManyAsync(championsList);

                return Ok("Data inserted into MongoDB.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("GetByKey/{key}")]
        public async Task<IActionResult> GetChampionByKey(long key)
        {
            var champ = await _championCollection.Find(x => x.Key == key).FirstOrDefaultAsync();
            return Ok(champ);
        }


    }

}
