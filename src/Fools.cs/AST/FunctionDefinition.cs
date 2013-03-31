using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class FunctionDefinition
	{
		public string name { get; set; }
		private IList<object> _body;
		public IList<object> body { get { return _body; } set { _body = value.without_nulls(); } }
	}
}
