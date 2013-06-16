// TestRun.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace Fools.cs.Api
{
	public class TestRun
	{
		private readonly MissionControl _mission_control;
		private readonly MailRoom _mail_room;

		public TestRun(MissionControl mission_control, MailRoom home_office)
		{
			_mission_control = mission_control;
			_mail_room = home_office.create_satellite_office();
		}

		public MailRoom mail_room { get { return _mail_room; } }
	}
}
