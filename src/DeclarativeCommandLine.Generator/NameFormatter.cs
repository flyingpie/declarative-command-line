namespace DeclarativeCommandLine.Generator;

public static class NameFormatter
{
	public static string CommandTypeToCommandName(string typeName)
	{
		if (string.IsNullOrWhiteSpace(typeName))
		{
			throw new ArgumentNullException(nameof(typeName));
		}

		var kebabName = ToKebabCase(typeName);

		if (kebabName.EndsWith("command", StringComparison.OrdinalIgnoreCase))
		{
			kebabName = kebabName.Substring(0, kebabName.Length - "command".Length);
		}

		kebabName = kebabName.Trim('-');

		return kebabName;
	}

	public static string PropertyNameToOptionName(string propertyName)
	{
		if (string.IsNullOrWhiteSpace(propertyName))
		{
			throw new ArgumentNullException(nameof(propertyName));
		}

		var kebabName = ToKebabCase(propertyName);

		kebabName = kebabName.Trim('-');

		return $"--{kebabName}";
	}

	private static string ToKebabCase(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			throw new ArgumentNullException(nameof(value));
		}

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