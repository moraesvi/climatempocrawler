using CTCrawler.Parse.Model;
using System.Collections.Generic;

namespace CTCrawler.Parse.Interface
{
    public interface ICTFifteenDaysParse
    {
        IEnumerable<WeatherCT> Parse(string html);
    }
}
