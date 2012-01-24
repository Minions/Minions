using Fools.Ast;
using NUnit.Framework;

namespace Fools.Tests
{
	[TestFixture, Ignore]
	public class BlockAndStatementParsing
	{
		[SetUp]
		public void SetUp()
		{
			_testSubject = new FoolsStructure();
		}

		[Test]
		public void ParseOneInvalidStatement()
		{
			_AssertParsesAs(
				"some statement",
				new Block(
					new UnrecognizedStatement
					{
						text = "some statement"
					}));
		}

		[Test]
		public void ParseOneAssignmentStatement()
		{
			_AssertParsesAs(
				"foo = 1",
				new Block(
					new AssignmentStatement
					{
						variable = "foo",
						value = new NumberLiteral(1)
					}));
		}

		[Test]
		public void ParseOnePrintStatement()
		{
			_AssertParsesAs(
				"print(foo)",
				new Block(
					new PrintStatement
					{
						variable = "foo"
					}));
		}

		private void _AssertParsesAs(string code, INode parseTree)
		{
			INode actualParse = _testSubject.Parse(code);
			Assert.That(actualParse, Is.EqualTo(parseTree));
		}

		private FoolsStructure _testSubject;
	}
}
