namespace DeclarativeCommandLine;

public class DelegatingCommandFactory : ICommandFactory
{
	private readonly Func<Type, object> _factory;

	public DelegatingCommandFactory(Func<Type, object> factory)
	{
		_factory = factory;
	}

	public object? CreateCommand(Type commandType)
	{
		return _factory(commandType);
	}
}
