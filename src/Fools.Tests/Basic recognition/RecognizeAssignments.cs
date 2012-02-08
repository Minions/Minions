using FluentAssertions;
using Fools.Ast;
using Fools.Compilation.Tokenization;
using Fools.Tests.Support;
using NUnit.Framework;

namespace Fools.Tests.Basic_recognition
{
	[TestFixture]
	public class RecognizeAssignments
	{
		[Test, Ignore]
		public void ShouldDetectSimpleAssignmentFromNumericLiteral()
		{
			UnrecognizedStatement source = With.Statement(Identifier("variable"), Identifier("="), Identifier("value"));
			FindAssignments(source).Should().Be(Assigment(Identifier("value"), Identifier("variable")));
		}

		[Test]
		public void ShouldLeaveNonAssignmentUnchanged()
		{
			UnrecognizedStatement source = With.Statement(Identifier("a.b"), Identifier(","), Identifier("c.d"));
			FindAssignments(source).Should().Be(source);
		}

		private IStatement FindAssignments(UnrecognizedStatement source)
		{
			return source;
		}

		private AssignmentStatement Assigment(IdentifierToken from, IdentifierToken to)
		{
			return new AssignmentStatement
			       {value = new VariableReferenceExpression(@from), variable = new VariableReferenceExpression(to)};
		}

		private static IdentifierToken Identifier(string value)
		{
			return new IdentifierToken(value);
		}
	}
}
