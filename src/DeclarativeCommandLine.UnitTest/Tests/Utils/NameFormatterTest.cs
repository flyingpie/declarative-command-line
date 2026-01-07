using DeclarativeCommandLine.Generator;

namespace DeclarativeCommandLine.UnitTest.Tests.Utils;

[TestClass]
public class NameFormatterTest
{
	[TestMethod]
	[DataRow("ArgumentName", "argument-name")]
	[DataRow("ArgumentName01", "argument-name-01")]
	public void PropertyNameToArgumentName(string propertyName, string expectedArgumentName)
	{
		Assert.AreEqual(expectedArgumentName, NameFormatter.PropertyNameToArgumentName(propertyName));
	}

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
