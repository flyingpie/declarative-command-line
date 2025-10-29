using System.CommandLine;

namespace DeclarativeCommandLine.UnitTest;

[TestClass]
public class ScratchTest
{
	[TestMethod]
	public void Test1()
	{
		var rootCmd = new RootCommand();
		rootCmd.Hidden = false;
		// global::DeclarativeCommandLine.UnitTest.Commands.AliasesCommand
		{
			var cmd1 = new Command("aliases");
			rootCmd.Add(cmd1);
			cmd1.Hidden = false;

			var opt1 = new Option<String>("--grand-child-argument-a");
			{
				cmd1.Add(opt1);
				opt1.Description = "";
				opt1.Hidden = false;
				opt1.Required = false;
				opt1.DefaultValueFactory = argRes => "";

				opt1.AcceptOnlyFromAmong([]);
				opt1.AcceptLegalFileNamesOnly();
				opt1.AcceptLegalFilePathsOnly();

			}

			cmd1.SetAction(res =>
			{
				var x = res.GetValue(opt1) ?? "abc";
			});
		}
	}
}