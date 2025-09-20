namespace DeclarativeCommandLine.Utils;

public static class NameFormatter
{
	public static string CommandTypeToCommandName(Type type)
	{
		ArgumentNullException.ThrowIfNull(type);

		var typeName = ToKebabCase(type.Name);

		if (typeName.EndsWith("command", StringComparison.OrdinalIgnoreCase))
		{
			typeName = typeName[..^"command".Length];
		}

		typeName = typeName.Trim('-');

		return typeName;
	}

	public static string ToKebabCase(string value)
	{
		ArgumentNullException.ThrowIfNull(value);

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