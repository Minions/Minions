using System.Collections.Generic;

namespace Fools.Tokenization
{
	public class FoolsTokenStream
	{
		public FoolsTokenStream(string fileContents)
		{
		}

		public IEnumerable<Token> Tokens { get { return new[] {new IndentationToken(0)}; } }
	}
}