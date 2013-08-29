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
			var stop = new MissionDescription<OverlordThrone>(() => this);
			stop.spawns_when<DoMyBidding>();
			stop.responds_to_message<AppQuit>(stop_program);
			mission_control.execute_as_needed(stop);
		}

		private static void stop_program([NotNull] OverlordThrone lab, [NotNull] AppQuit message)
		{
			lab._result = message.result;
			lab._program_complete.Set();
		}
	}
}
