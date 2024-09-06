namespace StorageService.Models
{
    public class WeatherRecord
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Timezone { get; set; }
        public HourlyData TempData { get; set; }
    }
}
