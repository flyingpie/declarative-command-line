using System;
using System.Text;

namespace DeclarativeCommandLine.Generator;

public static class NameFormatter
{
	public static string CommandTypeToCommandName(string typeName)
	{
		// ArgumentNullException.ThrowIfNull(type);

		var kebabName = ToKebabCase(typeName);

		if (kebabName.EndsWith("command", StringComparison.OrdinalIgnoreCase))
		{
			// kebabName = kebabName[..^"command".Length];
			kebabName = kebabName.Substring(0, kebabName.Length - "command".Length);
		}

		kebabName = kebabName.Trim('-');

		return kebabName;
	}

	public static string ToKebabCase(string value)
	{
		// ArgumentNullException.ThrowIfNull(value);

		var sb = new StringBuilder();

		for (var i = 0; i < value.Length; i++)
		{
			var c = value[i];

			if (i > 0 && char.IsUpper(c))
			{
				sb.Append('-');
			}

			sb.Append(char.ToLowerInvariant(value[i]));
		}

		return sb.ToString();
	}
}
