namespace DeclarativeCommandLine;

public interface IAsyncCommandWithParseResult
{
	Task ExecuteAsync(ParseResult parseResult, CancellationToken ct = default);
}