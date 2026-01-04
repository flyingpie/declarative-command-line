namespace DeclarativeCommandLine;

public interface IAsyncCommand
{
	Task ExecuteAsync(CancellationToken ct = default);
}
