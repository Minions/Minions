using Fools.Ast;
using Fools.Recognizing;
using Fools.Tokenization;
using NUnit.Framework;
using System.Reactive.Linq;
using FluentAssertions;

namespace Fools.Tests
{
	[TestFixture]
	public class BlockAndStatementParsing
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = new RecognizeBlocksAndStatements();
		}

		[Test]
		public void ParseOneInvalidStatement()
		{
			_AssertTokensParseAs(
				new UnrecognizedStatement(new IdentifierToken("some"), new IdentifierToken("statement")),
				new IndentationToken(0),
				new IdentifierToken("some"),
				new IdentifierToken("statement"),
				new EndOfStatementToken());
		}

		private void _AssertTokensParseAs(INode expected, params Token[] tokenStream)
		{
			var results = _testSubject.Collect();
			foreach(var token in tokenStream)
			{
				_testSubject.OnNext(token);
			}
			_testSubject.OnCompleted();
			results.Should().Equal(expected);
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

		private RecognizeBlocksAndStatements _testSubject;
	}
}
