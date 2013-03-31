using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
	static internal class AstExtensions
	{
		public static IList<T> without_nulls<T>(this IEnumerable<T> data) where T : class
		{
			return data.Where(e => e != null).ToList();
		}
	}
}