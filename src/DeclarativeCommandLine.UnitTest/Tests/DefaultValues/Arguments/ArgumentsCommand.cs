namespace DeclarativeCommandLine.UnitTest.Tests.DefaultValues.Arguments;

[Command(Parent = typeof(DefaultValuesCommand))]
public class ArgumentsCommand
{
	[Command(Parent = typeof(ArgumentsCommand))]
	public class IntsCommand
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
		public string? String { get; set; }

		public void Execute()
		{
			output.WriteLine($"{nameof(String)}:{String}");
		}
	}
}
