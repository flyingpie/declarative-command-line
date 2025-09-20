namespace DeclarativeCommandLine.TestApp.Commands;

[Command(Description = "Test app root command.")]
public class TestRootCommand
{
	public TestRootCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}

	[Option("global", Description = "Some global option", Recursive = true)]
	public string SomeGlobalOption { get; set; }
}