namespace DeclarativeCommandLine;

/// <summary>
/// Flags a command as async-executable.
/// </summary>
public interface IAsyncCommand
{
	/// <summary>
	/// Called when the command is executed.
	/// </summary>
	Task ExecuteAsync(CancellationToken ct = default);
}
