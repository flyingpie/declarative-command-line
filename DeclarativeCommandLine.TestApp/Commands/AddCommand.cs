namespace DeclarativeCommandLine.TestApp.Commands;

[Command]
public class AddCommand
{
	[Option]
	public int NumberA { get; set; }

	[Option]
	public int NumberB { get; set; }

	[CommandHandler]
	public void Handle()
	{
		Console.WriteLine($"{NumberA} + {NumberB} = {NumberA + NumberB}");
	}
}