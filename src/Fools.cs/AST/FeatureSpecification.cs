using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
	public class FeatureSpecification : Declaration
	{
		public string feature { get; set; }
		private IList<Node> _body;
		public IList<Node> body { get { return _body; } set { _body = value.without_nulls().ToList(); } }
	}
}