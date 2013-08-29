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
		[NotNull] private readonly TaskFactory _task_factory;
		[NotNull] private readonly CancellationTokenSource _cancellation;
		[NotNull] private readonly Fool<MailRoom> _postal_carrier;
		[NotNull] private readonly OverlordThrone _overlord_throne;

		public MissionControl()
		{
			_overlord_throne = new OverlordThrone();
			_cancellation = new CancellationTokenSource();
			_task_factory = new TaskFactory(_cancellation.Token,
				TaskCreationOptions.PreferFairness,
				TaskContinuationOptions.ExecuteSynchronously,
				TaskScheduler.Default);
			_postal_carrier = new Fool<MailRoom>(_create_starting_task(), new MailRoom());

			_overlord_throne.prepare_missions(this);
		}

		[NotNull]
		public OverlordThrone overlord_throne { get { return _overlord_throne; } }

		public void Dispose()
		{
			_cancellation.Cancel();
			_cancellation.Dispose();
		}

		public void execute_as_needed<TLab>([NotNull] MissionDescription<TLab> mission) where TLab : class
		{
			_postal_carrier.do_work(
				mail_room => mission.spawning_messages.Each(message_type => // ReSharper disable PossibleNullReferenceException
					mail_room
						// ReSharper restore PossibleNullReferenceException
						// ReSharper disable AssignNullToNotNullAttribute
						.subscribe(message_type, (message, done) => _spawn_fool(mission, done, message))),
				// ReSharper restore AssignNullToNotNullAttribute
				_noop);
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

		[NotNull]
		private Task _create_starting_task()
		{
			return _task_factory.StartNew(_noop);
		}

		private void _spawn_fool<TLab>([NotNull] MissionDescription<TLab> mission,
			[NotNull] Action done_creating_fool,
			[NotNull] MailMessage constructor_message) where TLab : class
		{
			var fool = new Fool<TLab>(_create_starting_task(), mission.make_lab());
			fool_proces_ctor(mission, done_creating_fool, constructor_message, fool);
			subscribe_handlers_for_rest_of_mission(mission, fool);
		}

		private static void fool_proces_ctor<TLab>([NotNull] MissionDescription<TLab> mission,
			[NotNull] Action done_creating_fool,
			[NotNull] MailMessage constructor_message,
			[NotNull] Fool<TLab> fool) where TLab : class
		{
			var constructor_impl = mission.message_handlers.FirstOrDefault(item => item.Key == constructor_message.GetType());
			if (constructor_impl.Value == null) done_creating_fool();
			else fool.process_message(constructor_impl.Value, constructor_message, done_creating_fool);
		}

		private void subscribe_handlers_for_rest_of_mission<TLab>([NotNull] MissionDescription<TLab> mission,
			[NotNull] Fool<TLab> fool) where TLab : class
		{
			_postal_carrier.upon_completion_of_this_task(
				mail_room => mission.message_handlers.Each(kv => // ReSharper disable PossibleNullReferenceException
					mail_room
						// ReSharper restore PossibleNullReferenceException
						.subscribe( // ReSharper disable AssignNullToNotNullAttribute
							kv.Key, (message, done_handling_message) => fool.process_message(kv.Value, message, done_handling_message))));
			// ReSharper restore AssignNullToNotNullAttribute
		}

		private static void _noop() {}
	}
}
