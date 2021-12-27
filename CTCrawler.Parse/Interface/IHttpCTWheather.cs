using System.Threading.Tasks;

namespace CTCrawler.Parse.Interface
{
    public interface IHttpCTWheather
    {
        Task<string> RequestCity(string cityName);
        Task<string> RequestWeatherOfFifteenDays(int cityId, string cityName);
    }
}
