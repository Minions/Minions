using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Fools.Ast;
using Fools.Tests.BlocksAndStatements;
using Fools.Tests.Support;
using Fools.Tokenization;
using NUnit.Framework;

namespace Fools.Tests
{
	[TestFixture]
	public class BlockAndStatementParsing
	{
		[Test]
		public void ShouldDetectASimpleStatement()
		{
			Lines(
				Line(0, Identifier("some"), Identifier("statement"))
				)
				.ShouldBeRecognizedAs(
					Statement(Identifier("some"), Identifier("statement")));
		}

		[Test]
		public void ShouldDetectANonNestedBlock()
		{
			Lines(
				Line(0, Identifier("some"), Identifier("statement"), Identifier(":")),
				Line(1, Identifier("pass"))
				)
				.ShouldBeRecognizedAs(
					new Block(
						With.Tokens(Identifier("some"), Identifier("statement")),
						Statement(Identifier("pass"))));
		}

		[Test]
		public void ShouldDetectSequentialNonNestedBlocks()
		{
			Lines(
				Line(0, Identifier("some"), Identifier("statement"), Identifier(":")),
				Line(1, Identifier("pass")),
				Line(0, Identifier("another"), Identifier("statement"), Identifier(":")),
				Line(1, Identifier("pass"))
				)
				.ShouldBeRecognizedAs(
					new Block(
						With.Tokens(Identifier("some"), Identifier("statement")),
						Statement(Identifier("pass"))),
					new Block(
						With.Tokens(Identifier("another"), Identifier("statement")),
						Statement(Identifier("pass"))));
		}

		private static IEnumerable<Line> Lines(params Line[] lines)
		{
			return lines;
		}

		private static Line Line(int indentationLevel, params Token[] contents)
		{
			return new Line(indentationLevel, contents);
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

namespace Fools.Tests.BlocksAndStatements
{
	public static class BlockAndStatementParsingHelpers
	{
		public static void ShouldBeRecognizedAs(this IEnumerable<Line> tokenStream, params INode[] expected)
		{
			var source = new ObserveLists<INode>();
			ReadOnlyListSubject<INode> results = source.RecognizeBlocksAndStatements().Collect();
			source.Send(tokenStream);
			results.Should().Equal(expected);
		}
	}
}
