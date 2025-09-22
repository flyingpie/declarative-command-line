using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[GitHubActions(
	"ci",
	GitHubActionsImage.UbuntuLatest,
	FetchDepth = 0,
	OnPushBranches = ["master"],
	OnWorkflowDispatchOptionalInputs = ["name"],
	EnableGitHubToken = true,
	ImportSecrets = ["NUGET_API_KEY"],
	InvokedTargets = [nameof(PublishRelease)])]
[SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "MvdO: Build script.")]
public sealed class Build : NukeBuild
{
	[CanBeNull]
	private string _suffix;

	#region Parameters

	[Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
	private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

	[GitRepository]
	[Required]
	private readonly GitRepository GitRepository;

	[Parameter("GitHub Token")]
	private readonly string GitHubToken;

	[Parameter("NuGet API Key")]
	[Secret]
	[CanBeNull]
	private readonly string NuGetApiKey;

	[Parameter("NuGet Source")]
	private readonly string NuGetSource = "https://api.nuget.org/v3/index.json";

	#endregion

	public static int Main() => Execute<Build>(x => x.PublishDebug);

	private AbsolutePath VersionFile => RootDirectory / "VERSION";

	private AbsolutePath OutputDirectory => RootDirectory / "artifacts";

	private AbsolutePath ArtifactsDirectory => OutputDirectory / "nupkg";

	[Solution(GenerateProjects = true, SuppressBuildProjectCheck = true)]
	private readonly Solution Solution;

	private GitHubActions GitHubActions => GitHubActions.Instance;

	/// <summary>
	/// Returns the version as defined by VERSION (eg. "2.3.4").
	/// </summary>
	private string SemVerVersion => File.ReadAllText(VersionFile).Trim();

	/// <summary>
	/// Returns the version for use in assembly versioning.
	/// </summary>
	private string AssemblyVersion => $"{SemVerVersion}";

	/// <summary>
	/// Returns the version for use in assembly versioning.
	/// </summary>
	private string InformationalVersion => $"{SemVerVersion}.{DateTimeOffset.UtcNow:yyyyMMdd}+{GitRepository.Commit}";

	private Target ReportInfo => _ => _
		.Executes(() =>
		{
			Log.Information("SemVerVersion:{SemVerVersion}", SemVerVersion);
			Log.Information("AssemblyVersion:{AssemblyVersion}", AssemblyVersion);
			Log.Information("InformationalVersion:{InformationalVersion}", InformationalVersion);
		});

	/// <summary>
	/// Clean output directories.
	/// </summary>
	private Target Clean => _ => _
		.DependsOn(ReportInfo)
		.Executes(() =>
		{
			OutputDirectory.CreateOrCleanDirectory();
		});

	/// <summary>
	/// Set version suffix for prereleases.
	/// </summary>
	private Target SetVersionSuffix => _ => _
		.Before(Clean)
		.Executes(() =>
		{
			_suffix = $"{GitRepository.Branch}-{DateTime.UtcNow:yyyy-MM-dd-HHmm}";
		});

	/// <summary>
	/// Build NuGet package.
	/// </summary>
	private Target Pack => _ => _
		.DependsOn(Clean)
		.Produces(ArtifactsDirectory)
		.Executes(() =>
		{
			DotNetPack(_ =>
			{
				var p = _
					.SetAssemblyVersion(AssemblyVersion)
					.SetInformationalVersion(InformationalVersion)
					.SetConfiguration(Configuration)
					.SetProject(Solution._0_Lib.DeclarativeCommandLine)
					.SetVersionPrefix(SemVerVersion)
					.SetOutputDirectory(ArtifactsDirectory);

				if (_suffix != null)
				{
					p = p.SetVersionSuffix(_suffix);
				}

				return p;
			});
		});

	/// <summary>
	/// Push NuGet package.
	/// </summary>
	private Target Push => _ => _
		.DependsOn(Pack)
		.Produces(ArtifactsDirectory)
		.Executes(() =>
		{
			DotNetNuGetPush(p => p
				.SetApiKey(NuGetApiKey)
				.SetSource(NuGetSource)
				.SetTargetPath(ArtifactsDirectory.GlobFiles("*.nupkg").First())
			);
		});

	private Target PublishDebug => _ => _
		.DependsOn(SetVersionSuffix)
		.DependsOn(Push)
		.Executes();

	// [SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "MvdO: Invoked manually.")]
	private Target PublishRelease => _ => _
		.DependsOn(Push)
		.Executes();
}