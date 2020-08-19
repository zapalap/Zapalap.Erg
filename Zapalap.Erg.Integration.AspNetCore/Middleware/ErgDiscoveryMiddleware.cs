using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zapalap.Erg.Core.Models;

namespace Zapalap.Erg.Integration.AspNetCore.Middleware
{
    public class ErgDiscoveryMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly IActionDescriptorCollectionProvider ActionDescriptorCollectionProvider;

        private readonly List<DiscoverableEndpoint> DiscoverableEndpoints = new List<DiscoverableEndpoint>();

        public ErgDiscoveryMiddleware(RequestDelegate next, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            Next = next;
            ActionDescriptorCollectionProvider = actionDescriptorCollectionProvider;

            DiscoverableEndpoints = GetDiscoverableEndpoints();
        }

        private List<DiscoverableEndpoint> GetDiscoverableEndpoints()
        {
            var assembly = Assembly.GetEntryAssembly();

            var ergEndpointMembers = assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
                    .SelectMany(t => t.GetMembers()
                                  .Where(m => Attribute.IsDefined(m, typeof(ErgEndpointAttribute))));

            var discoverableEndpoints = new List<DiscoverableEndpoint>();

            foreach (var ergEndpointMember in ergEndpointMembers)
            {
                var attributeData = ergEndpointMember.GetCustomAttribute<ErgEndpointAttribute>();
                var endpoint = new DiscoverableEndpoint
                {
                    Alias = attributeData.Alias,
                    Description = attributeData.Description,
                    Url = GetUrlForActionName(ergEndpointMember.Name),
                    Method = GetMethodForActionName(ergEndpointMember.Name)
                };

                discoverableEndpoints.Add(endpoint);
            }

            return discoverableEndpoints;
        }

        private ActionDescriptor GetActionDescriptorForAction(string actionName)
        {
            var actionDescriptors = ActionDescriptorCollectionProvider.ActionDescriptors.Items;
            var firstMatching = actionDescriptors.FirstOrDefault(a => a.RouteValues["Action"] == actionName);
            return firstMatching;
        }

        private string GetUrlForActionName(string actionName)
        {
            return GetActionDescriptorForAction(actionName)?.AttributeRouteInfo.Template ?? "Error getting url";
        }

        private string GetMethodForActionName(string actionName)
        {
            return "GET";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var isACallToErgDiscoveryUrl = context.Request.Path.Value.EndsWith("/erg/discover");

            if (!isACallToErgDiscoveryUrl)
            {
                await Next(context);
                return;
            }

            foreach (var endpoint in DiscoverableEndpoints)
            {
                endpoint.Url = $"{context.Request.Scheme}://{context.Request.Host}/{endpoint.Url}";
            }

            context.Response.StatusCode = 200;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(DiscoverableEndpoints));
        }
    }
}
