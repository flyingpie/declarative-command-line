namespace DeclarativeCommandLine.UnitTest.Tests.FromAmong.Options;

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