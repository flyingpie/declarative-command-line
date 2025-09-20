namespace DeclarativeCommandLine.Attributes;

public abstract class BaseCommandAttribute : Attribute
{
	public string[]? Aliases { get; set; }

	public string? Description { get; set; }

	public bool Hidden { get; set; }

	public string? Name { get; set; }

	public bool TreatUnmatchedTokensAsErrors { get; set; }
}