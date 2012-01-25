using System.Collections.Generic;
using System.IO;

namespace Fools.Tokenization
{
	public class FoolsTokenStream
	{
		private readonly StringReader _fileContents;

		public FoolsTokenStream(string fileContents)
		{
			_fileContents = new StringReader(fileContents);
		}

		public IEnumerable<Token> Tokens
		{
			get
			{
				string line;
				var hadContents = false;
				while(null != (line = _fileContents.ReadLine()))
				{
					hadContents = true;
					yield return new IndentationToken(0);
					if(!string.IsNullOrEmpty(line))
						yield return new IdentifierToken(line);
					yield return new EndOfStatementToken();
				}
				if(!hadContents)
				{
					yield return new IndentationToken(0);
					yield return new EndOfStatementToken();
				}
			}
		}
	}
}
