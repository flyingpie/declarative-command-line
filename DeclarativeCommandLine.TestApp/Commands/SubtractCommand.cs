namespace DeclarativeCommandLine.TestApp.Commands;

[Command<TestRootCommand>]
public class SubtractCommand
{
	[Argument]
	public int ValueA { get; set; }

	[Argument]
	public int ValueB { get; set; }

	[CommandHandler]
	public void Handle()
	{
		Console.WriteLine($"{ValueA} - {ValueB} = {ValueA - ValueB}");
	}
}