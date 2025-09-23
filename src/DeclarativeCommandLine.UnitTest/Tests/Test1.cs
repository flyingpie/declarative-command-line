using System.Diagnostics.CodeAnalysis;
using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests;

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

	[TestClass]
	[UsesVerify]
	public partial class CommandTest
	{
		[TestMethod]
		public async Task Aliases_Parent()
		{
			// Act
			var res = await RunAsync(["aliases"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(1, res.Code);
		}

		[TestMethod]
		[DataRow("aliases-0")]
		public async Task Aliases_0(string cmd)
		{
			// Act
			var res = await RunAsync(["aliases", cmd]);

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
			var res = await RunAsync(["aliases", cmd]);

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
			var res = await RunAsync(["aliases", cmd]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}
	}
}