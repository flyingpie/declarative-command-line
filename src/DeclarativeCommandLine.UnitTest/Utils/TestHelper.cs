using System.CommandLine;
using System.CommandLine.Parsing;

namespace DeclarativeCommandLine.UnitTest.Utils;

public static class TestHelper
{
	public static async Task<(int Code, string Output)> RunAsync(string[] args)
	{
		var outp = new StringWriter();

		var p = new ServiceCollection().AddSingleton<IOutput>(new TextWriterOutput(outp)).AddCommands().BuildServiceProvider();

		var b = new CommandBuilder();

		var cmd = b.Build(t => p.GetRequiredService(t));

		var conf = new ParserConfiguration();
		var res = CommandLineParser.Parse(cmd, args, conf);

		var invConf = new InvocationConfiguration()
		{
			EnableDefaultExceptionHandler = false,
			Error = outp,
			Output = outp,
		};

		var result = await res.InvokeAsync(invConf);

		await outp.FlushAsync();
		var str = outp.ToString();

		return (result, str);
	}
}
