using DeclarativeCommandLine.UnitTest.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.IO;
using System.Threading.Tasks;
using VerifyMSTest;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests;

[TestClass]
[UsesVerify]
public partial class Test1
{
	[TestMethod]
	public Task VerifyCheck() => VerifyChecks.Run();

	[TestMethod]
	public async Task Help()
	{
		// Act
		var res = await RunAsync(["--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_Bare()
	{
		// Act
		var res = await RunAsync(["math", "add"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(1, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_Help()
	{
		// Act
		var res = await RunAsync(["math", "add", "--help"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_MissingOneOpt()
	{
		// Act
		var res = await RunAsync(["math", "add", "--value-a", "12"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(1, res.Code);
	}

	[TestMethod]
	public async Task MathAdd_Success()
	{
		// Act
		var res = await RunAsync(["math", "add", "--value-a", "12", "--value-b", "34"]);

		// Assert
		await Verify(res.Output);
		Assert.AreEqual(0, res.Code);
	}

	private static async Task<(int Code, string Output)> RunAsync(string[] args)
	{
		var outp = new StringWriter();

		var p = new ServiceCollection()
			.AddSingleton<IConsole>(new TextWriterConsole(outp))
			.AddTransient<AppRootCommand>()
			.AddTransient<MathCommand>()
			.AddTransient<AddCommand>()
			.BuildServiceProvider();

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