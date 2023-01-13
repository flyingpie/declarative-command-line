namespace DeclarativeCommandLine;

public interface ICommandFactory
{
	object? CreateCommand(Type commandType);
}

public class ActivatorCommandFactory : ICommandFactory
{
	public object? CreateCommand(Type commandType)
	{
		return Activator.CreateInstance(commandType);
	}
}