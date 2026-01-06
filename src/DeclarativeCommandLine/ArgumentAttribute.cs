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

	/// <inheritdoc cref="Argument.HelpName" />
	public string? HelpName { get; set; }

	/// <inheritdoc cref="Argument.Hidden" />
	public bool Hidden { get; set; }

	/// <inheritdoc cref="Argument.Name" />
	public string? Name { get; }
}
