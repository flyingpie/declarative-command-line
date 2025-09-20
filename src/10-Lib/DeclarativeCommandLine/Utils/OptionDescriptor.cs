namespace DeclarativeCommandLine.Utils;

public class OptionDescriptor(Option option, PropertyInfo prop)
{
	public Option Option { get; } = option ?? throw new ArgumentNullException(nameof(option));

	public PropertyInfo Property { get; } = prop ?? throw new ArgumentNullException(nameof(prop));
}