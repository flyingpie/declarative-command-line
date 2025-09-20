using Microsoft.CodeAnalysis;

namespace DeclarativeCommandLine.Generator;

public class DeclTypes
{
	public DeclTypes(Compilation compilation)
	{
		ICommand = compilation.GetTypeByMetadataName(ICommandTypeName)!;
		IAsyncCommand = compilation.GetTypeByMetadataName(IAsyncCommandTypeName)!;
		IAsyncCommandWithParseResult = compilation.GetTypeByMetadataName(IAsyncCommandWithParseResultTypeName)!;
		ExecutableCommandTypeNames =
		[
			ICommand,
			IAsyncCommand,
			IAsyncCommandWithParseResult,
		];

		CommandAttribute = compilation.GetTypeByMetadataName(CommandAttributeTypeName)!;
		ArgumentAttribute = compilation.GetTypeByMetadataName(ArgumentAttributeTypeName)!;
		OptionAttribute = compilation.GetTypeByMetadataName(OptionAttributeTypeName)!;
	}

	public const string ICommandTypeName = "DeclarativeCommandLine.ICommand";
	public const string IAsyncCommandTypeName = "DeclarativeCommandLine.IAsyncCommand";
	public const string IAsyncCommandWithParseResultTypeName = "DeclarativeCommandLine.IAsyncCommandWithParseResult";


	public const string CommandAttributeTypeName = "DeclarativeCommandLine.CommandAttribute";
	public const string ArgumentAttributeTypeName = "DeclarativeCommandLine.ArgumentAttribute";
	public const string OptionAttributeTypeName = "DeclarativeCommandLine.OptionAttribute";

	public INamedTypeSymbol ICommand { get; set; }

	public INamedTypeSymbol IAsyncCommand { get; set; }

	public INamedTypeSymbol IAsyncCommandWithParseResult { get; set; }

	public INamedTypeSymbol[] ExecutableCommandTypeNames { get; }

	public INamedTypeSymbol CommandAttribute { get; set; }

	public INamedTypeSymbol ArgumentAttribute { get; set; }

	public INamedTypeSymbol OptionAttribute { get; set; }
}