using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fools.Tests
{
	[TestFixture]
	public class CompilerAcceptanceTests
	{
		[Test]
		public void PrintThisOut()
		{
//         var fool = @"
//def foo():
//	p";
			var fool = "a=3";
			var interpreter = new Interpreter();
			interpreter.evalute(Compile(fool));
			Assert.That(interpreter.Variables["a"], Is.EqualTo(3));
		}

		private CodeUnit Compile(string fool)
		{
			var node = new FoolsStructure().Parse(fool);
			return new CodeUnit(node);
		}
	}
}