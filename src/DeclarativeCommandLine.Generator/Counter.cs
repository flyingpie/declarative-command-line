using System.Threading;

namespace DeclarativeCommandLine.Generator;

public static class Counter
{
	public static int _num;

	public static int Next()
	{
		Interlocked.Increment(ref _num);

		return _num;
	}

	public static void Reset() => _num = 0;
}