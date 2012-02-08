namespace Fools.Tokenization
{
	public class MeasureIndentation : ITokenizationState
	{
		private readonly FoolsTokenStream _tokens;
		private int _indentationLevel;

		public MeasureIndentation(FoolsTokenStream tokens)
		{
			_tokens = tokens;
		}

		public void EnterState()
		{
			_indentationLevel = 0;
		}

		public void HandleEndOfLine()
		{
			EmitIndentation();
			_tokens.StateLookingThroughCode.HandleEndOfLine();
		}

		public void HandleCharacter(char ch)
		{
			if (ch == '\t')
				_indentationLevel += 1;
			else
			{
				EmitIndentation();
				_tokens.StateLookingThroughCode.HandleCharacter(ch);
			}
		}

		private void EmitIndentation()
		{
			_tokens.Indent(_indentationLevel);
			_tokens.SetStateTo(_tokens.StateLookingThroughCode);
		}
	}
}
