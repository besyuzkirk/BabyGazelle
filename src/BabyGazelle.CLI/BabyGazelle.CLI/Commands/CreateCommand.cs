using BabyGazelle.CLI.Commands.Settings;
using Spectre.Console.Cli;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyGazelle.CLI.Commands
{
    public class CreateCommand : AsyncCommand<CreateCommandSettings>
    {
        public override async Task<int> ExecuteAsync(CommandContext context, CreateCommandSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ProjectPath))
            {
                Console.WriteLine("Path is required.");
                return -1;
            }

            var projectPath = settings.ProjectPath;

            try
            {
                Directory.CreateDirectory(projectPath);

                var processInfo = new ProcessStartInfo("dotnet", $"new webapi -o {projectPath}")
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = processInfo })
                {
                    process.Start();
                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode == 0)
                    {
                        Console.WriteLine($"Project created successfully at {projectPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {error}");
                        return process.ExitCode;
                    }
                }

                var modelsPath = Path.Combine(projectPath, "Models");
                Directory.CreateDirectory(modelsPath);
                Console.WriteLine($"'Models' folder created at {modelsPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return -1;
            }

            return 0;
        }
    }
}
