namespace DeclarativeCommandLine.UnitTest.Tests.Descriptions.Arguments;

/// <summary>
/// Description test at the "argument" level.
/// </summary>
[Command(Parent = typeof(DescriptionsCommand))]
public class ArgumentsCommand
{
	[Argument(Description = "A regular string")]
	public string? RegularString { get; set; }

	[Argument(Description = "A regular string\nBut with multiple\nlines")]
	public string? RegularStringMultiLine { get; set; }

	[Argument(Description = """A raw string literal""")]
	public string? RawStringLiteral { get; set; }

	[Argument(
		Description = """
			A raw string literal
			But with multiple
			lines
			"""
	)]
	public string? RawStringLiteralMultiLine { get; set; }
}
