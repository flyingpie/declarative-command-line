# Declarative Command Line

[![GitHub Actions](https://github.com/flyingpie/declarative-command-line/actions/workflows/dotnet.yml/badge.svg)](https://github.com/flyingpie/declarative-command-line/actions/workflows/dotnet.yml)

[![NuGet latest release](https://img.shields.io/nuget/v/DeclarativeCommandLine.svg)]([https://www.nuget.org/packages/Docker.DotNet](https://www.nuget.org/packages/DeclarativeCommandLine))

Attribute-driven layer on top of System.CommandLine to make the most common use cases easier to set up.

## Minimalistic Example

A minimal example, using DI to instantiate command objects:

### Add NuGet Packages

```xml
<ItemGroup>
  <PackageReference Include="DeclarativeCommandLine" Version="2.0.0-g498c5dd86c" />
  <PackageReference Include="DeclarativeCommandLine.Generator" Version="2.0.0-g498c5dd86c" />
  <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="9.0.9"/>
</ItemGroup>
```

### Program.cs

```cs
using DeclarativeCommandLine;
using Microsoft.Extensions.DependencyInjection;

namespace MyApp;

[Command(Description = "Math commands")]
public class AppRootCommand
{
}

[Command(Description = "Add 2 numbers", Parent = typeof(AppRootCommand))]
public class AddCommand : ICommand
{
    [Option(Required = true)]
    public int ValueA { get; set; }

    [Option(Required = true)]
    public int ValueB { get; set; }

    public void Execute()
    {
        Console.WriteLine($"A={ValueA} + {ValueB} = {ValueA + ValueB}");
    }
}

public static class Program
{
    public static int Main(string[] args)
    {
        var p = new ServiceCollection()
            .AddTransient<AppRootCommand>()
            .AddTransient<AddCommand>()
            .BuildServiceProvider();

        return new CommandBuilder()
            .Build(t => p.GetRequiredService(t))
            .Parse(args)
            .Invoke();
    }
}
```
