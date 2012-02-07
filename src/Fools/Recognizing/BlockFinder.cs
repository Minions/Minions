using System.Linq;
using Fools.Ast;
using Fools.Tokenization;

namespace Fools.Recognizing
{
	public class BlockFinder : Transformation<INode, INode>
	{
		private static readonly IdentifierToken Colon = new IdentifierToken(":");
		private Block _currentBlock;

		public override void OnNext(INode value)
		{
			HandleLine((Line) value);
		}

		public override void OnCompleted()
		{
			if(_currentBlock != null) SendNext(_currentBlock);
			base.OnCompleted();
		}

		private void HandleLine(Line value)
		{
			if(value.Tokens.Last() == Colon)
				AddBlock(new Block(value.Tokens.Take(value.Tokens.Count - 1)));
			else
				AddStatementToCurrentBlock(new UnrecognizedStatement(value.Tokens));
		}

		private void AddStatementToCurrentBlock(UnrecognizedStatement statement)
		{
			if(_currentBlock == null)
				SendNext(statement);
			else
				_currentBlock.AddStatement(statement);
		}

		private void AddBlock(Block block)
		{
			_currentBlock = block;
		}
	}
}
