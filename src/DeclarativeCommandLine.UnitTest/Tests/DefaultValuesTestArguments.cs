using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests;

[TestClass]
[UsesVerify]
public partial class DefaultValuesTestArguments
{
	[TestClass]
	[UsesVerify]
	public partial class IntsTest
	{
		[TestMethod]
		public async Task Help()
		{
			// Act
			var res = await RunAsync(["test", "default-value", "arguments", "ints", "--help"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		[TestMethod]
		[DataRow("int",					new string[0])]
		[DataRow("int",					new[] { "123" })]
		[DataRow("int-nullable",		new string[0])]
		[DataRow("int-nullable",		new[] { "123" })]
		public async Task IntArg(string arg, string[] val)
		{
			// Act
			var args = new List<string>(["test", "default-value", "arguments", "ints", arg]);
			args.AddRange(val);
			var res = await RunAsync(args.ToArray());

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}
	}
}