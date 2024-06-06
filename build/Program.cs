const string image = "dotnetsdk:latest";
const string repoImage = "nikolayp/dotnetsdk:latest";

Tools.SetupEnvironment();
new DockerCustom("build", "-t", image, ".").Run("building");
new DockerCustom("login").Run("login");
new DockerCustom("tag", image, repoImage).Run("tagging");
new DockerCustom("image", "push", repoImage).Run("pushing");