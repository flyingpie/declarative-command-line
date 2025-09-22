namespace DeclarativeCommandLine.TestApp;

public static class Program
{
	public static async Task<int> Main(string[] args)
	{
		return await new DeclarativeCommandLineFactory().InvokeAsync(args);
	}
}