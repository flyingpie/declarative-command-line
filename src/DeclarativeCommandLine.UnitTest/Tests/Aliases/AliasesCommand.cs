namespace DeclarativeCommandLine.UnitTest.Tests.Aliases;

[Command(Parent = typeof(TestCommand))]
[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "MvdO:")]
public class AliasesCommand { }
