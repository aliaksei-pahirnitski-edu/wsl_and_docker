using System.Linq;

namespace wsl_and_docker.Files
{
    public record FileOrDirExistance (bool Exists, bool IsDirectory, string FullPath, bool IsValid);

    internal static class FilesEndpoint
    {
        public static FileOrDirExistance Exists(string path)
        {
            if (File.Exists(path))
            {
                return new FileOrDirExistance(true, false, new FileInfo(path).FullName, true);
            }

            // file not exists
            if (Directory.Exists(path))
            {
                return new FileOrDirExistance(true, true, new DirectoryInfo(path).FullName, true);
            }

            // neiter file nor dir not exists
            bool isDirectory = Path.EndsInDirectorySeparator(path);
            char[] invalidChars = Path.GetInvalidFileNameChars();
            bool isValid = !invalidChars.Intersect(path).Any();
            return new FileOrDirExistance(false, isDirectory && isValid, Path.GetFullPath(path), isValid);
        }
    }
}
