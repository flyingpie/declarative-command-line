using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;

namespace DeclarativeCommandLine.UnitTest.Tests.FromAmong.Arguments;

[TestClass]
public partial class ArgumentsTest
{
	[TestClass]
	[UsesVerify]
	public partial class IntsTest
	{
		[TestMethod]
		public async Task Help()
		{
			// Act
			var res = await RunAsync(["test", "from-among", "arguments", "ints", "--help"]);

			// Assert
			await Verifier.Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		// csharpier-ignore-start
		[TestMethod]
		// int-arg
		[DataRow("int-arg",                 "123",  0)]
		[DataRow("int-arg-null",            "123",  0)]
		[DataRow("int-arg-empty-array",     "123",  0)]
		// int-arg-1-value
		[DataRow("int-arg-1-value",         "1",    0)]
		[DataRow("int-arg-1-value",         "2",    1)]
		// int-arg-2-value
		[DataRow("int-arg-2-values",        "1",    0)]
		[DataRow("int-arg-2-values",        "2",    0)]
		[DataRow("int-arg-2-values",        "3",    1)]
		// int-arg-3-value
		[DataRow("int-arg-3-values",        "1",    0)]
		[DataRow("int-arg-3-values",        "2",    0)]
		[DataRow("int-arg-3-values",        "3",    0)]
		[DataRow("int-arg-3-values",        "4",    1)]
		// csharpier-ignore-end
		public async Task IntArg(string arg, string val, int exitCode)
		{
			// Act
			var res = await RunAsync(["test", "from-among", "arguments", "ints", arg, val]);

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
			var res = await RunAsync(["test", "from-among", "arguments", "strings", "--help"]);

			// Assert
			await Verifier.Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		// csharpier-ignore-start
		[TestMethod]
		// string-arg
		[DataRow("string-arg",              "abc",    0)]
		[DataRow("string-arg-null",         "abc",    0)]
		[DataRow("string-arg-empty-array",  "abc",    0)]
		// string-arg-1-value
		[DataRow("string-arg-1-value",      "val-1",  0)]
		[DataRow("string-arg-1-value",      "abc",    1)]
		// string-arg-2-value
		[DataRow("string-arg-2-values",     "val-1",  0)]
		[DataRow("string-arg-2-values",     "val-2",  0)]
		[DataRow("string-arg-2-values",     "abc",    1)]
		// string-arg-3-value
		[DataRow("string-arg-3-values",     "val-1",  0)]
		[DataRow("string-arg-3-values",     "val-2",  0)]
		[DataRow("string-arg-3-values",     "val-3",  0)]
		[DataRow("string-arg-3-values",     "abc",    1)]
		// csharpier-ignore-end
		public async Task Strings(string arg, string val, int exitCode)
		{
			// Act
			var res = await RunAsync(["test", "from-among", "arguments", "strings", arg, val]);

			// Assert
			await Verifier.Verify(res.Output);
			Assert.AreEqual(exitCode, res.Code);
		}
	}
}
