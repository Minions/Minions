// MissionControl.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class MissionControl : IDisposable
	{
		[NotNull] private readonly MailRoom _mail_room = new MailRoom();
		[NotNull] private readonly TaskFactory _task_factory;
		[NotNull] private readonly CancellationTokenSource _cancellation;
		[NotNull] private readonly Fool<MailRoom> _postal_carrier;

		public MissionControl()
		{
			_cancellation = new CancellationTokenSource();
			_task_factory = new TaskFactory(_cancellation.Token,
				TaskCreationOptions.PreferFairness,
				TaskContinuationOptions.ExecuteSynchronously,
				TaskScheduler.Default);
			_postal_carrier = new Fool<MailRoom>(_task_factory.StartNew(_noop), _mail_room);
		}

		public void Dispose()
		{
			_cancellation.Cancel();
			_cancellation.Dispose();
		}

		[NotNull]
		internal MailRoom test_access_to_mail_room { get { return _mail_room; } }

		[NotNull]
		public Building create_building()
		{
			return new Building(this, _mail_room);
		}

		[NotNull]
		internal Task schedule([NotNull] Action operation)
		{
			return _task_factory.StartNew(operation);
		}

		public void execute_as_needed<TLab>([NotNull] MissionDescription<TLab> mission) where TLab : class
		{
			// ReSharper disable AssignNullToNotNullAttribute
			mission.spawning_messages.Each(
				message_type => _mail_room.subscribe(message_type, (_, done) => _spawn_fool(mission, done)));
			// ReSharper restore AssignNullToNotNullAttribute
		}

		public void announce([NotNull] MailMessage what_happened)
		{
			_postal_carrier.do_work(mail_room => // ReSharper disable PossibleNullReferenceException
				mail_room
					// ReSharper restore PossibleNullReferenceException
					.announce(what_happened),
				_noop);
		}

		public bool announce_and_wait([NotNull] MailMessage what_happened, TimeSpan wait_duration)
		{
			bool? result = null;
			return _postal_carrier.do_work_and_wait(mail_room => {
				result = // ReSharper disable PossibleNullReferenceException
					mail_room
						// ReSharper restore PossibleNullReferenceException
						.announce_and_wait(what_happened, wait_duration);
			},
				wait_duration) && result.GetValueOrDefault(false);
		}

		private void _spawn_fool<TLab>([NotNull] MissionDescription<TLab> mission, [NotNull] Action done_creating_fool)
			where TLab : class
		{
			var fool = new Fool<TLab>(schedule(_noop), mission.make_lab());
			mission.message_handlers.Each(kv => _mail_room.subscribe( // ReSharper disable AssignNullToNotNullAttribute
				kv.Key, (message, done_handling_message) => fool.process_message(kv.Value, message, done_handling_message)));
			// ReSharper restore AssignNullToNotNullAttribute
			done_creating_fool();
		}

		private void _noop() {}
	}
}
