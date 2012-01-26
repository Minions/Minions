using System;
using System.Collections.Generic;

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
	}
}
