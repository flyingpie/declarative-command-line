using Microsoft.CodeAnalysis;

namespace DeclarativeCommandLine.Generator;

public class DeclTypes(Compilation compilation)
{
	public const string CommandTypeName = "DeclarativeCommandLine.CommandAttribute";
	public const string ArgumentTypeName = "DeclarativeCommandLine.ArgumentAttribute";
	public const string OptionTypeName = "DeclarativeCommandLine.OptionAttribute";

	public INamedTypeSymbol CommandAttribute { get; set; } = compilation.GetTypeByMetadataName(CommandTypeName)!;

	public INamedTypeSymbol ArgumentAttribute { get; set; } = compilation.GetTypeByMetadataName(ArgumentTypeName)!;

	public INamedTypeSymbol OptionAttribute { get; set; } = compilation.GetTypeByMetadataName(OptionTypeName)!;
}