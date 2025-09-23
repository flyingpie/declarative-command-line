using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.UnitTest.Commands;

public interface IOutput
{
	void WriteLine(string line);
}

public class ConsoleOutput : IOutput
{
	public void WriteLine(string line)
	{
		Console.WriteLine(line);
	}
}

public class TextWriterOutput(TextWriter writer) : IOutput
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
public class AddCommand(IOutput output) : ICommand
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
		output.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");
	}
}

[Command(Parent = typeof(MathCommand))]
public class SubtractCommand(IOutput output) : IAsyncCommand
{
	[Option(Required = true)]
	public int ValueA { get; set; }

	[Option(Required = true)]
	public int ValueB { get; set; }

	public Task ExecuteAsync(CancellationToken ct = default)
	{
		output.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");

		return Task.CompletedTask;
	}
}

[Command(Parent = typeof(AppRootCommand))]
public class StringCommand
{
}

[Command(Parent = typeof(AppRootCommand))]
public class AliasesCommand
{
	[Command("aliases-0", Aliases = [], Parent = typeof(AliasesCommand))]
	public class CommandWithAliases0(IOutput output) : ICommand
	{
		public void Execute()
		{
			output.WriteLine("Command with 0 aliases");
		}
	}

	[Command("aliases-1", Aliases = ["alias1"], Parent = typeof(AliasesCommand))]
	public class CommandWithAliases1(IOutput output) : ICommand
	{
		public void Execute()
		{
			output.WriteLine("Command with 1 alias");
		}
	}

	[Command("aliases-2", Aliases = ["alias2-1", "alias2-2"], Parent = typeof(AliasesCommand))]
	public class CommandWithAliases2(IOutput output) : ICommand
	{
		public void Execute()
		{
			output.WriteLine("Command with 2 aliases");
		}
	}
}