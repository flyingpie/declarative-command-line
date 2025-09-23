using DeclarativeCommandLine.UnitTest.Commands;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Threading.Tasks;
using static VerifyMSTest.Verifier;

namespace DeclarativeCommandLine.UnitTest.Tests;

[TestClass]
[UsesVerify]
public partial class Test1
{
	[TestMethod]
	public Task VerifyCheck() => VerifyChecks.Run();

	public static void Test()
	{
		var rootCmd = new RootCommand();
		var cmd = new Command("");
		var opt = new Option<int>("");
		var arg = new Argument<int>("");
		var dir = new Directive("");
	}

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

	[TestClass]
	[UsesVerify]
	public partial class CommandTest
	{
		[TestMethod]
		public async Task Aliases_Parent()
		{
			// Act
			var res = await RunAsync(["aliases"]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(1, res.Code);
		}

		[TestMethod]
		[DataRow("aliases-0")]
		public async Task Aliases_0(string cmd)
		{
			// Act
			var res = await RunAsync(["aliases", cmd]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		[TestMethod]
		[DataRow("aliases-1")]
		[DataRow("alias1")]
		public async Task Aliases_1(string cmd)
		{
			// Act
			var res = await RunAsync(["aliases", cmd]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}

		[TestMethod]
		[DataRow("aliases-2")]
		[DataRow("alias2-1")]
		[DataRow("alias2-2")]
		public async Task Aliases_2(string cmd)
		{
			// Act
			var res = await RunAsync(["aliases", cmd]);

			// Assert
			await Verify(res.Output);
			Assert.AreEqual(0, res.Code);
		}
	}

	private static async Task<(int Code, string Output)> RunAsync(string[] args)
	{
		var outp = new StringWriter();

		var p = new ServiceCollection()
			.AddSingleton<IOutput>(new TextWriterOutput(outp))
			.AddTransient<AliasesCommand>()
			.AddTransient<AliasesCommand.CommandWithAliases0>()
			.AddTransient<AliasesCommand.CommandWithAliases1>()
			.AddTransient<AliasesCommand.CommandWithAliases2>()
			.AddTransient<AppRootCommand>()
			.AddTransient<MathCommand>()
			.AddTransient<AddCommand>()
			.BuildServiceProvider();

		var b = new CommandBuilder();

		var cmd = b.Build(t => p.GetRequiredService(t));

		var conf = new ParserConfiguration();
		var res = CommandLineParser.Parse(cmd, args, conf);

		var invConf = new InvocationConfiguration() { EnableDefaultExceptionHandler = false, Error = outp, Output = outp, };

		var result = await res.InvokeAsync(invConf);

		await outp.FlushAsync();
		var str = outp.ToString();

		return (result, str);
	}
}