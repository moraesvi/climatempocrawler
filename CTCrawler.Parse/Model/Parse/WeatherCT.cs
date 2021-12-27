using System.Text.Json.Serialization;

namespace CTCrawler.Parse.Model
{
    public class WeatherCT : WeatherImgCT
    {
        public WeatherCT(string imgFilter)
        {
            WeatherImg = WeatherImgCT.WeatherImage(imgFilter);
        }
        public string Title { get; set; }
        public int Day { get; set; }
        public string WeekDay { get; set; }
        public short MinTemp { get; set; }
        public short MaxTemp { get; set; }
        public WeatherPreceptCT Precept { get; set; } = new WeatherPreceptCT();
        public string Desc { get; set; }
        public WeatherDayDetailCT[] Periods { get; set; } = new WeatherDayDetailCT[] { };

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public WeatherImgId WeatherImg { get; set; }
    }
}
