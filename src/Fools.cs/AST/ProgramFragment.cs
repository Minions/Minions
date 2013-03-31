using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class ProgramFragment
	{
		public List<Declaration> declarations = new List<Declaration>();
		public List<ErrorReport> errors = new List<ErrorReport>();
	}
}
