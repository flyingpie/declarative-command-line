namespace DeclarativeCommandLine.UnitTest.Utils;

public class TextWriterOutput(TextWriter writer) : IOutput
{
	private readonly TextWriter _writer = writer ?? throw new ArgumentNullException(nameof(writer));

	public void WriteLine(string line)
	{
		_writer.WriteLine(line);
	}
}