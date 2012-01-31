using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
			ReadOnlyListSubject<Token> tokensObserved = testSubject.Collect();
			testSubject.Read();
			tokensObserved.Should().Equal(tokens);
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
			AssertThat("\n")
				.TokenizesTo(EmptyLine);
			AssertThat("\r")
				.TokenizesTo(EmptyLine);
			AssertThat("\r\n")
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
		public void UnicodeEscapeSequencesShouldBeAllowedInIdentifiers()
		{
			AssertThat("a\\u4018")
				.TokenizesTo(
					LineContaining(Identifier("a\u4018")));
		}

		[Test]
		public void OneCharacterFileWithFinalNewlineShouldBeEquivalentToOneCharacterFile()
		{
			AssertThat("a\n")
				.TokenizesTo(
					LineContaining(Identifier("a")));
			AssertThat("a\r")
				.TokenizesTo(
					LineContaining(Identifier("a")));
			AssertThat("a\r\n")
				.TokenizesTo(
					LineContaining(Identifier("a")));
		}

		[Test]
		public void TwoLineFileShouldResultInTwoStatements()
		{
			AssertThat("a\nb")
				.TokenizesTo(
					LineContaining(Identifier("a")),
					LineContaining(Identifier("b")));
		}

		[Test]
		public void ShouldCalculateCorrectIndentationOnEachLine()
		{
			AssertThat("\ta\n\t\tb")
				.TokenizesTo(
					LineContaining(1, Identifier("a")),
					LineContaining(2, Identifier("b")));
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

		[Test]
		public void MultipleSpacesShouldBehaveLikeSingleSpaces()
		{
			AssertThat("eh   bee")
				.TokenizesTo(
					LineContaining(Identifier("eh"), Identifier("bee")));
		}

		[Test, Ignore]
		public void SpacesAroundPeriodsInIdentifiersShouldBeStripped()
		{
			AssertThat("eh. bee .si .  Dee")
				.TokenizesTo(
					LineContaining(Identifier("eh.bee.si.Dee")));
		}

		[Test]
		public void AnEscapedNewlineShouldBeTreatedAsAContiuationOfThePreviousLine()
		{
			AssertThat("eh\\\nbee")
				.TokenizesTo(
					LineContaining(Identifier("eh"), Identifier("bee")));
			AssertThat("eh\\\rbee")
				.TokenizesTo(
					LineContaining(Identifier("eh"), Identifier("bee")));
			AssertThat("eh\\\r\nbee")
				.TokenizesTo(
					LineContaining(Identifier("eh"), Identifier("bee")));
		}

		[Test]
		public void LeadingWhitespaceShouldBeIgnoredOnContinuationLines()
		{
			AssertThat("eh\\\n  \t \t \t\v\t bee")
				.TokenizesTo(
					LineContaining(Identifier("eh"), Identifier("bee")));
		}

		private static FoolsTokenStream AssertThat(string fileContents)
		{
			return new FoolsTokenStream(fileContents);
		}

		private static IEnumerable<Token> EmptyLine { get { return LineContaining(); } }

		private static IEnumerable<Token> LineContaining(int indentationLevel, params Token[] tokens)
		{
			return
				new Token[] {new IndentationToken(indentationLevel)}.Concat(tokens).Concat(new Token[] {new EndOfStatementToken()});
		}

		private static IEnumerable<Token> LineContaining(params Token[] tokens)
		{
			return LineContaining(0, tokens);
		}

		private static IdentifierToken Identifier(string value)
		{
			return new IdentifierToken(value);
		}
	}
}
