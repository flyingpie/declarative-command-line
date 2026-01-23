using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DeclarativeCommandLine.Generator;

public static class SystemExtensions
{
	public static string ToEscapedCSharpString(this string source) =>
		source == null
			? throw new ArgumentNullException(nameof(source))
			: SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(source)).ToFullString();

	[SuppressMessage(
		"Globalization",
		"CA1308:Normalize strings to uppercase",
		Justification = "MvdO: We want the version of the boolean as it should appear in C#, so 'true' or 'false'."
	)]
	public static string ToCSharpBoolString(this bool b) => b.ToString().ToLowerInvariant();
}
