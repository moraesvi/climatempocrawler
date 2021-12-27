using CTCrawler.Parse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTCrawler.Parse.Interface
{
    public interface ICTWheather
    {
        Task<CityCT[]> SearchCity(string cityName);
        Task<IEnumerable<WeatherCT>> SearchWeatherOfFifteenDays(int cityId, string cityName);
    }
}
