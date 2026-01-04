using DeclarativeCommandLine.Generator;

namespace DeclarativeCommandLine.UnitTest.Tests.Utils;

[TestClass]
public class NameFormatterTest
{
	[TestMethod]
	[DataRow("TypeName", "type-name")]
	[DataRow("TypeName01", "type-name-01")]
	public void CommandTypeToCommandNameTest(string commandTypeName, string expectedCommandName)
	{
		Assert.AreEqual(expectedCommandName, NameFormatter.CommandTypeToCommandName(commandTypeName));
	}

	[TestMethod]
	[DataRow("OptionName", "--option-name")]
	[DataRow("OptionName01", "--option-name-01")]
	public void PropertyNameToOptionName(string propertyName, string expectedOptionName)
	{
		Assert.AreEqual(expectedOptionName, NameFormatter.PropertyNameToOptionName(propertyName));
	}
}
