using System.Collections.Generic;

namespace Fools.Tokenization
{
	public class FoolsTokenStream
	{
		private readonly string _fileContents;

		public FoolsTokenStream(string fileContents)
		{
			_fileContents = fileContents;
		}

		public IEnumerable<Token> Tokens
		{
			get
			{
				yield return new IndentationToken(0);
				if(!string.IsNullOrEmpty(_fileContents))
					yield return new IdentifierToken(_fileContents);
				yield return new EndOfStatementToken();
			}
		}
	}
}
