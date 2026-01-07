namespace DeclarativeCommandLine.Generator;

public static class CompilationExtensions
{
	public static bool EqualsNamedSymbol(this INamedTypeSymbol s1, INamedTypeSymbol s2) => SymbolEqualityComparer.Default.Equals(s1, s2);

	public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamedTypeSymbol type)
	{
		return type == null ? throw new ArgumentNullException(nameof(type)) : GetAllTypesInternal(type);
	}

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

	public static IReadOnlyCollection<T> GetNamedArgumentArray<T>(
		this ICollection<KeyValuePair<string, TypedConstant>> arguments,
		string name
	)
	{
		if (arguments == null)
		{
			throw new ArgumentNullException(nameof(arguments));
		}

		if (string.IsNullOrWhiteSpace(nameof(name)))
		{
			throw new ArgumentNullException(nameof(name));
		}

		var arg = arguments.FirstOrDefault(a => a.Key.Equals(name, StringComparison.Ordinal));

		if (arg.Value.Kind != TypedConstantKind.Array)
		{
			return [];
		}

		if (arg.Value.IsNull)
		{
			return [];
		}

		return [.. arg.Value.Values.Select(v => (T?)v.Value).Where(v => v is not null).Select(v => v!)];
	}

	private static IEnumerable<INamedTypeSymbol> GetAllTypesInternal(this INamedTypeSymbol type)
	{
		yield return type;

		if (type.BaseType == null)
		{
			yield break;
		}

		foreach (var res in type.BaseType.GetAllTypes())
		{
			yield return res;
		}
	}
}
