using System;
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
	}
}
