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

		private readonly List<KeyValuePair<int, IObserver<Token>>> _observers =
			new List<KeyValuePair<int, IObserver<Token>>>();

		public FoolsTokenStream(string fileContents)
			: this(new StringReader(fileContents))
		{
		}

		public FoolsTokenStream(StringReader fileContents)
		{
			_fileContents = fileContents;
		}

		public void Read()
		{
			string line;
			bool hadContents = false;
			while(null != (line = _fileContents.ReadLine()))
			{
				hadContents = true;
				TokenizeLine(line);
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
			foreach(string token in line.Split(' '))
			{
				if(!string.IsNullOrEmpty(token))
					Identifier(token);
			}
			EndStatement();
		}

		private void Indent(int indentationLevel)
		{
			Notify(new IndentationToken(indentationLevel));
		}

		private void Identifier(string token)
		{
			Notify(new IdentifierToken(token));
		}

		private void EndStatement()
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
