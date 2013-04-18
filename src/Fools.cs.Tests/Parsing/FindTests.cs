using ApprovalTests.Reporters;
using NUnit.Framework;
using Fools.cs.Tests.Support;

namespace Fools.cs.Tests.Parsing
{
	[TestFixture, UseReporter(typeof(DiffReporter))]
	public class FindTests
	{
		[Test]
		public void should_locate_test_cases()
		{
			@"
specify feature the test framework:
	requires a simple empty test should pass:
		pass
".find_blocks().should_parse_correctly();
		}

	}
}
