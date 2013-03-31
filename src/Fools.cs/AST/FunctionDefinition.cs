using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class FunctionDefinition : Declaration
	{
		public string name { get; set; }
		private IList<Node> _body;
		public IList<Node> body { get { return _body; } set { _body = value.without_nulls(); } }
	}
}
