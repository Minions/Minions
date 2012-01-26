using System;
using System.Text;

namespace Fools.Tokenization
{
	public class LookingThroughCode : ITokenizationState
	{
		private readonly FoolsTokenStream _tokens;
		private readonly StringBuilder _currentIdentifier = new StringBuilder();

		public LookingThroughCode(FoolsTokenStream tokens)
		{
			_tokens = tokens;
		}

		public void HandleEndOfLine()
		{
			EmitToken();
			_tokens.EndStatement();
		}

		public void HandleCharacter(Char ch)
		{
			if(ch == ' ')
			{
				EmitToken();
			}
			else
			{
				_currentIdentifier.Append(ch);
			}
		}

		private void EmitToken()
		{
			string token = _currentIdentifier.ToString();
			if(!string.IsNullOrEmpty(token))
				_tokens.Identifier(token);
			_currentIdentifier.Clear();
		}
	}
}