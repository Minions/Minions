// MissionControl.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fools.cs.Utilities;
using Fools.cs.builtins;

namespace Fools.cs.Api
{
	public class MissionControl : IDisposable
	{
		[NotNull] private readonly MailRoom _mail_room = new MailRoom();
		[NotNull] private readonly TaskFactory _task_factory;
		[NotNull] private readonly CancellationTokenSource _cancellation;

		public MissionControl()
		{
			_cancellation = new CancellationTokenSource();
			_task_factory = new TaskFactory(_cancellation.Token,
				TaskCreationOptions.PreferFairness,
				TaskContinuationOptions.ExecuteSynchronously,
				TaskScheduler.Default);
		}

		public void Dispose()
		{
			_cancellation.Cancel();
			_cancellation.Dispose();
		}

		[NotNull]
		public MailRoom mail_room { get { return _mail_room; } }

		[NotNull]
		public Building create_building()
		{
			return new Building(this, _mail_room);
		}

		public void accomplish([NotNull] MissionSpecification mission)
		{
			new OldFool(mission, this).schedule_active_missions();
		}

		[NotNull]
		internal Task schedule([NotNull] Action operation)
		{
			return _task_factory.StartNew(operation);
		}

		public void execute_as_needed<TLab>([NotNull] MissionDescription<TLab> mission) where TLab : class
		{
			// ReSharper disable AssignNullToNotNullAttribute
			mission.spawning_messages.Each(message_type => _mail_room.subscribe(message_type, _spawn_fool(mission)));
			// ReSharper restore AssignNullToNotNullAttribute
		}

		[NotNull]
		private MailRoom.MessageHandler _spawn_fool<TLab>([NotNull] MissionDescription<TLab> mission)
			where TLab : class
		{
			return (m, done) => {
				var fool = new Fool<TLab>(schedule(() => { }), mission.make_lab());
				mission.message_handlers.Each(kv => _mail_room.subscribe( // ReSharper disable AssignNullToNotNullAttribute
					kv.Key, fool.execute_action(kv.Value)));
				// ReSharper restore AssignNullToNotNullAttribute
				// ReSharper disable PossibleNullReferenceException
				done();
				// ReSharper restore PossibleNullReferenceException
			};
		}
	}
}
