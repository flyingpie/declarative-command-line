namespace DeclarativeCommandLine.TestApp.Commands.Math;

[Command<MathCommand>]
public class AbsCommand : ICommand
{
	public AbsCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}

	[Option(DefaultValue = 42)]
	public int NumberA { get; set; }

	[Option(Required = true)]
	public int NumberB { get; set; }

	public void Execute()
	{
		Console.WriteLine($"{NumberA} + {NumberB} = {NumberA + NumberB}");

		throw new InvalidOperationException("Bleh");
	}
}