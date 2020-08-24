using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zapalap.Erg.Cli.Verbs
{
    [Verb("discover", HelpText = "Discover possible Erg endpoints in a given url")]
    public class DiscoverOptions
    {
        [Value(0, MetaName = "Url", HelpText ="Url with discoverable Erg endpoints", Required = true)]
        public string Url { get; set; }
    }
}
