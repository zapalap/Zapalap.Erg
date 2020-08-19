using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zapalap.Erg.Cli.Config;
using Zapalap.Erg.Cli.Utils;
using Zapalap.Erg.Cli.Verbs;
using Zapalap.Erg.Core.Models;

namespace Zapalap.Erg.Cli.Commands
{
    public class Discover
    {
        private readonly ConfigWriter ConfigWriter;
        private readonly HttpClient HttpClient;
        private readonly WelcomeHelper WelcomeHelper;

        public Discover()
        {
            ConfigWriter = new ConfigWriter("endpoints.json");
            HttpClient = new HttpClient();
            WelcomeHelper = new WelcomeHelper();
        }

        public async Task<int> Execute(DiscoverOptions options)
        {
            WelcomeHelper.PrintWelcomeMessage();

            var baseUrl = new Uri(options.Url);
            var targetUrl = new Uri(baseUrl, "erg/discover");
            var response = await HttpClient.GetAsync(targetUrl);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[erg-discover] could not discover runnable endpoints @ {targetUrl}");
                Console.WriteLine($"[erg-discover] {response.StatusCode}");
                return -1;
            }

            var content = await response.Content.ReadAsStringAsync();
            var discoverableEndpoints = JsonConvert.DeserializeObject<List<DiscoverableEndpoint>>(content);

            Console.WriteLine($"[erg-discover] discovered following endpoints @ {targetUrl}");
            Console.WriteLine("");

            ConsoleTable.From(discoverableEndpoints).Write(Format.Minimal);
            Console.WriteLine();

            try
            {
                ConfigWriter.WriteEndpointMetadata(discoverableEndpoints);
                Console.WriteLine($"[erg-discover] Successfully saved endpoint data");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[erg-discover] Error while saving endpoint data: {ex.Message}");
                return 1;
            }

            return 0;
        }
    }
}
