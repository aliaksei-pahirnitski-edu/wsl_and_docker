﻿
namespace wsl_and_docker.Env
{
    public static class EnvEndpoints
    {
        public static string OSName()
        {
            var osName = Environment.OSVersion.ToString();
            Console.WriteLine($"OSName=[{osName}]:");
            return osName;
        }

        internal static string? GetEnv(string envName)
        {
            Console.WriteLine($"Env for [{envName}]:");
            var result = Environment.GetEnvironmentVariable(envName);
            Console.WriteLine($"Env [{envName}] = [{result}]");
            return result is null ? "Not exists" : $"Env [{envName}] = [{result}]";
        }
    }
}