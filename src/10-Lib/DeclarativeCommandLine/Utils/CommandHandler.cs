using DeclarativeCommandLine.Exceptions;

namespace DeclarativeCommandLine.Utils;

internal sealed class CommandHandler(CommandDescriptor cmdDescr)
{
	private readonly CommandDescriptor _cmdDescr =
		cmdDescr ?? throw new ArgumentNullException(nameof(cmdDescr));

	public async Task<int> HandlerAsync(object cmdInst, ParseResult ctx)
	{
		ArgumentNullException.ThrowIfNull(ctx);

		// Arguments
		HandleArguments(cmdInst, ctx);

		// Options
		HandleOptions(cmdInst, ctx);

		if (cmdInst is ICommand cmd)
		{
			// TODO: Try catch here or somewhere else?
			cmd.Execute();
			return 0;
		}

		if (cmdInst is IAsyncCommand asyncCmd)
		{
			await asyncCmd.ExecuteAsync().ConfigureAwait(false);
			return 0;
		}

		throw new DeclarativeCommandLineException(
			$"Type '{_cmdDescr.Type.FullName}' does not inherit from interface '{typeof(IAsyncCommand).FullName}'.");
	}

	private void HandleArguments(object cmdInst, ParseResult ctx)
	{
		foreach (var arg in _cmdDescr.Arguments)
		{
			var argVal = ctx.GetValue<object>(arg.Argument.Name); // TODO: Check if works
			arg.Property.SetValue(cmdInst, argVal);
		}
	}

	private void HandleOptions(object cmdInst, ParseResult ctx)
	{
		foreach (var opt1 in _cmdDescr.Options)
		{
			var optVal = ctx.GetValue<object>(opt1.Option.Name); // TODO: Check if works
			opt1.Property.SetValue(cmdInst, optVal);
		}
	}
}