using System;

namespace CTCrawler.Common.Config
{
    public class ConfigUrl
    {
        private string _ctUrl;
        public string CTUrl
        {
            get
            {
                return _ctUrl;
            }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidOperationException("Clima Tempo URI is required");

                _ctUrl = value;
                if (_ctUrl.LastIndexOf("/") < 0)
                    _ctUrl = string.Concat(_ctUrl, "/");
            }
        }
    }
}
