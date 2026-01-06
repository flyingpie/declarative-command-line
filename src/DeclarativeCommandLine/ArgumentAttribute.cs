namespace DeclarativeCommandLine;

/// <summary>
/// An argument is an unnamed parameter that can be passed to a command. The following example shows an argument for the build command.
/// <code>
/// dotnet build myapp.csproj
///              ^----------^
/// </code>
/// When you configure an argument, you specify the argument name (it's not used for parsing, but it can be used for getting parsed
/// values by name or displaying help).
/// </summary>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/standard/commandline/syntax#arguments"/>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ArgumentAttribute : Attribute
{
	public ArgumentAttribute() { }

	public ArgumentAttribute(string name)
	{
		Name = name;
	}

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
	/// The allowed values for this argument.
	/// </summary>
	public string[]? FromAmong { get; set; }

	/// <summary>
	/// How the argument should be named in the --help text.<br/>
	/// Note that this is only used for the --help text, not for parsing.
	/// </summary>
	public string? HelpName { get; set; }

	/// <summary>
	/// Whether to hide the argument from the --help text.
	/// </summary>
	public bool Hidden { get; set; }

	/// <summary>
	/// The name of the argument. Note that this is only used to refer to the argument during parsing,
	/// as arguments are not named when used through the cli.
	/// </summary>
	[SuppressMessage(
		"Design",
		"CA1019:Define accessors for attribute arguments",
		Justification = "MvdO: I want to have both options available, either as a constructor argument, or a named one, whatever the user prefers."
	)]
	public string? Name { get; }
}
