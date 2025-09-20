namespace DeclarativeCommandLine.Utils;

public static class SystemCommandLineUtils
{
	public static Argument ConstructArgument(string argName, Type argValueType)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(argName);
		ArgumentNullException.ThrowIfNull(argValueType);

		var argType = typeof(Argument<>).MakeGenericType(argValueType);
		return (Argument)Activator.CreateInstance(
			argType, // Argument value type
			argName)!; // Argument name
	}

	public static Option ConstructOption(string optName, Type optValueType)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(optName);
		ArgumentNullException.ThrowIfNull(optValueType);

		var optType = typeof(Option<>).MakeGenericType(optValueType);
		return (Option)Activator.CreateInstance(
			optType, // Option value type
			optName)!; // Option name
	}

	// public static void SetDefaultValue(Argument arg)
	// {
	// 	var a = new Argument<string>();
	// 	a.DefaultValueFactory
	// }
}