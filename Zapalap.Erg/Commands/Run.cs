using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zapalap.Erg.Cli.Config;
using Zapalap.Erg.Cli.Verbs;
using Zapalap.Erg.Core.Models;

namespace Zapalap.Erg.Cli.Commands
{
    public class Run
    {
        private readonly ConfigReader ConfigReader;
        private readonly HttpClient HttpClient;

        public Run()
        {
            ConfigReader = new ConfigReader("endpoints.json");
            HttpClient = new HttpClient();
        }

        public async Task<int> Execute(RunOptions options)
        {
            var endpoints = ConfigReader.GetEndpoints();

            var endpoint = endpoints.FirstOrDefault(e => e.Alias == options.CommandAlias);

            if (endpoint is null)
            {
                Console.WriteLine($"Could not find endpoint {options.CommandAlias}. Please run 'erg discover <url> first to find out runnable endpoints'");
                return 1;
            }

            return await RunEndpoint(endpoint);
        }

        private async Task<int> RunEndpoint(DiscoverableEndpoint endpoint)
        {
            Console.WriteLine($"{endpoint.Alias}: running GET {endpoint.Url}");

            var response = await HttpClient.GetAsync(endpoint.Url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{endpoint.Alias}: an error occurred");
                Console.WriteLine($"{endpoint.Alias}: {response.StatusCode} {response.ReasonPhrase}");
                return 1;
            }

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"{endpoint.Alias}: {response.StatusCode} {response.ReasonPhrase}");
            Console.WriteLine($"{endpoint.Alias}: {content}");

            return 0;
        }
    }
}
