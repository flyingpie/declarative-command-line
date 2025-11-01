using DeclarativeCommandLine.UnitTest.Commands;
using System.Linq;

namespace DeclarativeCommandLine.UnitTest;

public static class ConfigurationExtensions
{
	public static IServiceCollection AddCommands(this IServiceCollection services)
	{
		ArgumentNullException.ThrowIfNull(services);

		var types = typeof(AppRootCommand)
			.Assembly
			.GetTypes()
			.Where(t => !t.IsAbstract)
			.ToList();

		foreach (var t in types)
		{
			services.AddTransient(t);
		}

		return services;

		// return services
		// 	.AddTransient<AppRootCommand>()
		// 	.AddTransient<AliasesCommand>()
		// 	.AddTransient<AliasesCommand.CommandWithAliases0>()
		// 	.AddTransient<AliasesCommand.CommandWithAliases1>()
		// 	.AddTransient<AliasesCommand.CommandWithAliases2>()
		// 	.AddTransient<InheritanceCommand>()
		// 	.AddTransient<InheritanceCommand.CommandBaseClass>()
		// 	.AddTransient<InheritanceCommand.CommandChildClass>()
		// 	.AddTransient<InheritanceCommand.CommandGrandChildClass>()
		// 	.AddTransient<TestCommand>()
		// 	.AddTransient<FromAmongCommand>()
		// 	.AddTransient<FromAmongCommand.ArgumentsCommand>()
		// 	.AddTransient<FromAmongCommand.ArgumentsCommand.IntsCommand>()
		// 	.AddTransient<FromAmongCommand.ArgumentsCommand.StringsCommand>()
		// 	.AddTransient<FromAmongCommand.OptionsCommand>()
		// 	.AddTransient<FromAmongCommand.OptionsCommand.IntsCommand>()
		// 	.AddTransient<FromAmongCommand.OptionsCommand.StringsCommand>()
		// 	.AddTransient<MathCommand>()
		// 	.AddTransient<MathCommand.AddCommand>()
		// 	.AddTransient<MathCommand.SubtractCommand>()
		// 	;
	}
}