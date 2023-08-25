var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var outputDirectory = Argument("output-directory", "./dist/Server/publish");

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
    DotNetPublish("./src/Server/NetAdmin.BizServer.Host/NetAdmin.BizServer.Host.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});

Task("Publish-SdkServer")
    .Does(context =>
{
    DotNetPublish("./src/Server/NetAdmin.SdkServer.Host/NetAdmin.SdkServer.Host.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});

Task("Publish-ManServer")
    .Does(context =>
{
    DotNetPublish("./src/Server/NetAdmin.ManServer.Host/NetAdmin.ManServer.Host.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});

Task("Publish-SdkService")
    .Does(context =>
{
    DotNetPublish("./src/Server/NetAdmin.SdkService/NetAdmin.SdkService.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});

Task("Publish-ManService")
    .Does(context =>
{
    DotNetPublish("./src/Server/NetAdmin.ManService/NetAdmin.ManService.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});

Task("Publish-CallbackService")
    .Does(context =>
{
    DotNetPublish("./src/Server/NetAdmin.CallbackService/NetAdmin.CallbackService.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});


Task("Publish-ScheduledService")
    .Does(context =>
{
    DotNetPublish("./src/Server/NetAdmin.ScheduledService/NetAdmin.ScheduledService.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});


Task("Publish-PushService")
    .Does(context =>
{
    DotNetPublish("./src/Server/NetAdmin.PushService/NetAdmin.PushService.csproj", new DotNetPublishSettings {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = new DirectoryPath(outputDirectory)
    });
});


// Task("Test")
//     .IsDependentOn("Build")
//     .Does(context =>
// {
//     DotNetTest("./test/Spectre.Console.Tests/Spectre.Console.Tests.csproj", new DotNetTestSettings {
//         Configuration = configuration,
//         NoRestore = true,
//         NoBuild = true,
//     });
//
//     DotNetTest("./test/Spectre.Console.Cli.Tests/Spectre.Console.Cli.Tests.csproj", new DotNetTestSettings {
//         Configuration = configuration,
//         NoRestore = true,
//         NoBuild = true,
//     });
//
//     DotNetTest("./test/Spectre.Console.Analyzer.Tests/Spectre.Console.Analyzer.Tests.csproj", new DotNetTestSettings {
//         Configuration = configuration,
//         NoRestore = true,
//         NoBuild = true,
//     });
// });


// Task("Publish-GitHub")
//     .WithCriteria(ctx => BuildSystem.IsRunningOnGitHubActions, "Not running on GitHub Actions")
//     //.IsDependentOn("Package")
//     .Does(context =>
// {
//     var apiKey = Argument<string>("github-key", null);
//     if(string.IsNullOrWhiteSpace(apiKey)) {
//         throw new CakeException("No GitHub API key was provided.");
//     }
//
//     // Publish to GitHub Packages
//     var exitCode = 0;
//     foreach(var file in context.GetFiles("./.artifacts/*.nupkg"))
//     {
//         context.Information("Publishing {0}...", file.GetFilename().FullPath);
//         exitCode += StartProcess("dotnet",
//             new ProcessSettings {
//                 Arguments = new ProcessArgumentBuilder()
//                     .Append("gpr")
//                     .Append("push")
//                     .AppendQuoted(file.FullPath)
//                     .AppendSwitchSecret("-k", " ", apiKey)
//             }
//         );
//     }
//
//     if(exitCode != 0)
//     {
//         throw new CakeException("Could not push GitHub packages.");
//     }
// });

// Task("Publish-NuGet")
//     //.WithCriteria(ctx => BuildSystem.IsRunningOnGitHubActions, "Not running on GitHub Actions")
//     //.IsDependentOn("Package")
//     .Does(context =>
// {
//     var apiKey = Argument<string>("nuget-key", null);
//     if(string.IsNullOrWhiteSpace(apiKey)) {
//         throw new CakeException("No NuGet API key was provided.");
//     }
//
//     // Publish to GitHub Packages
//     foreach(var file in context.GetFiles("./.artifacts/*.nupkg"))
//     {
//         context.Information("Publishing {0}...", file.GetFilename().FullPath);
//         DotNetNuGetPush(file.FullPath, new DotNetNuGetPushSettings
//         {
//             Source = "https://api.nuget.org/v3/index.json",
//             ApiKey = apiKey,
//             SkipDuplicate = true
//         });
//     }
// });

////////////////////////////////////////////////////////////////
// Targets

// Task("Publish")
//     .IsDependentOn("Publish-GitHub")
//     .IsDependentOn("Publish-NuGet");

Task("Default")
    .IsDependentOn("Build");

////////////////////////////////////////////////////////////////
// Execution

RunTarget(target)