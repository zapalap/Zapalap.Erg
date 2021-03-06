﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Zapalap.Erg.Integration.AspNetCore;
using Zapalap.Erg.Integration.AspNetCore.Attributes;

namespace Zapalap.Erg.DemoWeb.Controllers
{
    public class ServiceMethods : ControllerBase
    {
        [UtilityEndpoint("api/v1/employees/removesalaries",
            Alias = "fix-salaries",
            Description = "Removes salary information from RawData column in database.")]
        public ActionResult RemoveSalaryInformationFromRawData()
        {
            var count = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var item in Enumerable.Range(1, 1000))
            {
                count++;
            }
            stopwatch.Stop();

            return Content($"Processed {count} items in {stopwatch.Elapsed.TotalSeconds} seconds");
        }
    }
}
