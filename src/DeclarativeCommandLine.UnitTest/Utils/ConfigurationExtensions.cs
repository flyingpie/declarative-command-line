using System.Linq;

namespace DeclarativeCommandLine.UnitTest.Utils;

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
	}
}