using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
	public class Mission : Declaration
	{
		public enum Purpose
		{
			Normal,
			Preparation,
			CleanUp
		}
		public string name { get; set; }
		private IList<Node> _body;
		public IList<Node> body { get { return _body; } set { _body = value.without_nulls().ToList(); } }
	}
}