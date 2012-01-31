using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fools.Utils;

namespace Fools.Tokenization
{
	public class FoolsTokenStream : IObservable<Token>
	{
		private readonly StringReader _fileContents;
		private ITokenizationState _currentState;

		private readonly List<KeyValuePair<int, IObserver<Token>>> _observers =
			new List<KeyValuePair<int, IObserver<Token>>>();

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
			NotifyDone();
		}

		public IDisposable Subscribe(IObserver<Token> observer)
		{
			int nextId = 1 + (_observers.Any() ? _observers.Max(p => p.Key) : 0);
			_observers.Add(new KeyValuePair<int, IObserver<Token>>(nextId, observer));
			return new StackJanitor(() => _observers.RemoveAll(p => p.Key == nextId));
		}

		private void TokenizeLine(string line)
		{
			line.Each(ch => _currentState.HandleCharacter(ch));
			_currentState.HandleEndOfLine();
		}

		public void Indent(int indentationLevel)
		{
			Notify(new IndentationToken(indentationLevel));
		}

		public void Identifier(string token)
		{
			Notify(new IdentifierToken(token));
		}

		public void EndStatement()
		{
			Notify(new EndOfStatementToken());
		}

		private void Notify(Token token)
		{
			foreach(IObserver<Token> observer in _observers.Select(p => p.Value))
			{
				observer.OnNext(token);
			}
		}

		private void NotifyDone()
		{
			foreach(IObserver<Token> observer in _observers.Select(p => p.Value))
			{
				observer.OnCompleted();
			}
		}
	}
}
