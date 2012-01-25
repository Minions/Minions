using System.Collections.Generic;
using System.Linq;
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
			tokens.Tokens.Should().Equal(EmptyLine);
		}

		[Test]
		public void OneCharacterFileShouldBeSingleStatementWithThatToken()
		{
			var tokens = new FoolsTokenStream("a");
			tokens.Tokens.Should().Equal(LineContaining(Identifier("a")));
		}

		private static IEnumerable<Token> EmptyLine { get { return LineContaining(); } }

		private static IEnumerable<Token> LineContaining(params Token[] tokens)
		{
			return new Token[] {new IndentationToken(0)}.Concat(tokens).Concat(new Token[] {new EndOfStatementToken()});
		}

		private static IdentifierToken Identifier(string value)
		{
			return new IdentifierToken(value);
		}
	}
}
