using System.Collections.Generic;
using System.Linq;
using Fools.Ast;
using Fools.Tokenization;

namespace Fools.Recognizing
{
	public class BlockFinder : Transformation<INode, INode>
	{
		private static readonly IdentifierToken Colon = new IdentifierToken(":");
		private readonly Stack<Block> _currentBlocks = new Stack<Block>();

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
			EndBlockIfNeeded(value.IndentationLevel);
			if(value.Contents.Last() == Colon)
				AddBlock(new Block(value.Contents.Take(value.Contents.Count - 1)));
			else
				AddStatementToCurrentBlock(new UnrecognizedStatement(value.Contents));
		}

		private void AddStatementToCurrentBlock(UnrecognizedStatement statement)
		{
			if(_currentBlocks.Count == 0)
				SendNext(statement);
			else
				_currentBlocks.Peek().AddStatement(statement);
		}

		private void EndBlockIfNeeded(int newIndentationLevel)
		{
			if(newIndentationLevel >= _currentBlocks.Count || _currentBlocks.Count == 0) return;
			while (_currentBlocks.Count > 1) _currentBlocks.Pop();
			SendNext(_currentBlocks.Pop());
		}

		private void AddBlock(Block block)
		{
			if(_currentBlocks.Count > 0) _currentBlocks.Peek().AddStatement(block);
			_currentBlocks.Push(block);
		}
	}
}
