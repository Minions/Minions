// Fool.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class Fool<TLab> where TLab : class
	{
		[NotNull] private Task _previous_operation;

		[NotNull] private readonly TLab _lab;
		[NotNull] private readonly object _task_lock = new object();
		[NotNull] private readonly List<Action<TLab>> _upon_completion = new List<Action<TLab>>();

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

		/// <summary>
		///    Must be called by a thread that is currently executing an action for this Fool. No
		///    attempt is made to ensure that this requirement is met.
		/// </summary>
		/// <param name="work"></param>
		public void upon_completion_of_this_task([NotNull] Action<TLab> work)
		{
			_upon_completion.Add(work);
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
						_upon_completion.ForEach(w => // ReSharper disable PossibleNullReferenceException
							w(_lab));
						// ReSharper restore PossibleNullReferenceException
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
						// TODO: What about exceptions thrown by action? When should they be observed?
					}
					_upon_completion.Clear();
					done();
				});
				_previous_operation = next_operation;
			}
			return next_operation;
		}
	}
}
