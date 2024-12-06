using DeclarativeCommandLine.Extensions;
using DeclarativeCommandLine.TestApp.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace DeclarativeCommandLine.TestApp;

public static class Program
{
	public static async Task<int> Main(string[] args)
	{
		return await new ServiceCollection()
			.AddDeclarativeCommandLine()
			// .AddCommand<AddCommand>()
			// .AddCommand<SubtractCommand>()
			// .AddCommand<DivideCommand>()
			.AddAllCommandsFromAssemblies<TestRootCommand>()

			.BuildServiceProvider()

			.RunCliAsync(args);

		//return await new DeclarativeCommandLineFactory().InvokeAsync(args);
	}
}