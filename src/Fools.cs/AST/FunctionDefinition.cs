using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class FunctionDefinition
	{
		public string name { get; set; }
		public IList<object> body { get; set; }
	}
}