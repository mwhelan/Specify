#tool "nuget:?package=GitReleaseNotes"
#tool "nuget:?package=GitVersion.CommandLine"

var target = Argument("target", "Default");
var specifyProj = "./src/app/Specify/project.json";
var outputDir = "./artifacts/";

Task("Clean")
    .Does(() => {
        if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, recursive:true);
        }
    });

Task("Restore")
    .Does(() => {
        DotNetCoreRestore("src");
    });

GitVersion versionInfo = null;
Task("Version")
    .Does(() => {
        GitVersion(new GitVersionSettings{
            UpdateAssemblyInfo = true,
            OutputType = GitVersionOutput.BuildServer
        });
        versionInfo = GitVersion(new GitVersionSettings{ OutputType = GitVersionOutput.Json });
        // Update project.json
        var updatedProjectJson = System.IO.File.ReadAllText(specifyProj)
            .Replace("1.0.0-*", versionInfo.NuGetVersion);

        System.IO.File.WriteAllText(specifyProj, updatedProjectJson);
    });

Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Version")
    .IsDependentOn("Restore")
    .Does(() => {
        MSBuild("./src/Specify.sln");
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        DotNetCoreTest("./src/tests/Specify.Tests");
        DotNetCoreTest("./src/Samples/Examples/Specify.Examples.UnitSpecs");
    });

Task("Package")
    .IsDependentOn("Test")
    .Does(() => {
        var settings = new DotNetCorePackSettings
        {
            OutputDirectory = outputDir,
            NoBuild = true
        };

        DotNetCorePack(specifyProj, settings);

        var releaseNotesExitCode = StartProcess(
            @"tools\GitReleaseNotes\tools\gitreleasenotes.exe", 
            new ProcessSettings { Arguments = ". /o artifacts/releasenotes.md" });
        if (string.IsNullOrEmpty(System.IO.File.ReadAllText("./artifacts/releasenotes.md")))
            System.IO.File.WriteAllText("./artifacts/releasenotes.md", "No issues closed since last release");

        if (releaseNotesExitCode != 0) throw new Exception("Failed to generate release notes");

        System.IO.File.WriteAllLines(outputDir + "artifacts", new[]{
            "nuget:Specify." + versionInfo.NuGetVersion + ".nupkg",
            "nugetSymbols:Specify." + versionInfo.NuGetVersion + ".symbols.nupkg",
            "releaseNotes:releasenotes.md"
        });

        if (AppVeyor.IsRunningOnAppVeyor)
        {
            foreach (var file in GetFiles(outputDir + "**/*"))
                AppVeyor.UploadArtifact(file.FullPath);
        }
    });

Task("Default")
    .IsDependentOn("Package");

RunTarget(target);