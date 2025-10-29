using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Commands;

[Command(Parent = typeof(AppRootCommand))]
public class StringCommand
{
	[Command(Parent = typeof(StringCommand))]
	public class AddCommand(IOutput output) : ICommand
	{
		[Option(
			Description = "The first value.",
			DefaultValue = "My Default Value",
			FromAmong = ["1", "2", "3"],
			Required = true)]
		public string ValueA { get; set; }

		[Option(
			Description = "The second value.",
			DefaultValue = 1234,
			Required = true)]
		public int ValueB { get; set; }

		public void Execute()
		{
			output.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");
		}
	}
}