using Pegasus.Common;

namespace Fools.cs
{
	public static class Report
	{
		public static string indentation_error(Cursor state, int expected, int actual, string snippet)
		{
			throw new FatalParseError(new ErrorReport("Inconsistent indentation", state, expected, actual, snippet));
		}
	}
}
