using DeclarativeCommandLine.Generator;

namespace DeclarativeCommandLine.UnitTest.Tests;

[TestClass]
public class NameFormatterTest
{
	[TestMethod]
	[DataRow("TypeName", "type-name")]
	public void CommandTypeToCommandNameTest(string commandTypeName, string expectedCommandName)
	{
		Assert.AreEqual(expectedCommandName, NameFormatter.CommandTypeToCommandName(commandTypeName));
	}
}