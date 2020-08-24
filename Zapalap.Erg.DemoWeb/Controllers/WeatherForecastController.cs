using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Zapalap.Erg.Integration.AspNetCore;
using Zapalap.Erg.Integration.AspNetCore.Attributes;

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

        [UtilityEndpoint("api/v1/recalcacl",
            Alias = "acl-recalc",
            Description = "Recalculates ACL entries for all units. Warning! Takes some time!")]
        public ActionResult RecalculateAclForAllUnits()
        {
            var countProcessed = 0;

            var watch = new Stopwatch();
            watch.Start();
            foreach (var item in Enumerable.Range(1, 10000))
            {
                countProcessed++;
            }
            watch.Stop();
            return Content($"Processed {countProcessed} in {watch.Elapsed.TotalSeconds} seconds");
        }
    }
}
