using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.SignTool;
using Nuke.Common.Utilities.Collections;
using Nuke.Common;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.IO;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.SignTool;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.IO;
using Nuke.Common.IO;
using Nuke.Common;
using static Nuke.Common.ControlFlow;
using static Nuke.Common.Logger;
using static Nuke.Common.IO.CompressionTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;
using static Nuke.Common.Tools.SignTool.SignToolTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;
using static Nuke.Common.IO.TextTasks;
using static Nuke.Common.IO.XmlTasks;
using static Nuke.Common.EnvironmentInfo;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Default);
    [Parameter] readonly string Configuration = "Release";
    AbsolutePath SettingsFile => RootDirectory / "settings.json";
    object SkipBuild = Argument<string>("skipBuild", "false").ToLower() == "true" || Argument<string>("skipBuild", "false") == "1";
    object BuildSettings = SettingsUtils.LoadSettings(Context, settingsFile);
    object VersionInfo = VersionUtils.LoadVersion(Context, buildSettings);
    ///////////////////////////////////////////////////////////////////////////////
    // GLOBAL VARIABLES
    ///////////////////////////////////////////////////////////////////////////////
    AbsolutePath Solutions => buildSettings.Build.SolutionFilePath;
    object SolutionPaths = solutions.Select(solution => solution.GetDirectory());
    /*
    Setup((c) =>
    {
        c.Info("Command Line:");
        c.Info("\tConfiguration: {0}", Configuration);
        c.Info("\tSettings Files: {0}", SettingsFile);
        c.Info("\tSkip Build: {0}", SkipBuild);
        c.Info("\tSolutions Found: {0}", Solutions.Count);
        BuildSettings.Display(c);
        VersionInfo.Display(c);
    }); */
    /*
    Teardown((c) =>
    {
        Info("Finished running tasks.");
    }); */

    Target Clean => _ => _.Description("Cleans all directories that are used during the build process.")
        .OnlyWhenStatic(() => !SkipBuild)
        .Executes(() =>
    {
        // Clean solution directories.
        foreach (var path in SolutionPaths)
        {
            Info("Cleaning {0}", path);
            path + "/**/bin/" + Configuration.ForEach(EnsureCleanDirectory);
            path + "/**/obj/" + Configuration.ForEach(EnsureCleanDirectory);
        }
    });

    Target CleanAll => _ => _.Description("Cleans all directories that are used during the build process.")
        .Executes(() =>
    {
        // Clean solution directories.
        foreach (var path in SolutionPaths)
        {
            Info("Cleaning {0}", path);
            path + "/**/bin".ForEach(EnsureCleanDirectory);
            path + "/**/obj".ForEach(EnsureCleanDirectory);
            path + "/packages/**/*".ForEach(EnsureCleanDirectory);
            path + "/artifacts/**/*".ForEach(EnsureCleanDirectory);
            path + "/packages".ForEach(EnsureCleanDirectory);
            path + "/artifacts".ForEach(EnsureCleanDirectory);
        }
    });

    Target CleanPackages => _ => _.Description("Cleans all packages that are used during the build process.")
        .Executes(() =>
    {
        // Clean solution directories.
        foreach (var path in SolutionPaths)
        {
            Info("Cleaning {0}", path);
            path + "/packages/**/*".ForEach(EnsureCleanDirectory);
            path + "/packages".ForEach(EnsureCleanDirectory);
        }
    });

    Target Restore => _ => _.Description("Restores all the NuGet packages that are used by the specified solution.")
        .OnlyWhenStatic(() => !SkipBuild)
        .Executes(() =>
    {
        // Restore all NuGet packages.
        foreach (var solution in Solutions)
        {
            Info("Restoring {0}...", solution);
            NuGetRestore(_ => _
            .SetTargetPath(solution)); //, new NuGetRestoreSettings { ConfigFile = buildSettings.Build.NugetConfigPath });
        }
    });

    Target Build => _ => _.Description("Builds all the different parts of the project.")
        .OnlyWhenStatic(() => !SkipBuild)
        .DependsOn(Clean)
        .DependsOn(Restore)
        .DependsOn(UpdateVersion)
        .Executes(() =>
    {
        if (BuildSettings.Version.AutoIncrementVersion)
        {
            RunTarget("IncrementVersion");
        }

        // Build all projects.
        // iOS project cannot be built using .sln file on Mac OS X, using XBuild
        // reason: facade assemblies not referenced!
        foreach (var solution in Solutions)
        {
            Info("Building {0}", solution);
            var msBuildSettings = new MSBuildSettings{MaxCpuCount = 1, Configuration = Configuration, MSBuildTargetPlatform = MSBuildTargetPlatform.MSIL, ToolVersion = MSBuildToolsVersion.VS2017, }.WithProperty("TreatWarningsAsErrors", BuildSettings.Build.TreatWarningsAsErrors.ToString()).WithTarget("Build");
            MSBuild(_ => _
            .SetTargetPath(solution));
        }
    });

    Target Run_Unit_Tests => _ => _
        .DependsOn(Build)
        .Executes(() =>
    {
        NUnit3("./**/bin/" + Configuration + "/*.Tests.dll", new NUnit3Settings{NoResults = true});
    });

    Target Package => _ => _.Description("Packages all nuspec files into nupkg packages.")
        .DependsOn(Run-Unit-Tests)
        .Executes(() =>
    {
        var artifactsPath = BuildSettings.NuGet.ArtifactsPath;
        var nugetProps = new Dictionary<string, string>()
        {{"Configuration", Configuration}};
        EnsureExistingDirectory(artifactsPath);
        Info("Nuspec path: " + BuildSettings.NuGet.NuSpecFileSpec);
        var nuspecFiles = BuildSettings.NuGet.NuSpecFileSpec;
        foreach (var nsf in nuspecFiles)
        {
            Info("Packaging {0}", nsf);
            if (BuildSettings.NuGet.UpdateVersion)
            {
                VersionUtils.UpdateNuSpecVersion(Context, BuildSettings, VersionInfo, nsf.ToString());
            }

            VersionUtils.UpdateNuSpecVersionDependency(Context, BuildSettings, VersionInfo, nsf.ToString());
            NuGetPack(_ => _
            .SetTargetPath(nsf)
            .SetVersion(VersionInfo.ToString())
            .SetSymbols(true)
            .SetProperties(nugetProps)
            .SetOutputDirectory(artifactsPath)
            .SetProcessArgumentConfigurator(args => args.Add("-NoDefaultExcludes")));
        }
    });

    Target Publish => _ => _.Description("Publishes all of the nupkg packages to the nuget server. ")
        .DependsOn(Package)
        .Executes(() =>
    {
        var nupkgFiles = BuildSettings.NuGet.NuGetPackagesSpec;
        foreach (var pkg in nupkgFiles)
        {
            // Lets skip everything except the current version and we can skip the symbols pkg for now
            if (!pkg.ToString().Contains(VersionInfo.ToString()) || pkg.ToString().Contains("symbols"))
            {
                Info("Skipping {0}", pkg);
                continue;
            }

            Info("Publishing {0}", pkg);
            var nugetSettings = new NuGetPushSettings{Source = BuildSettings.NuGet.FeedUrl, ConfigFile = BuildSettings.NuGet.NuGetConfig, Verbosity = NuGetVerbosity.Detailed};
            if (!string.IsNullOrEmpty(BuildSettings.NuGet.FeedApiKey))
            {
                nugetSettings.ApiKey = BuildSettings.NuGet.FeedApiKey;
            }

            NuGetPush(pkg, nugetSettings);
        }
    });

    Target UnPublish => _ => _.Description("UnPublishes all of the current nupkg packages from the nuget server. Issue: versionToDelete must use : instead of . due to bug in cake")
        .Executes(() =>
    {
        var v = Argument<string>("versionToDelete", VersionInfo.ToString()).Replace(":", ".");
        var nuspecFiles = BuildSettings.NuGet.NuSpecFileSpec;
        foreach (var f in nuspecFiles)
        {
            Info("UnPublishing {0}", f.GetFilenameWithoutExtension());
            var args = string.Format("delete {0} {1} -Source {2} -NonInteractive", f.GetFilenameWithoutExtension(), v, BuildSettings.NuGet.FeedUrl);
            if (BuildSettings.NuGet.FeedApiKey != "VSTS")
            {
                args = args + string.Format(" -ApiKey {0}", BuildSettings.NuGet.FeedApiKey);
            }

            if (!string.IsNullOrEmpty(BuildSettings.NuGet.NuGetConfig))
            {
                args = args + string.Format(" -Config {0}", BuildSettings.NuGet.NuGetConfig);
            }

            Info("NuGet Command Line: {0}", args);
            using (var process = StartAndReturnProcess("tools\\nuget.exe", new ProcessSettings{Arguments = args}))
            {
                process.WaitForExit();
                Info("nuget delete exit code: {0}", process.GetExitCode());
            }
        }
    });

    Target UpdateVersion => _ => _.Description("Updates the version number in the necessary files")
        .Executes(() =>
    {
        Info("Updating Version to {0}", VersionInfo.ToString());
        VersionUtils.UpdateVersion(Context, BuildSettings, VersionInfo);
    });

    Target IncrementVersion => _ => _.Description("Increments the version number and then updates it in the necessary files")
        .DependsOn(UpdateVersion)
        .Executes(() =>
    {
        var oldVer = VersionInfo.ToString();
        if (VersionInfo.IsPreRelease)
            VersionInfo.PreRelease++;
        else
            VersionInfo.Build++;
        Info("Incrementing Version {0} to {1}", oldVer, VersionInfo.ToString());
        VersionUtils.UpdateVersion(Context, BuildSettings, VersionInfo);
    });

    Target BuildNewVersion => _ => _.Description("Increments and Builds a new version")
        .DependsOn(IncrementVersion)
        .DependsOn(Run-Unit-Tests)
        .Executes(() =>
    {
    });

    Target PublishNewVersion => _ => _.Description("Increments, Builds, and publishes a new version")
        .DependsOn(BuildNewVersion)
        .DependsOn(Publish)
        .Executes(() =>
    {
    });

    Target DisplaySettings => _ => _.Description("Displays All Settings.")
        .Executes(() =>
    {
    // Settings will be displayed as they are part of the Setup task
    });

    Target Default => _ => _.Description("This is the default task which will be ran if no specific target is passed in.")
        .DependsOn(Run-Unit-Tests);
}
