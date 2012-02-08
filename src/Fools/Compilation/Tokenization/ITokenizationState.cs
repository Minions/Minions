using System;

namespace Fools.Compilation.Tokenization
{
	public interface ITokenizationState
	{
		void EnterState();
		void HandleEndOfLine();
		void HandleCharacter(Char ch);
	}
}