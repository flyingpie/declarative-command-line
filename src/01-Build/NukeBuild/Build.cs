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
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[GitHubActions(
	"ci",
	GitHubActionsImage.WindowsLatest,
	FetchDepth = 0,
	OnPushBranches = ["master"],
	OnWorkflowDispatchOptionalInputs = [ "name" ],
	EnableGitHubToken = true,
	InvokedTargets = [nameof(PublishRelease)])]	
[SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "MvdO: Build script.")]
public sealed class Build : NukeBuild
{
	public static int Main() => Execute<Build>(x => x.PublishDebug);

	[Nuke.Common.Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
	private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

	[Required]
	[GitRepository]
	private readonly GitRepository GitRepository;

	[Nuke.Common.Parameter("GitHub Token")]
	private readonly string GitHubToken;

	private AbsolutePath VersionFile => RootDirectory / "VERSION";

	private AbsolutePath OutputDirectory => RootDirectory / "_output";

	private AbsolutePath ArtifactsDirectory => OutputDirectory / "artifacts";

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
	/// Windows x64 self contained.
	/// </summary>
	private Target Publish => _ => _
		.DependsOn(Clean)
//		.Produces(PathToLinux64SelfContainedZip)
		.Executes(() =>
		{
			DotNetPublish(_ => _
				.SetAssemblyVersion(AssemblyVersion)
				.SetInformationalVersion(InformationalVersion)
				.SetConfiguration(Configuration)
				.SetProject(Solution._0_Lib.DeclarativeCommandLine)
				.SetOutput(ArtifactsDirectory));
		});

	private Target PublishDebug => _ => _
		.DependsOn(Clean)
		.DependsOn(Publish)
		.Executes();

	[SuppressMessage("Major Code Smell", "S1144:Unused private types or members should be removed", Justification = "MvdO: Invoked manually.")]
	private Target PublishRelease => _ => _
		.DependsOn(Clean)
		.DependsOn(Publish)
		// TODO: Nuget push
		.Executes();
}