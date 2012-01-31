using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Fools.Ast;
using Fools.Recognizing;
using Fools.Tests.Support;
using Fools.Tokenization;
using Fools.Utils;
using NUnit.Framework;

namespace Fools.Tests
{
	public static class BlockAndStatementParsingHelpers
	{
		public static void ShouldBeRecognizedAs(this IEnumerable<Token> tokenStream, params INode[] expected)
		{
			var testSubject = new RecognizeBlocksAndStatements();
			ReadOnlyListSubject<INode> results = testSubject.Collect();
			tokenStream.SendTo(testSubject);
			results.Should().Equal(expected);
		}
	}

	[TestFixture]
	public class BlockAndStatementParsing
	{
		[Test]
		public void ShouldDetectASimpleStatement()
		{
			The.File(
				With.Line(Identifier("some"), Identifier("statement"))
				)
				.ShouldBeRecognizedAs(
					Statement(Identifier("some"), Identifier("statement")));
		}

		[Test, Ignore]
		public void ShouldDetectANonNestedBlock()
		{
			The.File(
				With.Line(Identifier("some"), Identifier("statement"), Identifier(":")),
				With.Line(1, Identifier("pass"))
				)
				.ShouldBeRecognizedAs(
					new Block(
						With.Tokens(Identifier("some"), Identifier("statement")),
						Statement(Identifier("pass"))));
		}

		private static IdentifierToken Identifier(string value)
		{
			return new IdentifierToken(value);
		}

		private static UnrecognizedStatement Statement(params Token[] tokens)
		{
			return new UnrecognizedStatement(tokens);
		}
	}
}
