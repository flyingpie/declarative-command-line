#tool "nuget:?package=GitVersion.CommandLine&version=4.0.0"
#tool "nuget:?package=vswhere&version=2.6.7"

var configuration = Argument("configuration", "Release");
var isPreRelease = Argument("isPreRelease", true);
var output = Argument("output", "artifacts");
var target = Argument("target", "Default");

var nugetApiUrl = Argument("nugetApiUrl", "https://api.nuget.org/v3/index.json");
var nugetApiKey = Argument("nugetApiKey", "");

var sln = "DeclarativeCommandLine.sln";
var nupkgs = $"DeclarativeCommandLine/bin/{configuration}/*.nupkg";

// Determine package version
var gv = GitVersion();
var branch = gv.BranchName;
if (branch.Contains("/")) branch = branch.Substring(branch.LastIndexOf('/') + 1);

var version = XmlPeek(GetFiles("**/*.csproj").First(), "//Version");
var versionPkg = !isPreRelease ? version : $"{version}-{branch}-{DateTime.Now:MMddHHmm}";

Task("Clean")
	.Does(() =>
	{
		CleanDirectory(output);
	});

Task("Build")
	.IsDependentOn("Clean")
	.Does(() =>
	{
		MSBuild(sln, new MSBuildSettings
		{
			Configuration = configuration,
			Restore = true,
			ToolPath = GetFiles(VSWhereLatest() + "/**/MSBuild.exe").FirstOrDefault()
		}
			.WithProperty("AssemblyVersion", version)
			.WithProperty("FileVersion", versionPkg)
			.WithProperty("InformationalVersion", versionPkg)
			.WithProperty("PackageVersion", versionPkg)
		);
	});

Task("Test")
	.IsDependentOn("Build")
	.Does(() =>
	{
		VSTest($"./**/bin/{configuration}/net6.0/*.UnitTest.dll", new VSTestSettings
		{
			ToolPath = GetFiles(VSWhereLatest() + "/**/vstest.console.exe").FirstOrDefault()
		});
	});

Task("Artifact.NuGet")
	.IsDependentOn("Test")
	.Does(() =>
	{
		CopyFiles(nupkgs, output);
	});

Task("Push")
	.IsDependentOn("Artifact.NuGet")
	.Does(() =>
	{
		var package = GetFiles(nupkgs).FirstOrDefault()
			?? throw new InvalidOperationException($"No NuGet package found at '{nupkgs}'.");

		Information($"Package: {package}");

		NuGetPush(package, new NuGetPushSettings {
			Source = nugetApiUrl,
			ApiKey = nugetApiKey
		});
	});

Task("Default")
	.IsDependentOn("Artifact.NuGet")
	.Does(() => {})
;

RunTarget(target);
