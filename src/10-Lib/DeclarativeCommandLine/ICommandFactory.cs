namespace DeclarativeCommandLine;

public interface ICommandFactory
{
	object? CreateCommand(Type commandType);
}
