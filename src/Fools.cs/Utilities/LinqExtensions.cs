// LinqExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Collections.Generic;
using Fools.cs.Utilities;

namespace System.Linq
{
	public static class LinqExtensions
	{
		public static void Each<T>([NotNull] this IEnumerable<T> items, [NotNull] Action<T> op)
		{
			foreach (var item in items)
			{
				op(item);
			}
		}

		public static void if_valid<T>([CanBeNull] this T arg, [NotNull] Action<T> op) where T : class
		{
			if (null != arg) op(arg);
		}

		public static TResult if_valid<TArg, TResult>([CanBeNull] this TArg arg, [NotNull] Func<TArg, TResult> op) where TArg : class
		{
			return null == arg ? default(TResult) : op(arg);
		}
	}
}
