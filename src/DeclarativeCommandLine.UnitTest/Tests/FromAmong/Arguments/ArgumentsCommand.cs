namespace DeclarativeCommandLine.UnitTest.Tests.FromAmong.Arguments;

[Command(Parent = typeof(FromAmongCommand))]
public class ArgumentsCommand
{
	[Command(Parent = typeof(ArgumentsCommand))]
	public class IntsCommand
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
	public class StringsCommand
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