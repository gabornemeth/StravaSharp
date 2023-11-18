using System;
using System.Linq;
using System.Xml.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Package);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = Configuration.Release;// IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter]
    public Version Version { get; set; } = new Version(0, 6, 9);

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
        });

    Target Restore => _ => _
        .Executes(() =>
        {
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(RootDirectory / "src/StravaSharp.sln")
                .SetConfiguration(Configuration)
            );
        });

    Target Package => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var nuspec = RootDirectory / "nuspec/StravaSharp.nuspec";
            UpdateNuspecVersion(nuspec);
            NuGetPack(s => s
                .SetTargetPath(nuspec)
            );
        });

    private void UpdateNuspecVersion(AbsolutePath nuspecPath)
    {
        var nuspec = XDocument.Load(nuspecPath);

        XElement version = nuspec.Root.Descendants()
            .Where(x => x.Name.LocalName == "version")
            .FirstOrDefault();
        if (version != null)
        {
            version.Value = Version.ToString();
        }
        else
        {
            var metadata = nuspec.Root.Descendants()
                .Where(x => x.Name.LocalName == "metadata")
                .First();
            metadata.Add(new XElement(XName.Get("version", metadata.Name.NamespaceName), Version));
        }

        nuspec.Save(nuspecPath);
    }
}
