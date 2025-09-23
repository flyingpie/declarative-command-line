using DeclarativeCommandLine.UnitTest.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.TestApp;

public static class Program
{
	public static Task<int> Main(string[] args)
	{
		var p = new ServiceCollection()
			.AddTransient<IOutput, ConsoleOutput>()
			.AddTransient<AddCommand>()
			.AddTransient<MathCommand>()
			.AddTransient<AppRootCommand>()
			.AddTransient<AliasesCommand>()
			.AddTransient<AliasesCommand.CommandWithAliases0>()
			.AddTransient<AliasesCommand.CommandWithAliases1>()
			.AddTransient<AliasesCommand.CommandWithAliases2>()
			.BuildServiceProvider();

		return new CommandBuilder()
			.Build(t => p.GetRequiredService(t))
			.Parse(args)
			.InvokeAsync();
	}
}