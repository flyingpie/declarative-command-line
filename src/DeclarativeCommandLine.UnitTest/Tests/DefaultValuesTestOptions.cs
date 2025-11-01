using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests;

[TestClass]
[UsesVerify]
public partial class DefaultValuesTestOptions
{
	[TestClass]
	[UsesVerify]
	public partial class IntsTest
	{
		[TestMethod]
		public async Task Help()
		{
			// Act
			var res = await RunAsync(["test", "default-value", "options", "--help"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		[TestMethod]
		public async Task Default()
		{
			// Act
			var res = await RunAsync(["test", "default-value", "options"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		[TestMethod]
		[DataRow("--int",					"123")]
		[DataRow("--int-nullable",			"123")]
		[DataRow("--string",				"The Explicit String")]
		public async Task Explicit(string opt, string val)
		{
			// Act
			var res = await RunAsync(["test", "default-value", "options", opt, val]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}
	}
}