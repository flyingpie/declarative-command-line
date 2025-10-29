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

		var cmd1 = new Command("aliases");
		rootCmd.Add(cmd1);
		cmd1.Hidden = false;

		var opt1 = new Option<int?>("--int-1");
		{
			cmd1.Add(opt1);
			opt1.Required = false;
			opt1.DefaultValueFactory = argRes => 1234;

			// opt1.AcceptOnlyFromAmong([]);
			// opt1.AcceptLegalFileNamesOnly();
			// opt1.AcceptLegalFilePathsOnly();
		}

		cmd1.SetAction(res =>
		{
			var x = res.GetValue(opt1);

			var xx = 2;
		});

		var res = rootCmd.Parse("aliases");

		res.Invoke();

		var dbg = 2;
	}
}