namespace DeclarativeCommandLine.Utils;

public class ArgumentDescriptor(Argument arg, PropertyInfo prop)
{
	public Argument Argument { get; } = arg ?? throw new ArgumentNullException(nameof(arg));

	public PropertyInfo Property { get; } = prop ?? throw new ArgumentNullException(nameof(prop));
}