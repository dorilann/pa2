namespace RiotService.Models.Account
{
    public class AccountModel
    {
        public string Puuid { get; set; }
        public string GameName { get; set; }
        public string TagLine { get; set; }
        public string AccountId { get; set; }
        public int ProfileIconId { get; set; }
        public long RevisionDate { get; set; }
        public string SummonerId { get; set; }
        public long SummonerLevel { get; set; }
    }
}
