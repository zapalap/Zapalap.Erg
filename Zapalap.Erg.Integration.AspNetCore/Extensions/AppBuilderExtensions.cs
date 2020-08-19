using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Zapalap.Erg.Integration.AspNetCore.Middleware;

namespace Zapalap.Erg.Integration.AspNetCore.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void UseErgEndpoints(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErgDiscoveryMiddleware>();
        }
    }
}
