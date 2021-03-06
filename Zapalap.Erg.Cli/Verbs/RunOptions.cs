﻿using CommandLine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Zapalap.Erg.Cli.Verbs
{

    [Verb("run", HelpText = "Invoke Erg endpoint")]
    public class RunOptions
    {
        [Value(0, MetaName = "CommandAlias", HelpText = "Alias of a previously discovered Erg endpoint", Required = true)]
        public string CommandAlias { get; set; }
    }
}
