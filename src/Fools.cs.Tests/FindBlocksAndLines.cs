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
some block:
	pass
".find_blocks().should_parse_correctly();
		}
	}
}
