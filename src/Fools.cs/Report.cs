using Pegasus.Common;

namespace Fools.cs
{
	public static class Report
	{
		public static string indentation_error(Cursor state, int expected, int actual, string snippet)
		{
			throw new FatalParseError(new ErrorReport("Inconsistent indentation", state, expected, actual, snippet, "Doublecheck the amount of indentation on this line and on the previous line. The most common cause of this error is accidentally de-indenting a line, which will cause the next indented line to fail."));
		}

		public static string indent_with_spaces_error(Cursor state, string snippet)
		{
			throw new FatalParseError(new ErrorReport("Spaces used in indentation", state, "tabs only", "spaces", snippet, "All lines must be indented using only tabs. Each tab character represents one level of block nesting. Lines cannot be aligned with middle parts of previous lines: it is an error to have any spaces after the indentation tabs. This ensures that the visual indent on the left of the line always aligns with its semantic meaning, regardless of editor settings."));
		}

		public static string unrecognized_statement(Cursor state, string snippet)
		{
			throw new FatalParseError(new ErrorReport("Unrecognized statement", state, null, null, snippet, "I could not figure out this statement. Sorry that I can't give you a more useful error message. Perhaps you need to make Fools smarter?"));
		}
	}
}
