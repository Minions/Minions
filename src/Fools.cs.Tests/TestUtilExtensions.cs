using ApprovalTests;
using Fools.cs.AST;

namespace Fools.cs.Tests
{
	internal static class TestUtilExtensions
	{
		public static void VerifyAst(this ProgramFragment programFragment)
		{
			Approvals.Verify(ProgrammerDisplay.Format(programFragment));
		}
	}

	internal class ProgrammerDisplay
	{
		public static object Format(ProgramFragment programFragment)
		{
			return "\r\n";
		}
	}
}
