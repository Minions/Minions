using FluentAssertions;
using Fools.Tokenization;
using NUnit.Framework;

namespace Fools.Tests
{
	[TestFixture]
	public class Tokenization
	{
		[Test]
		public void ShouldGetCorrectIndentationLevelForEmptyLine()
		{
			var tokens = new FoolsTokenStream("");
			tokens.Tokens.Should().Equal(new Token[] {new IndentationToken(0), new EndOfStatementToken()});
		}
	}
}
