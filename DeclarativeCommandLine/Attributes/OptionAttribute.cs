namespace DeclarativeCommandLine;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
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

	public string? Alias
	{
		get => null;
		set
		{
			if (value != null)
			{
				Aliases = new[] { value };
			}
		}
	}

	public string[]? Aliases { get; set; }

	public bool AllowMultipleArgumentsPerToken { get; set; }

	public string? ArgumentHelpName { get; set; }

	public ArgumentArity Arity { get; set; }

	public string[]? Completions { get; set; }

	public object? DefaultValue { get; set; }

	public string? Description { get; set; }

	public string[]? FromAmong { get; set; }

	public bool IsGlobal { get; set; }

	public bool IsHidden { get; set; }

	public bool IsRequired { get; set; }

	public bool LegalFileNamesOnly { get; set; }

	public bool LegalFilePathsOnly { get; set; }

	public string? Name { get; set; }
}