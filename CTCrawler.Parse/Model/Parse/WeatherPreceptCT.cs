namespace CTCrawler.Parse.Model
{
    public class WeatherPreceptCT
    {
        public float Percent { get; set; }
        public WeatherRaindropCT Raindrop { get; set; } = new WeatherRaindropCT();
    }
}
