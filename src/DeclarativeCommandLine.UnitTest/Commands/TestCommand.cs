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

[Command(Parent = typeof(TestCommand))]
public class FromAmongCommand
{
	[Command(Parent = typeof(FromAmongCommand))]
	public class ArgumentsCommand
	{
		[Command(Parent = typeof(ArgumentsCommand))]
		public class IntsCommand(IOutput output)
		{
			[Command(Parent = typeof(IntsCommand))]
			public class IntArgCommand(IOutput output) : ICommand
			{
				[Argument]
				public int? IntArg { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(IntArg)}:{IntArg}");
				}
			}

			[Command(Parent = typeof(IntsCommand))]
			public class IntArgNullCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = null)]
				public int? IntArgNull { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(IntArgNull)}:{IntArgNull}");
				}
			}

			[Command(Parent = typeof(IntsCommand))]
			public class IntArgEmptyArrayCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = [])]
				public int? IntArgEmptyArray { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(IntArgEmptyArray)}:{IntArgEmptyArray}");
				}
			}

			[Command(Parent = typeof(IntsCommand))]
			public class IntArg1ValueCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = ["1"])]
				public int? IntArg1Value { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(IntArg1Value)}:{IntArg1Value}");
				}
			}

			[Command(Parent = typeof(IntsCommand))]
			public class IntArg2ValuesCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = ["1", "2"])]
				public int? IntArg2Values { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(IntArg2Values)}:{IntArg2Values}");
				}
			}

			[Command(Parent = typeof(IntsCommand))]
			public class IntArg3ValuesCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = ["1", "2", "3"])]
				public int? IntArg3Values { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(IntArg3Values)}:{IntArg3Values}");
				}
			}
		}

		[Command(Parent = typeof(ArgumentsCommand))]
		public class StringsCommand(IOutput output)
		{
			[Command(Parent = typeof(StringsCommand))]
			public class StringArgCommand(IOutput output) : ICommand
			{
				[Argument]
				public string? StringArg { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(StringArg)}:{StringArg}");
				}
			}

			[Command(Parent = typeof(StringsCommand))]
			public class StringArgNullCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = null)]
				public string? StringArgNull { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(StringArgNull)}:{StringArgNull}");
				}
			}

			[Command(Parent = typeof(StringsCommand))]
			public class StringArgEmptyArrayCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = [])]
				public string? StringArgEmptyArray { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(StringArgEmptyArray)}:{StringArgEmptyArray}");
				}
			}

			[Command(Parent = typeof(StringsCommand))]
			public class StringArg1ValueCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = ["val-1"])]
				public string? StringArg1Value { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(StringArg1Value)}:{StringArg1Value}");
				}
			}

			[Command(Parent = typeof(StringsCommand))]
			public class StringArg2ValuesCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = ["val-1", "val-2"])]
				public string? StringArg2Values { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(StringArg2Values)}:{StringArg2Values}");
				}
			}

			[Command(Parent = typeof(StringsCommand))]
			public class StringArg3ValuesCommand(IOutput output) : ICommand
			{
				[Argument(FromAmong = ["val-1", "val-2", "val-3"])]
				public string? StringArg3Values { get; set; }

				public void Execute()
				{
					output.WriteLine($"{nameof(StringArg3Values)}:{StringArg3Values}");
				}
			}
		}
	}

	[Command(Parent = typeof(FromAmongCommand))]
	public class OptionsCommand
	{
		[Command(Parent = typeof(OptionsCommand))]
		public class IntsCommand(IOutput output) : ICommand
		{
			[Option]
			public int? IntOpt { get; set; }

			[Option(FromAmong = null)]
			public int? IntOptNull { get; set; }

			[Option(FromAmong = [])]
			public int? IntOptEmptyArray { get; set; }

			[Option(FromAmong = ["1"])]
			public int? IntOpt1Value { get; set; }

			[Option(FromAmong = ["1", "2"])]
			public int? IntOpt2Values { get; set; }

			[Option(FromAmong = ["1", "2", "3"])]
			public int? IntOpt3Values { get; set; }

			public void Execute()
			{
				output.WriteLine($"{nameof(IntOpt)}:.............{IntOpt}");
				output.WriteLine($"{nameof(IntOptNull)}:.........{IntOptNull}");
				output.WriteLine($"{nameof(IntOptEmptyArray)}:...{IntOptEmptyArray}");
				output.WriteLine($"{nameof(IntOpt1Value)}:.......{IntOpt1Value}");
				output.WriteLine($"{nameof(IntOpt2Values)}:......{IntOpt2Values}");
				output.WriteLine($"{nameof(IntOpt3Values)}:......{IntOpt3Values}");
			}
		}

		[Command(Parent = typeof(OptionsCommand))]
		public class StringsCommand(IOutput output) : ICommand
		{
			[Option]
			public string? StringOpt { get; set; }

			[Option(FromAmong = null)]
			public string? StringOptNull { get; set; }

			[Option(FromAmong = [])]
			public string? StringOptEmptyArray { get; set; }

			[Option(FromAmong = ["val-1"])]
			public string? StringOpt1Value { get; set; }

			[Option(FromAmong = ["val-1", "val-2"])]
			public string? StringOpt2Values { get; set; }

			[Option(FromAmong = ["val-1", "val-2", "val-3"])]
			public string? StringOpt3Values { get; set; }

			public void Execute()
			{
				output.WriteLine($"{nameof(StringOpt)}:.............{StringOpt}");
				output.WriteLine($"{nameof(StringOptNull)}:.........{StringOptNull}");
				output.WriteLine($"{nameof(StringOptEmptyArray)}:...{StringOptEmptyArray}");
				output.WriteLine($"{nameof(StringOpt1Value)}:.......{StringOpt1Value}");
				output.WriteLine($"{nameof(StringOpt2Values)}:......{StringOpt2Values}");
				output.WriteLine($"{nameof(StringOpt3Values)}:......{StringOpt3Values}");
			}
		}
	}
}