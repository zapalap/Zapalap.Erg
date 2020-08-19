using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zapalap.Erg.Core.Models;

namespace Zapalap.Erg.Cli.Config
{
    public class ConfigReader
    {
        private readonly string ConfigFileName;

        public ConfigReader(string configFileName)
        {
            ConfigFileName = configFileName;
        }

        public IEnumerable<DiscoverableEndpoint> GetEndpoints()
        {
            var data = File.ReadAllText(ConfigFileName);
            var endpoints = JsonConvert.DeserializeObject<IEnumerable<DiscoverableEndpoint>>(data);

            return endpoints;
        }
    }
}
