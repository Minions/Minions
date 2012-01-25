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
		public void FileContainingOnlyANewlineShouldBeSameAsEmptyFile()
		{
			var tokens = new FoolsTokenStream(@"
");
			tokens.Tokens.Should().Equal(EmptyLine);
		}

		[Test]
		public void OneCharacterFileShouldBeSingleStatementWithThatToken()
		{
			var tokens = new FoolsTokenStream("a");
			tokens.Tokens.Should().Equal(LineContaining(Identifier("a")));
		}

		[Test]
		public void OneCharacterFileWithFinalNewlineShouldBeEquivalentToOneCharacterFile()
		{
			var tokens = new FoolsTokenStream(@"a
");
			tokens.Tokens.Should().Equal(LineContaining(Identifier("a")));
		}

		[Test]
		public void TwoLineFileShouldResultInTwoStatements()
		{
			var tokens = new FoolsTokenStream(@"a
b");
			tokens.Tokens.Should().Equal(Lines(LineContaining(Identifier("a")), LineContaining(Identifier("b"))));
		}

		[Test]
		public void MultipartIdentifiersShouldComeAsOneToken()
		{
			var tokens = new FoolsTokenStream("eh.bee.si.Dee");
			tokens.Tokens.Should().Equal(LineContaining(Identifier("eh.bee.si.Dee")));
		}

		[Test]
		public void IdentifiersSeparatedBySpacesShouldComeAsTwoTokens()
		{
			var tokens = new FoolsTokenStream("eh bee");
			tokens.Tokens.Should().Equal(LineContaining(Identifier("eh"), Identifier("bee")));
		}

		[Test, Ignore]
		public void AnEscapedNewlineShouldBeTreatedAsAContiuationOfThePreviousLine()
		{
			var tokens = new FoolsTokenStream(@"eh\
bee");
			tokens.Tokens.Should().Equal(Lines(LineContaining(Identifier("eh"), Identifier("bee"))));
		}

		private IEnumerable<Token> Lines(params IEnumerable<Token>[] lines)
		{
			return lines.Aggregate(Enumerable.Empty<Token>(), (current, line) => current.Concat(line));
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
