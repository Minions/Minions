using System;
using Fools.Ast;
using Fools.Recognizing;
using Fools.Tokenization;

namespace Fools.Compilation
{
	public static class Stage
	{
		public static IObservable<INode> DetectLines(this IObservable<Token> source)
		{
			return new LineDetector().SubscribedTo(source);
		}

		public static IObservable<INode> RecognizeBlocksAndStatements(this IObservable<INode> source)
		{
			return new BlockFinder().SubscribedTo(source);
		}

		public static IObservable<TDest> SubscribedTo<TSource, TDest>(this ITransformation<TSource, TDest> dest, IObservable<TSource> source)
		{
			source.Subscribe(dest);
			return dest;
		}
	}
}
