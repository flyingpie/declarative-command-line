using DeclarativeCommandLine.Generated;
using DeclarativeCommandLine.UnitTest.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.UnitTest.Tests;

[TestClass]
public class Test1
{
	[TestMethod]
	public async Task METHOD()
	{
		var args = new[] { "--help" };

		var p = new ServiceCollection()
			.AddTransient<UnitTestRootCommand>()
			.AddTransient<MathCommand>()
			.AddTransient<AddCommand>()
			.BuildServiceProvider();

		var b = new CommandBuilder();

		var cmd = b.Build(t => p.GetRequiredService(t));

		var conf = new ParserConfiguration();
		var res = CommandLineParser.Parse(cmd, args, conf);

		var invConf = new InvocationConfiguration() { EnableDefaultExceptionHandler = false, };

		var result = await res.InvokeAsync(invConf);

		var dbg = 2;
	}
}