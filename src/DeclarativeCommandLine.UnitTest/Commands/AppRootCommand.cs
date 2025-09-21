using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
		_writer.WriteLine(line);
	}
}

[Command]
public class AppRootCommand
{
}

[Command(Parent = typeof(AppRootCommand))]
public class MathCommand
{
}

[Command(Parent = typeof(MathCommand))]
public class AddCommand(IConsole console) : ICommand
{
	[Option(
		Description = "The first value.",
		Required = true)]
	public int ValueA { get; set; }

	[Option(
		Description = "The second value.",
		Required = true)]
	public int ValueB { get; set; }

	public void Execute()
	{
		console.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");
	}
}

[Command(Parent = typeof(MathCommand))]
public class SubtractCommand(IConsole console) : IAsyncCommand
{
	[Option(Required = true)]
	public int ValueA { get; set; }

	[Option(Required = true)]
	public int ValueB { get; set; }

	public Task ExecuteAsync(CancellationToken ct = default)
	{
		console.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");

		return Task.CompletedTask;
	}
}

[Command(Parent = typeof(AppRootCommand))]
public class StringCommand
{
}