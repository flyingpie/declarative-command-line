using DeclarativeCommandLine.Generated;
using DeclarativeCommandLine.TestApp.Commands;
using DeclarativeCommandLine.TestApp.Commands.Math;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.TestApp;

public static class Program
{
	public static async Task<int> Main(string[] args)
	{
		Console.WriteLine("Hello world!");

		var sc = new ServiceCollection()
			.AddTransient<AddCommand>()
			.AddTransient<MathCommand>()
			.AddTransient<TestRootCommand>()
			;

		// return await new ServiceCollection()
		// 	.AddDeclarativeCommandLine()
		// 	.AddAllCommandsFromAssemblies<TestRootCommand>()
		// 	.BuildServiceProvider()
		// 	.RunCliAsync(args)
		// 	.ConfigureAwait(false);

		// await ExecuteCmdAsync(sc.BuildServiceProvider(), args).ConfigureAwait(false);
		await new CommandBuilder().Build(sc.BuildServiceProvider()).Parse(args).InvokeAsync().ConfigureAwait(false);

		return 0;
	}

	public static async Task ExecuteCmdAsync(IServiceProvider serviceProvider, string[] args)
	{
		var rootCmd = new RootCommand();
		{
			var mathCmd = new Command("math");
			rootCmd.Add(mathCmd);

			var addCmd = new Command("add");
			{
				mathCmd.Add(addCmd);

				// Options
				var opt1 = new Option<int>("--number-a");
				{
					addCmd.Add(opt1);
					opt1.Required = true;
				}

				var opt2 = new Option<int>("--number-b");
				{
					addCmd.Add(opt2);
				}

				addCmd.SetAction(async (parseResult, ct) =>
				{
					Console.WriteLine("Hello Math.Add!");

					var addCmdInst = serviceProvider.GetRequiredService<AddCommand>();

					// Options
					addCmdInst.NumberA = parseResult.GetValue(opt1);
					addCmdInst.NumberB = parseResult.GetValue(opt2);

					// Execution
					if (addCmdInst is IAsyncCommandWithParseResult cmd001)
					{
						await cmd001.ExecuteAsync(parseResult, ct).ConfigureAwait(false);
					}

					if (addCmdInst is IAsyncCommand cmd002)
					{
						await cmd002.ExecuteAsync(ct).ConfigureAwait(false);
					}

					if (addCmdInst is ICommand cmd003)
					{
						cmd003.Execute();
					}
				});
			}

			var subtractCmd = new Command("subtract");
			{
				mathCmd.Add(subtractCmd);
			}
		}

		await rootCmd.Parse(args).InvokeAsync().ConfigureAwait(false);
	}
}