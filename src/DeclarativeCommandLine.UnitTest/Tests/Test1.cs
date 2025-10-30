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
	public partial class FromAmongTest
	{
		[TestClass]
		public partial class OptionsTest
		{
			[TestClass]
			[UsesVerify]
			public partial class IntsTest
			{
				[TestMethod]
				public async Task Help()
				{
					// Act
					var res = await RunAsync(["test", "from-among", "options", "ints", "--help"]);

					// Assert
					await Verify(res.Output);
					Assert.AreEqual(0, res.Code);
				}

				[TestMethod]
				[DataRow("--int-opt",				"123",		0)]
				[DataRow("--int-opt-null",			"123",		0)]
				[DataRow("--int-opt-empty-array",	"123",		0)]
				//
				[DataRow("--int-opt1-value",		"1",		0)]
				[DataRow("--int-opt1-value",		"2",		1)]
				//
				[DataRow("--int-opt2-values",		"1",		0)]
				[DataRow("--int-opt2-values",		"2",		0)]
				[DataRow("--int-opt2-values",		"3",		1)]
				//
				[DataRow("--int-opt3-values",		"1",		0)]
				[DataRow("--int-opt3-values",		"2",		0)]
				[DataRow("--int-opt3-values",		"3",		0)]
				[DataRow("--int-opt3-values",		"4",		1)]
				public async Task Ints(string arg, string val, int exitCode)
				{
					// Act
					var res = await RunAsync(["test", "from-among", "options", "ints", arg, val]);

					// Assert
					await Verify(res.Output);
					Assert.AreEqual(exitCode, res.Code);
				}
			}

			[TestClass]
			[UsesVerify]
			public partial class StringsTest
			{
				[TestMethod]
				public async Task Help()
				{
					// Act
					var res = await RunAsync(["test", "from-among", "options", "strings", "--help"]);

					// Assert
					await Verify(res.Output);
					Assert.AreEqual(0, res.Code);
				}

				[TestMethod]
				[DataRow("--string-opt",				"abc",		0)]
				[DataRow("--string-opt-null",			"abc",		0)]
				[DataRow("--string-opt-empty-array",	"abc",		0)]
				//
				[DataRow("--string-opt1-value",			"val-1",	0)]
				[DataRow("--string-opt1-value",			"abc",		1)]
				//
				[DataRow("--string-opt2-values",		"val-1",	0)]
				[DataRow("--string-opt2-values",		"val-2",	0)]
				[DataRow("--string-opt2-values",		"abc",		1)]
				//
				[DataRow("--string-opt3-values",		"val-1",	0)]
				[DataRow("--string-opt3-values",		"val-2",	0)]
				[DataRow("--string-opt3-values",		"val-3",	0)]
				[DataRow("--string-opt3-values",		"abc",		1)]
				public async Task Strings(string arg, string val, int exitCode)
				{
					// Act
					var res = await RunAsync(["test", "from-among", "options", "strings", arg, val]);

					// Assert
					await Verify(res.Output);
					Assert.AreEqual(exitCode, res.Code);
				}
			}
		}
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

	[TestClass]
	[UsesVerify]
	public partial class InheritanceTest
	{
		[TestMethod]
		public async Task BaseCommand()
		{
			// Act
			var res = await RunAsync(["inheritance", "inheritance-0", "--base-argument-a", "a1", "--base-option-a", "a2"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		[TestMethod]
		public async Task ChildCommand()
		{
			// Act
			var res = await RunAsync(["inheritance", "inheritance-1", "--base-argument-a", "a1", "--base-option-a", "a2", "--child-argument-a", "b1", "--child-option-a", "b2"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		[TestMethod]
		public async Task GrandChildCommand()
		{
			// Act
			var res = await RunAsync(["inheritance", "inheritance-2", "--base-argument-a", "a1", "--base-option-a", "a2", "--child-argument-a", "b1", "--child-option-a", "b2", "--grand-child-argument-a", "c1", "--grand-child-option-a", "c2"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}
	}
}