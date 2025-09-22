using DeclarativeCommandLine.Attributes;
using DeclarativeCommandLine.Utils;

namespace DeclarativeCommandLine;

public class DeclarativeCommandLineFactory(
	ICommandFactory commandFactory,
	IServiceProvider serviceProvider,
	IEnumerable<CommandDescriptor> commandDescriptors
)
{
	private readonly ICommandFactory _commandFactory =
		commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

	private readonly IEnumerable<CommandDescriptor> _commandDescriptors =
		commandDescriptors ?? throw new ArgumentNullException(nameof(commandDescriptors));

	//public RootCommand BuildRootCommand(params Type[] types)
	//{
	//	if (types == null || types.Length == 0)
	//	{
	//		throw new ArgumentException("At least 1 type is required.", nameof(types));
	//	}

	//var cmdTypes = types
	//	.Select(type => CommandDescriptor.TryCreate(type, out var cmdDescr) ? cmdDescr : null)
	//	.Where(cmdDescr => cmdDescr != null)
	//	.Select(cmdDescr => cmdDescr!)
	//	.ToList();

	// TODO: Add IAsyncCommand? (DI now done through constructor).
	public RootCommand BuildRootCommand()
	{
		var roots = _commandDescriptors.Where(c => c.IsRoot).ToList();
		var root = roots.FirstOrDefault();

		if (root == null)
		{
			root = new CommandDescriptor(
				typeof(DefaultRootCommand),
				new RootCommandAttribute() { }
			);
		}

		if (roots.Count > 1)
		{
			throw new InvalidOperationException(
				$"Multiple root commands found: {string.Join(", ", roots.Select(c => c.Type))}."
			);
		}

		// Relate non-root commands without a parent command to the root command.
		foreach (var cmdType in _commandDescriptors)
		{
			if (!cmdType.IsRoot && cmdType.ParentType == null)
			{
				cmdType.ParentType = root.Type;
			}
		}

		var rootCmd = BuildCommandTree(_commandDescriptors, root);

		if (rootCmd is not RootCommand rootc)
		{
			throw new InvalidOperationException("This shouldn't happen");
		}

		return rootc;
	}

	public async Task<int> InvokeAsync(string[] args)
	{
		//return await InvokeAsync(args, Assembly.GetEntryAssembly()).ConfigureAwait(false);
		return await BuildRootCommand().InvokeAsync(args).ConfigureAwait(false);
	}

	//public async Task<int> InvokeAsync(string[] args, params Assembly[] assemblies)
	//{
	//	return await BuildRootCommand(assemblies).InvokeAsync(args).ConfigureAwait(false);
	//}

	private Command BuildCommandTree(
		IEnumerable<CommandDescriptor> cmdDescrs,
		CommandDescriptor cmdDescr
	)
	{
		var cmd = cmdDescr.IsRoot
			? new RootCommand()
			: new Command(
				cmdDescr.CommandAttribute.Name
					?? NameFormatter.CommandTypeToCommandName(cmdDescr.Type)
			);

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
		var props = cmdDescr.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

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

		// TODO: Validators

		if (cmdDescr.IsExecutable)
		{
			cmdDescr.Command.SetHandler(
				new Func<InvocationContext, Task<int>>(async ctx =>
				{
					var cmdInst =
						_commandFactory.CreateCommand(cmdDescr.Type)
						?? throw new InvalidOperationException(
							$"Could not create an instance of type '{cmdDescr.Type}'"
						);

					return await new CommandHandler(cmdDescr)
						.HandlerAsync(cmdInst, ctx)
						.ConfigureAwait(false);
				})
			);
		}

		// Child commands
		var childCmds = cmdDescrs
			.OrderBy(c => c.Type.Name)
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
		PropertyInfo prop
	)
	{
		var argName = argAttr.Name ?? NameFormatter.ToKebabCase(prop.Name);
		var argType = typeof(Argument<>).MakeGenericType(prop.PropertyType);
		var arg = (Argument)
			Activator.CreateInstance(
				argType, // Argument value type
				argName, // Argument name
				null
			)!; // Argument description, we set this later

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

		cmdDescr.Arguments.Add(new ArgumentDescriptor() { Argument = arg, Property = prop });
	}

	private static void HandleOption(
		CommandDescriptor cmdDescr,
		OptionAttribute optAttr,
		PropertyInfo prop
	)
	{
		var optName = optAttr.Name ?? $"--{NameFormatter.ToKebabCase(prop.Name)}";
		var optType = typeof(Option<>).MakeGenericType(prop.PropertyType);
		var opt = (Option)
			Activator.CreateInstance(
				optType, // Option value type
				optName, // Option name
				null // Option description, we set this later
			)!;

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

		cmdDescr.Options.Add(new OptionDescriptor() { Option = opt, Property = prop });
	}
}
