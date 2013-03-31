using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
	public static class UtilityExtensions
	{
		public static string Flatten(this IEnumerable<string> data)
		{
			var result = new StringBuilder();
			foreach(var elt in data)
			{
				result.Append(elt);
			}
			return result.ToString();
		}
	}
}
