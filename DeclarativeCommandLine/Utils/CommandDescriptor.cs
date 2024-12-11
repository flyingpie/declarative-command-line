namespace DeclarativeCommandLine.Utils;

public class CommandDescriptor
{
	public static bool TryCreate(Type type, out CommandDescriptor? cmdDescr)
	{
		if (type == null) throw new ArgumentNullException(nameof(type));

		cmdDescr = null;

		var cmdAttr = type.GetCustomAttribute<BaseCommandAttribute>(inherit: false);

		if (cmdAttr != null)
		{
			cmdDescr = new CommandDescriptor(type, cmdAttr);
			return true;
		}

		return false;
	}

	public CommandDescriptor(Type type, BaseCommandAttribute cmdAttr)
	{
		Type = type ?? throw new ArgumentNullException(nameof(type));
		CommandAttribute = cmdAttr ?? throw new ArgumentNullException(nameof(cmdAttr));
		ParentType = CommandAttribute is CommandAttribute subCommand ? subCommand.Parent : null;
		IsRoot = CommandAttribute is RootCommandAttribute;
	}

	public Type? ParentType { get; set; }

	public Type Type { get; }

	public bool IsExecutable
	{
		get
		{
			var t = new[]
			{
				typeof(ICommand),
				typeof(IAsyncCommand),
			};

			return t.Any(tt => tt.IsAssignableFrom(Type));
		}
	}

	public BaseCommandAttribute CommandAttribute { get; }

	public bool IsRoot { get; }

	private object? Instance { get; set; }

	public CommandDescriptor Parent { get; set; }

	public Command Command { get; set; }

	public List<ArgumentDescriptor> Arguments { get; set; } = new();

	public List<OptionDescriptor> Options { get; set; } = new();
}