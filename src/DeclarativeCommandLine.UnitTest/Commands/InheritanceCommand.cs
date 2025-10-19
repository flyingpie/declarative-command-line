using DeclarativeCommandLine.UnitTest.Utils;

namespace DeclarativeCommandLine.UnitTest.Commands;

[Command(Parent = typeof(AppRootCommand))]
public class InheritanceCommand
{
	[Command("inheritance-0", Parent = typeof(InheritanceCommand))]
	public class CommandBaseClass(IOutput output) : ICommand
	{
		[Option]
		public string BaseArgumentA { get; set; }

		[Option]
		public string BaseOptionA { get; set; }

		public virtual void Execute()
		{
			output.WriteLine("Base command");
			output.WriteLine($"- {nameof(BaseArgumentA)}:{BaseArgumentA}");
			output.WriteLine($"- {nameof(BaseOptionA)}:{BaseOptionA}");
		}
	}

	[Command("inheritance-1", Parent = typeof(InheritanceCommand))]
	public class CommandChildClass(IOutput output) : CommandBaseClass(output), ICommand
	{
		[Option]
		public string ChildArgumentA { get; set; }

		[Option]
		public string ChildOptionA { get; set; }

		public override void Execute()
		{
			output.WriteLine("Child command");
			output.WriteLine($"- {nameof(BaseArgumentA)}:{BaseArgumentA}");
			output.WriteLine($"- {nameof(BaseOptionA)}:{BaseOptionA}");
			output.WriteLine($"- {nameof(ChildArgumentA)}:{ChildArgumentA}");
			output.WriteLine($"- {nameof(ChildOptionA)}:{ChildOptionA}");
		}
	}

	[Command("inheritance-2", Parent = typeof(InheritanceCommand))]
	public class CommandGrandChildClass(IOutput output) : CommandChildClass(output), ICommand
	{
		[Option]
		public string GrandChildArgumentA { get; set; }

		[Option]
		public string GrandChildOptionA { get; set; }

		public override void Execute()
		{
			output.WriteLine("Grand child command");
			output.WriteLine($"- {nameof(BaseArgumentA)}:{BaseArgumentA}");
			output.WriteLine($"- {nameof(BaseOptionA)}:{BaseOptionA}");
			output.WriteLine($"- {nameof(ChildArgumentA)}:{ChildArgumentA}");
			output.WriteLine($"- {nameof(ChildOptionA)}:{ChildOptionA}");
			output.WriteLine($"- {nameof(GrandChildArgumentA)}:{GrandChildArgumentA}");
			output.WriteLine($"- {nameof(GrandChildOptionA)}:{GrandChildOptionA}");
		}
	}
}