const string image = "dotnetsdk";
var tags = new[] { "latest", "9.0" };
const string projectBodyToTest = """
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFrameworks>net9.0;net8.0;net7.0;net6.0</TargetFrameworks>
    </PropertyGroup>
</Project>
""";

Tools.SetupEnvironment();

new DockerCustom("build", "-t", image, "docker")
    .Run().EnsureSuccess();

var tempDir = Directory.CreateTempSubdirectory("dotnetsdk_");
try
{
    File.WriteAllText(
        Path.Combine(tempDir.FullName, "MyLib.csproj"),
        projectBodyToTest);

    var dotnetNew = new DotNetNew()
        .WithName("MyLib")
        .WithTemplateName("classlib");
    
    new DockerRun()
        .WithAutoRemove(true)
        .WithImage(image)
        .WithVolumes((tempDir.FullName, "/MyLib"))
        .WithContainerWorkingDirectory("/MyLib")
        .WithCommandLine(dotnetNew)
        .Build().EnsureSuccess();
}
finally
{
    try
    {
        tempDir.Delete(true);
    }
    catch
    {
        // ignored
    }
}

new DockerCustom("login")
    .Run().EnsureSuccess();

foreach (var tag in tags)
{
    var repoImage = $"nikolayp/{image}:{tag}";

    new DockerCustom("tag", image, repoImage)
        .Run().EnsureSuccess();
    new DockerCustom("image", "push", repoImage)
        .Run().EnsureSuccess();
}