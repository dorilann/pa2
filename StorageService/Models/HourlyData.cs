namespace StorageService.Models
{
    public class HourlyData
    {
        public List<string> Time { get; set; }  
        public List<string> Temperature_2m { get; set; }
        public List<string> Wind_speed_80m { get; set; }
    }
}