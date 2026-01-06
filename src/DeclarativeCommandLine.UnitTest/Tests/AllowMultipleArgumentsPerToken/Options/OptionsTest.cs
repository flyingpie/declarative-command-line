using static DeclarativeCommandLine.UnitTest.Utils.TestHelper;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests.AllowMultipleArgumentsPerToken.Options;

[TestClass]
[UsesVerify]
public partial class OptionsTest
{
	[TestMethod]
	public async Task Help()
	{
		// Act
		var res = await RunAsync(["test", "allow-multiple-arguments-per-token", "options", "--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	[DataRow(0, new[] { "--int", "123" })]
	[DataRow(1, new[] { "--int", "123", "321" })]
	[DataRow(0, new[] { "--int", "123", "--int", "321" })]
	[DataRow(1, new[] { "--int", "123", "321", "231" })]
	[DataRow(0, new[] { "--int", "123", "--int", "321", "--int", "231" })]
	// --int-nullable
	[DataRow(0, new[] { "--int-nullable", "123" })]
	[DataRow(1, new[] { "--int-nullable", "123", "321" })]
	[DataRow(0, new[] { "--int-nullable", "123", "--int-nullable", "321" })]
	[DataRow(1, new[] { "--int-nullable", "123", "321", "231" })]
	[DataRow(0, new[] { "--int-nullable", "123", "--int-nullable", "321", "--int-nullable", "231" })]
	// --int-array
	[DataRow(0, new[] { "--int-array", "123" })]
	[DataRow(0, new[] { "--int-array", "123", "321" })]
	[DataRow(0, new[] { "--int-array", "123", "--int-array", "321" })]
	[DataRow(0, new[] { "--int-array", "123", "321", "231" })]
	[DataRow(0, new[] { "--int-array", "123", "--int-array", "321", "--int-array", "231" })]
	// --int-array-nullable
	[DataRow(0, new[] { "--int-array-nullable", "123" })]
	[DataRow(0, new[] { "--int-array-nullable", "123", "321" })]
	[DataRow(0, new[] { "--int-array-nullable", "123", "--int-array-nullable", "321" })]
	[DataRow(0, new[] { "--int-array-nullable", "123", "321", "231" })]
	[DataRow(0, new[] { "--int-array-nullable", "123", "--int-array-nullable", "321", "--int-array-nullable", "231" })]
	// --string
	[DataRow(0, new[] { "--string", "The First String" })]
	[DataRow(1, new[] { "--string", "The First String", "The Second String" })]
	[DataRow(0, new[] { "--string", "The First String", "--string", "The Second String" })]
	[DataRow(0, new[] { "--string-array", "The First String" })]
	[DataRow(0, new[] { "--string-array", "The First String", "The Second String" })]
	[DataRow(0, new[] { "--string-array", "The First String", "--string-array", "The Second String" })]
	public async Task Multiple(int exitCode, string[] val)
	{
		// Act
		var res = await RunAsync(["test", "allow-multiple-arguments-per-token", "options", .. val]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(exitCode, res.Code);
	}
}
