using System.Diagnostics.CodeAnalysis;

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

	[SuppressMessage(
		"Design",
		"CA1019:Define accessors for attribute arguments",
		Justification = "MvdO: I want to have both options available, either as a constructor argument, or a named one, whatever the user prefers.")]
	public string? Name { get; set; }

	public Type? Parent { get; set; }

	public bool TreatUnmatchedTokensAsErrors { get; set; }
}