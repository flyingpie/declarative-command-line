namespace DeclarativeCommandLine.TestApp.Commands.Errors;

[Command<ErrorsCommand>]
public class ExceptionCommand : ICommand
{
	public void Execute()
	{
		throw new Exception("A generic exception!");
	}
}
