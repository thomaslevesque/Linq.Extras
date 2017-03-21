///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

var projectName = "Linq.Extras";

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./{projectName}/bin/{configuration}");
});

Task("Restore")
    .Does(() => DotNetCoreRestore());

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() =>
{
    DotNetCoreBuild(".", new DotNetCoreBuildSettings { Configuration = configuration });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest(
        $"{projectName}.Tests/{projectName}.Tests.csproj",
        new DotNetCoreTestSettings { Configuration = configuration });
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetCorePack(projectName, new DotNetCorePackSettings { Configuration = configuration });
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
