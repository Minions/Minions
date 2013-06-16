// Building.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace Fools.cs.Api
{
	public class Building
	{
		private readonly MissionControl _mission_control;
		private readonly MailRoom _mail_room;

		public Building(MissionControl mission_control, MailRoom central_mail_room)
		{
			_mission_control = mission_control;
			_mail_room = central_mail_room.create_satellite_office();
		}

		public MailRoom mail_room { get { return _mail_room; } }
	}
}
