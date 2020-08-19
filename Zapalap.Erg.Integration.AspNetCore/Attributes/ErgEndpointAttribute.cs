using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zapalap.Erg.Integration.AspNetCore
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ErgEndpointAttribute : RouteAttribute 
    {
        public string Alias { get; private set; }
        public string Description { get; private set; }

        public ErgEndpointAttribute(string route, string alias, string description) : base(route)
        {
            Alias = alias;
            Description = description;
        }
    }
}
