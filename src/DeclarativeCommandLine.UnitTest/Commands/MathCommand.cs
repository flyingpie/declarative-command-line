namespace DeclarativeCommandLine.UnitTest.Commands;

[Command(Parent = typeof(AppRootCommand))]
public class MathCommand
{
	[Command(Parent = typeof(MathCommand))]
	public class AddCommand(IOutput output) : ICommand
	{
		[Option(
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

	[Command(Parent = typeof(MathCommand))]
	public class SubtractCommand(IOutput output) : IAsyncCommand
	{
		[Option(Required = true)]
		public int ValueA { get; set; }

		[Option(Required = true)]
		public int ValueB { get; set; }

		public Task ExecuteAsync(CancellationToken ct = default)
		{
			output.WriteLine($"{ValueA} + {ValueB} = {ValueA + ValueB}");

			return Task.CompletedTask;
		}
	}
}