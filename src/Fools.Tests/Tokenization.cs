using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Fools.Tokenization;
using NUnit.Framework;

namespace Fools.Tests
{
	public static class TemporaryExtensions
	{
		public static void TokenizesTo(this FoolsTokenStream testSubject, params IEnumerable<Token>[] lines)
		{
			IEnumerable<Token> tokens = lines.Aggregate(Enumerable.Empty<Token>(), (current, line) => current.Concat(line));
			testSubject.Tokens.Should().Equal(tokens);
		}
	}

	[TestFixture]
	public class Tokenization
	{
		[Test]
		public void EmptyFileShouldTokenizeToSingleEmptyStatementWithNoIndentation()
		{
			AssertThat("")
				.TokenizesTo(EmptyLine);
		}

		[Test]
		public void FileContainingOnlyANewlineShouldBeSameAsEmptyFile()
		{
			AssertThat(@"
")
				.TokenizesTo(EmptyLine);
		}

		[Test]
		public void OneCharacterFileShouldBeSingleStatementWithThatToken()
		{
			AssertThat("a")
				.TokenizesTo(
					LineContaining(Identifier("a")));
		}

		[Test]
		public void OneCharacterFileWithFinalNewlineShouldBeEquivalentToOneCharacterFile()
		{
			AssertThat(@"a
")
				.TokenizesTo(
					LineContaining(Identifier("a")));
		}

		[Test]
		public void TwoLineFileShouldResultInTwoStatements()
		{
			AssertThat(@"a
b")
				.TokenizesTo(
					LineContaining(Identifier("a")),
					LineContaining(Identifier("b")));
		}

		[Test]
		public void MultipartIdentifiersShouldComeAsOneToken()
		{
			AssertThat("eh.bee.si.Dee")
				.TokenizesTo(
					LineContaining(Identifier("eh.bee.si.Dee")));
		}

		[Test]
		public void IdentifiersSeparatedBySpacesShouldComeAsTwoTokens()
		{
			AssertThat("eh bee")
				.TokenizesTo(
					LineContaining(Identifier("eh"), Identifier("bee")));
		}

		[Test, Ignore]
		public void AnEscapedNewlineShouldBeTreatedAsAContiuationOfThePreviousLine()
		{
			AssertThat(@"eh\
bee")
				.TokenizesTo(
					LineContaining(Identifier("eh"), Identifier("bee")));
		}

		private static FoolsTokenStream AssertThat(string fileContents)
		{
			return new FoolsTokenStream(fileContents);
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
