using CTCrawler.Parse.Interface;
using CTCrawler.Parse.Model;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace CTCrawler.Parse
{
    public class CTWheather : ICTWheather
    {
        private readonly IHttpCTWheather _httpCT;
        private readonly ICTFifteenDaysParse _fifteenDaysParse;
        public CTWheather(IHttpCTWheather httpCT, ICTFifteenDaysParse fifteenDaysParse) 
        {
            _httpCT = httpCT;
            _fifteenDaysParse = fifteenDaysParse;
        }
        public async Task<CityCT[]> SearchCity(string cityName)
        {
            string json = await _httpCT.RequestCity(cityName);

            CityCT[] cityResponse = JsonSerializer.Deserialize<CityCT[]>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            });

            return cityResponse;
        }
        public async Task<IEnumerable<WeatherCT>> SearchWeatherOfFifteenDays(int cityId, string cityName)
        {
            string html = await _httpCT.RequestWeatherOfFifteenDays(cityId, cityName);
            return _fifteenDaysParse.Parse(html);
        }
    }
}
