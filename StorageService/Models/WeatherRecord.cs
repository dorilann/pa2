using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StorageService.Models
{
    public class WeatherRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Timezone { get; set; }
        public HourlyData TempData { get; set; }
    }
}
