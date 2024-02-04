
namespace wsl_and_docker.Env
{
    public static class EnvEndpoints
    {
        public static string OSName() => Environment.OSVersion.ToString();

        internal static string? GetEnv(string envName) => Environment.GetEnvironmentVariable(envName);
    }
}
