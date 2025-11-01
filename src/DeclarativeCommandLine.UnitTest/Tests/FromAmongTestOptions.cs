using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Tests;

[TestClass]
public partial class FromAmongTestOptions
{
	[TestClass]
	[UsesVerify]
	public partial class IntsTest
	{
		[TestMethod]
		public async Task Help()
		{
			// Act
			var res = await TestHelper.RunAsync(["test", "from-among", "options", "ints", "--help"]);

			// Assert
			await Verifier.Verify(res.Output);
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
			var res = await TestHelper.RunAsync(["test", "from-among", "options", "ints", arg, val]);

			// Assert
			await Verifier.Verify(res.Output);
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
			var res = await TestHelper.RunAsync(["test", "from-among", "options", "strings", "--help"]);

			// Assert
			await Verifier.Verify(res.Output);
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
			var res = await TestHelper.RunAsync(["test", "from-among", "options", "strings", arg, val]);

			// Assert
			await Verifier.Verify(res.Output);
			Assert.AreEqual(exitCode, res.Code);
		}
	}
}