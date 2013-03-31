using System.Collections.Generic;
using System.Linq;

namespace Fools.cs.AST
{
	static internal class AstExtensions
	{
		public static IList<object> without_nulls(this IEnumerable<object> data)
		{
			return data.Where(e => e != null).ToList();
		}
	}
}