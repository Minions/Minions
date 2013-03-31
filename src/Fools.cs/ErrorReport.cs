using Pegasus.Common;

namespace Fools.cs
{
	public class ErrorReport
	{
		public string file_name { get; private set; }
		public int line { get; private set; }
		public string error_type { get; private set; }
		public int expected { get; private set; }
		public int actual { get; private set; }
		public string snippet { get; private set; }

		public override string ToString()
		{
			return
				string.Format(
					"{4} on line {2}.\r\n\tExpected code to be indented {0}, but it was indented {1}.\r\n\tThe code reads:\r\n\"{3}\"",
					expected,
					actual,
					line,
					snippet,
					error_type);
		}

		public ErrorReport(string error_type, Cursor state, int expected, int actual, string snippet)
		{
			line = state.Line;
			file_name = state.FileName;
			this.error_type = error_type;
			this.expected = expected;
			this.actual = actual;
			this.snippet = snippet;
		}
	}
}