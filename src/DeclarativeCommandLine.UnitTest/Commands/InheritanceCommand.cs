using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Commands;

[Command(Parent = typeof(AppRootCommand))]
public class InheritanceCommand
{
	[Command("inheritance-0", Parent = typeof(InheritanceCommand))]
	public class CommandBaseClass(IOutput output) : ICommand
	{
		private readonly IOutput _output = output ?? throw new ArgumentNullException(nameof(output));

		[Option]
		public string BaseArgumentA { get; set; }

		[Option]
		public string BaseOptionA { get; set; }

		public virtual void Execute()
		{
			_output.WriteLine("Base command");
			_output.WriteLine($"- {nameof(BaseArgumentA)}:{BaseArgumentA}");
			_output.WriteLine($"- {nameof(BaseOptionA)}:{BaseOptionA}");
		}
	}

	[Command("inheritance-1", Parent = typeof(InheritanceCommand))]
	public class CommandChildClass(IOutput output) : CommandBaseClass(output)
	{
		private readonly IOutput _output = output ?? throw new ArgumentNullException(nameof(output));

		[Option]
		public string ChildArgumentA { get; set; }

		[Option]
		public string ChildOptionA { get; set; }

		public override void Execute()
		{
			_output.WriteLine("Child command");
			_output.WriteLine($"- {nameof(BaseArgumentA)}:{BaseArgumentA}");
			_output.WriteLine($"- {nameof(BaseOptionA)}:{BaseOptionA}");
			_output.WriteLine($"- {nameof(ChildArgumentA)}:{ChildArgumentA}");
			_output.WriteLine($"- {nameof(ChildOptionA)}:{ChildOptionA}");
		}
	}

	[Command("inheritance-2", Parent = typeof(InheritanceCommand))]
	public class CommandGrandChildClass(IOutput output) : CommandChildClass(output)
	{
		private readonly IOutput _output1 = output ?? throw new ArgumentNullException(nameof(output));

		[Option]
		public string GrandChildArgumentA { get; set; }

		[Option]
		public string GrandChildOptionA { get; set; }

		public override void Execute()
		{
			_output1.WriteLine("Grand child command");
			_output1.WriteLine($"- {nameof(BaseArgumentA)}:{BaseArgumentA}");
			_output1.WriteLine($"- {nameof(BaseOptionA)}:{BaseOptionA}");
			_output1.WriteLine($"- {nameof(ChildArgumentA)}:{ChildArgumentA}");
			_output1.WriteLine($"- {nameof(ChildOptionA)}:{ChildOptionA}");
			_output1.WriteLine($"- {nameof(GrandChildArgumentA)}:{GrandChildArgumentA}");
			_output1.WriteLine($"- {nameof(GrandChildOptionA)}:{GrandChildOptionA}");
		}
	}
}