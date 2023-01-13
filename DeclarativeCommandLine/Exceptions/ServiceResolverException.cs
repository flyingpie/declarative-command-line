using System.Runtime.Serialization;

namespace DeclarativeCommandLine.Exceptions;

public class ServiceResolverException : Exception
{
	public ServiceResolverException()
	{
	}

	public ServiceResolverException(string message)
		: base(message)
	{
	}

	public ServiceResolverException(string message, Exception innerException)
		: base(message, innerException)
	{
	}

	protected ServiceResolverException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}
}