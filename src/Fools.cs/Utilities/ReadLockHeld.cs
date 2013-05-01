// ReadLockHeld.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace System.Threading
{
	public class ReadLockHeld : IDisposable
	{
		private readonly ReaderWriterLockSlim _guard;

		public ReadLockHeld(ReaderWriterLockSlim guard)
		{
			_guard = guard;
			guard.EnterReadLock();
		}

		public void Dispose()
		{
			_guard.ExitReadLock();
		}
	}

	public class UpgradableLockHeld : IDisposable
	{
		private readonly ReaderWriterLockSlim _guard;

		public UpgradableLockHeld(ReaderWriterLockSlim guard)
		{
			_guard = guard;
			guard.EnterUpgradeableReadLock();
		}

		public void Dispose()
		{
			_guard.ExitUpgradeableReadLock();
		}

		public IDisposable allow_writes()
		{
			return new WriteLockHeld(_guard);
		}
	}

	public class WriteLockHeld : IDisposable
	{
		private readonly ReaderWriterLockSlim _guard;

		public WriteLockHeld(ReaderWriterLockSlim guard)
		{
			_guard = guard;
			guard.EnterWriteLock();
		}

		public void Dispose()
		{
			_guard.ExitWriteLock();
		}
	}
}
