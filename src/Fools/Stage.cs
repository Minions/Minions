using System;
using System.Reactive.Linq;
using Fools.Ast;
using Fools.Recognizing;
using Fools.Tokenization;

namespace Fools
{
	public static class Stage
	{
		public static IObservable<INode> DetectLines(this IObservable<Token> source)
		{
			var result = new LineDetector();
			source.Subscribe(result);
			return result;
		}

		public static IObservable<INode> RecognizeBlocksAndStatements(this IObservable<INode> source)
		{
			//var result = new BlockFinder();
			//source.Subscribe(result);
			//return result;
			return source.Select(line => new UnrecognizedStatement(((Line)line).Tokens));
		}
	}

	public class BlockFinder : IObserver<INode>, IObservable<INode>
	{
		public void OnNext(INode value)
		{
			throw new NotImplementedException();
		}

		public void OnError(Exception error)
		{
			throw new NotImplementedException();
		}

		public void OnCompleted()
		{
			throw new NotImplementedException();
		}

		public IDisposable Subscribe(IObserver<INode> observer)
		{
			throw new NotImplementedException();
		}
	}
}
