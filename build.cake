///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var version = Argument<string>("version", null);
var projectName = "Linq.Extras";
var solutionFile = $"./{projectName}.sln";


///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(() =>
{
    // Executed BEFORE the first task.
    Information("Running tasks...");
});

Teardown(() =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./{projectName}/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    MSBuild(solutionFile,
        settings => settings.SetConfiguration(configuration));
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    NUnit(new[] { $"{projectName}.Tests/bin/{configuration}/{projectName}.Tests.dll" });
});

Task("Pack")
    .IsDependentOn("Test")
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

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Test");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
