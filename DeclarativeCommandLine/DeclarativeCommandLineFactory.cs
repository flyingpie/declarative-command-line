using DeclarativeCommandLine.Utils;

namespace DeclarativeCommandLine;

public class DeclarativeCommandLineFactory
{
	private readonly ICommandFactory _commandFactory = new ActivatorCommandFactory();
	private readonly IServiceProvider _serviceProvider;

	public DeclarativeCommandLineFactory()
		: this(new NonConfiguredServiceProvider())
	{
	}

	public DeclarativeCommandLineFactory(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
	}

	public RootCommand BuildRootCommand(params Assembly[] assemblies)
	{
		if (assemblies == null || assemblies.Length == 0)
		{
			throw new ArgumentException("At least 1 assemblies is required.", nameof(assemblies));
		}

		return BuildRootCommand(assemblies.SelectMany(ass => ass.GetTypes()).ToArray());
	}

	public RootCommand BuildRootCommand(params Type[] types)
	{
		if (types == null || types.Length == 0)
		{
			throw new ArgumentException("At least 1 type is required.", nameof(types));
		}

		var cmdTypes = types
			.Select(type => CommandDescriptor.TryCreate(type, out var cmdDescr) ? cmdDescr : null)
			.Where(cmdDescr => cmdDescr != null)
			.Select(cmdDescr => cmdDescr!)
			.ToList();

		var roots = cmdTypes.Where(c => c.IsRoot).ToList();
		var root = roots.FirstOrDefault();

		if (root == null)
		{
			//throw new InvalidOperationException($"No root command found. Please define a command and decorate it with the '{nameof(RootCommand)}' attribute.");

			root = new CommandDescriptor(
				typeof(DefaultRootCommand),
				new RootCommandAttribute()
				{
				});
		}

		if (roots.Count > 1)
		{
			throw new InvalidOperationException($"Multiple root commands found: {string.Join(", ", roots.Select(c => c.Type))}.");
		}

		// Relate non-root commands without a parent command to the root command.
		foreach (var cmdType in cmdTypes)
		{
			if (!cmdType.IsRoot && cmdType.ParentType == null)
			{
				cmdType.ParentType = root.Type;
			}
		}

		var rootCmd = BuildCommandTree(cmdTypes, root);

		if (rootCmd is not RootCommand rootc)
		{
			throw new InvalidOperationException("This shouldn't happen");
		}

		return rootc;
	}

	public async Task<int> InvokeAsync(string[] args)
	{
		return await InvokeAsync(args, Assembly.GetEntryAssembly()).ConfigureAwait(false);
	}

	public async Task<int> InvokeAsync(string[] args, params Assembly[] assemblies)
	{
		return await BuildRootCommand(assemblies).InvokeAsync(args).ConfigureAwait(false);
	}

	private Command BuildCommandTree(IEnumerable<CommandDescriptor> cmdDescrs, CommandDescriptor cmdDescr)
	{
		var cmdInst = _commandFactory.CreateCommand(cmdDescr.Type)
			?? throw new InvalidOperationException($"Could not create an instance of type '{cmdDescr.Type}'");

		cmdDescr.Instance = cmdInst;

		var cmd = cmdDescr.IsRoot
			? new RootCommand()
			: new Command(cmdDescr.CommandAttribute.Name ?? NameFormatter.CommandTypeToCommandName(cmdDescr.Type));

		cmdDescr.Command = cmd;

		foreach (var alias in cmdDescr.CommandAttribute.Aliases ?? Array.Empty<string>())
		{
			cmd.AddAlias(alias);
		}

		cmd.Description = cmdDescr.CommandAttribute.Description;
		cmd.IsHidden = cmdDescr.CommandAttribute.IsHidden;
		cmd.TreatUnmatchedTokensAsErrors = cmdDescr.CommandAttribute.TreatUnmatchedTokensAsErrors;

		// TODO: Aliases, Arguments, Subcommands, Handler, IsHidden, Name, TreatUnmatchedTokensAsErrors
		// Options
		var props = cmdInst.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

		var methods = cmdDescr.Type.GetMethods(BindingFlags.Instance | BindingFlags.Public);

		// Properties
		foreach (var prop in props)
		{
			// TODO: Arguments
			var argAttr = prop.GetCustomAttribute<ArgumentAttribute>();
			if (argAttr != null)
			{
				HandleArgument(cmdDescr, argAttr, prop);
			}

			// Options
			var optAttr = prop.GetCustomAttribute<OptionAttribute>();
			if (optAttr != null)
			{
				HandleOption(cmdDescr, optAttr, prop);
			}
		}

		// Methods
		foreach (var method in methods)
		{
			// Command builder
			var cmdBuilderAttr = method.GetCustomAttribute<CommandBuilderAttribute>();
			if (cmdBuilderAttr != null)
			{
				method.Invoke(cmdDescr.Instance, new object?[] { cmdDescr.Command });
			}

			// Command handler
			var handlerAttr = method.GetCustomAttribute<CommandHandlerAttribute>();
			if (handlerAttr != null)
			{
				cmdDescr.Command.SetHandler(new Func<InvocationContext, Task<int>>(async ctx =>
				{
					return await new CommandHandler(cmdDescr, method, _serviceProvider).HandlerAsync(ctx).ConfigureAwait(false);
				}));
			}

			// TODO: Validators
		}

		// Child commands
		var childCmds = cmdDescrs
			.Where(c => c.ParentType == cmdDescr.Type)
			.ToList();

		foreach (var childCmd in childCmds)
		{
			var childC = BuildCommandTree(cmdDescrs, childCmd);

			cmd.AddCommand(childC);
		}

		return cmd;
	}

