using System;
using Fools.Ast;
using Fools.Tokenization;

namespace Fools
{
	public class UnderstandFools : IObservable<INode>
	{
		private readonly FoolsTokenStream _pipelineStart;
		private readonly IObservable<INode> _parser;

		public UnderstandFools(string fool)
		{
			_pipelineStart = new FoolsTokenStream(fool);
			_parser = _pipelineStart
				.DetectLines();
		}

		public IDisposable Subscribe(IObserver<INode> observer)
		{
			return _parser.Subscribe(observer);
		}

		public void Go()
		{
			_pipelineStart.Read();
		}
	}
}
