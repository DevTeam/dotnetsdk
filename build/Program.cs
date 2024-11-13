const string image = "dotnetsdk";
var tags = new[] { "latest", "9.0" };

new DockerCustom("build", "-t", image, "docker").TryRun();
new DockerCustom("login").TryRun();
foreach (var tag in tags)
{
    var repoImage = $"nikolayp/{image}:{tag}";
    new DockerCustom("tag", image, repoImage).TryRun();
    new DockerCustom("image", "push", repoImage).TryRun();   
}