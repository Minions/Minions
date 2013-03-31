using Fools.cs.AST;
using Pegasus.Common;

namespace Fools.cs
{
	public class Report
	{
		private readonly ProgramFragment _result;

		public Report(ProgramFragment result)
		{
			_result = result;
		}

		public bool indentation_error(Cursor state, int expected, int actual, string snippet)
		{
			return error(
				new ErrorReport(
					"Inconsistent indentation",
					state,
					expected,
					actual,
					snippet,
					"Doublecheck the amount of indentation on this line and on the previous line. The most common cause of this error is accidentally de-indenting a line, which will cause the next indented line to fail."));
		}

		public bool indent_with_spaces_error(Cursor state, string snippet)
		{
			return fatal(
				new ErrorReport(
					"Spaces used in indentation",
					state,
					"tabs only",
					"spaces",
					snippet,
					"All lines must be indented using only tabs. Each tab character represents one level of block nesting. Lines cannot be aligned with middle parts of previous lines: it is an error to have any spaces after the indentation tabs. This ensures that the visual indent on the left of the line always aligns with its semantic meaning, regardless of editor settings."));
		}

		public bool implicitly_empty_block(Cursor state, string snippet)
		{
			return error(
				new ErrorReport(
					"Block without a body",
					state,
					null,
					null,
					snippet,
					"This block appears to have no body. If you are meaning to state an empty block, please use an explicit pass statement as the block's body."));
		}

		public bool unrecognized_statement(Cursor state, string snippet)
		{
			return error(
				new ErrorReport(
					"Unrecognized statement",
					state,
					null,
					null,
					snippet,
					"I could not figure out this statement. Sorry that I can't give you a more useful error message. Perhaps you need to make Fools smarter?"));
		}

		private bool error(ErrorReport error_report)
		{
			_result.errors.Add(error_report);
			return false;
		}

		private bool fatal(ErrorReport error_report)
		{
			throw new FatalParseError(error_report);
		}
	}
}
