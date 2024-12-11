namespace DeclarativeCommandLine.TestApp.Commands;

[RootCommand]
public class TestRootCommand
{
	public TestRootCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}
}