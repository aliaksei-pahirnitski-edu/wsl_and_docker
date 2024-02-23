

using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace wsl_and_docker.Env
{
    public static class EnvEndpoints
    {
        public static string CurrentTime()
        {
            var localNow = DateTime.Now.ToString();
            Console.WriteLine($"Current _HotReload_ Time=[{localNow}]");
            return $"Current Time=[{localNow}]";
        }

        public static string OSName()
        {
            var osName = Environment.OSVersion.ToString();
            Console.WriteLine($"OSName=[{osName}]:");
            return osName;
        }

        // intentianoly produce exception
        internal static string Exception(HttpContext context)
        {
            string x = null!;
            return $"Will be exception here: {x.Length}";
        }

        internal static string? GetEnv(string envName)
        {
            Console.WriteLine($"Env for [{envName}]:");
            var result = Environment.GetEnvironmentVariable(envName);
            Console.WriteLine($"Env [{envName}] = [{result}]");

            var all = Environment.GetEnvironmentVariables();
            writeEnvList(all);

            var machine = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine);
            writeEnvList(machine);

            var process = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process);
            writeEnvList(process);

            var user = Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User);
            writeEnvList(user);

            return result is null ? "Not exists" : $"Env [{envName}] = [{result}]";

            static void writeEnvList(IDictionary envList, [CallerArgumentExpression(nameof(envList))] string listType = "")
            {
                Console.WriteLine($"List env for: {listType}");
                foreach (var key in envList.Keys.Cast<string>().OrderBy(key => key))
                {
                    Console.WriteLine($"[{key}] = [{envList[key]}]");
                }
                Console.WriteLine($"( total count: {envList.Count} \n");
            }
        }

        internal static string GetArgs()
        {
            var args = Environment.GetCommandLineArgs();
            Console.WriteLine($"Get Args: {args?.Length ?? -1}");
            foreach(var arg in args ?? Array.Empty<string>())
            {
                Console.WriteLine($"[{arg}]");
            }
            Console.WriteLine();

            var argsToText = string.Join(' ', args ?? Array.Empty<string>());
            return "Args: " + argsToText;
        }
    }
}
