using FluentAssertions;
using Fools.Tokenization;
using NUnit.Framework;

namespace Fools.Tests
{
	[TestFixture]
	public class Tokenization
	{
		[Test]
		public void EmptyFileShouldTokenizeToSingleEmptyStatementWithNoIndentation()
		{
			var tokens = new FoolsTokenStream("");
			tokens.Tokens.Should().Equal(new Token[] {new IndentationToken(0), new EndOfStatementToken()});
		}

		[Test]
		public void OneCharacterFileShouldBeSingleStatementWithThatToken()
		{
			var tokens = new FoolsTokenStream("a");
			tokens.Tokens.Should().Equal(
				new Token[] {new IndentationToken(0), new IdentifierToken("a"), new EndOfStatementToken()});
		}
	}
}
