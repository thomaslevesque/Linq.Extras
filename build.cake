#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.1

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

var projectName = "Linq.Extras";
var solutionFile = $"./{projectName}.sln";

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./{projectName}/bin/{configuration}");
});

Task("Restore")
    .Does(() => NuGetRestore("."));

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    MSBuild(solutionFile,
        settings => settings.SetConfiguration(configuration));
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit3(new[] { $"{projectName}.Tests/bin/{configuration}/{projectName}.Tests.dll" });
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    string outDir = $"{projectName}/bin/{configuration}";
    string nupkgDir = $"{outDir}/nupkg";
    var targets = new[] { $"dotnet", $"portable-net45+win8+wpa81+wp8" };
    var files = new[] { $"{projectName}.dll", $"{projectName}.xml" };
    CreateDirectory(nupkgDir);
    foreach (var target in targets)
    {
        string targetDir = $"{nupkgDir}/lib/{target}";
        CreateDirectory(targetDir);
        foreach (var file in files)
        {
            CopyFileToDirectory($"{outDir}/{file}", targetDir);
        }
    }
    var packSettings = new NuGetPackSettings
    {
        BasePath = nupkgDir,
        OutputDirectory = outDir
    };
    NuGetPack($"{projectName}/{projectName}.nuspec", packSettings);
});

Task("Doc")
    .Does(() =>
{
    CleanDirectory("Documentation/Help");
    MSBuild("Documentation/Documentation.shfbproj");
});

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Test")
    .IsDependentOn("Pack");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
