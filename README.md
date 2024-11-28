# Declarative Command Line

[![GitHub Actions](https://github.com/flyingpie/declarative-command-line/actions/workflows/dotnet.yml/badge.svg)](https://github.com/flyingpie/declarative-command-line/actions/workflows/dotnet.yml)

[![NuGet latest release](https://img.shields.io/nuget/v/DeclarativeCommandLine.svg)]([https://www.nuget.org/packages/Docker.DotNet](https://www.nuget.org/packages/DeclarativeCommandLine))

Attribute-driven layer on top of System.CommandLine to make the most common use cases easier to set up.

## Minimalistic Example

The very smallest a command can be:

```cs
using DeclarativeCommandLine;

[Command]
public class AddNumbersCommand : ICommand
{
	[Option]
	public int NumberA { get; set; }

	[Option]
	public int NumberB { get; set; }

	public void Execute()
	{
		Console.WriteLine($"{NumberA} + {NumberB} = {NumberA + NumberB}");
	}
}
```

## Contents

- [Commands](#commands)
- [Arguments](#arguments)
- [Options](#options)
- [Command Handlers](#command-handlers)
- [Command Builders](#command-builders)
- [Command Inheritance](#command-inheritance)
- [Dependency Injection](#dependency-injection)
- [Root Command](#root-command)
- [Command Hierarchy](#command-hierarchy)
- [Automatic Naming](#automatic-naming)

## Commands

TODO:
- Name
- Aliases
- Description
- IsHidden
- Parent
- TreatUnmatchedTokensAsErrors

```cs
public class MyCommand
{

}
```

## Arguments

```cs
[Command]
public class MyCommand
{
	[Argument]
	public string MyArgument { get; set; }
}
```

TODO:
- Name
- Description
- DefaultValue
- ArgumentArity Arity
- Completions
- FromAmong
- HelpName
- IsHidden
- LegalFileNamesOnly
- LegalFilePathsOnly

## Options

```cs
[Command]
public class MyCommand
{
	[Option]
	public string MyOption { get; set; }
}
```

TODO:
- Name
- Aliases
- DefaultValue
- Description
- ArgumentHelpName
- AllowMultipleArgumentsPerToken
- ArgumentArity Arity
- Completions
- FromAmong
- IsRequired
- IsGlobal
- IsHidden
- LegalFileNamesOnly
- LegalFilePathsOnly

## Command Handlers

TODO

```cs
[RootCommand]
public class AppRootCommand
{
	[CommandHandler]
	public void Handle()
	{
	}
}
```

## Command Builders

TODO

```cs
[RootCommand]
public class AppRootCommand
{
	[CommandBuilder]
	public void Handle(System.CommandLine.Command command)
	{
	}
}
```

## Command Inheritance

TODO

## Dependency Injection

TODO

## Root Command
Generally, an app has exactly 1 "root command", which handles the case where no explicit command is specified.

For example, with an argument:
```
my-app.exe C:/path/to/file.txt
```

With an option:
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

## Command Hierarchy

Commands can be arranged in a hierarchy, to add structure and share arguments and options.

Here's a couple example commands that illustrate the concept.

```
my-app.exe file create my-file.txt
```

```
my-app.exe dir create my-dir
```

We could define the commands as:

```cs
[Command("file")]
public class FileCommand
{
}
```

```cs
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

```cs
[Command("dir")]
public class DirCommand
{
}
```

```cs
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

We can now add more commands under these, for example:

```cs
[Command("current", Parent = typeof(DirCommand))]
public class CurrentDirCommand
{
	[CommandHandler]
	public void Handle()
	{
		Console.WriteLine($"CURRENT: {Directory.GetCurrentDirectory()}");
	}
}
```

```
my-app.exe dir current
CURRENT: C:\path\to\net6.0
```

## Automatic Naming

TODO
