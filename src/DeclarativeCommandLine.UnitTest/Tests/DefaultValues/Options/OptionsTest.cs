using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.DefaultValues.Options;

[TestClass]
[UsesVerify]
public partial class OptionsTest
{
	[TestMethod]
	public async Task Help()
	{
		// Act
		var res = await RunAsync(["test", "default-values", "options", "--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	public async Task Default()
	{
		// Act
		var res = await RunAsync(["test", "default-values", "options"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	// csharpier-ignore-start
	[TestMethod]
	[DataRow("--int",             "123")]
	[DataRow("--int-nullable",    "123")]
	[DataRow("--string",          "The Explicit String")]
	// csharpier-ignore-end
	public async Task Explicit(string opt, string val)
	{
		// Act
		var res = await RunAsync(["test", "default-values", "options", opt, val]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}
}
