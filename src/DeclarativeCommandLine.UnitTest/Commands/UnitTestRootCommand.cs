using System;
using System.IO;

namespace DeclarativeCommandLine.UnitTest.Commands;

public interface IConsole
{
	void WriteLine(string line);
}

public class TextWriterConsole(TextWriter writer) : IConsole
{
	private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));

	public void WriteLine(string line)
	{
		writer.WriteLine(line);
	}
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
	[Option(Required = true)]
	public int ValueA { get; set; }

	[Option(Required = true)]
	public int ValueB { get; set; }

	public void Execute()
	{
		console.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");
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