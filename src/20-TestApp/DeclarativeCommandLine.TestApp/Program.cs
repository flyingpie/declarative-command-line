using DeclarativeCommandLine.Extensions;
using DeclarativeCommandLine.TestApp.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.TestApp;

public static class Program
{
	public static async Task<int> Main(string[] args) =>
		await new ServiceCollection()
			.AddDeclarativeCommandLine()
			.AddAllCommandsFromAssemblies<TestRootCommand>()
			.BuildServiceProvider()
			.RunCliAsync(args)
			.ConfigureAwait(false);
}