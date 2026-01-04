using System.Diagnostics.CodeAnalysis;

namespace DeclarativeCommandLine.Generator;

[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "MvdO: Named according to the type they represent.")]
public class DeclTypes
{
	private const string ICommandTypeName = "DeclarativeCommandLine.ICommand";
	private const string IAsyncCommandTypeName = "DeclarativeCommandLine.IAsyncCommand";
	private const string IAsyncCommandWithParseResultTypeName = "DeclarativeCommandLine.IAsyncCommandWithParseResult";

	private const string CommandAttributeTypeName = "DeclarativeCommandLine.CommandAttribute";
	private const string ArgumentAttributeTypeName = "DeclarativeCommandLine.ArgumentAttribute";
	private const string OptionAttributeTypeName = "DeclarativeCommandLine.OptionAttribute";

	public DeclTypes(Compilation compilation)
	{
		if (compilation == null)
		{
			throw new ArgumentNullException(nameof(compilation));
		}

		ICommand = compilation.GetTypeByMetadataName(ICommandTypeName)!;
		IAsyncCommand = compilation.GetTypeByMetadataName(IAsyncCommandTypeName)!;
		IAsyncCommandWithParseResult = compilation.GetTypeByMetadataName(IAsyncCommandWithParseResultTypeName)!;
		ExecutableCommandTypeNames = [ICommand, IAsyncCommand, IAsyncCommandWithParseResult];

		CommandAttribute = compilation.GetTypeByMetadataName(CommandAttributeTypeName)!;
		ArgumentAttribute = compilation.GetTypeByMetadataName(ArgumentAttributeTypeName)!;
		OptionAttribute = compilation.GetTypeByMetadataName(OptionAttributeTypeName)!;
	}

	public INamedTypeSymbol ICommand { get; set; }

	public INamedTypeSymbol IAsyncCommand { get; set; }

	public INamedTypeSymbol IAsyncCommandWithParseResult { get; set; }

	public IReadOnlyCollection<INamedTypeSymbol> ExecutableCommandTypeNames { get; }

	public INamedTypeSymbol CommandAttribute { get; set; }

	public INamedTypeSymbol ArgumentAttribute { get; set; }

	public INamedTypeSymbol OptionAttribute { get; set; }
}
