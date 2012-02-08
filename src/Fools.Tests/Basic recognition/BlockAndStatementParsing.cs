using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Fools.Ast;
using Fools.Compilation;
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
				Line(0, Identifier("some"), Identifier("block.header"), Identifier(":")),
				Line(1, Identifier("do.something")),
				Line(1, Identifier("do.something.else"))
				)
				.ShouldBeRecognizedAs(
					new Block(
						With.Tokens(Identifier("some"), Identifier("block.header")),
						Statement(Identifier("do.something")),
						Statement(Identifier("do.something.else"))));
		}

		[Test]
		public void ShouldDetectSequentialNonNestedBlocks()
		{
			Lines(
				Line(0, Identifier("some"), Identifier("block.header"), Identifier(":")),
				Line(1, Identifier("pass")),
				Line(0, Identifier("another"), Identifier("block.header"), Identifier(":")),
				Line(1, Identifier("pass"))
				)
				.ShouldBeRecognizedAs(
					new Block(
						With.Tokens(Identifier("some"), Identifier("block.header")),
						Statement(Identifier("pass"))),
					new Block(
						With.Tokens(Identifier("another"), Identifier("block.header")),
						Statement(Identifier("pass"))));
		}

		[Test]
		public void ShouldDetectNestedBlocks()
		{
			Lines(
				Line(0, Identifier("some"), Identifier("block.header"), Identifier(":")),
				Line(1, Identifier("another"), Identifier("block.header"), Identifier(":")),
				Line(2, Identifier("pass"))
				)
				.ShouldBeRecognizedAs(
					new Block(
						With.Tokens(Identifier("some"), Identifier("block.header")),
						new Block(
							With.Tokens(Identifier("another"), Identifier("block.header")),
							Statement(Identifier("pass")))));
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
