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
	}
}
