using System.Runtime.CompilerServices;
using VerifyTests;

namespace DeclarativeCommandLine.UnitTest.Tests;

public static class ModuleInit
{
	[ModuleInitializer]
	public static void Init()
	{
		VerifierSettings.AddScrubber(_ => _
			.Replace("ReSharperTestRunner", "the_executable")
			.Replace("DeclarativeCommandLine.UnitTest", "the_executable"));
	}
}