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
		public void ParseOneInvalidStatement()
		{
			The.File(
				Line.Containing(Identifier("some"), Identifier("statement"))
				)
				.ShouldBeRecognizedAs(
					new UnrecognizedStatement(new IdentifierToken("some"), new IdentifierToken("statement")));
		}

		//[Test, Ignore]
		//public void ParseOneAssignmentStatement()
		//{
		//   _AssertParsesAs(
		//      "foo = 1",
		//      new AssignmentStatement
		//      {
		//         variable = "foo",
		//         value = new NumberLiteral(1)
		//      });
		//}

		//[Test, Ignore]
		//public void ParseOnePrintStatement()
		//{
		//   _AssertParsesAs(
		//      "print(foo)",
		//      new PrintStatement
		//      {
		//         variable = "foo"
		//      });
		//}

		//private void _AssertParsesAs(string code, INode parseTree)
		//{
		//   INode actualParse = _testSubject.Parse(code);
		//   Assert.That(actualParse, Is.EqualTo(parseTree));
		//}

		private static IdentifierToken Identifier(string value)
		{
			return new IdentifierToken(value);
		}
	}
}
