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
			return source.Select(line => new UnrecognizedStatement(((Line) line).Tokens));
		}
	}
}
