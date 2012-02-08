using System;

namespace Fools.Tokenization
{
	public interface ITokenizationState
	{
		void EnterState();
		void HandleEndOfLine();
		void HandleCharacter(Char ch);
	}
}