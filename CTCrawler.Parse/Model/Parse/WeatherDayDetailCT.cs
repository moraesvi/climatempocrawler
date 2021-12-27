using System.Text.Json.Serialization;

namespace CTCrawler.Parse.Model
{
    public class WeatherDayDetailCT
    {
        public WeatherDayDetailCT(string imgFilter)
        {
            WeatherImg = WeatherImgCT.WeatherImage(imgFilter);
        }
        public string Period { get; set; }
        public string Desc { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WeatherImgId WeatherImg { get; set; }
    }
}
