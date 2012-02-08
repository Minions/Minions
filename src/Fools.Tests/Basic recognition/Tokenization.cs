using System.Collections.Generic;
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
			ReadOnlyListSubject<Token> tokensObserved = testSubject.Collect();
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
				.TokenizesTo(With.EmptyLine);
		}

		[Test]
		public void FileContainingOnlyANewlineShouldBeSameAsEmptyFile()
		{
			AssertThat("\n")
				.TokenizesTo(With.EmptyLine);
			AssertThat("\r")
				.TokenizesTo(With.EmptyLine);
			AssertThat("\r\n")
				.TokenizesTo(With.EmptyLine);
		}

		[Test]
		public void OneCharacterFileShouldBeSingleStatementWithThatToken()
		{
			AssertThat("a")
				.TokenizesTo(
					With.Line(Identifier("a")));
		}

		[Test]
		public void UnicodeEscapeSequencesShouldBeAllowedInIdentifiers()
		{
			AssertThat("a\\u4018")
				.TokenizesTo(
					With.Line(Identifier("a\u4018")));
		}

		[Test]
		public void OneCharacterFileWithFinalNewlineShouldBeEquivalentToOneCharacterFile()
		{
			AssertThat("a\n")
				.TokenizesTo(
					With.Line(Identifier("a")));
			AssertThat("a\r")
				.TokenizesTo(
					With.Line(Identifier("a")));
			AssertThat("a\r\n")
				.TokenizesTo(
					With.Line(Identifier("a")));
		}

		[Test]
		public void TwoLineFileShouldResultInTwoStatements()
		{
			AssertThat("a\nb")
				.TokenizesTo(
					With.Line(Identifier("a")),
					With.Line(Identifier("b")));
		}

		[Test]
		public void EscapedNewlineShouldBehaveJustLikeNewlineCharacter()
		{
			AssertThat("a\\nb")
				.TokenizesTo(
					With.Line(Identifier("a")),
					With.Line(Identifier("b")));
		}

		[Test]
		public void ShouldCalculateCorrectIndentationOnEachLine()
		{
			AssertThat("\ta\n\t\tb")
				.TokenizesTo(
					With.Line(1, Identifier("a")),
					With.Line(2, Identifier("b")));
		}

		[Test]
		public void MultipartIdentifiersShouldComeAsOneToken()
		{
			AssertThat("eh.bee.si.Dee")
				.TokenizesTo(
					With.Line(Identifier("eh.bee.si.Dee")));
		}

		[Test]
		public void IdentifiersSeparatedBySpacesShouldComeAsTwoTokens()
		{
			AssertThat("eh bee")
				.TokenizesTo(
					With.Line(Identifier("eh"), Identifier("bee")));
		}

		[Test]
		public void MultipleSpacesShouldBehaveLikeSingleSpaces()
		{
			AssertThat("eh   bee")
				.TokenizesTo(
					With.Line(Identifier("eh"), Identifier("bee")));
		}

		[Test, Ignore]
		public void SpacesAroundPeriodsInIdentifiersShouldBeStripped()
		{
			AssertThat("eh. bee .si .  Dee")
				.TokenizesTo(
					With.Line(Identifier("eh.bee.si.Dee")));
		}

		[Test]
		public void AnEscapedNewlineShouldBeTreatedAsAContiuationOfThePreviousLine()
		{
			AssertThat("eh\\\nbee")
				.TokenizesTo(
					With.Line(Identifier("eh"), Identifier("bee")));
			AssertThat("eh\\\rbee")
				.TokenizesTo(
					With.Line(Identifier("eh"), Identifier("bee")));
			AssertThat("eh\\\r\nbee")
				.TokenizesTo(
					With.Line(Identifier("eh"), Identifier("bee")));
		}

		[Test]
		public void LeadingWhitespaceShouldBeIgnoredOnContinuationLines()
		{
			AssertThat("eh\\\n  \t \t \t\v\t bee")
				.TokenizesTo(
					With.Line(Identifier("eh"), Identifier("bee")));
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
