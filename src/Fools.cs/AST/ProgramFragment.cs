using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class ProgramFragment
	{
		public List<Node> declarations = new List<Node>();
		public List<ErrorReport> errors = new List<ErrorReport>();
	}
}
