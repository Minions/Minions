using System;
using System.Collections.Generic;
using System.Linq;
using Fools.Compilation.Tokenization;

namespace Fools.Utils
{
	public static class HelperExtensions
	{
		public static void Each<T>(this IEnumerable<T> items, Action<T> operation)
		{
			foreach(var item in items)
			{
				operation(item);
			}
		}

		public static IEnumerable<Token> Flatten(this IEnumerable<IEnumerable<Token>> lists)
		{
			return lists.Aggregate(Enumerable.Empty<Token>(), (current, line) => current.Concat(line));
		}

		public static void SendTo<T>(this IEnumerable<T> stream, IObserver<T> destination)
		{
			foreach(var token in stream)
			{
				destination.OnNext(token);
			}
			destination.OnCompleted();
		}
	}
}
