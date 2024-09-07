using MongoDB.Bson.Serialization.Attributes;

namespace StorageService.Models.Account
{
    public class AccountInfo
    {
        public string Puuid { get; set; }
        public string GameName { get; set; }
        public string TagLine { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime LastChanged { get; set; } = DateTime.UtcNow;
    }
}
