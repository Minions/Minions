using System.Collections.Generic;
using System.Reflection.Emit;
using ApprovalTests;
using ApprovalTests.Reporters;
using Fools.Ast;
using MetaSharp.Transformation;
using NUnit.Framework;

namespace Fools.Tests
{
	[TestFixture, UseReporter(typeof(DiffReporter))]
	public class EmitCode
	{
		[Test] public void assignment() { ApproveResultOfExecution("a=3"); }

		private static void ApproveResultOfExecution(string fool)
		{
			var frame = _evaluate_and_return_frame(fool);
			Approvals.Approve(frame, kv => string.Format("{0} = {1} [{2}]", kv.Key, kv.Value, kv.Value.GetType()));
		}

		private static Dictionary<string, object> _evaluate_and_return_frame(string fool)
		{
			var node = new FoolsStructure().Parse(fool);
			var method = _make_an_assignment(node);
			var frame = new Dictionary<string, object>();
			method.Invoke(null, new[] {frame});
			return frame;
		}

		private static DynamicMethod _make_an_assignment(INode node)
		{
			var factory = new MethodBuilder();
			factory.AddAssignmentStatement(node as AssignmentStatement);
			return factory.ToCode();
		}
	}
}
