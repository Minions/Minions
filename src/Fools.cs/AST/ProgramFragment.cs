using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class ProgramFragment
	{
		public List<object> declarations = new List<object>();
		public List<ErrorReport> errors = new List<ErrorReport>();
	}
}
