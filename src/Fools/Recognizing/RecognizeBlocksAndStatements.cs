using System;
using System.Collections.Generic;
using Fools.Ast;
using Fools.Tokenization;
using Fools.Utils;

namespace Fools.Recognizing
{
	public class RecognizeBlocksAndStatements : IObservable<INode>, IObserver<Token>
	{
		private readonly ObservableMulticaster<INode> _observers = new ObservableMulticaster<INode>();
		private readonly List<Token> _tokens = new List<Token>();

		public IDisposable Subscribe(IObserver<INode> observer)
		{
			return _observers.Subscribe(observer);
		}

		public void OnNext(Token value)
		{
			if (value is EndOfStatementToken)
			{
				_observers.Notify(new UnrecognizedStatement(_tokens.ToArray()));
			}
			else if(value is IdentifierToken)
			{
				_tokens.Add(value);
			}
		}

		public void OnError(Exception error)
		{
		}

		public void OnCompleted()
		{
			_observers.NotifyDone();
		}
	}
}
