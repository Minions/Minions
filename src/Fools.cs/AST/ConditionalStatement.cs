using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class ConditionalStatement
	{
		public string condition { get; set; }
		public IList<object> statements { get; set; }
	}
}