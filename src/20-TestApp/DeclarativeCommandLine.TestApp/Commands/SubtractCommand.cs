﻿using DeclarativeCommandLine.Attributes;

namespace DeclarativeCommandLine.TestApp.Commands;

[Command]
public class SubtractCommand : ICommand
{
	public SubtractCommand()
	{
		Console.WriteLine($"new {GetType().FullName}()");
	}

	[Argument]
	public int ValueA { get; set; }

	[Argument]
	public int ValueB { get; set; }

	public void Execute()
	{
		Console.WriteLine($"{ValueA} - {ValueB} = {ValueA - ValueB}");
	}
}