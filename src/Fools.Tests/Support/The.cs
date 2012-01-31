using System.Collections.Generic;
using Fools.Tokenization;
using Fools.Utils;

namespace Fools.Tests.Support
{
	public class The
	{
		public static IEnumerable<Token> File(params IEnumerable<Token>[] containing)
		{
			return containing.Flatten();
		}
	}
}