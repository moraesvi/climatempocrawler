namespace CTCrawler.Parse.Model
{
    public class CityResponseCT
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Time { get; set; }
        public object TotalRows { get; set; }
        public object TotalPages { get; set; }
        public object Page { get; set; }
        public DatumCT[] Data { get; set; }
    }
}
