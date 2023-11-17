var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var outputDirectory = Argument("output-directory", "./dist/backend/publish");

////////////////////////////////////////////////////////////////
// Tasks

Task("Clean")
    .Does(context =>
{
    context.CleanDirectory("./dist");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(context =>
{
    DotNetBuild("./NetAdmin.sln", new DotNetBuildSettings {
        Configuration = configuration
    });
});

Task("Publish-BizServer")
    .Does(context =>
{
    DotNetPublish("./src/backend/NetAdmin.BizServer.Host/NetAdmin.BizServer.Host.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});

Task("Publish-ScheduledService")
    .Does(context =>
{
    DotNetPublish("./src/backend/NetAdmin.ScheduledService/NetAdmin.ScheduledService.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});

Task("Default")
    .IsDependentOn("Build");

////////////////////////////////////////////////////////////////
// Execution

RunTarget(target)