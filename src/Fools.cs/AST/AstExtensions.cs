using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
	static internal class AstExtensions
	{
		public static IEnumerable<T> without_nulls<T>(this IEnumerable<T> data) where T : class
		{
			return data.Where(e => e != null);
		}
	}
}