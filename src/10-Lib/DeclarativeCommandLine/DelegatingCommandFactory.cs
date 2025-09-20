namespace DeclarativeCommandLine;

public class DelegatingCommandFactory(Func<Type, object> factory) : ICommandFactory
{
	public object? CreateCommand(Type commandType)
	{
		ArgumentNullException.ThrowIfNull(commandType);

		return factory(commandType);
	}
}