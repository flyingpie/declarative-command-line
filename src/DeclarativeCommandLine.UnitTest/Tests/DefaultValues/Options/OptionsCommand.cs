namespace DeclarativeCommandLine.UnitTest.Tests.DefaultValues.Options;

[Command(Parent = typeof(DefaultValuesCommand))]
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