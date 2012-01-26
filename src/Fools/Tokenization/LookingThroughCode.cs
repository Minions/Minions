using System;
using System.Text;

namespace Fools.Tokenization
{
	public class LookingThroughCode : ITokenizationState
	{
		private readonly FoolsTokenStream _tokens;
		private readonly StringBuilder _currentIdentifier = new StringBuilder();
		private char _prevChar;

		public LookingThroughCode(FoolsTokenStream tokens)
		{
			_tokens = tokens;
		}

		public void EnterState()
		{
		}

		public void HandleEndOfLine()
		{
			EmitToken();
			if (_prevChar == '\\')
			{
				_tokens.SetStateTo(_tokens.StateSkippingWhitespace);
			}
			else
			{
				_tokens.EndStatement();
				_tokens.SetStateTo(_tokens.StateMeasureIndentation);
			}
		}

		public void HandleCharacter(Char ch)
		{
			_prevChar = ch;
			if (ch == ' ')
			{
				EmitToken();
			}
			else if(ch != '\\')
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
