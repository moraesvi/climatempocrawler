using CTCrawler.Common;
using CTCrawler.Common.Config;
using CTCrawler.Common.Helper;
using CTCrawler.Parse.Interface;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace CTCrawler.Parse
{
    public class HttpCTWheather : IHttpCTWheather
    {
        private int CT_MINUTES_SESSION_TIME = 30;

        private const string CT_FIND_CITY_URI = "json/busca-por-nome";
        private const string CT_FIND_FIFTEEN_DAYS_WEATHER_URI = "previsao-do-tempo/15-dias/cidade/{0}/{1}";

        private static readonly int _maxRetryAttempts = 3;
        private static readonly TimeSpan _pauseBetweenFailures = TimeSpan.FromSeconds(2);
        private static readonly AsyncRetryPolicy _retryPolicy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(_maxRetryAttempts, i => _pauseBetweenFailures);

        private readonly IOptions<ConfigUrl> _configUrl;
        private readonly CookieWebClient _wc;
        public HttpCTWheather(IOptions<ConfigUrl> configUrl)
        {
            _configUrl = configUrl;
            _wc = new CookieWebClient();
        }
        public async Task<string> RequestCity(string cityName)
        {
            await InitSession();

            return await _retryPolicy.ExecuteAsync(async () =>
            {
                Uri uriAdrress = new Uri(string.Concat(_configUrl.Value.CTUrl, CT_FIND_CITY_URI));

                NameValueCollection form = new NameValueCollection();
                form.Add("name", cityName);

                using (CookieWebClient wc = new CookieWebClient(_wc.CookieContainer))
                {
                    string result = await wc.UploadValuesTaskAsync(uriAdrress, form)
                                            .ToStringUTF8();

                    return result;
                }
            });
        }
        public async Task<string> RequestWeatherOfFifteenDays(int cityId, string cityName)
        {
            await InitSession();

            return await _retryPolicy.ExecuteAsync(async () =>
            {
                Uri uriAdrress = new Uri(string.Concat(_configUrl.Value.CTUrl, string.Format(CT_FIND_FIFTEEN_DAYS_WEATHER_URI, cityId, cityName)));

                using (CookieWebClient wc = new CookieWebClient(_wc.CookieContainer))
                {
                    string result = await wc.DownloadStringTaskAsync(uriAdrress);

                    return result;
                }
            });
        }

        #region Private Methods
        private async Task InitSession()
        {
            if (CTSessionIsValid())
                return;

            await _retryPolicy.ExecuteAsync(async () =>
            {
                await _wc.DownloadStringTaskAsync(_configUrl.Value.CTUrl);
            });
        }
        private bool CTSessionIsValid()
        {
            if (_wc.Session == DateTime.MinValue)
                return false;

            return (DateTime.Now - _wc.Session).TotalMinutes <= CT_MINUTES_SESSION_TIME;
        }
        #endregion
    }
}