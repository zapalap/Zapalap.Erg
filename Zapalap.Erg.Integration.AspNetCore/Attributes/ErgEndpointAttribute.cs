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

        public ErgEndpointAttribute(string template, string alias, string description) : base(template)
        {
            Alias = alias;
            Description = description;
        }
    }
}
