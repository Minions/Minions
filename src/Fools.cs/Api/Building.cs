// Building.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace Fools.cs.Api
{
	public class Building
	{
		private readonly MissionControl _mission_control;

		public Building(MissionControl mission_control)
		{
			_mission_control = mission_control;
		}

		public void send(MailMessage what_happened)
		{
			_mission_control.announce(what_happened);
		}
	}
}
