using System;
using System.Collections.Generic;
using System.Linq;
using Fools.Ast;
using Fools.Tokenization;
using Fools.Utils;

namespace Fools.Recognizing
{
	public class LineDetector : IObservable<INode>, IObserver<Token>
	{
		private readonly ObservableMulticaster<INode> _observers = new ObservableMulticaster<INode>();
		private readonly List<Token> _tokensSeen = new List<Token>();

		public IDisposable Subscribe(IObserver<INode> observer)
		{
			return _observers.Subscribe(observer);
		}

		public void OnNext(Token value)
		{
			if(value is EndOfStatementToken)
			{
				_observers.Notify(new Line(Indent, LineContents));
				_tokensSeen.Clear();
			}
			else
			{
				_tokensSeen.Add(value);
			}
		}

		protected Token[] LineContents { get { return _tokensSeen.Skip(1).ToArray(); } }

		private int Indent { get { return ((IndentationToken) _tokensSeen[0]).IndentationLevel; } }

		public void OnError(Exception error)
		{
		}

		public void OnCompleted()
		{
			_observers.NotifyDone();
		}
	}
}
