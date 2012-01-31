using System;
using System.Collections.Generic;
using System.Linq;

namespace Fools.Utils
{
	public class ObservableMulticaster<T>
	{
		private readonly List<KeyValuePair<int, IObserver<T>>> _observers =
			new List<KeyValuePair<int, IObserver<T>>>();

		public IDisposable Subscribe(IObserver<T> observer)
		{
			var nextId = 1 + (_observers.Any() ? _observers.Max(p => p.Key) : 0);
			_observers.Add(new KeyValuePair<int, IObserver<T>>(nextId, observer));
			return new StackJanitor(() => _observers.RemoveAll(p => p.Key == nextId));
		}

		public void Notify(T token)
		{
			foreach(var observer in _observers.Select(p => p.Value))
			{
				observer.OnNext(token);
			}
		}

		public void NotifyDone()
		{
			foreach(var observer in _observers.Select(p => p.Value))
			{
				observer.OnCompleted();
			}
		}
	}
}
