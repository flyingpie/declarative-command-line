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
		public class CharsCommand
		{
			[Argument]
			public char? CharArg { get; set; }

			[Argument(FromAmong = null)]
			public char? CharArgNull { get; set; }

			[Argument(FromAmong = [])]
			public char? CharArgEmptyArray { get; set; }

			[Argument(FromAmong = ["true"])]
			public char? CharArg1Value { get; set; }

			[Argument(FromAmong = ["val-1", "val-2"])]
			public char? CharArg2Values { get; set; }

			[Argument(FromAmong = ["val-1", "val-2", "val-3"])]
			public char? CharArg3Values { get; set; }
		}

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
		public class CharsCommand
		{
			[Option]
			public char? CharOpt { get; set; }

			[Option(FromAmong = null)]
			public char? CharOptNull { get; set; }

			[Option(FromAmong = [])]
			public char? CharOptEmptyArray { get; set; }

			[Option(FromAmong = ["a"])]
			public char? CharOpt1Value { get; set; }

			[Option(FromAmong = ["a", "b"])]
			public char? CharOpt2Values { get; set; }

			[Option(FromAmong = ["a", "b", "c"])]
			public char? CharOpt3Values { get; set; }
		}

		[Command(Parent = typeof(OptionsCommand))]
		public class IntsCommand
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