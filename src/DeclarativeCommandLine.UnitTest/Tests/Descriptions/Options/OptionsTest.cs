using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.Descriptions.Options;

[TestClass]
[UsesVerify]
public partial class OptionsTest
{
	[TestMethod]
	public async Task Help()
	{
		// Act
		var res = await RunAsync(["test", "descriptions", "options", "--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}
}
