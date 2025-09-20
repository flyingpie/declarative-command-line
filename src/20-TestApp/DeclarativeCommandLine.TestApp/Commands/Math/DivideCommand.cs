namespace DeclarativeCommandLine.TestApp.Commands.Math;

[Command<MathCommand>]
public class DivideCommand : ICommand
{
	public DivideCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}

	[Option(Required = true)]
	public int NumberA { get; set; }

	[Option(Required = true)]
	public int NumberB { get; set; }

	public void Execute()
	{
		Console.WriteLine($"{NumberA} + {NumberB} = {NumberA + NumberB}");
	}
}