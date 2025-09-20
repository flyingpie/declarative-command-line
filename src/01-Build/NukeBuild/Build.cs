#pragma warning disable // MvdO: Build scripts are funky

using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Serilog;
using System.Linq;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[GitHubActions(
	"ci-debug",
	GitHubActionsImage.UbuntuLatest,
	FetchDepth = 0,
	OnPushBranches = ["*"],
	OnWorkflowDispatchOptionalInputs = ["name"],
	EnableGitHubToken = true,
	ImportSecrets = ["NUGET_API_KEY"],
	InvokedTargets = [nameof(Push)])]
[GitHubActions(
	"ci-stable",
	GitHubActionsImage.UbuntuLatest,
	FetchDepth = 0,
	OnPushBranches = ["master"],
	OnWorkflowDispatchOptionalInputs = ["name"],
	EnableGitHubToken = true,
	ImportSecrets = ["NUGET_API_KEY"],
	InvokedTargets = [nameof(Push)])]
public sealed class Build : NukeBuild
{
	[CanBeNull]
	private string _suffix;

	#region Parameters

	[Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
	private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

	[GitRepository] [Required]
	private readonly GitRepository GitRepository;

	[Parameter("GitHub Token")]
	private readonly string GitHubToken;

	[Parameter("NuGet API Key")] [Secret] [CanBeNull]
	private readonly string NuGetApiKey;

	[Parameter("NuGet Source")]
	private readonly string NuGetSource = "https://api.nuget.org/v3/index.json";

	[Parameter("Public Release")]
	private readonly bool PublicRelease;

	[Parameter("Public Release")]
	private string PublicReleaseStr => PublicRelease.ToString().ToLowerInvariant();

	#endregion

	public static int Main() => Execute<Build>(x => x.Pack);

	private AbsolutePath OutputDirectory => RootDirectory / "artifacts";

	private AbsolutePath PackageDirectory => OutputDirectory / "package";

	[Solution(GenerateProjects = true, SuppressBuildProjectCheck = true)]
	private readonly Solution Solution;

	private Target ReportInfo => _ => _
		.Executes(() =>
		{
			Log.Information("Configuration:{Configuration}", Configuration);
			Log.Information("PublicRelease:{PublicRelease}", PublicReleaseStr);
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
	/// Build NuGet package.
	/// </summary>
	private Target Pack => _ => _
		.DependsOn(Clean)
		.Produces(PackageDirectory)
		.Executes(() =>
		{
			DotNetPack(_ => _
				.SetConfiguration(Configuration)
				.SetProject(Solution._0_Lib.DeclarativeCommandLine)
				.SetOutputDirectory(PackageDirectory).SetProperty("PublicRelease", PublicReleaseStr)
			);
		})
		.Executes(() =>
		{
			DotNetPack(_ => _
				.SetConfiguration(Configuration)
				.SetProject(Solution._0_Lib.DeclarativeCommandLine_Generator)
				.SetOutputDirectory(PackageDirectory).SetProperty("PublicRelease", PublicReleaseStr)
			);
		});

	/// <summary>
	/// Push NuGet package.
	/// </summary>
	private Target Push => _ => _
		.DependsOn(Pack)
		.Produces(PackageDirectory)
		.Executes(() =>
		{
			foreach (var nupkg in PackageDirectory.GlobFiles("*.nupkg"))
			{
				DotNetNuGetPush(p => p
					.SetApiKey(NuGetApiKey)
					.SetSkipDuplicate(true)
					.SetSource(NuGetSource)
					.SetTargetPath(nupkg)
				);
			}
		});
}