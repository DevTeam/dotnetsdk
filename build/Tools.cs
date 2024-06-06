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

    public static void Run(this ICommandLine command, string shortName)
    {
        if (command.Run(i => WriteLine($"{command}> {i.Line}", Color.Trace)) == 0)
        {
            return;
        }
    
        Error($"Error when {command}.");
        Environment.Exit(1);
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