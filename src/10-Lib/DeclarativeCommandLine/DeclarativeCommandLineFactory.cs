using DeclarativeCommandLine.Attributes;
using DeclarativeCommandLine.Utils;

namespace DeclarativeCommandLine;

public class DeclarativeCommandLineFactory(
	ICommandFactory commandFactory,
	IEnumerable<CommandDescriptor> commandDescriptors)
{
	private readonly ICommandFactory _commandFactory =
		commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));

	private readonly IEnumerable<CommandDescriptor> _commandDescriptors =
		commandDescriptors ?? throw new ArgumentNullException(nameof(commandDescriptors));

	// TODO: Add IAsyncCommand? (DI now done through constructor).
	public RootCommand BuildRootCommand()
	{
		var roots = _commandDescriptors.Where(c => c.IsRoot).ToList();
		var root = roots.FirstOrDefault();

		if (root == null)
		{
			root = new CommandDescriptor(
				typeof(DefaultRootCommand),
				new RootCommandAttribute() { });
		}

		if (roots.Count > 1)
		{
			throw new InvalidOperationException(
				$"Multiple root commands found: {string.Join(", ", roots.Select(c => c.Type))}.");
		}

		// Relate non-root commands without a parent command to the root command.
		foreach (var cmdType in _commandDescriptors)
		{
			if (!cmdType.IsRoot && cmdType.ParentType == null)
			{
				cmdType.ParentType = root.Type;
			}
		}

		var rootCmd = BuildCommandTree([.. _commandDescriptors], root);

		if (rootCmd is not RootCommand rootc)
		{
			throw new InvalidOperationException("This shouldn't happen");
		}

		return rootc;
	}

	public async Task<int> InvokeAsync(string[] args)
	{
		return await BuildRootCommand().Parse(args).InvokeAsync().ConfigureAwait(false);
	}

	private static void HandleArgument(
		CommandDescriptor cmdDescr,
		ArgumentAttribute argAttr,
		PropertyInfo prop)
	{
		var argName = argAttr.Name ?? NameFormatter.ToKebabCase(prop.Name);
		var arg = SystemCommandLineUtils.ConstructArgument(argName, prop.PropertyType);

		// AllowedValues
		if (argAttr.AllowedValues != null && argAttr.AllowedValues.Length > 0)
		{
			// TODO: Use validator
		}

		// Description
		arg.Description = argAttr.Description;

		// DefaultValue
		if (argAttr.DefaultValue != null)
		{
			// TODO
		}

		// HelpName
		arg.HelpName = argAttr.HelpName;

		// Hidden
		arg.Hidden = argAttr.Hidden;

		cmdDescr.Arguments.Add(new ArgumentDescriptor(arg, prop));
		cmdDescr.CommandRequired.Add(arg);
	}

	private static void HandleOption(
		CommandDescriptor cmdDescr,
		OptionAttribute optAttr,
		PropertyInfo prop)
	{
		var optName = optAttr.Name ?? $"--{NameFormatter.ToKebabCase(prop.Name)}";
		var opt = SystemCommandLineUtils.ConstructOption(optName, prop.PropertyType);

		// Aliases
		foreach (var alias in optAttr.Aliases ?? Array.Empty<string>())
		{
			opt.Aliases.Add(alias);
		}

		// AllowMultipleArgumentsPerToken
		opt.AllowMultipleArgumentsPerToken = optAttr.AllowMultipleArgumentsPerToken;

		// ArgumentHelpName
		opt.HelpName = optAttr.ArgumentHelpName;

		// Description
		opt.Description = optAttr.Description;

		// SetDefaultValue
		if (optAttr.DefaultValue != null)
		{
			// TODO
		}

		// Hidden
		opt.Hidden = optAttr.Hidden;

		// Recursive
		opt.Recursive = optAttr.Recursive;

		// Required
		opt.Required = optAttr.Required;

		cmdDescr.CommandRequired.Add(opt);
		cmdDescr.Options.Add(new OptionDescriptor(opt, prop));
	}

	private Command BuildCommandTree(
		ICollection<CommandDescriptor> cmdDescrs,
		CommandDescriptor cmdDescr)
	{
		var cmd = cmdDescr.IsRoot
			? new RootCommand()
			: new Command(
				cmdDescr.CommandAttribute.Name
				?? NameFormatter.CommandTypeToCommandName(cmdDescr.Type));

		cmdDescr.Command = cmd;

		foreach (var alias in cmdDescr.CommandAttribute.Aliases ?? Array.Empty<string>())
		{
			cmd.Aliases.Add(alias);
		}

		cmd.Description = cmdDescr.CommandAttribute.Description;
		cmd.Hidden = cmdDescr.CommandAttribute.Hidden;
		cmd.TreatUnmatchedTokensAsErrors = cmdDescr.CommandAttribute.TreatUnmatchedTokensAsErrors;

		// TODO: Aliases, Arguments, Subcommands, Handler, IsHidden, Name, TreatUnmatchedTokensAsErrors
		// Options
		var props = cmdDescr.Type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

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

		if (cmdDescr.IsExecutable)
		{
			cmdDescr.Command.SetAction(async ctx =>
			{
				var cmdInst =
					_commandFactory.CreateCommand(cmdDescr.Type)
					?? throw new InvalidOperationException(
						$"Could not create an instance of type '{cmdDescr.Type}'");

				return await new CommandHandler(cmdDescr)
					.HandlerAsync(cmdInst, ctx)
					.ConfigureAwait(false);
			});
		}

		// Child commands
		var childCmds = cmdDescrs
			.Where(c => c.ParentType == cmdDescr.Type)
			.OrderBy(c => c.Type.Name)
			.ToList();

		foreach (var childCmd in childCmds)
		{
			var childC = BuildCommandTree(cmdDescrs, childCmd);

			cmd.Add(childC);
		}

		return cmd;
	}
}