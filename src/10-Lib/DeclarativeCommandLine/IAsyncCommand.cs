using System.Threading;

namespace DeclarativeCommandLine;

public interface IAsyncCommand
{
	Task ExecuteAsync(CancellationToken ct = default);
}

public interface IAsyncCommandWithParseResult
{
	Task ExecuteAsync(ParseResult parseResult, CancellationToken ct = default);
}