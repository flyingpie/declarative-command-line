using System.Runtime.CompilerServices;
using VerifyTests;

namespace DeclarativeCommandLine.UnitTest;

public static class ModuleInit
{
	[ModuleInitializer]
	public static void Init()
	{
		VerifierSettings.AddScrubber(_ =>
			_.Replace("DeclarativeCommandLine.UnitTest", "the_executable")
				.Replace("ReSharperTestRunner", "the_executable")
				.Replace("testhost", "the_executable")
		);
	}
}
