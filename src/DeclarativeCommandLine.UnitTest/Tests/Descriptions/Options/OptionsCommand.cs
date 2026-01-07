namespace DeclarativeCommandLine.UnitTest.Tests.Descriptions.Options;

/// <summary>
/// Description test at the "option" level.
/// </summary>
[Command(Parent = typeof(DescriptionsCommand))]
public class OptionsCommand
{
	[Option(Description = "A regular string")]
	public string? RegularString { get; set; }

	[Option(Description = "A regular string\nBut with multiple\nlines")]
	public string? RegularStringMultiLine { get; set; }

	[Option(Description = """A raw string literal""")]
	public string? RawStringLiteral { get; set; }

	[Option(
		Description = """
			A raw string literal
			But with multiple
			lines
			"""
	)]
	public string? RawStringLiteralMultiLine { get; set; }
}
