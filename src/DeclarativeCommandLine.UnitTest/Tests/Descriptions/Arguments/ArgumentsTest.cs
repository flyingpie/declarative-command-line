using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.Descriptions.Arguments;

[TestClass]
[UsesVerify]
public partial class ArgumentsTest
{
	[TestMethod]
	public async Task Help()
	{
		// Act
		var res = await RunAsync(["test", "descriptions", "arguments", "--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}
}
