using Microsoft.AspNetCore.Mvc;

namespace wsl_and_docker.Files
{
    public record FileOrDirExistance (bool Exists, bool IsDirectory, string FullPath, bool IsValid);
    public record CreatedDirectoryResult (bool IsCreated, string FullPath, bool WasExisting, string? Error)
    {
        public CreatedDirectoryResult(bool IsCreated, string FullPath, bool WasExisting) 
            : this(IsCreated, FullPath, WasExisting, null) { }
    }

    public record AppendLogRequest(string File, string Message);
    public record AppendLogResult(bool Success, string FullPath, string? Error)
    {
        public static AppendLogResult Ok(string fullPath) => new AppendLogResult(true, fullPath, null);
        public static AppendLogResult Fail(string path, string error) => new AppendLogResult(false, path, error);
    };

    internal static class FilesEndpoint
    {
        public static FileOrDirExistance Exists(string path)
        {
            Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] {nameof(Exists)} [{path}]");
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

        public static CreatedDirectoryResult CreateDir(string path)
        {
            Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] {nameof(CreateDir)} [{path}]");
            bool existed = Directory.Exists(path);
            if (!existed)
            {
                try
                {
                    var info = Directory.CreateDirectory(path);
                    return new CreatedDirectoryResult(true, info.FullName, existed);
                }
                catch(Exception e)
                {
                    string error = $"{e.GetType().Name}: [{e.Message}]";
                    return new CreatedDirectoryResult(false, path, existed, error);
                }
            }

            return new CreatedDirectoryResult(false, Path.GetFullPath(path), existed);
        }

        public static async Task<AppendLogResult> AppendLog(/*[FromBody]*/AppendLogRequest request)
        {
            Console.WriteLine($"[{DateTime.Now:hh:mm:ss}] {nameof(AppendLog)} [{request}]");
            try
            {
                var fullPath = Path.GetFullPath(request.File);
                var dirInfo = new FileInfo(fullPath).Directory;
                if (dirInfo is null) return AppendLogResult.Fail(request.File, "Directory not exists");
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
                using var writer = new StreamWriter(fullPath, append: true);
                await writer.WriteLineAsync($"[{DateTime.Now: hh:mm:ss}]: {request.Message}");
                return AppendLogResult.Ok(fullPath);
            }
            catch (Exception exc)
            {
                return AppendLogResult.Fail(request.File, $"{exc.GetType().Name}: [{exc.Message}]");
            }
        }
    }
}
