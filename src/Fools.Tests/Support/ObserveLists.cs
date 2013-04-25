using System;
using System.Collections.Generic;
using Fools.Utils;

namespace Fools.Tests.Support
{
	public class ObserveLists<T> : IObservable<T>
	{
		private readonly ObservableMulticaster<T> _observers = new ObservableMulticaster<T>();

		public IDisposable Subscribe(IObserver<T> observer)
		{
			return _observers.Subscribe(observer);
		}

		public void Send(IEnumerable<T> source)
		{
			foreach (var item in source)
			{
				_observers.Notify(item);
			}
			_observers.NotifyDone();
		}
	}
}