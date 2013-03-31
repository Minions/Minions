using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class ConditionalStatement : ExecutableStatement
	{
		public string condition { get; set; }
		private IList<Node> _body_when_true;
		public IList<Node> body_when_true { get { return _body_when_true; } set { _body_when_true = value.without_nulls(); } }
	}
}
