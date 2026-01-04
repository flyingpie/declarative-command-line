using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.Utils;

[TestClass]
[UsesVerify]
public partial class Test1
{
	[TestMethod]
	public Task VerifyCheck() => VerifyChecks.Run();

	[TestMethod]
	public async Task Help()
	{
		// Act
		var res = await RunAsync(["--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_Bare()
	{
		// Act
		var res = await RunAsync(["math", "add"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(1, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_Help()
	{
		// Act
		var res = await RunAsync(["math", "add", "--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_MissingOneOpt()
	{
		// Act
		var res = await RunAsync(["math", "add", "--value-a", "12"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(1, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_Success()
	{
		// Act
		var res = await RunAsync(["math", "add", "--value-a", "12", "--value-b", "34"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}
}
