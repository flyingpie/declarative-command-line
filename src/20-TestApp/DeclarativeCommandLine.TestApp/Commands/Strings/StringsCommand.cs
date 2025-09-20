namespace DeclarativeCommandLine.TestApp.Commands.Strings;

[Command<TestRootCommand>]
public class StringsCommand
{
	[Option("a-global", Recursive = true)]
	public string AnotherSomeGlobalOption { get; set; }
}