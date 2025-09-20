namespace DeclarativeCommandLine.Attributes;

[AttributeUsage(AttributeTargets.Class)]
[SuppressMessage("Performance", "CA1813:Avoid unsealed attributes", Justification = "MvdO: We have a generic version that inherits from this.")]
public class CommandAttribute : BaseCommandAttribute
{
	public CommandAttribute()
	{
	}

	public CommandAttribute(string name)
	{
		Name = name;
	}

	public Type? Parent { get; set; }
}