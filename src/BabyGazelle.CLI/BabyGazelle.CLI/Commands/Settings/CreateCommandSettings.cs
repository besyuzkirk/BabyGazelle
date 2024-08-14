using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BabyGazelle.CLI.Commands.Settings
{
    public class CreateCommandSettings : CommandSettings
    {
        [CommandArgument(0, "[modelPath]")]
        [Description("Model path")]
        public string ModelPath { get; init; } = string.Empty;

        [CommandArgument(0, "[projectPath]")]
        [Description("Project path")]
        public string ProjectPath { get; init; } = string.Empty;

    }
}
