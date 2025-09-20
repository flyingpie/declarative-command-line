namespace DeclarativeCommandLine.TestApp.Commands.Math;

[Command(Name = "add", Parent = typeof(MathCommand))]
public class AddCommand : ICommand
{
	public AddCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}

	[Option("--number-a", Description = "The first value.", Required = true)]
	public int NumberA { get; set; }

	[Option(Required = true)]
	public int NumberB { get; set; }

	public void Execute()
	{
		Console.WriteLine($"{NumberA} + {NumberB} = {NumberA + NumberB}");
	}
}