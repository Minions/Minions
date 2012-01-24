using System.Collections.Generic;

namespace Fools.Tokenization
{
	public class FoolsTokenStream
	{
		public FoolsTokenStream(string fileContents)
		{
		}

		public IEnumerable<Token> Tokens { get { return new Token[] {new IndentationToken(0), new EndOfStatementToken()}; } }
	}
}
