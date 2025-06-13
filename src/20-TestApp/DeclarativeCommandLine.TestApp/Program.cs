using System.CommandLine;
using System.CommandLine.Builder;
using System.Threading.Tasks;
using DeclarativeCommandLine.Extensions;
using DeclarativeCommandLine.TestApp.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace DeclarativeCommandLine.TestApp;

public static class Program
{
	public static async Task<int> Main(string[] args)
	{
		try
		{
			return await new ServiceCollection()
				.AddDeclarativeCommandLine()
				.AddAllCommandsFromAssemblies<TestRootCommand>()
				.BuildServiceProvider()
				.RunCliAsync(args)
				.ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"EXCEPTION: {ex.Message}");
			return -1;
		}
	}
}
