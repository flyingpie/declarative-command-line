namespace DeclarativeCommandLine;

[AttributeUsage(AttributeTargets.Property)]
public sealed class ArgumentAttribute : Attribute
{
	public string[]? FromAmong { get; set; }

	public object? DefaultValue { get; set; }

	public string? Description { get; set; }

	public string? HelpName { get; set; }

	public bool Hidden { get; set; }

	public string? Name { get; set; }
}