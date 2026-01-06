namespace DeclarativeCommandLine;

/// <summary>
/// Flags a command as async-executable.<br/>
/// Includes <seealso cref="ParseResult"/> in the <seealso cref="ExecuteAsync"/> method.
/// </summary>
public interface IAsyncCommandWithParseResult
{
	/// <summary>
	/// Called when the command is executed.
	/// </summary>
	Task ExecuteAsync(ParseResult parseResult, CancellationToken ct = default);
}
