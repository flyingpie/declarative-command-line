namespace DeclarativeCommandLine;

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

[AttributeUsage(AttributeTargets.Class)]
public sealed class CommandAttribute<TParentCommand> : CommandAttribute
{
	public CommandAttribute()
	{
		Parent = typeof(TParentCommand);
	}

	public CommandAttribute(string name)
		: base(name)
	{
		Parent = typeof(TParentCommand);
	}
}