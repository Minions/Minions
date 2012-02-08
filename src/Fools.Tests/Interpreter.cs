using System;
using System.Collections.Generic;
using Fools.Ast;

namespace Fools.Tests
{
	public class Interpreter
	{
		public Interpreter()
		{
			Variables = new Dictionary<string, object>();
		}

		public void evalute(CodeUnit code)
		{
			foreach(INode operation in code.Statements)
			{
				execute(operation);
			}
		}

		private object execute(INode operation)
		{
			if(operation is AssignmentStatement)
			{
				var a = (AssignmentStatement) operation;
				Variables[a.variable] = execute(a.value);
				return null;
			}
			else if(operation is NumberLiteral)
			{
				return ((NumberLiteral) operation).value;
			}
			throw new ArgumentException(String.Format("I don't know how to handle Nodes of type: {0}", operation.GetType()));
		}

		public Dictionary<string, object> Variables { get; private set; }
	}
}
