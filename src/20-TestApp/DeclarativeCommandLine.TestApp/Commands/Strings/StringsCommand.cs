namespace DeclarativeCommandLine.TestApp.Commands.Strings;

[Command(Parent = typeof(TestRootCommand))]
public class StringsCommand
{
	[Option("a-global", Recursive = true)]
	public string AnotherSomeGlobalOption { get; set; }
}