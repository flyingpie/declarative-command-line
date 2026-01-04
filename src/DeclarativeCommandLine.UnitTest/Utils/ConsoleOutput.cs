namespace DeclarativeCommandLine.UnitTest.Utils;

public class ConsoleOutput : IOutput
{
	public void WriteLine(string line)
	{
		Console.WriteLine(line);
	}
}
