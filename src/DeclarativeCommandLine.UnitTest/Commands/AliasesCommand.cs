using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Commands;

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