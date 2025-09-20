namespace DeclarativeCommandLine.TestApp.Commands;

[RootCommand]
public class TestRootCommand
{
	public TestRootCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}

	[Option("global", Recursive = true)]
	public string SomeGlobalOption { get; set; }
}