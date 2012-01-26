using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Fools.Utils;

namespace Fools.Tokenization
{
	public class FoolsTokenStream : IObservable<Token>
	{
		private readonly StringReader _fileContents;
		private ITokenizationState _currentState;
		private readonly LookingThroughCode _lookingThroughCode;

		private readonly List<KeyValuePair<int, IObserver<Token>>> _observers =
			new List<KeyValuePair<int, IObserver<Token>>>();

		public FoolsTokenStream(string fileContents)
			: this(new StringReader(fileContents))
		{
		}

		public FoolsTokenStream(StringReader fileContents)
		{
			_fileContents = fileContents;
			_lookingThroughCode = new LookingThroughCode(this);
			_currentState = _lookingThroughCode;
		}

		public void Read()
		{
			string line;
			bool hadContents = false;
			var effectiveLine = new StringBuilder();
			while(null != (line = _fileContents.ReadLine()))
			{
				hadContents = true;
				if(line.EndsWith("\\"))
				{
					effectiveLine.Append(line, 0, line.Length - 1);
					effectiveLine.Append(' ');
				}
				else
				{
					effectiveLine.Append(line);
					TokenizeLine(effectiveLine.ToString());
					effectiveLine.Clear();
				}
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
			Indent(0);
			line.Each(_currentState.HandleCharacter);
			_currentState.HandleEndOfLine();
		}

		private void Indent(int indentationLevel)
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
