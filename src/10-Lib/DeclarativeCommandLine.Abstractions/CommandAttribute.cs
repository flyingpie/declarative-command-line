namespace DeclarativeCommandLine;

[AttributeUsage(AttributeTargets.Class)]
[SuppressMessage("Performance", "CA1813:Avoid unsealed attributes", Justification = "MvdO: We have a generic version that inherits from this.")]
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

	// public bool Root { get; set; }
}