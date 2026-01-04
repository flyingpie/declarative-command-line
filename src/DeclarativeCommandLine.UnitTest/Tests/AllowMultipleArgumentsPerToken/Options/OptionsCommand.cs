namespace DeclarativeCommandLine.UnitTest.Tests.AllowMultipleArgumentsPerToken.Options;

[Command(Parent = typeof(AllowMultipleArgumentsPerTokenCommand))]
public class OptionsCommand(IOutput output) : ICommand
{
	[Option(AllowMultipleArgumentsPerToken = true)]
	public int Int { get; set; }

	[Option(AllowMultipleArgumentsPerToken = true)]
	public int? IntNullable { get; set; }

	[Option(AllowMultipleArgumentsPerToken = true)]
	public int[] IntArray { get; set; }

	[Option(AllowMultipleArgumentsPerToken = true)]
	public int[]? IntArrayNullable { get; set; }

	[Option(AllowMultipleArgumentsPerToken = true)]
	public string String { get; set; }

	[Option(AllowMultipleArgumentsPerToken = true)]
	public string[] StringArray { get; set; }

	public void Execute()
	{
		output.WriteLine($"{nameof(Int)}:...............{string.Join(", ", Int)}");
		output.WriteLine($"{nameof(IntArray)}:..........{string.Join(", ", IntArray)}");
		output.WriteLine($"{nameof(IntArrayNullable)}:..{string.Join(", ", IntArrayNullable)}");
		output.WriteLine($"{nameof(IntNullable)}:.......{string.Join(", ", IntNullable)}");

		output.WriteLine($"{nameof(String)}:............{String}");
		output.WriteLine($"{nameof(StringArray)}:.......{string.Join(", ", StringArray)}");
	}
}
