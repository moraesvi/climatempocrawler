using CTCrawler.Common.Config;
using CTCrawler.Parse;
using CTCrawler.Parse.Interface;
using CTCrawler.Parse.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CTCrawler.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IHttpCTWheather httpCT = new HttpCTWheather(null);
            ICTFifteenDaysParse fifteenDaysParse = new CTFifteenDaysParse();

            ICTWheather wheather = new CTWheather(httpCT, fifteenDaysParse);

            DatumCT[] datumCTs = wheather.SearchCity("São Paulo").Result.GetDatum();

            IEnumerable<WeatherCT> te = wheather.SearchWeatherOfFifteenDays(datumCTs.FirstOrDefault().IdCity, datumCTs.FirstOrDefault().City).Result;
        }
    }
}
