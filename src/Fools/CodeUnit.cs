using System.Collections.Generic;
using Fools.Ast;

namespace Fools
{
	public class CodeUnit {
		private readonly INode _node;

		public CodeUnit(INode node)
		{
			_node = node;
		}

		public IEnumerable<INode> Statements
		{
			get { yield return _node; }
		}
	}
}