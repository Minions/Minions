// OverlordThrone.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System.Threading;
using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class OverlordThrone
	{
		private AppErrorLevel _result = AppErrorLevel.Ok;
		[NotNull] private readonly ManualResetEventSlim _program_complete = new ManualResetEventSlim(false);

		public AppErrorLevel watch_the_fools_dance()
		{
			_program_complete.Wait();
			return _result;
		}

		public void submit_missions_to([NotNull] MissionControl mission_control)
		{
			var tell_overlord_when_all_work_is_done = new MissionDescription<OverlordThrone>(() => this);
			tell_overlord_when_all_work_is_done.send_new_fool_when<DoMyBidding>();
			tell_overlord_when_all_work_is_done.fools_shall_do<AppQuit>(stop_program);
			mission_control.send_out_fools_to(tell_overlord_when_all_work_is_done);
		}

		private static void stop_program([NotNull] OverlordThrone lab, [NotNull] AppQuit message)
		{
			lab._result = message.result;
			lab._program_complete.Set();
		}
	}
}
