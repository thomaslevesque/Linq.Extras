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

Task("Push")
    .WithCriteria(configuration == "Release")
    .IsDependentOn("Test")
    .Does(() =>
{
    if (string.IsNullOrEmpty(version))
        throw new ArgumentNullException("version");
    //NuGetPush
    Information($"{projectName}/bin/{configuration}/{projectName}.{version}.nupkg");
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
