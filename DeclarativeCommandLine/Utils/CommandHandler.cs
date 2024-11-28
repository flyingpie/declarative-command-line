using DeclarativeCommandLine.Exceptions;

namespace DeclarativeCommandLine.Utils;

internal class CommandHandler
{
	private readonly CommandDescriptor _cmdDescr;
	//private readonly MethodInfo _methodInfo;
	//private readonly IServiceProvider _serviceProvider;

	public CommandHandler(
		CommandDescriptor cmdDescr)
	{
		_cmdDescr = cmdDescr ?? throw new ArgumentNullException(nameof(cmdDescr));
		//_methodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
		//_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
	}

	public async Task<int> HandlerAsync(InvocationContext ctx)
	{
		if (ctx == null)
		{
			throw new ArgumentNullException(nameof(ctx));
		}

		//var methodParams = _methodInfo.GetParameters();

		// Arguments
		HandleArguments(ctx);

		// Options
		HandleOptions(ctx);

		if (_cmdDescr.Instance is ICommand cmd)
		{
			// TODO: Try catch here or somewhere else?
			cmd.Execute();
			return 0;
		}

		if (_cmdDescr.Instance is IAsyncCommand asyncCmd)
		{
			await asyncCmd.ExecuteAsync();
			return 0;
		}

		throw new DeclarativeCommandLineException($"Type '{_cmdDescr.Type.FullName}' does not inherit from interface '{typeof(IAsyncCommand).FullName}'.");

		return 0;

		//var methodParamValues = CreateParameterValues(ctx, methodParams);

		// Return value
		// TODO: Check that this actually works, and doesn't override any
		// explicitly set return codes (through InvocationContext).
		//var result = _methodInfo.Invoke(_cmdDescr.Instance, methodParamValues.ToArray());

		//return await HandleReturnValueAsync(result).ConfigureAwait(false);
	}

	//private List<object> CreateParameterValues(
	//	InvocationContext ctx,
	//	ParameterInfo[] methodParams)
	//{
	//	var mp = new List<object?>();
	//	foreach (var methodP in methodParams)
	//	{
	//		var fromServicesAttr = methodP.GetCustomAttribute<FromServicesAttribute>();

	//		if (methodP.ParameterType == typeof(InvocationContext))
	//		{
	//			mp.Add(ctx);
	//		}
	//		else
	//		{
	//			var inst = _serviceProvider.GetService(methodP.ParameterType)
	//				?? throw new ServiceResolverException($"Could not resolved instance of type '{methodP.ParameterType}'");

	//			mp.Add(inst);
	//		}
	//	}

	//	return mp;
	//}

	private void HandleArguments(InvocationContext ctx)
	{
		foreach (var arg in _cmdDescr.Arguments)
		{
			var argVal = ctx.ParseResult.GetValueForArgument(arg.Argument);
			arg.Property.SetValue(_cmdDescr.Instance, argVal);
		}
	}

	private void HandleOptions(InvocationContext ctx)
	{
		foreach (var opt1 in _cmdDescr.Options)
		{
			var optVal = ctx.ParseResult.GetValueForOption(opt1.Option);
			opt1.Property.SetValue(_cmdDescr.Instance, optVal);
		}
	}

	private async Task<int> HandleReturnValueAsync(object result)
	{
		if (result is Task<int> asTaskInt)
		{
			return await asTaskInt.ConfigureAwait(false);
		}

		if (result is Task asTask)
		{
			await asTask.ConfigureAwait(false);
			return 0;
		}

		if (result is int asInt)
		{
			return asInt;
		}

		return 0;
	}
}