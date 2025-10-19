using System.Diagnostics.CodeAnalysis;

namespace DeclarativeCommandLine.Generator;

public static class CompilationExtensions
{
	public static bool EqualsNamedSymbol(this INamedTypeSymbol s1, INamedTypeSymbol s2) => SymbolEqualityComparer.Default.Equals(s1, s2);

	public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamedTypeSymbol type)
	{
		if (type == null)
		{
			throw new ArgumentNullException(nameof(type));
		}

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

	public static T?[] GetNamedArgumentArray<T>(this ICollection<KeyValuePair<string, TypedConstant>> arguments, string name)
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

		return [.. arg.Value.Values.Select(v => (T?)v.Value)];
	}

	[SuppressMessage(
		"Globalization",
		"CA1308:Normalize strings to uppercase",
		Justification = "MvdO: We want the version of the boolean as it should appear in C#, so 'true' or 'false'.")]
	public static string ToCSharpBoolString(this bool b) => b.ToString().ToLowerInvariant();
}