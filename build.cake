#addin nuget:?package=Cake.Docker&version=1.1.2
using System.Reflection;

var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");
var solutions = GetFiles("./src/**/**/*.sln");

Task("Clean")
    .Does(() => {

        foreach (var solution in solutions)
        {
            Information($"Cleaning {solution}...");
            DotNetClean(solution.FullPath, new DotNetCleanSettings() {
                Configuration = configuration
            });

        }
    });

Task("Restore")
    .Does(() => {
        foreach(var solution in solutions)
        {
            Information($"Restoring {solution}...");
            DotNetRestore(solution.FullPath, new DotNetRestoreSettings());
        }
    });


Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {

        foreach (var solution in solutions) {
            Information($"Building {solution}...");
            DotNetBuild(solution.FullPath, new DotNetBuildSettings {
                Configuration = configuration
            });
        }
    });



Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        foreach (var solution in solutions) {
            Information($"Testing {solution}...");
            DotNetTest(solution.FullPath, new DotNetTestSettings {
                Configuration = configuration,
                NoBuild = true,
            });
        }
    });

Task("Docker-Build")
    .IsDependentOn("Test")
    .Does(() => {
        DockerBuild(new DockerImageBuildSettings() {
            Tag = new string[] {
                "rrice/todo-service:1.0",
                "rrice/todo-service:latest"
            }
        }, "./src/services/TodoService/src/TodoService");
    });

RunTarget(target);

