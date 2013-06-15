// ThreadingExtensions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace System.Threading
{
	public static class ThreadingExtensions
	{
		public static ReadLockHeld acquire_read_access(this ReaderWriterLockSlim guard)
		{
			return new ReadLockHeld(guard);
		}
		public static UpgradableLockHeld acquire_blocking_read_access(this ReaderWriterLockSlim guard)
		{
			return new UpgradableLockHeld(guard);
		}
		public static WriteLockHeld acquire_write_access(this ReaderWriterLockSlim guard)
		{
			return new WriteLockHeld(guard);
		}
	}
}
