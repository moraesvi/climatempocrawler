using CTCrawler.Common.Config;
using CTCrawler.Parse.Interface;
using CTCrawler.Parse.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTCrawler.WebApi.Controllers
{
    [Route("api/ctCrawler")]
    [ApiController]
    public class CTController : ControllerBase
    {
        private readonly ICTWheather _wheather;
        public CTController(ICTWheather wheather) 
        {
            _wheather = wheather;
        }

        [HttpGet("getCity/{cityName}")]
        public async Task<ActionResult> GetCity(string cityName) 
        {
            if (string.IsNullOrEmpty(cityName))
                return BadRequest("campo nulo");

            CityCT[] cities = await _wheather.SearchCity(cityName);

            return Ok(cities);
        }

        [HttpGet("getWeatherFifteenDays/{cityId}")]
        public async Task<ActionResult> SearchWeatherOfFifteenDays(int cityId)
        {
            if (cityId <= 0)
                return BadRequest("campo nulo");

            IEnumerable<WeatherCT> weatherFifteenDays = await _wheather.SearchWeatherOfFifteenDays(cityId, string.Empty);

            return Ok(weatherFifteenDays);
        }
    }
}
