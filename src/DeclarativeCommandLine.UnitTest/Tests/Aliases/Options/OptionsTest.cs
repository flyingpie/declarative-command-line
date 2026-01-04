using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.Aliases.Options;

[TestClass]
[UsesVerify]
public partial class OptionsTest
{
	[TestMethod]
	public async Task AliasesHelp()
	{
		// Act
		var res = await RunAsync(["test", "aliases", "options", "--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	[DataRow("--aliases-0")]
	public async Task Aliases0(string option)
	{
		// Act
		var res = await RunAsync(["test", "aliases", "options", option, "val-1"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	[DataRow("--aliases-1")]
	[DataRow("--alias-a-1")]
	public async Task Aliases1(string option)
	{
		// Act
		var res = await RunAsync(["test", "aliases", "options", option, "val-1"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	[DataRow("--aliases-2")]
	[DataRow("--alias-b-1")]
	[DataRow("--alias-b-2")]
	public async Task Aliases2(string option)
	{
		// Act
		var res = await RunAsync(["test", "aliases", "options", option, "val-1"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}
}