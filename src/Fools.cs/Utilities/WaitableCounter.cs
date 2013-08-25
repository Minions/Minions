// WaitableCounter.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading;

namespace Fools.cs.Utilities
{
	public abstract class WaitableCounter
	{
		[NotNull]
		public static WaitableCounter starting_at(int initial_value)
		{
			return new CountingImpl(initial_value);
		}

		[NotNull]
		public static WaitableCounter non_counting()
		{
			return new NonCountingCounter();
		}

		public abstract void begin();
		public abstract void done();
		public abstract bool wait(TimeSpan wait_duration);
		public abstract bool is_signaled();

		private class CountingImpl : WaitableCounter
		{
			private int _count;
			[NotNull] private readonly object _guard = new object();
			[NotNull] private readonly ManualResetEventSlim _is_signaled;

			public CountingImpl(int initial_value)
			{
				_count = initial_value;
				_is_signaled = new ManualResetEventSlim(initial_value == 0);
			}

			public override void begin()
			{
				lock (_guard)
				{
					++_count;
					_is_signaled.Reset();
				}
			}

			public override void done()
			{
				lock (_guard)
				{
					--_count;
					if (_count == 0) _is_signaled.Set();
				}
			}

			public override bool wait(TimeSpan wait_duration)
			{
				return _is_signaled.Wait(wait_duration);
			}

			public override bool is_signaled()
			{
				return _is_signaled.IsSet;
			}
		}

		private class NonCountingCounter : WaitableCounter
		{
			public override void begin() {}

			public override void done() {}

			public override bool wait(TimeSpan wait_duration)
			{
				return true;
			}

			public override bool is_signaled()
			{
				return true;
			}
		}
	}
}
