using Fools.Ast;
using NUnit.Framework;
using FluentAssertions;

namespace Fools.Tests
{
	[TestFixture, Ignore]
	public class CompilerAcceptanceTests
	{
		[Test]
		public void PrintThisOut()
		{
//         var fool = @"
//def foo():
//	p";
			const string fool = "a=3";
			var interpreter = new Interpreter();
			interpreter.evalute(Compile(fool));
			interpreter.Variables.Should().Contain("a", 3);
		}

		private CodeUnit Compile(string fool)
		{
			//INode node = new FoolsStructure().Parse(fool);
			//return new CodeUnit(node);
			return null;
		}
	}
}
