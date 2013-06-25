// MissionControl.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
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
			new Fool(mission, this).schedule_active_missions();
		}

		internal void schedule([NotNull] Action operation)
		{
			_task_factory.StartNew(operation);
		}
	}
}
