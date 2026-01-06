using System.Diagnostics.CodeAnalysis;

namespace DeclarativeCommandLine;

/// <summary>
/// An option is a named parameter that can be passed to a command. POSIX CLIs typically prefix the option name with two hyphens (--).
/// The following example shows two options:
/// <code>
/// dotnet tool update dotnet-suggest --verbosity quiet --global
///                                   ^---------^       ^------^
/// </code>
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax#options"/>
[AttributeUsage(AttributeTargets.Property)]
public sealed class OptionAttribute : Attribute
{
	public OptionAttribute() { }

	public OptionAttribute(string name)
	{
		Name = name;
	}

	public OptionAttribute(string name, params string[] aliases)
	{
		Name = name;
		Aliases = aliases;
	}

	/// <summary>
	/// In both POSIX and Windows, it's common for some commands and options to have aliases. These are usually short forms
	/// that are easier to type. Aliases can also be used for other purposes, such as to simulate case-insensitivity and to
	/// support alternate spellings of a word.
	/// POSIX short forms typically have a single leading hyphen followed by a single character. The following commands are equivalent:
	/// <code>
	/// dotnet build --verbosity quiet
	/// dotnet build -v quiet
	/// </code>
	/// </summary>
	/// <seealso href="https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax#aliases"/>
	[SuppressMessage(
		"Design",
		"CA1019:Define accessors for attribute arguments",
		Justification = "MvdO: I want to have both options available, either as a constructor argument, or a named one, whatever the user prefers."
	)]
	public string[]? Aliases { get; set; }

	/// <summary>
	/// By default, when you call a command, you can repeat an option name to specify multiple arguments for an option that has
	/// maximum arity greater than one.
	/// <code>
	/// myapp --items one --items two --items three
	/// </code>
	/// To allow multiple arguments without repeating the option name, set AllowMultipleArgumentsPerToken to true.
	/// This setting lets you enter the following command line.
	/// <code>
	/// myapp --items one two three
	/// </code>
	/// </summary>
	/// <seealso href="https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax#multiple-arguments"/>
	public bool AllowMultipleArgumentsPerToken { get; set; }

	/// <summary>
	/// Both arguments and options can have default values that apply if no argument is explicitly provided.
	/// For example, many options are implicitly Boolean parameters with a default of true when the option name is in the command line.
	/// The following command-line examples are equivalent:
	/// <code>
	/// dotnet tool update dotnet-suggest --global
	///                                   ^------^
	/// dotnet tool update dotnet-suggest --global true
	///                                   ^-----------^
	/// </code>
	/// </summary>
	/// <seealso href="https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax#default-values"/>
	public object? DefaultValue { get; set; }

	/// <summary>
	/// A descriptive text that's printed when using the "--help" option, or when parsing fails.
	/// </summary>
	public string? Description { get; set; }

	/// <summary>
	/// The allowed values for this option.
	/// </summary>
	public string[]? FromAmong { get; set; }

	/// <inheritdoc cref="Option.HelpName" />
	public string? HelpName { get; set; }

	/// <inheritdoc cref="Option.Hidden" />
	public bool Hidden { get; set; }

	/// <inheritdoc cref="Option.Name" />
	public string? Name { get; }

	/// <inheritdoc cref="Option.Recursive" />
	public bool Recursive { get; set; }

	/// <inheritdoc cref="Option.Required" />
	public bool Required { get; set; }
}
