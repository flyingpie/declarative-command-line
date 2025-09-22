namespace DeclarativeCommandLine.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ArgumentAttribute : Attribute
{
	public ArgumentAttribute() { }

	public ArgumentAttribute(string name)
	{
		Name = name;

		// Validators
	}

	public ArgumentArity Arity { get; set; }

	public string[]? Completions { get; set; }

	public object? DefaultValue { get; set; }

	public string? Description { get; set; }

	public string[]? FromAmong { get; set; }

	public string? HelpName { get; set; }

	public bool IsHidden { get; set; }

	public bool LegalFileNamesOnly { get; set; }

	public bool LegalFilePathsOnly { get; set; }

	public string? Name { get; set; }
}
