namespace DeclarativeCommandLine.UnitTest.Tests.Aliases.Commands;

/// <summary>
/// Alias test at the "command" level.
/// </summary>
[Command(Parent = typeof(AliasesCommand))]
public class CommandsCommand
{
	/// <summary>
	/// Command with no aliases.
	/// </summary>
	[Command("aliases-0", Aliases = [], Parent = typeof(CommandsCommand))]
	public class CommandWithAliases0(IOutput output) : ICommand
	{
		public void Execute()
		{
			output.WriteLine("Command with 0 aliases");
		}
	}

	/// <summary>
	/// Command with 1 alias.
	/// </summary>
	[Command("aliases-1", Aliases = ["alias1"], Parent = typeof(CommandsCommand))]
	public class CommandWithAliases1(IOutput output) : ICommand
	{
		public void Execute()
		{
			output.WriteLine("Command with 1 alias");
		}
	}

	/// <summary>
	/// Command with 2 aliases.
	/// </summary>
	[Command("aliases-2", Aliases = ["alias2-1", "alias2-2"], Parent = typeof(CommandsCommand))]
	public class CommandWithAliases2(IOutput output) : ICommand
	{
		public void Execute()
		{
			output.WriteLine("Command with 2 aliases");
		}
	}
}
