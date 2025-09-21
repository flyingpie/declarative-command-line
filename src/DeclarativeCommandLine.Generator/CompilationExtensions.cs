namespace DeclarativeCommandLine.Generator;

public static class CompilationExtensions
{
	public static bool EqualsNamedSymbol(this INamedTypeSymbol s1, INamedTypeSymbol s2) => SymbolEqualityComparer.Default.Equals(s1, s2);

	public static object? GetNamedArgument(this ICollection<KeyValuePair<string, TypedConstant>> arguments, string name)
	{
		if (arguments == null)
		{
			throw new ArgumentNullException(nameof(arguments));
		}

		if (string.IsNullOrWhiteSpace(nameof(name)))
		{
			throw new ArgumentNullException(nameof(name));
		}

		return arguments.FirstOrDefault(a => a.Key.Equals(name, StringComparison.Ordinal)).Value.Value;
	}

	public static string ToCSharpBoolString(this bool b) => b.ToString().ToLowerInvariant();
}