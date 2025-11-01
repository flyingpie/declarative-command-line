using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Commands;

[Command(Parent = typeof(AppRootCommand))]
public class TestCommand
{
}

[Command(Parent = typeof(TestCommand))]
public class DefaultValueCommand
{
	[Command(Parent = typeof(DefaultValueCommand))]
	public class ArgumentsCommand
	{
		[Command(Parent = typeof(ArgumentsCommand))]
		public class IntsCommand(IOutput output)
		{
			[Command(Parent = typeof(IntsCommand))]
			public class IntCommand(IOutput output) : ICommand
			{
				[Argument(DefaultValue = 1001)]
				public int Int { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(Int)}:{Int}");
				}
			}

			[Command(Parent = typeof(IntsCommand))]
			public class IntNullableCommand(IOutput output) : ICommand
			{
				[Argument(DefaultValue = 1002)]
				public int? IntNullable { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(IntNullable)}:{IntNullable}");
				}
			}
		}

		[Command(Parent = typeof(ArgumentsCommand))]
		public class StringsCommand(IOutput output) : ICommand
		{
			[Argument(DefaultValue = "The Default Value")]
			public string String { get; set; }

			public void Execute()
			{
				output.WriteLine($"{nameof(String)}:{String}");
			}
		}
	}

	[Command(Parent = typeof(DefaultValueCommand))]
	public class OptionsCommand(IOutput output) : ICommand
	{
		[Option(DefaultValue = 1001)]
		public int Int { get; set; }

		[Option(DefaultValue = 1002)]
		public int? IntNullable { get; set; }

		[Option(DefaultValue = "The Default Value")]
		public string String { get; set; }

		public void Execute()
		{
			output.WriteLine($"{nameof(Int)}:..........{Int}");
			output.WriteLine($"{nameof(IntNullable)}:..{IntNullable}");
			output.WriteLine($"{nameof(String)}:.......{String}");
		}
	}
}