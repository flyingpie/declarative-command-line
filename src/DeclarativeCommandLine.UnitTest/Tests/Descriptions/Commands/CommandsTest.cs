using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.Descriptions.Commands;

[TestClass]
[UsesVerify]
public partial class CommandsTest
{
	[TestMethod]
	public async Task Help()
	{
		// Act
		var res = await RunAsync(["test", "descriptions", "commands"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(1, res.Code);
	}
}
