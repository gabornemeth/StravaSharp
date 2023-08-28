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
#addin nuget:?package = Cake.Json&version = 1.0.2.13
    public class SettingsUtils
    {
        public static Settings LoadSettings(ICakeContext context, string settingsFile)
        {
            context.Info("Loading Settings: {0}", settingsFile);
            if (!context.FileExists(settingsFile))
            {
                context.Error("Settings File Does Not Exist");
                return null;
            }

            var settings = context.DeserializeJsonFromFile<Settings>(settingsFile);
            var envFeedUrl = context.GetVariable<string>("NUGET_FEED_URL");
            if (!string.IsNullOrEmpty(envFeedUrl))
                settings.NuGet.FeedUrl = envFeedUrl;
            var envFeedApiKey = context.GetVariable<string>("NUGET_FEED_APIKEY");
            if (!string.IsNullOrEmpty(envFeedApiKey))
                settings.NuGet.FeedApiKey = envFeedApiKey;
            return settings;
        }
    }

    public class Settings
    {
        public VersionSettings Version { get; set; }

        public BuildSettings Build { get; set; }

        public NuGetSettings NuGet { get; set; }

        public void Display(ICakeContext context)
        {
            context.Info("Settings:");
            Version.Display(context);
            Build.Display(context);
            NuGet.Display(context);
        }
    }

    public class VersionSettings
    {
        public VersionSettings()
        {
            LoadFrom = "VersionFile";
        }

        public string VersionFile { get; set; }

        public string AssemblyInfoFile { get; set; }

        public bool LoadFromGit { get; set; }

        public string LoadFrom { get; set; }

        public bool AutoIncrementVersion { get; set; }

        public string NamespaceBase { get; set; }

        public void Display(ICakeContext context)
        {
            context.Info("Version Settings:");
            context.Info("\tVersion File: {0}", VersionFile);
            context.Info("\tAssemblyInfo File: {0}", AssemblyInfoFile);
            context.Info("\tLoad From: {0}", LoadFrom);
            context.Info("\tAutoIncrement Version: {0}", AutoIncrementVersion);
            context.Info("\tNamespace Base: {0}", NamespaceBase);
        }
    }

    public class BuildSettings
    {
        public BuildSettings()
        {
            SourcePath = RootDirectory / "src";
            SolutionFileSpec = "*.sln";
            TreatWarningsAsErrors = false;
            NugetConfigPath = RootDirectory / ".nuget" / "NuGet.Config";
        }

        public string SourcePath { get; set; }

        public string SolutionFileSpec { get; set; }

        public bool TreatWarningsAsErrors { get; set; }

        public string NugetConfigPath { get; set; }

        public string SolutionFilePath
        {
            get
            {
                if (SolutionFileSpec.Contains(RootDirectory))
                    return SolutionFileSpec;
                return string.Format("{0}{1}{2}", SourcePath, SolutionFileSpec.Contains("*") ? (RootDirectory).GlobDirectories("**") : "", SolutionFileSpec);
            }
        }

        public IEnumerable<AbsolutePath> GetProjectFiles(ICakeContext context)
        {
            foreach (var solutionFile in SolutionFilePath)
            {
                foreach (var csprojFile in solutionFile.GetDirectory() + "/**/*.csproj")
                {
                    yield return csprojFile;
                }
            }
        }

        public void Display(ICakeContext context)
        {
            context.Info("Build Settings:");
            context.Info("\tSource Path: {0}", SourcePath);
            context.Info("\tSolution File Spec: {0}", SolutionFileSpec);
            context.Info("\tSolution File Path: {0}", SolutionFilePath);
            context.Info("\tTreat Warnings As Errors: {0}", TreatWarningsAsErrors);
            context.Info("\tNuget Config Path: {0}", NugetConfigPath);
        }
    }

    public class NuGetSettings
    {
        public NuGetSettings()
        {
            NuSpecPath = RootDirectory / "nuspec";
            NuGetConfig = RootDirectory / ".nuget" / "NuGet.Config";
            ArtifactsPath = RootDirectory / "artifacts" / "packages";
            UpdateVersion = false;
            VersionDependencyForLibrary = VersionDependencyTypes.none;
        }

        public string NuGetConfig { get; set; }

        public string FeedUrl { get; set; }

        public string FeedApiKey { get; set; }

        public string NuSpecPath { get; set; }

        public string ArtifactsPath { get; set; }

        public bool UpdateVersion { get; set; }

        public VersionDependencyTypes VersionDependencyForLibrary { get; set; }

        public string NuSpecFileSpec
        {
            get
            {
                return string.Format((RootDirectory / "{0}").GlobDirectories("*.nuspec"), NuSpecPath);
            }
        }

        public string NuGetPackagesSpec
        {
            get
            {
                return string.Format((RootDirectory / "{0}").GlobDirectories("*.nupkg"), ArtifactsPath);
            }
        }

        public void Display(ICakeContext context)
        {
            context.Info("NuGet Settings:");
            context.Info("\tNuGet Config: {0}", NuGetConfig);
            context.Info("\tFeed Url: {0}", FeedUrl);
            //context.Information("\tFeed API Key: {0}", FeedApiKey);
            context.Info("\tNuSpec Path: {0}", NuSpecPath);
            context.Info("\tNuSpec File Spec: {0}", NuSpecFileSpec);
            context.Info("\tArtifacts Path: {0}", ArtifactsPath);
            context.Info("\tNuGet Packages Spec: {0}", NuGetPackagesSpec);
            context.Info("\tUpdate Version: {0}", UpdateVersion);
            context.Info("\tForce Version Match: {0}", VersionDependencyForLibrary);
        }
    }

    public class VersionDependencyTypes
    {
        public string Value { get; set; }

        public VersionDependencyTypes(string value)
        {
            Value = value;
        }

        public static implicit operator string (VersionDependencyTypes x)
        {
            return x.Value;
        }

        public static implicit operator VersionDependencyTypes(String text)
        {
            return new VersionDependencyTypes(text);
        }

        public static VersionDependencyTypes none = new VersionDependencyTypes("none");
        public static VersionDependencyTypes exact = new VersionDependencyTypes("exact");
        public static VersionDependencyTypes greaterthan = new VersionDependencyTypes("greaterthan");
        public static VersionDependencyTypes greaterthanorequal = new VersionDependencyTypes("greaterthanorequal");
        public static VersionDependencyTypes lessthan = new VersionDependencyTypes("lessthan");
    }
}
