using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Fools.cs.Tests
{
	[TestFixture, UseReporter(typeof(DiffReporter))]
	public class FindBlocksAndLines
	{
		[Test]
		public void ShouldFindNothingInAnEmptyFile()
		{
			FoolsParser.FindBlocks(string.Empty).VerifyAst();
		}
	}
}
