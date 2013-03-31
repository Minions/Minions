using System;

namespace Fools.cs.Tests
{
	internal class StackJanitor : IDisposable
	{
		private Action _undo;

		public StackJanitor(Action enter, Action undo)
		{
			_undo = undo;
			enter();
		}

		public void commit()
		{
			_undo = () => { };
		}

		public void Dispose()
		{
			_undo();
		}
	}
}