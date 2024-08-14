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
            CreateAspNetCoreProject(settings.ProjectPath);

            // 2. Copy the model to the project
            CopyModelToProject(settings.ModelPath, settings.ProjectPath);

            // 3. Determine the entity name from the model file
            string entityName = Path.GetFileNameWithoutExtension(settings.ModelPath); // Assuming the entity name is the same as the file name

            // 4. Create the necessary folders based on the entity name
            CreateEntityFolders(settings.ProjectPath, entityName);

            // 5. Create the empty Handler and Endpoint classes
            CreateHandlerAndEndpointClasses(settings.ProjectPath, entityName);

            return 0;

        }

        public void CopyModelToProject(string modelPath, string projectPath)
        {
            string modelsFolderPath = Path.Combine(projectPath, "Models");

            // Ensure the Models directory exists
            if (!Directory.Exists(modelsFolderPath))
            {
                Directory.CreateDirectory(modelsFolderPath);
            }

            // Copy the model file to the Models directory
            string modelFileName = Path.GetFileName(modelPath);
            string destinationPath = Path.Combine(modelsFolderPath, modelFileName);
            File.Copy(modelPath, destinationPath, overwrite: true);
        }

        public void CreateAspNetCoreProject(string projectPath)
        {
            // Ensure the directory exists
            if (!Directory.Exists(projectPath))
            {
                Directory.CreateDirectory(projectPath);
            }

            // Run the `dotnet new webapi` command to create a new ASP.NET Core Web API project
            var processInfo = new ProcessStartInfo("dotnet", $"new webapi --output \"{projectPath}\"")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(processInfo))
            {
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"Failed to create ASP.NET Core project: {error}");
                }
            }
        }

        public void CreateEntityFolders(string projectPath, string entityName)
        {
            string baseFolderPath = Path.Combine(projectPath, entityName);

            string[] folderNames = {
        $"Create{entityName}",
        $"Get{entityName}ById",
        $"Get{entityName}s",
        $"Delete{entityName}",
        $"Update{entityName}"
    };

            foreach (var folderName in folderNames)
            {
                string folderPath = Path.Combine(baseFolderPath, folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
        }

        public void CreateHandlerAndEndpointClasses(string projectPath, string entityName)
        {
            string baseFolderPath = Path.Combine(projectPath, entityName);

            string[] folderNames = {
        $"Create{entityName}",
        $"Get{entityName}ById",
        $"Get{entityName}s",
        $"Delete{entityName}",
        $"Update{entityName}"
    };

            foreach (var folderName in folderNames)
            {
                string handlerClassPath = Path.Combine(baseFolderPath, folderName, $"{folderName}Handler.cs");
                string endpointClassPath = Path.Combine(baseFolderPath, folderName, $"{folderName}Endpoint.cs");

                // Create empty Handler class
                File.WriteAllText(handlerClassPath, $"namespace {entityName}.{folderName}\n{{\n    public class {folderName}Handler {{ }}\n}}");

                // Create empty Endpoint class
                File.WriteAllText(endpointClassPath, $"namespace {entityName}.{folderName}\n{{\n    public class {folderName}Endpoint {{ }}\n}}");
            }
        }


    }


}
