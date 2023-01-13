using DeclarativeCommandLine.Exceptions;

namespace DeclarativeCommandLine.Utils;

public class NonConfiguredServiceProvider : IServiceProvider
{
	public object GetService(Type serviceType)
	{
		throw new ServiceResolverException(
			$"No service provider has been specified, which is required " +
			$"when requesting arbitrary services in the command handler. " +
			$"Please specifiy a service provider in the {nameof(DeclarativeCommandLineFactory)} constructor.");
	}
}