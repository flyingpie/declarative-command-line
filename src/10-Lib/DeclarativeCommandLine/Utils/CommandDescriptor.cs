using DeclarativeCommandLine.Attributes;
using System.Collections.ObjectModel;

namespace DeclarativeCommandLine.Utils;

public class CommandDescriptor
{
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
			var t = new[] { typeof(ICommand), typeof(IAsyncCommand) };

			return t.Any(tt => tt.IsAssignableFrom(Type));
		}
	}

	public BaseCommandAttribute CommandAttribute { get; }

	public bool IsRoot { get; }

	public Command? Command { get; set; }

	public Command CommandRequired => Command ?? throw new InvalidOperationException($"Expected property '{nameof(Command)}' to be not null, but it was .");

	public Collection<ArgumentDescriptor> Arguments { get; } = [];

	public Collection<OptionDescriptor> Options { get; } = [];

	public static bool TryCreate(Type type, [NotNullWhen(true)] out CommandDescriptor? cmdDescr)
	{
		ArgumentNullException.ThrowIfNull(type);

		cmdDescr = null;

		var cmdAttr = type.GetCustomAttribute<BaseCommandAttribute>(inherit: false);

		if (cmdAttr != null)
		{
			cmdDescr = new CommandDescriptor(type, cmdAttr);
			return true;
		}

		return false;
	}
}