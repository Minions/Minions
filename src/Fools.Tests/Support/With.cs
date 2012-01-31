using System.Collections.Generic;
using System.Linq;
using Fools.Tokenization;

namespace Fools.Tests.Support
{
	public class With
	{
		public static IEnumerable<Token> EmptyLine { get { return Line(); } }

		public static IEnumerable<Token> Line(int indentationLevel, params Token[] tokens)
		{
			return
				new Token[] {new IndentationToken(indentationLevel)}.Concat(tokens).Concat(new Token[] {new EndOfStatementToken()});
		}

		public static IEnumerable<Token> Line(params Token[] tokens)
		{
			return Line(0, tokens);
		}

		public static IEnumerable<Token> Tokens(params Token[] tokens)
		{
			return tokens;
		}
	}
}
