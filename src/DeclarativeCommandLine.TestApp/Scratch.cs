#pragma warning ignore

using System;
using System.CommandLine;

namespace DeclarativeCommandLine.TestApp;

public static class Scratch
{
	public static RootCommand Build(Func<Type, object> serviceProvider)
	{
		// Root command
		var rootCmd = new RootCommand();
		rootCmd.Hidden = false;

		// Command
		var cmd = new Command("inheritance");
		rootCmd.Add(cmd);
		// - [x] cmd.Action								Through .SetAction()
		// - [x] cmd.Aliases							Through constructor or property
		// - [x] cmd.Description						Through property
		// - [x] cmd.Hidden								Through property
		// - [x] cmd.Name								Through constructor
		// - [ ] cmd.TreatUnmatchedTokensAsErrors
		// - [ ] cmd.Validators

		// Argument
		var arg = new Argument<string>("");
		cmd.Add(arg);
		// - [ ] arg.Arity
		// - [ ] arg.CompletionSources
		// - [ ] arg.CustomParser
		// - [x] arg.DefaultValueFactory				Through "DefaultValue" property, but should still be extended with more advanced logic.
		// - [x] arg.Description						Through property
		// - [ ] arg.HelpName
		// - [ ] arg.Hidden
		// - [ ] arg.Name
		// - [ ] arg.Validators

		// Option
		var opt = new Option<String>("--base-argument-a");
		cmd.Add(opt);
		// - [ ] opt.Action
		// - [x] opt.Aliases							Through property
		// - [x] opt.AllowMultipleArgumentsPerToken		Through property
		// - [ ] opt.Arity
		// - [ ] opt.CompletionSources
		// - [ ] opt.CustomParser
		// - [x] opt.DefaultValueFactory				Through "DefaultValue" property, but could still be extended with more advanced logic.
		// - [x] opt.Description						Through property
		// - [ ] opt.HelpName
		// - [x] opt.Hidden								Through property
		// - [x] opt.Name								Through constructor
		// - [x] opt.Recursive							Through property
		// - [x] opt.Required							Through property
		// - [ ] opt.Validators

		cmd.SetAction(async (parseResult, ct) => { });

		return rootCmd;
	}
}
