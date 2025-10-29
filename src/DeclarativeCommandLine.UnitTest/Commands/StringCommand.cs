using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Commands;

[Command(Parent = typeof(AppRootCommand))]
public class StringCommand
{
	[Command(Parent = typeof(MathCommand))]
	public class AddCommand(IOutput output) : ICommand
	{
		[Option(
			AllowedValues = ["value-1", "value-2", "value-3"],
			Description = "The first value.",
			Required = true)]
		public int ValueA { get; set; }

		[Option(
			Description = "The second value.",
			Required = true)]
		public int ValueB { get; set; }

		public void Execute()
		{
			output.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");
		}
	}
}