namespace Fools.Tokenization
{
	public class SkippingWhitespace : ITokenizationState
	{
		private readonly FoolsTokenStream _tokens;

		public SkippingWhitespace(FoolsTokenStream tokens)
		{
			_tokens = tokens;
		}

		public void EnterState()
		{
		}

		public void HandleEndOfLine()
		{
			_tokens.SetStateTo(_tokens.StateLookingThroughCode);
			_tokens.StateLookingThroughCode.HandleEndOfLine();
		}

		public void HandleCharacter(char ch)
		{
			if(ch == ' ' || ch == '\t' || ch == '\v') return;
			_tokens.SetStateTo(_tokens.StateLookingThroughCode);
			_tokens.StateLookingThroughCode.HandleCharacter(ch);
		}
	}
}
