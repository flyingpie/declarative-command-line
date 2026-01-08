namespace DeclarativeCommandLine.UnitTest.Tests.Descriptions.Commands;

/// <summary>
/// Description test at the "command" level.
/// </summary>
[Command(Parent = typeof(DescriptionsCommand))]
public class CommandsCommand
{
	[Command(Parent = typeof(CommandsCommand), Description = "A regular string")]
	public class RegularStringCommand { }

	[Command(Parent = typeof(CommandsCommand), Description = "A regular string\nBut with multiple\nlines")]
	public class RegularStringMultiLineCommand { }

	[Command(Parent = typeof(CommandsCommand), Description = """A raw string literal""")]
	public class RawStringLiteralCommand { }

	[Command(
		Parent = typeof(CommandsCommand),
		Description = """
			A raw string literal
			But with multiple
			lines
			"""
	)]
	public class RawStringLiteralMultiLineCommand { }

	[Command(
		Parent = typeof(CommandsCommand),
		Description = """
			________  _________ .____
			\______ \ \_   ___ \|    |
			 |    |  \/    \  \/|    |
			 |    `   \     \___|    |___
			/_______  /\______  /_______ \
			        \/        \/        \/
			"""
	)]
	public class EscapingCommand { }
}
