using DeclarativeCommandLine.UnitTest.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DeclarativeCommandLine.TestApp;

public static class Program
{
	public static Task<int> Main(string[] args)
	{
		var p = new ServiceCollection()
			.AddTransient<AddCommand>()
			.AddTransient<MathCommand>()
			.AddTransient<AppRootCommand>()
			.BuildServiceProvider();

		return new CommandBuilder()
			.Build(t => p.GetRequiredService(t))
			.Parse(args)
			.InvokeAsync();
	}
}