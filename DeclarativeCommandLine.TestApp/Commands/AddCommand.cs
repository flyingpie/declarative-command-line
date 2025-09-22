namespace DeclarativeCommandLine.TestApp.Commands;

[Command<TestRootCommand>]
public class AddCommand
{
	[Option(Alias = "-a", IsRequired = true)]
	public int ValueA { get; set; }

	[Option(Alias = "-b", IsRequired = true)]
	public int ValueB { get; set; }

	[CommandHandler]
	public void Handle()
	{
		Console.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");
	}
}