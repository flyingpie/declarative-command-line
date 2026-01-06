using System.Diagnostics.CodeAnalysis;

namespace DeclarativeCommandLine;

/// <summary>
/// A command in command-line input is a token that specifies an action or defines a group of related actions. For example.
/// <ul>
/// <li>In dotnet run, run is a command that specifies an action.</li>
/// <li>In dotnet tool install, install is a command that specifies an action, and tool is a command that specifies a group of related commands.
///   There are other tool-related commands, such as tool uninstall, tool list, and tool update.</li>
/// </ul>
/// An application needs exactly 1 "root command", which is a command without a parent command. All executions are done from this one root command.<br/>
/// Further "sub commands" can then be connected to this root command, by setting <see cref="CommandAttribute.Parent"/> to the
/// application root command, or another sub command.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class CommandAttribute : Attribute
{
	public CommandAttribute() { }

	public CommandAttribute(string name)
	{
		Name = name;
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
	public string[]? Aliases { get; set; }

	/// <summary>
	/// A descriptive text that's printed when using the "--help" option, or when parsing fails.
	/// </summary>
	public string? Description { get; set; }

	/// <inheritdoc cref="Command.Hidden" />
	public bool Hidden { get; set; }

	/// <inheritdoc cref="Command.Name" />
	[SuppressMessage(
		"Design",
		"CA1019:Define accessors for attribute arguments",
		Justification = "MvdO: I want to have both options available, either as a constructor argument, or a named one, whatever the user prefers."
	)]
	public string? Name { get; set; }

	/// <summary>
	/// The command's "parent command", which can be either the application root command, or another sub command, to group multiple commands together.<br/>
	/// An app should have exactly 1 "root command"; a command without a parent.
	/// </summary>
	public Type? Parent { get; set; }

	/// <inheritdoc cref="Command.TreatUnmatchedTokensAsErrors" />
	public bool TreatUnmatchedTokensAsErrors { get; set; }
}
