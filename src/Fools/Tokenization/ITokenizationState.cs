using System;

namespace Fools.Tokenization
{
	public interface ITokenizationState
	{
		void HandleEndOfLine();
		void HandleCharacter(Char ch);
		void EnterState();
	}
}