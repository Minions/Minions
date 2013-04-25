using System;
using System.Text;

namespace Fools.Compilation.Tokenization
{
	public class LookingThroughCode : ITokenizationState
	{
		private readonly FoolsTokenStream _tokens;
		private readonly StringBuilder _currentIdentifier = new StringBuilder();

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
			_tokens.EndStatement();
			_tokens.SetStateTo(_tokens.StateMeasureIndentation);
		}

		public void HandleCharacter(Char ch)
		{
			switch(ch)
			{
				case ' ':
					EmitToken();
					break;
				case '\\':
					_tokens.SetStateTo(_tokens.StateHandleEscapeSequence);
					_tokens.StateHandleEscapeSequence.ReturnTo(this);
					break;
				default:
					_currentIdentifier.Append(ch);
					break;
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
