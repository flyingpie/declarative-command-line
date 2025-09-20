namespace DeclarativeCommandLine;

[AttributeUsage(AttributeTargets.Class)]
public sealed class CommandAttribute : Attribute
{
	public CommandAttribute()
	{
	}

	public CommandAttribute(string name)
	{
		Name = name;
	}

	public string[]? Aliases { get; set; }

	public string? Description { get; set; }

	public bool Hidden { get; set; }

	public string? Name { get; set; }

	public Type? Parent { get; set; }

	public bool TreatUnmatchedTokensAsErrors { get; set; }
}