namespace DeclarativeCommandLine;

public abstract class BaseCommandAttribute : Attribute
{
	public string? Alias
	{
		// Note that we don't actually use the single version get (only the array version),
		// but otherwise the property cannot be used in the attribute.
		get => null;
		set
		{
			if (value != null)
			{
				Aliases = new[] { value };
			}
		}
	}

	public string[]? Aliases { get; set; }

	public string? Description { get; set; }

	public bool IsHidden { get; set; }

	public string? Name { get; set; }

	public bool TreatUnmatchedTokensAsErrors { get; set; }
}