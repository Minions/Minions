// ReadLockHeld.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Utilities;

namespace System.Threading
{
	public class ReadLockHeld : IDisposable
	{
		[NotNull] private readonly ReaderWriterLockSlim _guard;

		public ReadLockHeld([NotNull] ReaderWriterLockSlim guard)
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
		[NotNull] private readonly ReaderWriterLockSlim _guard;

		public UpgradableLockHeld([NotNull] ReaderWriterLockSlim guard)
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
		[NotNull] private readonly ReaderWriterLockSlim _guard;

		public WriteLockHeld([NotNull] ReaderWriterLockSlim guard)
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
