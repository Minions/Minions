using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Fools.Tests.Support;
using Fools.Tokenization;
using Fools.Utils;
using NUnit.Framework;

namespace Fools.Tests
{
	public static class TemporaryExtensions
	{
		public static void TokenizesTo(this FoolsTokenStream testSubject, params IEnumerable<Token>[] lines)
		{
			var tokensObserved = testSubject.Collect();
			testSubject.Read();
			tokensObserved.Should().Equal(lines.Flatten());
		}
	}

	[TestFixture]
	public class Tokenization
	{
		[Test]
		public void EmptyFileShouldTokenizeToSingleEmptyStatementWithNoIndentation()
		{
			AssertThat("")
				.TokenizesTo(Line.Empty);
		}

		[Test]
		public void FileContainingOnlyANewlineShouldBeSameAsEmptyFile()
		{
			AssertThat("\n")
				.TokenizesTo(Line.Empty);
			AssertThat("\r")
				.TokenizesTo(Line.Empty);
			AssertThat("\r\n")
				.TokenizesTo(Line.Empty);
		}

		[Test]
		public void OneCharacterFileShouldBeSingleStatementWithThatToken()
		{
			AssertThat("a")
				.TokenizesTo(
					Line.Containing(Identifier("a")));
		}

		[Test]
		public void UnicodeEscapeSequencesShouldBeAllowedInIdentifiers()
		{
			AssertThat("a\\u4018")
				.TokenizesTo(
					Line.Containing(Identifier("a\u4018")));
		}

		[Test]
		public void OneCharacterFileWithFinalNewlineShouldBeEquivalentToOneCharacterFile()
		{
			AssertThat("a\n")
				.TokenizesTo(
					Line.Containing(Identifier("a")));
			AssertThat("a\r")
				.TokenizesTo(
					Line.Containing(Identifier("a")));
			AssertThat("a\r\n")
				.TokenizesTo(
					Line.Containing(Identifier("a")));
		}

		[Test]
		public void TwoLineFileShouldResultInTwoStatements()
		{
			AssertThat("a\nb")
				.TokenizesTo(
					Line.Containing(Identifier("a")),
					Line.Containing(Identifier("b")));
		}

		[Test]
		public void EscapedNewlineShouldBehaveJustLikeNewlineCharacter()
		{
			AssertThat("a\\nb")
				.TokenizesTo(
					Line.Containing(Identifier("a")),
					Line.Containing(Identifier("b")));
		}

		[Test]
		public void ShouldCalculateCorrectIndentationOnEachLine()
		{
			AssertThat("\ta\n\t\tb")
				.TokenizesTo(
					Line.Containing(1, Identifier("a")),
					Line.Containing(2, Identifier("b")));
		}

		[Test]
		public void MultipartIdentifiersShouldComeAsOneToken()
		{
			AssertThat("eh.bee.si.Dee")
				.TokenizesTo(
					Line.Containing(Identifier("eh.bee.si.Dee")));
		}

		[Test]
		public void IdentifiersSeparatedBySpacesShouldComeAsTwoTokens()
		{
			AssertThat("eh bee")
				.TokenizesTo(
					Line.Containing(Identifier("eh"), Identifier("bee")));
		}

		[Test]
		public void MultipleSpacesShouldBehaveLikeSingleSpaces()
		{
			AssertThat("eh   bee")
				.TokenizesTo(
					Line.Containing(Identifier("eh"), Identifier("bee")));
		}

		[Test, Ignore]
		public void SpacesAroundPeriodsInIdentifiersShouldBeStripped()
		{
			AssertThat("eh. bee .si .  Dee")
				.TokenizesTo(
					Line.Containing(Identifier("eh.bee.si.Dee")));
		}

		[Test]
		public void AnEscapedNewlineShouldBeTreatedAsAContiuationOfThePreviousLine()
		{
			AssertThat("eh\\\nbee")
				.TokenizesTo(
					Line.Containing(Identifier("eh"), Identifier("bee")));
			AssertThat("eh\\\rbee")
				.TokenizesTo(
					Line.Containing(Identifier("eh"), Identifier("bee")));
			AssertThat("eh\\\r\nbee")
				.TokenizesTo(
					Line.Containing(Identifier("eh"), Identifier("bee")));
		}

		[Test]
		public void LeadingWhitespaceShouldBeIgnoredOnContinuationLines()
		{
			AssertThat("eh\\\n  \t \t \t\v\t bee")
				.TokenizesTo(
					Line.Containing(Identifier("eh"), Identifier("bee")));
		}

		private static FoolsTokenStream AssertThat(string fileContents)
		{
			return new FoolsTokenStream(fileContents);
		}

		private static IdentifierToken Identifier(string value)
		{
			return new IdentifierToken(value);
		}
	}
}
