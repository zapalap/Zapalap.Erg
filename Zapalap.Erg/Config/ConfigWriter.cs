using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Zapalap.Erg.Core.Models;

namespace Zapalap.Erg.Cli.Config
{
    public class ConfigWriter
    {
        private readonly string ConfigFileName;

        public ConfigWriter(string configFileName)
        {
            ConfigFileName = configFileName;

        }

        public void WriteEndpointMetadata(IEnumerable<DiscoverableEndpoint> endpoints)
        {
            var json = JsonConvert.SerializeObject(endpoints, Formatting.Indented);
            File.WriteAllText(ConfigFileName, json);
        }
    }
}
