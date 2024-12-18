namespace DeclarativeCommandLine.Attributes;

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