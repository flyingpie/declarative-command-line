# Declarative Command Line
Attribute-driven layer on top of System.CommandLine to make the most common use cases easier to set up.

## Minimalistic Example

The very smallest a command can be:

```cs
using DeclarativeCommandLine;

[Command]
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

### Root Command
Generally, an app has exactly 1 "root command", which handles the case where no explicit command is specified.

For example:

Argument:
```
my-app.exe C:/path/to/file.txt
```

Option:
```
my-app.exe --verbose
```

Cases like these are handled through the root command. If no root command is defined, an empty default root command is used, to cut down on boilerplate.

But one can be defined explicitly, like this:

```cs
[RootCommand]
public class AppRootCommand
{
	[Argument]
	public string MyFirstArgument { get; set; }

	[Option]
	public bool Verbose { get; set; }
}
```

### Hierarchy

Commands can be arranged in a hierarchy, to add structure and share arguments and options.

Here's a couple commands that illustrate the concept.

Create a file named "my-file.txt"
```
my-app.exe file create my-file.txt
```

Create a directory named "my-dir"
```
my-app.exe dir create my-dir
```

We could define the commands as:

```cs
[Command("file")]
public class FileCommand
{
}

[Command("create", Parent = typeof(FileCommand))]
public class CreateFileCommand
{
	[Argument]
	public string FileName { get; set; }

	[CommandHandler]
	public void Handle()
	{
		File.WriteAllText(FileName, string.Empty);
	}
}
```

And:

```cs
[Command("dir")]
public class DirCommand
{
}

[Command("create", Parent = typeof(DirCommand))]
public class CreateDirCommand
{
	[Argument]
	public string DirName { get; set; }

	[CommandHandler]
	public void Handle()
	{
		Directory.CreateDirectory(DirName);
	}
}
```
