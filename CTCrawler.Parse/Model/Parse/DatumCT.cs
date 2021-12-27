namespace CTCrawler.Parse.Model
{
    public class DatumCT
    {
        public int IdLocale { get; set; }
        public int IdCity { get; set; }
        public bool Capital { get; set; }
        public int IdCountry { get; set; }
        public string Ac { get; set; }
        public string Country { get; set; }
        public string Uf { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public bool Seaside { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Tourist { get; set; }
        public bool Agricultural { get; set; }
        public string Base { get; set; }
        public int SearchPoints { get; set; }
        public int? IdBeach { get; set; }
        public string Beach { get; set; }
    }
}
