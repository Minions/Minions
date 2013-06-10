// MissionControl.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading;
using System.Threading.Tasks;
using Fools.cs.builtins;

namespace Fools.cs.Api
{
	public class MissionControl : IDisposable
	{
		private readonly MailRoom _mail_room = new MailRoom();
		private readonly TaskFactory _task_factory;
		private readonly CancellationTokenSource _cancellation;

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

		public MailRoom mail_room { get { return _mail_room; } }

		public Building create_building()
		{
			return new Building(this, _mail_room);
		}

		public TestRun create_test_run()
		{
			return new TestRun(this, _mail_room);
		}

		public void accomplish(MissionSpecification mission)
		{
			new Minion(mission, this).schedule_active_missions();
		}

		internal void schedule(MissionSpecification.Requirements requirements, Action operation)
		{
			_task_factory.StartNew(operation);
		}
	}
}
