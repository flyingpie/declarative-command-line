namespace DeclarativeCommandLine.Generator;

public static class CompilationExtensions
{
	public static bool EqualsNamedSymbol(this INamedTypeSymbol s1, INamedTypeSymbol s2) => SymbolEqualityComparer.Default.Equals(s1, s2);
}