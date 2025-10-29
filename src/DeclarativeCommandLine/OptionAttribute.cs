namespace DeclarativeCommandLine;

[AttributeUsage(AttributeTargets.Property)]
public sealed class OptionAttribute : Attribute
{
	public OptionAttribute()
	{
	}

	public OptionAttribute(string name)
	{
		Name = name;
	}

	public OptionAttribute(string name, params string[] aliases)
	{
		Name = name;
		Aliases = aliases;
	}

	public string[]? Aliases { get; }

	public string[]? FromAmong { get; set; }

	public bool AllowMultipleArgumentsPerToken { get; set; }

	public string? ArgumentHelpName { get; set; }

	public object? DefaultValue { get; set; }

	public string? Description { get; set; }

	public bool Hidden { get; set; }

	public string? Name { get; }

	public bool Recursive { get; set; }

	public bool Required { get; set; }
}