using System;
using System.Globalization;
using System.Text;
using Rxx.Parsers;

namespace Fools.Tokenization
{
	public class HandleEscapeSequence : ITokenizationState
	{
		private readonly FoolsTokenStream _tokens;
		private ITokenizationState _resultHandler;
		private bool _inUnicodeSequence;
		private string _escapeSequence;

		public HandleEscapeSequence(FoolsTokenStream tokens)
		{
			_tokens = tokens;
		}

		public void EnterState()
		{
			_resultHandler = null;
			_inUnicodeSequence = false;
		}

		public void HandleEndOfLine()
		{
			_tokens.SetStateTo(_tokens.StateSkippingWhitespace);
			_resultHandler.HandleCharacter(' ');
		}

		public void HandleCharacter(char ch)
		{
			if (_inUnicodeSequence)
			{
				_escapeSequence += ch;
				if (_escapeSequence.Length == 4)
				{
					FinishAsCharacter((char) int.Parse(_escapeSequence, NumberStyles.HexNumber));
				}
				return;
			}
			if (ch == 'u')
			{
				_inUnicodeSequence = true;
				_escapeSequence = string.Empty;
				return;
			}
			if (ch == 'n')
			{
				FinishAsNewline();
				return;
			}
			throw new NotImplementedException("The tokenizer does recognize this escape sequence.");
		}

		private void FinishAsNewline()
		{
			_tokens.SetStateTo(_resultHandler);
			_resultHandler.HandleEndOfLine();
		}

		private void FinishAsCharacter(char ch)
		{
			_tokens.SetStateTo(_resultHandler);
			_resultHandler.HandleCharacter(ch);
		}

		public void ReturnTo(ITokenizationState resultHandler)
		{
			_resultHandler = resultHandler;
		}
	}
}