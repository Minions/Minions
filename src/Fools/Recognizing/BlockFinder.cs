using System.Linq;
using Fools.Ast;
using Fools.Tokenization;

namespace Fools.Recognizing
{
	public class BlockFinder : Transformation<INode, INode>
	{
		private static readonly IdentifierToken Colon = new IdentifierToken(":");
		private Block _currentBlock;
		private int _currentIndent = 0;

		public override void OnNext(INode value)
		{
			HandleLine((Line) value);
		}

		public override void OnCompleted()
		{
			EndBlockIfNeeded(0);
			base.OnCompleted();
		}

		private void HandleLine(Line value)
		{
			if(value.Contents.Last() == Colon)
				AddBlock(new Block(value.Contents.Take(value.Contents.Count - 1)));
			else
				AddStatementToCurrentBlock(new UnrecognizedStatement(value.Contents), value.IndentationLevel);
		}

		private void AddStatementToCurrentBlock(UnrecognizedStatement statement, int indentationLevel)
		{
			EndBlockIfNeeded(indentationLevel);
			if(_currentBlock == null)
				SendNext(statement);
			else
				_currentBlock.AddStatement(statement);
		}

		private void EndBlockIfNeeded(int newIndentationLevel)
		{
			if(newIndentationLevel >= _currentIndent || _currentBlock == null) return;
			SendNext(_currentBlock);
			_currentBlock = null;
			_currentIndent = newIndentationLevel;
		}

		private void AddBlock(Block block)
		{
			_currentBlock = block;
			_currentIndent += 1;
		}
	}
}
