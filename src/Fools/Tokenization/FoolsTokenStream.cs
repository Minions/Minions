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

		public IEnumerable<Token> Read()
		{
			string line;
			bool hadContents = false;
			while(null != (line = _fileContents.ReadLine()))
			{
				hadContents = true;
				foreach(Token token in TokenizeLine(line)) yield return token;
			}
			if(!hadContents)
			{
				yield return Indent(0);
				yield return EndStatement();
			}
			NotifyDone();
		}

		public IDisposable Subscribe(IObserver<Token> observer)
		{
			int nextId = 1 + (_observers.Any() ? _observers.Max(p => p.Key) : 0);
			_observers.Add(new KeyValuePair<int, IObserver<Token>>(nextId, observer));
			return new StackJanitor(() => _observers.RemoveAll(p => p.Key == nextId));
		}

		private IEnumerable<Token> TokenizeLine(string line)
		{
			yield return Indent(0);
			foreach(string token in line.Split(' '))
			{
				if(!string.IsNullOrEmpty(token))
					yield return Identifier(token);
			}
			yield return EndStatement();
		}

		private Token Indent(int indentationLevel)
		{
			return Notify(new IndentationToken(indentationLevel));
		}

		private Token Identifier(string token)
		{
			return Notify(new IdentifierToken(token));
		}

		private Token EndStatement()
		{
			return Notify(new EndOfStatementToken());
		}

		private Token Notify(Token token)
		{
			foreach(var observer in _observers.Select(p=>p.Value))
			{
				observer.OnNext(token);
			}
			return token;
		}

		private void NotifyDone()
		{
			foreach(var observer in _observers.Select(p => p.Value))
			{
				observer.OnCompleted();
			}
		}
	}
}
