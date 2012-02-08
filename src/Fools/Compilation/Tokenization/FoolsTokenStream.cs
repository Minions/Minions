using System;
using System.IO;
using Fools.Utils;

namespace Fools.Compilation.Tokenization
{
	public class FoolsTokenStream : IObservable<Token>
	{
		private readonly StringReader _fileContents;
		private ITokenizationState _currentState;
		private readonly ObservableMulticaster<Token> _observers = new ObservableMulticaster<Token>();

		public FoolsTokenStream(string fileContents)
			: this(new StringReader(fileContents))
		{
		}

		public LookingThroughCode StateLookingThroughCode { get; private set; }
		public SkippingWhitespace StateSkippingWhitespace { get; private set; }
		public MeasureIndentation StateMeasureIndentation { get; private set; }
		public HandleEscapeSequence StateHandleEscapeSequence { get; private set; }

		public FoolsTokenStream(StringReader fileContents)
		{
			_fileContents = fileContents;
			StateLookingThroughCode = new LookingThroughCode(this);
			StateSkippingWhitespace = new SkippingWhitespace(this);
			StateMeasureIndentation = new MeasureIndentation(this);
			StateHandleEscapeSequence = new HandleEscapeSequence(this);
			SetStateTo(StateMeasureIndentation);
		}

		public void SetStateTo(ITokenizationState nextState)
		{
			_currentState = nextState;
			nextState.EnterState();
		}

		public void Read()
		{
			string line;
			bool hadContents = false;
			while(null != (line = _fileContents.ReadLine()))
			{
				TokenizeLine(line);
				hadContents = true;
			}
			if(!hadContents)
			{
				Indent(0);
				EndStatement();
			}
			_observers.NotifyDone();
		}

		private void TokenizeLine(string line)
		{
			line.Each(ch => _currentState.HandleCharacter(ch));
			_currentState.HandleEndOfLine();
		}

		public void Indent(int indentationLevel)
		{
			_observers.Notify(new IndentationToken(indentationLevel));
		}

		public void Identifier(string token)
		{
			_observers.Notify(new IdentifierToken(token));
		}

		public void EndStatement()
		{
			_observers.Notify(new EndOfStatementToken());
		}

		public IDisposable Subscribe(IObserver<Token> observer)
		{
			return _observers.Subscribe(observer);
		}
	}
}
