namespace DeclarativeCommandLine.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute : BaseCommandAttribute
{
	public CommandAttribute()
	{
	}

	public CommandAttribute(string name)
	{
		Name = name;
	}

	public Type Parent { get; set; }
}