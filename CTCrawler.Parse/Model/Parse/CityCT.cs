using System.Linq;

namespace CTCrawler.Parse.Model
{
    public class CityCT
    {
        public string Type { get; set; }
        public CityResponseCT Response { get; set; }
    }
    public static class CityCTExtension
    {
        public static DatumCT[] GetDatum(this CityCT[] cities)
        {
            if (cities == null)
                return new DatumCT[] { };

            return cities.Where(c => string.Equals(c.Type, "city"))
                         .FirstOrDefault()
                         ?.Response
                         ?.Data ?? new DatumCT[] { };
        }
    }
}
