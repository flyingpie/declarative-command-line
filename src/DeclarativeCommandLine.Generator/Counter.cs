namespace DeclarativeCommandLine.Generator;

/// <summary>
/// Used to keep track of unique numeric id's assigned to commands, arguments, directives and options.<br/>
/// These ids are then used for variable names.
/// </summary>
public static class Counter
{
	private static int _num;

	public static int Next()
	{
		Interlocked.Increment(ref _num);

		return _num;
	}

	public static void Reset() => _num = 0;
}
