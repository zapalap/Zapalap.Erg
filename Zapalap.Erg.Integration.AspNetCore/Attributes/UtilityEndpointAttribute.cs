using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zapalap.Erg.Integration.AspNetCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class UtilityEndpointAttribute : RouteAttribute
    {
        public string Alias { get; set; }
        public string Description { get; set; }

        public UtilityEndpointAttribute(string route) : base(route)
        {
        }
    }
}
