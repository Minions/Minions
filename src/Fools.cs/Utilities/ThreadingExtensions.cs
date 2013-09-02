// ThreadingExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Utilities;

// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
	[PublicAPI]
	public static class ThreadingExtensions
	{
		[NotNull]
		public static ReadLockHeld acquire_read_access([NotNull] this ReaderWriterLockSlim guard)
		{
			return new ReadLockHeld(guard);
		}

		[NotNull]
		public static UpgradableLockHeld acquire_blocking_read_access([NotNull] this ReaderWriterLockSlim guard)
		{
			return new UpgradableLockHeld(guard);
		}

		[NotNull]
		public static WriteLockHeld acquire_write_access([NotNull] this ReaderWriterLockSlim guard)
		{
			return new WriteLockHeld(guard);
		}
	}
}
