namespace DeclarativeCommandLine.UnitTest.Commands;

public interface IConsole
{
	void WriteLine(string line);
}

[Command]
public class UnitTestRootCommand
{
}

[Command(Parent = typeof(UnitTestRootCommand))]
public class MathCommand
{
}

[Command(Parent = typeof(MathCommand))]
public class AddCommand(IConsole console) : ICommand
{
	[Option]
	public int Value1 { get; set; }

	[Option]
	public int Value2 { get; set; }

	public void Execute()
	{
		console.WriteLine($"{Value1} + {Value2} = {Value1 + Value2}");
	}
}

[Command(Parent = typeof(MathCommand))]
public class SubtractCommand
{
}

[Command(Parent = typeof(UnitTestRootCommand))]
public class StringCommand
{
}