using System;

namespace Fools.Utils
{
	public class StackJanitor : IDisposable
	{
		private Action _rollbackImplementation;

		public StackJanitor(Action rollbackImplementation)
		{
			_rollbackImplementation = rollbackImplementation;
		}

		public void Dispose()
		{
			_rollbackImplementation();
		}

		public void RollBack()
		{
			_rollbackImplementation();
		}

		public void Commit()
		{
			_rollbackImplementation = () => { };
		}
	}
}