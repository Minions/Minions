using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Fools.cs.Tests
{
	[TestFixture]
	public class FindBlocksAndLines
	{
		[Test]
		public void ShouldFindNothingInAnEmptyFile()
		{
			FoolsParser.FindBlocks(string.Empty).Should().BeEmpty();
		}

	}

	public class FoolsParser
	{
		public static IEnumerable<string> FindBlocks(string sourceCode)
		{
			return Enumerable.Empty<string>();
		}
	}
}
