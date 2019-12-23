using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Bullseye;
using McMaster.Extensions.CommandLineUtils;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace build
{
    class Build
    {
        static int Main(string[] args)
        {
            var app = new CommandLineApplication();
            app.HelpOption();
            var configurationOption = app.Option<string>("-c|--configuration", "The configuration to build", CommandOptionType.SingleValue);
            app.AddBullseyeOptions(o => o.LongName != "--clear");

            app.OnExecute(() =>
            {
                RunTargets(
                    app.GetBullseyeTargets(),
                    app.GetBullseyeOptions(),
                    configurationOption.Value() ?? "Release");
            });

            return app.Execute(args);
        }

        private static void RunTargets(IEnumerable<string> targets, Options options, string configuration)
        {
            Directory.SetCurrentDirectory(GetSolutionDirectory());

            string artifactsDir = Path.GetFullPath("artifacts");
            string logsDir = Path.Combine(artifactsDir, "logs");
            string buildLogFile = Path.Combine(logsDir, "build.binlog");
            string packagesDir = Path.Combine(artifactsDir, "packages");

            string solutionFile = "Linq.Extras.sln";
            string libraryProject = "src/Linq.Extras/Linq.Extras.csproj";
            string testProject = "tests/Linq.Extras.Tests/Linq.Extras.Tests.csproj";
            string docProject = "docs/Documentation.shfbproj";

            Target(
                "artifactDirectories",
                () =>
                {
                    Directory.CreateDirectory(artifactsDir);
                    Directory.CreateDirectory(logsDir);
                    Directory.CreateDirectory(packagesDir);
                });

            Target(
                "build",
                DependsOn("artifactDirectories"),
                () => Run(
                    "dotnet",
                    $"build -c \"{configuration}\" /bl:\"{buildLogFile}\" \"{solutionFile}\""));

            Target(
                "test",
                DependsOn("build"),
                () => Run(
                    "dotnet",
                    $"test -c \"{configuration}\" --no-build \"{testProject}\""));

            Target(
                "pack",
                DependsOn("artifactDirectories", "build"),
                () => Run(
                    "dotnet",
                    $"pack -c \"{configuration}\" --no-build -o \"{packagesDir}\" \"{libraryProject}\""));

            Target(
                "doc",
                DependsOn("build"),
                () =>
                {
                    var msbuild = GetMsBuildLocation();
                    Console.WriteLine(msbuild);
                    Run(msbuild, docProject);
                });

            Target("default", DependsOn("test", "pack"));

            RunTargetsAndExit(targets, options);
        }

        private static string GetSolutionDirectory() =>
            Path.GetFullPath(Path.Combine(GetScriptDirectory(), @"../.."));

        private static string GetScriptDirectory([CallerFilePath] string filename = null) => Path.GetDirectoryName(filename);

        private static string GetVsLocation() => Read(ToolPaths.VsWhere, "-property installationPath").Trim();

        private static string GetMsBuildLocation() => Path.Combine(GetVsLocation(), @"MSBuild/Current/Bin/MSBuild.exe");
    }
}