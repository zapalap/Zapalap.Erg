using CommandLine;
using System;
using System.Threading.Tasks;
using Zapalap.Erg.Cli.Commands;
using Zapalap.Erg.Cli.Verbs;

namespace Zapalap.Erg.Cli
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<DiscoverOptions, RunOptions>(args).MapResult(
                async (DiscoverOptions options) => await new Discover().Execute(options),
                async (RunOptions options) => await new Run().Execute(options),
                errors => Task.FromResult(1)
                );
        }
    }
}
