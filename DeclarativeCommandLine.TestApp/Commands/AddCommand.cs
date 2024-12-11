namespace DeclarativeCommandLine.TestApp.Commands;

[Command]
public class AddCommand : ICommand
{
	public AddCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}

	[Option(IsRequired = true)]
	public int NumberA { get; set; }

	[Option(IsRequired = true)]
	public int NumberB { get; set; }

	//[InvocationContext]
	//public InvocationContext Context { get; set; }

	public void Execute()
	{
		Console.WriteLine($"{NumberA} + {NumberB} = {NumberA + NumberB}");
	}
}