	private static void HandleArgument(
		CommandDescriptor cmdDescr,
		ArgumentAttribute argAttr,
		PropertyInfo prop)
	{
		var argName = argAttr.Name ?? NameFormatter.ToKebabCase(prop.Name);
		var argType = typeof(Argument<>).MakeGenericType(prop.PropertyType);
		var arg = (Argument)Activator.CreateInstance(
			argType, // Argument value type
			argName, // Argument name
			null)!; // Argument description, we set this later

		// Arity
		arg.Arity = argAttr.Arity;

		// Description
		arg.Description = argAttr.Description;

		// DefaultValue
		if (argAttr.DefaultValue != null)
		{
			arg.SetDefaultValue(argAttr.DefaultValue);
		}

		// HelpName
		arg.HelpName = argAttr.HelpName;

		// IsHidden
		arg.IsHidden = argAttr.IsHidden;

		// LegalFileNamesOnly
		if (argAttr.LegalFileNamesOnly)
		{
			arg.LegalFileNamesOnly();
		}

		// LegalFilePathsOnly
		if (argAttr.LegalFilePathsOnly)
		{
			arg.LegalFilePathsOnly();
		}

		// FromAmong
		if (argAttr.FromAmong != null && argAttr.FromAmong.Length > 0)
		{
			arg.FromAmong(argAttr.FromAmong);
		}

		// AddCompletions
		if (argAttr.Completions != null && argAttr.Completions.Length > 0)
		{
			arg.AddCompletions(argAttr.Completions);
		}

		// TODO: Complex completions
		// TODO: Validators

		cmdDescr.Command.AddArgument(arg);

		cmdDescr.Arguments.Add(new ArgumentDescriptor()
		{
			Argument = arg,
			Property = prop,
		});
	}

	private static void HandleOption(
		CommandDescriptor cmdDescr,
		OptionAttribute optAttr,
		PropertyInfo prop)
	{
		var optName = optAttr.Name ?? $"--{NameFormatter.ToKebabCase(prop.Name)}";
		var optType = typeof(Option<>).MakeGenericType(prop.PropertyType);
		var opt = (Option)Activator.CreateInstance(
			optType, // Option value type
			optName, // Option name
			null)!; // Option description, we set this later

		// Aliases
		foreach (var alias in optAttr.Aliases ?? Array.Empty<string>())
		{
			opt.AddAlias(alias);
		}

		// AllowMultipleArgumentsPerToken
		opt.AllowMultipleArgumentsPerToken = optAttr.AllowMultipleArgumentsPerToken;

		// ArgumentHelpName
		opt.ArgumentHelpName = optAttr.ArgumentHelpName;

		// Arity
		opt.Arity = optAttr.Arity;

		// Description
		opt.Description = optAttr.Description;

		// SetDefaultValue
		if (optAttr.DefaultValue != null)
		{
			opt.SetDefaultValue(optAttr.DefaultValue);
		}

		// IsHidden
		opt.IsHidden = optAttr.IsHidden;

		// IsRequired
		opt.IsRequired = optAttr.IsRequired;

		// LegalFileNamesOnly
		if (optAttr.LegalFileNamesOnly)
		{
			opt.LegalFileNamesOnly();
		}

		// LegalFilePathsOnly
		if (optAttr.LegalFilePathsOnly)
		{
			opt.LegalFilePathsOnly();
		}

		// FromAmong
		if (optAttr.FromAmong != null && optAttr.FromAmong.Length > 0)
		{
			opt.FromAmong(optAttr.FromAmong);
		}

		// AddCompletions
		if (optAttr.Completions != null && optAttr.Completions.Length > 0)
		{
			opt.AddCompletions(optAttr.Completions);
		}

		// TODO: Complex completions
		// TODO: Validators

		// IsGlobal
		if (optAttr.IsGlobal)
		{
			cmdDescr.Command.AddGlobalOption(opt);
		}
		else
		{
			cmdDescr.Command.AddOption(opt);
		}

		cmdDescr.Options.Add(new OptionDescriptor()
		{
			Option = opt,
			Property = prop,
		});
	}
}