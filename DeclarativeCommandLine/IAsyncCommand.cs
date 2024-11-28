namespace DeclarativeCommandLine;

public interface IAsyncCommand
{
	Task ExecuteAsync();
}