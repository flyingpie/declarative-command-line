using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Commands;

[Command(Parent = typeof(AppRootCommand))]
public class TestCommand
{
}

[Command(Parent = typeof(TestCommand))]
public class FromAmongCommand
{
	[Command(Parent = typeof(FromAmongCommand))]
	public class ArgumentsCommand
	{
		[Command(Parent = typeof(ArgumentsCommand))]
		public class IntsCommand
		{
			[Argument]
			public int? IntArg { get; set; }

			[Argument(FromAmong = null)]
			public int? IntArgNull { get; set; }

			[Argument(FromAmong = [])]
			public int? IntArgEmptyArray { get; set; }

			[Argument(FromAmong = ["1"])]
			public int? IntArg1Value { get; set; }

			[Argument(FromAmong = ["1", "2"])]
			public int? IntArg2Values { get; set; }

			[Argument(FromAmong = ["1", "2", "3"])]
			public int? IntArg3Values { get; set; }
		}

		[Command(Parent = typeof(ArgumentsCommand))]
		public class StringsCommand
		{
			[Argument]
			public string? StringArg { get; set; }

			[Argument(FromAmong = null)]
			public string? StringArgNull { get; set; }

			[Argument(FromAmong = [])]
			public string? StringArgEmptyArray { get; set; }

			[Argument(FromAmong = ["val-1"])]
			public string? StringArg1Value { get; set; }

			[Argument(FromAmong = ["val-1", "val-2"])]
			public string? StringArg2Values { get; set; }

			[Argument(FromAmong = ["val-1", "val-2", "val-3"])]
			public string? StringArg3Values { get; set; }
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