using System.Collections;
using System.Collections.Generic;

namespace Fools.cs.AST
{
	public class UnrecognizedBlock : Declaration
	{
		public UnrecognizedBlock(string header, IEnumerable<Node> body)
		{
			this.body = body;
			this.header = header;
		}

		public override void transform_with(DeclarationTransformer transformer)
		{
			transformer.transform(this);
		}

		public string header { get; private set; }
		public IEnumerable<Node> body { get; private set; }
	}
}