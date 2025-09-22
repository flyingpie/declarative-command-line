# Declarative Command Line
Attribute-driven layer on top of System.CommandLine to make the most common use cases easier to set up.

## Minimalistic Example

The very smallest a command can be:

```cs
using DeclarativeCommandLine;

[Command<AppRootCommand>]
public class AddNumbersCommand
{
	[Option]
	public int NumberA { get; set; }

	[Option]
	public int NumberB { get; set; }

	[CommandHandler]
	public void Handle()
	{
		Console.WriteLine($"{NumberA} + {NumberB} = {NumberA + NumberB}");
	}
}
```

## Command Hierarchy

Generally, an app needs exactly 1 "root command", which handles the case where no explicit command is specified.

```cs
[RootCommand]
public class AppRootCommand
{
}
```
