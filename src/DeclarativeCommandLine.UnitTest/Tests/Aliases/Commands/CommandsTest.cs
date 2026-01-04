using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.Aliases.Commands;

[TestClass]
[UsesVerify]
public partial class CommandsTest
{
	[TestMethod]
	public async Task Aliases_Parent()
	{
		// Act
		var res = await RunAsync(["test", "aliases", "commands"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(1, res.Code);
	}

	[TestMethod]
	[DataRow("aliases-0")]
	public async Task Aliases_0(string cmd)
	{
		// Act
		var res = await RunAsync(["test", "aliases", "commands", cmd]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	[DataRow("aliases-1")]
	[DataRow("alias1")]
	[SuppressMessage("Major Code Smell", "S4144:Methods should not have identical implementations", Justification = "MvdO: The output is different due to the command used.")]
	public async Task Aliases_1(string cmd)
	{
		// Act
		var res = await RunAsync(["test", "aliases", "commands", cmd]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	[DataRow("aliases-2")]
	[DataRow("alias2-1")]
	[DataRow("alias2-2")]
	[SuppressMessage("Major Code Smell", "S4144:Methods should not have identical implementations", Justification = "MvdO: The output is different due to the command used.")]
	public async Task Aliases_2(string cmd)
	{
		// Act
		var res = await RunAsync(["test", "aliases", "commands", cmd]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}
}