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
#addin nuget:?package = Cake.FileHelpers&version = 1.0.4
#tool nuget:?package = GitVersion.CommandLine
    public class VersionUtils
    {
        public static VersionInfo LoadVersion(ICakeContext context, Settings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            VersionInfo verInfo = null;
            if (!string.IsNullOrEmpty(settings.Version.VersionFile) && context.FileExists(settings.Version.VersionFile))
            {
                verInfo = LoadVersionFromJson(context, settings.Version.VersionFile);
            }

            if (verInfo == null && !string.IsNullOrEmpty(settings.Version.AssemblyInfoFile) && context.FileExists(settings.Version.AssemblyInfoFile))
            {
                verInfo = LoadVersionFromAssemblyInfo(context, settings.Version.AssemblyInfoFile);
            }

            if (verInfo == null && settings.Version.LoadFromGit)
            {
                verInfo = LoadVersionFromGit(context);
            }

            if (verInfo != null)
            {
                verInfo.CakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();
            }

            return verInfo;
        }

        private static VersionInfo LoadVersionFromJson(ICakeContext context, string versionFile)
        {
            context.Info("Loading Version Info From File: {0}", versionFile);
            if (!context.FileExists(versionFile))
            {
                context.Error("Version File Does Not Exist");
                return null;
            }

            var obj = context.DeserializeJsonFromFile<VersionInfo>(versionFile);
            return obj;
        }

        private static VersionInfo LoadVersionFromAssemblyInfo(ICakeContext context, string assemblyInfoFile)
        {
            context.Info("Fetching Version Info from AssemblyInfo File: {0}", assemblyInfoFile);
            if (!context.FileExists(assemblyInfoFile))
            {
                context.Error("AssemblyInfo file does not exist");
                return null;
            }

            try
            {
                var assemblyInfo = context.ParseAssemblyInfo(assemblyInfoFile);
                var v = new Version(assemblyInfo.AssemblyVersion);
                var verInfo = new VersionInfo{Major = v.Major, Minor = v.Minor, Build = v.Build, Semantic = assemblyInfo.AssemblyInformationalVersion, Milestone = string.Concat("v", v.ToString())};
                return verInfo;
            }
            catch
            {
            }

            return null;
        }

        private static VersionInfo LoadVersionFromGit(ICakeContext context)
        {
            context.Info("Fetching Verson Infop from Git");
            try
            {
                GitVersion assertedVersions = context.GitVersion(new GitVersionSettings{OutputType = GitVersionOutput.Json, });
                var verInfo = new VersionInfo{Major = assertedVersions.Major, Minor = assertedVersions.Minor, Build = assertedVersions.Patch, Semantic = assertedVersions.LegacySemVerPadded, Milestone = string.Concat("v", assertedVersions.MajorMinorPatch)};
                context.Info("Calculated Semantic Version: {0}", verInfo.Semantic);
                return verInfo;
            }
            catch
            {
            }

            return null;
        }

        public static void UpdateVersion(ICakeContext context, Settings settings, VersionInfo verInfo)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (!string.IsNullOrEmpty(settings.Version.VersionFile) && context.FileExists(settings.Version.VersionFile))
            {
                context.Info("Updating Version File {0}", settings.Version.VersionFile);
                context.SerializeJsonToFile(settings.Version.VersionFile, verInfo);
            }

            if (!string.IsNullOrEmpty(settings.Version.AssemblyInfoFile) && context.FileExists(settings.Version.AssemblyInfoFile))
            {
                context.Info("Updating Assembly Info File {0}", settings.Version.AssemblyInfoFile);
                context.ReplaceRegexInFiles(settings.Version.AssemblyInfoFile, "AssemblyVersion\\(.*\\)", string.Format("AssemblyVersion(\"{0}\")", verInfo.ToString(false)));
                context.ReplaceRegexInFiles(settings.Version.AssemblyInfoFile, "AssemblyFileVersion\\(.*\\)", string.Format("AssemblyFileVersion(\"{0}\")", verInfo.ToString(false)));
            }
        }

        public static void UpdateNuSpecVersion(ICakeContext context, Settings settings, VersionInfo verInfo, AbsolutePath nuspecFile)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var xpq = string.Format("/n:package/n:metadata/n:version");
            context.Info("\tUpdating Version in Nuspec File {0} to {1}", nuspecFile, verInfo.ToString());
            try
            {
                context.XmlPoke(nuspecFile, xpq, verInfo.ToString(), new XmlPokeSettings{PreserveWhitespace = true, Namespaces = new Dictionary<string, string>{{ /* Prefix */"n", /* URI */ "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd"}}});
            }
            catch
            {
            } // Its ok to throw these away as it most likely means the file didn't exist or the XPath didn't find any nodes
        }

        public static void UpdateNuSpecVersionDependency(ICakeContext context, Settings settings, VersionInfo verInfo, AbsolutePath nuspecFile)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (string.IsNullOrEmpty(settings.Version.NamespaceBase))
                return;
            var xpq = string.Format("/n:package/n:metadata/n:dependencies//n:dependency[starts-with(@id, '{0}')]/@version", settings.Version.NamespaceBase);
            var replacementStr = verInfo.ToString();
            switch (settings.NuGet.VersionDependencyForLibrary)
            {
                case "none":
                    break;
                case "exact":
                    replacementStr = string.Format("[{0}]", replacementStr);
                    break;
                case "greaterthan":
                    replacementStr = string.Format("(,{0})", replacementStr);
                    break;
                case "greaterthanorequal":
                    replacementStr = string.Format("(,{0}]", replacementStr);
                    break;
                case "lessthan":
                    replacementStr = string.Format("({0},)", replacementStr);
                    break;
            }

            context.Info("\tUpdating Version for {0} Namespace Assemblies in Nuspec File {1} to {2}", settings.Version.NamespaceBase, nuspecFile, replacementStr);
            try
            {
                context.XmlPoke(nuspecFile, xpq, replacementStr, new XmlPokeSettings{PreserveWhitespace = true, Namespaces = new Dictionary<string, string>{{ /* Prefix */"n", /* URI */ "http://schemas.microsoft.com/packaging/2012/06/nuspec.xsd"}}});
            }
            catch
            {
            } // Its ok to throw these away as it most likely means the file didn't exist or the XPath didn't find any nodes
        }
    }

    public class VersionInfo
    {
        [Newtonsoft.Json.JsonProperty("major")]
        public int Major { get; set; }

        [Newtonsoft.Json.JsonProperty("minor")]
        public int Minor { get; set; }

        [Newtonsoft.Json.JsonProperty("build")]
        public int Build { get; set; }

        [Newtonsoft.Json.JsonProperty("preRelease")]
        public int? PreRelease { get; set; }

        [Newtonsoft.Json.JsonProperty("releaseNotes")]
        public string[] ReleaseNotes { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Semantic { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Milestone { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string CakeVersion { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public bool IsPreRelease
        {
            get
            {
                return PreRelease != null && PreRelease != 0;
            }
        }

        public string ToString(bool includePreRelease = true)
        {
            var str = string.Format("{0:#0}.{1:#0}.{2:#0}", Major, Minor, Build);
            if (IsPreRelease && includePreRelease)
                str += string.Format("-pre{0:00}", PreRelease);
            return str;
        }

        public void Display(ICakeContext context)
        {
            context.Info("Version:");
            context.Info("\tMajor: {0}", Major);
            context.Info("\tMinor: {0}", Minor);
            context.Info("\tBuild: {0}", Build);
            context.Info("\tIs PreRelease: {0}", IsPreRelease);
            context.Info("\tPreRelease: {0}", PreRelease);
            context.Info("\tSemantic: {0}", Semantic);
            context.Info("\tMilestone: {0}", Milestone);
            context.Info("\tCake Version: {0}", CakeVersion);
            if (ReleaseNotes != null)
                context.Info("\tRelease Notes: {0}", ReleaseNotes);
        }
    }
}
