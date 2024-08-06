using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyGazelle.CLI.Commands
{
    public class HelloWorldCommand : Command<HelloWorldCommand.Settings>
    {
        public class Settings : CommandSettings
        {
            [CommandOption("-y")]
            [Description("Yes option")]
            public bool Yes { get; set; }

            [CommandOption("-n")]
            [Description("No option")]
            public bool No { get; set; }
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            if (settings.Yes)
            {
                Console.WriteLine("Hello A");
            }
            else if (settings.No)
            {
                Console.WriteLine("Hello B");
            }
            else
            {
                Console.WriteLine("Please provide either -y or -n option.");
            }

            return 0;
        }

    }
}
