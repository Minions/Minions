using Fools.Ast;

namespace Fools.Recognizing
{
	public class BlockFinder : Transformation<INode, INode>
	{
		public override void OnNext(INode value)
		{
			SendNext(new UnrecognizedStatement(((Line)value).Tokens));
		}
	}
}