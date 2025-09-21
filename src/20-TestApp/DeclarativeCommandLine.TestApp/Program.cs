using DeclarativeCommandLine.Generated;
using DeclarativeCommandLine.UnitTest.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.TestApp;

// TODO: Partial CommandBuilder, with separate methods per command, for easier extension
public static class Program
{
	public static async Task<int> Main(string[] args)
	{
		var p = new ServiceCollection()
			.AddTransient<AddCommand>()
			.AddTransient<MathCommand>()
			.AddTransient<UnitTestRootCommand>()
			.BuildServiceProvider();

		return await new CommandBuilder()
			.Build(t => p.GetRequiredService(t))
			.Parse(args)
			.InvokeAsync()
			.ConfigureAwait(false);
	}

	// public static async Task ExecuteCmdAsync(IServiceProvider serviceProvider, string[] args)
	// {
	// 	var rootCmd = new RootCommand();
	// 	{
	// 		var mathCmd = new Command("math");
	// 		rootCmd.Add(mathCmd);
	//
	// 		var addCmd = new Command("add");
	// 		{
	// 			mathCmd.Add(addCmd);
	//
	// 			// Options
	// 			var opt1 = new Option<int>("--number-a");
	// 			{
	// 				addCmd.Add(opt1);
	// 				opt1.Required = true;
	// 			}
	//
	// 			var opt2 = new Option<int>("--number-b");
	// 			{
	// 				addCmd.Add(opt2);
	// 			}
	//
	// 			addCmd.SetAction(async (parseResult, ct) =>
	// 			{
	// 				Console.WriteLine("Hello Math.Add!");
	//
	// 				var addCmdInst = serviceProvider.GetRequiredService<AddCommand>();
	//
	// 				// Options
	// 				addCmdInst.NumberA = parseResult.GetValue(opt1);
	// 				addCmdInst.NumberB = parseResult.GetValue(opt2);
	//
	// 				// Execution
	// 				if (addCmdInst is IAsyncCommandWithParseResult cmd001)
	// 				{
	// 					await cmd001.ExecuteAsync(parseResult, ct).ConfigureAwait(false);
	// 				}
	//
	// 				if (addCmdInst is IAsyncCommand cmd002)
	// 				{
	// 					await cmd002.ExecuteAsync(ct).ConfigureAwait(false);
	// 				}
	//
	// 				if (addCmdInst is ICommand cmd003)
	// 				{
	// 					cmd003.Execute();
	// 				}
	// 			});
	// 		}
	//
	// 		var subtractCmd = new Command("subtract");
	// 		{
	// 			mathCmd.Add(subtractCmd);
	// 		}
	// 	}
	//
	// 	await rootCmd.Parse(args).InvokeAsync().ConfigureAwait(false);
	// }
}