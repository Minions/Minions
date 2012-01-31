using System.Collections.Generic;
using System.Linq;
using Fools.Tokenization;

namespace Fools.Tests.Support
{
	public class Line
	{
		public static IEnumerable<Token> Empty { get { return Containing(); } }

		public static IEnumerable<Token> Containing(int indentationLevel, params Token[] tokens)
		{
			return
				new Token[] {new IndentationToken(indentationLevel)}.Concat(tokens).Concat(new Token[] {new EndOfStatementToken()});
		}

		public static IEnumerable<Token> Containing(params Token[] tokens)
		{
			return Containing(0, tokens);
		}
	}
}
