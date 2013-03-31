using ApprovalTests.Reporters;
using Fools.cs.AST;
using NUnit.Framework;

namespace Fools.cs.Tests
{
	[TestFixture, UseReporter(typeof(DiffReporter))]
	public class FindBlocksAndLines
	{
		[Test]
		public void should_find_nothing_in_an_empty_file()
		{
			string.Empty.find_blocks().should_parse_correctly();
		}

		[Test]
		public void file_with_a_single_empty_block_should_parse()
		{
			@"
def funky_function:
	a = b
	

c=a".find_blocks().should_parse_correctly();
		}

		[Test]
		public void should_reject_illegal_indentation_with_good_error_message()
		{
			"b = c\r\n\ta = b".find_blocks().should_fail();
		}

		[Test]
		public void spaces_at_start_of_a_line_should_be_illegal()
		{
			" b = c".find_blocks().should_fail();
		}

		[Test]
		public void spaces_in_the_indentation_should_be_illegal()
		{
			"if foo:\r\n\tif bar:\r\n\t \tb = c".find_blocks().should_fail();
		}
	}
}
