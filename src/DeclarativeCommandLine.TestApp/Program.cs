using DeclarativeCommandLine.UnitTest;
using DeclarativeCommandLine.UnitTest.Commands;
using DeclarativeCommandLine.UnitTest.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.TestApp;

internal static class Program
{
	public static Task<int> Main(string[] args)
	{
		var p = new ServiceCollection()
			.AddTransient<IOutput, ConsoleOutput>()
			.AddCommands()
			.BuildServiceProvider();

		return new CommandBuilder()
			.Build(t => p.GetRequiredService(t))
			.Parse(args)
			.InvokeAsync();
	}
}