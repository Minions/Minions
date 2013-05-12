// Minion.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.builtins;

namespace Fools.cs.Api
{
	public class Minion : MissionOperator
	{
		private readonly MissionSpecification _mission;
		private readonly MissionControl _mission_control;

		public Minion(MissionSpecification mission, MissionControl mission_control)
		{
			_mission = mission;
			_mission_control = mission_control;
		}

		public void schedule_active_missions()
		{
			_mission.execute(this);
		}

		public void execute(SinglePartMission mission)
		{
			if (mission.is_complete) return;
			_mission_control.schedule(mission.requirements, () => {
				mission.operation();
				mission.is_complete = true;
			});
		}
	}
}
