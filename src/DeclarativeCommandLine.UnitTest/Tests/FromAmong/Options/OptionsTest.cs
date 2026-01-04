namespace DeclarativeCommandLine.UnitTest.Tests.FromAmong.Options;

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
		[DataRow("--int-opt-1-value",		"1",		0)]
		[DataRow("--int-opt-1-value",		"2",		1)]
		//
		[DataRow("--int-opt-2-values",		"1",		0)]
		[DataRow("--int-opt-2-values",		"2",		0)]
		[DataRow("--int-opt-2-values",		"3",		1)]
		//
		[DataRow("--int-opt-3-values",		"1",		0)]
		[DataRow("--int-opt-3-values",		"2",		0)]
		[DataRow("--int-opt-3-values",		"3",		0)]
		[DataRow("--int-opt-3-values",		"4",		1)]
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
		[DataRow("--string-opt-1-value",			"val-1",	0)]
		[DataRow("--string-opt-1-value",			"abc",		1)]
		//
		[DataRow("--string-opt-2-values",		"val-1",	0)]
		[DataRow("--string-opt-2-values",		"val-2",	0)]
		[DataRow("--string-opt-2-values",		"abc",		1)]
		//
		[DataRow("--string-opt-3-values",		"val-1",	0)]
		[DataRow("--string-opt-3-values",		"val-2",	0)]
		[DataRow("--string-opt-3-values",		"val-3",	0)]
		[DataRow("--string-opt-3-values",		"abc",		1)]
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