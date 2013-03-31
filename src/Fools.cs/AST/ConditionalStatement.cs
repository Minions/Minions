using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class ConditionalStatement
	{
		public string condition { get; set; }
		private IList<object> _body_when_true;
		public IList<object> body_when_true { get { return _body_when_true; } set { _body_when_true = value.without_nulls(); } }
	}
}
