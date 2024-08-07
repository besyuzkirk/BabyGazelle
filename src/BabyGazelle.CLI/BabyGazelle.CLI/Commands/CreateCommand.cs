using BabyGazelle.CLI.Commands.Settings;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyGazelle.CLI.Commands
{
    public class CreateCommand : AsyncCommand<CreateCommandSettings>
    {
        public override Task<int> ExecuteAsync(CommandContext context, CreateCommandSettings settings)
        {
            var path = settings.ProjectPath;

            var filePath = System.IO.Path.Combine(path, "index.html");
            File.WriteAllText(filePath, "<html><body>Hello, World!</body></html>");
            Console.WriteLine($"HTML file created at: {filePath}");
            return Task.FromResult( 0 );
        }
    }
}
