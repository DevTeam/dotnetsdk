const string image = "dotnetsdk:latest";
const string repoImage = "nikolayp/dotnetsdk:latest";

new DockerCustom("build", "-t", image, "docker").TryRun();
new DockerCustom("login").TryRun();
new DockerCustom("tag", image, repoImage).TryRun();
new DockerCustom("image", "push", repoImage).TryRun();