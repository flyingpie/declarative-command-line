using DeclarativeCommandLine.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace DeclarativeCommandLine.Extensions;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDeclarativeCommandLine(this IServiceCollection services)
	{
		ArgumentNullException.ThrowIfNull(services);

		return services
			.AddTransient<DefaultRootCommand>()
			.AddTransient<ICommandFactory>(p => new DelegatingCommandFactory(p.GetRequiredService))
			.AddTransient<DeclarativeCommandLineFactory>();
	}

	public static IServiceCollection AddAllCommandsFromAssemblies(
		this IServiceCollection services,
		params Assembly[] assemblies)
	{
		ArgumentNullException.ThrowIfNull(services);

		var cmds = assemblies
			.SelectMany(ass => ass.GetTypes())
			.Select(type => CommandDescriptor.TryCreate(type, out var cmdDescr) ? cmdDescr : null)
			.Where(cmdDescr => cmdDescr != null)
			.Select(cmdDescr => cmdDescr!);

		foreach (var cmd in cmds)
		{
			services.AddTransient(cmd.Type).AddTransient(p => cmd);
		}

		return services;
	}

	public static IServiceCollection AddAllCommandsFromAssemblies(
		this IServiceCollection services,
		Type type)
	{
		ArgumentNullException.ThrowIfNull(services);
		ArgumentNullException.ThrowIfNull(type);

		return services.AddAllCommandsFromAssemblies(type.Assembly);
	}

	public static IServiceCollection AddAllCommandsFromAssemblies<T>(
		this IServiceCollection services)
		where T : class
	{
		return services.AddAllCommandsFromAssemblies(typeof(T).Assembly);
	}

	public static IServiceCollection AddCommand<T>(this IServiceCollection services)
		where T : class
	{
		ArgumentNullException.ThrowIfNull(services);

		return services.AddCommand(typeof(T));
	}

	public static IServiceCollection AddCommand(this IServiceCollection services, Type type)
	{
		ArgumentNullException.ThrowIfNull(services);
		ArgumentNullException.ThrowIfNull(type);

		return !CommandDescriptor.TryCreate(type, out var descr)
			? throw
				new InvalidOperationException($"Type '{type}' does not appear to be a command.") // TODO: Move to descriptor and add "Create()" that throws exception.
			: services.AddTransient(type).AddTransient(p => descr);
	}

	public static async Task<int> RunCliAsync(this IServiceProvider serviceProvider, string[] args)
	{
		ArgumentNullException.ThrowIfNull(serviceProvider);

		return await serviceProvider
			.GetRequiredService<DeclarativeCommandLineFactory>()
			.InvokeAsync(args)
			.ConfigureAwait(false);
	}
}