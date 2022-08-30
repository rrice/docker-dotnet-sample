#addin nuget:?package=Cake.Docker&version=1.1.2
using System.Reflection;

var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solution = "./MySimpleService.sln";

Task("Clean")
    .Does(() => {
        CleanDirectories("./src/**/bin/");
        Information("Cleaning directory ./src/**/bin");

        CleanDirectories("./src/**/obj/");
        Information("Cleaning directory ./src/**/obj");

        CleanDirectories("./tests/**/bin/");
        Information("Cleaning directory ./tests/**/bin");

        CleanDirectories("./tests/**/obj/");
        Information("Cleaning directory ./tests/**/obj");

    });

Task("Build")
    .IsDependentOn("Clean")
    .Does(() => {
        NuGetRestore(".");
        Information("Building {0}", solution);
        DotNetCoreBuild(solution, new DotNetCoreBuildSettings {
            Configuration = configuration,
        });
    });



Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        Information("Building {0}", solution);
        DotNetCoreTest(solution, new DotNetCoreTestSettings {
            Configuration = configuration,
            NoBuild = true,
        });
    });

Task("Docker-Build")
    .IsDependentOn("Test")
    .Does(() => {
        Information("Building docker image");
        DockerBuild( "./src/MySimpleService");
    });

RunTarget(target);

