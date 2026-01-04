namespace DeclarativeCommandLine.UnitTest.Tests.Aliases.Options;

/// <summary>
/// Alias test at the "option" level.
/// </summary>
[Command(Parent = typeof(AliasesCommand))]
public class OptionsCommand(IOutput output) : ICommand
{
	/// <summary>
	/// Option with no aliases.
	/// </summary>
	[Option]
	public string? Aliases0 { get; set; }

	/// <summary>
	/// Option with 1 alias.
	/// </summary>
	[Option(Aliases = ["--alias-a-1"])]
	public string? Aliases1 { get; set; }

	/// <summary>
	/// Option with 2 aliases.
	/// </summary>
	[Option(Aliases = ["--alias-b-1", "--alias-b-2"])]
	public string? Aliases2 { get; set; }

	public void Execute()
	{
		output.WriteLine($"Aliases0: {Aliases0}");
		output.WriteLine($"Aliases1: {Aliases1}");
		output.WriteLine($"Aliases2: {Aliases2}");
	}
}