using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using StorageService.Models;

namespace StorageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IMongoCollection<WeatherRecord> _collection;

        public HistoryController(IMongoDatabase database)
        {
            _collection = database.GetCollection<WeatherRecord>("WeatherHistory");
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecord([FromBody] WeatherRecord record)
        {
            await _collection.InsertOneAsync(record);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            var history = await _collection.Find(x => x.Timezone == "GMT").ToListAsync();
            return Ok(history);
        }
    }

}
