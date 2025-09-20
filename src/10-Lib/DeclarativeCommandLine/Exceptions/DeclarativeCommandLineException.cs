namespace DeclarativeCommandLine.Exceptions;

public sealed class DeclarativeCommandLineException : Exception
{
	public DeclarativeCommandLineException()
	{
	}

	public DeclarativeCommandLineException(string? message)
		: base(message)
	{
	}

	public DeclarativeCommandLineException(string? message, Exception? innerException)
		: base(message, innerException)
	{
	}
}