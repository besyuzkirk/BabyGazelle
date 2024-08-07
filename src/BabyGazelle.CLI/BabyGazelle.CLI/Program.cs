using BabyGazelle.CLI.Commands;
using Spectre.Console.Cli;

namespace BabyGazelle.CLI
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddCommand<CreateCommand>("create");
            });

            return app.Run(args);
        }
    }
}