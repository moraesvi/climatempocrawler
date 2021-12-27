using CTCrawler.Common.Helper;
using System;

namespace CTCrawler.Parse.Model
{
    public class WeatherImgCT
    {
        private const string _1_IMG = "Images/1_sun.png";
        private const string _2R_IMG = "Images/2r_SunnyClouds.png";
        private const string _2RN_IMG = "Images/2rn_Moon.png";
        private const string _3_IMG = "Images/3_rain.png";
        private const string _3N_IMG = "Images/3n_NightRain.png";
        private const string _4T_IMG = "Images/4t_CloudyRain.png";
        public static WeatherImgId WeatherImage(string filter)
        {
            if (filter.IndexOf("1.svg", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return WeatherImgId.Sun;
            }
            else if (filter.IndexOf("2r.svg", StringComparison.OrdinalIgnoreCase) >= 0 || filter.IndexOf("2.svg", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return WeatherImgId.SunnyClouds;
            }
            else if (filter.IndexOf("2rn.svg", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return WeatherImgId.Moon;
            }
            else if (filter.IndexOf("3.svg", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return WeatherImgId.Rain;
            }
            else if (filter.IndexOf("3n.svg", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return WeatherImgId.NightRain;
            }
            else if (filter.IndexOf("4t.svg", StringComparison.OrdinalIgnoreCase) >= 0 || filter.IndexOf("4.svg", StringComparison.OrdinalIgnoreCase) >= 0 || filter.IndexOf("4r.svg", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return WeatherImgId.CloudyRain;
            }

            return WeatherImgId.NaoDefinido;
        }
    }
}
