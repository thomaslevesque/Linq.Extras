using System.Xml.Linq;

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

var projectName = "Linq.Extras";
var libraryProject = $"src/{projectName}/{projectName}.csproj";
var testProject = $"tests/{projectName}.Tests/{projectName}.Tests.csproj";
var outDir = $"src/{projectName}/bin/{configuration}";

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(outDir);
    });

Task("Restore").Does(DotNetCoreRestore);

Task("JustBuild")
    .Does(() =>
    {
        DotNetCoreBuild(".", new DotNetCoreBuildSettings { Configuration = configuration });
    });

Task("JustTest")
    .Does(() =>
    {
        DotNetCoreTest(testProject, new DotNetCoreTestSettings { Configuration = configuration });
    });
    
Task("JustPack")
    .Does(() =>
    {
        DotNetCorePack(libraryProject, new DotNetCorePackSettings { Configuration = configuration });
    });

Task("JustPush")
    .Does(() =>
    {
        var doc = XDocument.Load(libraryProject);
        string version = doc.Root.Elements("PropertyGroup").Elements("Version").First().Value;
        string package = $"src/{projectName}/bin/{configuration}/{projectName}.{version}.nupkg";
        NuGetPush(package, new NuGetPushSettings());
    });

Task("Doc")
    .Does(() =>
    {
        CleanDirectory("Documentation/Help");
        MSBuild("Documentation/Documentation.shfbproj");
    });

// High level tasks

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("JustBuild");

Task("Test")
    .IsDependentOn("Build")
    .IsDependentOn("JustTest");

Task("Pack")
    .IsDependentOn("Build")
    .IsDependentOn("JustPack");

Task("Push")
    .IsDependentOn("Pack")
    .IsDependentOn("JustPush");

Task("Default")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

RunTarget(target);
