using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Zapalap.Erg.Integration.AspNetCore;

namespace Zapalap.Erg.DemoWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ErgEndpoint(route: "data", alias: "weather-service", description: "Downloads weather data")]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [ErgEndpoint(route: "~/api/recalculate-acl", alias: "recalculate-acl", description: "Recalculates all ACL entries for every entity")]
        [HttpGet]
        public ActionResult RecalculateAcl()
        {
            return Content("recalculated lol");
        }

        [ErgEndpoint(route: "updateWeatherStats", alias: "weather-stats", description: "Updates weather stats for all sources")]
        public ActionResult PostUpdateWeatherStats()
        {
            return Content("weather");
        }
    }
}
