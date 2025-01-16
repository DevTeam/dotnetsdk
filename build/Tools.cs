namespace build;

internal static class Tools
{
    private const string SolutionFile = "dotnetsdk.sln";
    
    public static void SetupEnvironment()
    {
        if (Path.GetDirectoryName(Environment.CurrentDirectory.TryFindFile(SolutionFile)) is { } solutionDir)
        {
            Environment.CurrentDirectory = solutionDir;
            return;
        }
        
        Error($"Solution file {SolutionFile} could not be found.");
    }
    
    private static string? TryFindFile(this string? path, string searchPattern)
    {
        string? target = default;
        while (path != default && target == default)
        {
            target = Directory.EnumerateFileSystemEntries(path, searchPattern).FirstOrDefault();
            path = Path.GetDirectoryName(path);
        }

        return target;
    }
}