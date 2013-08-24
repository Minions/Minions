// Fool.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading.Tasks;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class Fool<TLab> where TLab : class
	{
		[NotNull] private Task _previous_operation;

		[NotNull] private readonly TLab _lab;
		[NotNull] private readonly object _task_lock = new object();

		public Fool([NotNull] Task previous_operation, [NotNull] TLab lab)
		{
			_previous_operation = previous_operation;
			_lab = lab;
		}

		public void process_message([NotNull] Action<TLab, MailMessage> action,
			[NotNull] MailMessage message,
			[NotNull] Action done)
		{
			_do_work(lab => action(lab, message), done);
		}

		public bool do_work_and_wait([NotNull] Action<TLab> work, TimeSpan wait_duration)
		{
			return _do_work(work, () => { })
				.Wait(wait_duration);
		}

		public void do_work([NotNull] Action<TLab> work, [NotNull] Action done)
		{
			_do_work(work, done);
		}

		[NotNull]
		private Task _do_work([NotNull] Action<TLab> work, [NotNull] Action done)
		{
			Task next_operation;
			lock (_task_lock)
			{
				next_operation = _previous_operation.ContinueWith(r => {
					try
					{
						work(_lab);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						// TODO: What about exceptions thrown by action? When should they be observed?
					}
					done();
				});
				_previous_operation = next_operation;
			}
			return next_operation;
		}
	}
}
