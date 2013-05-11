// MissionControl.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

namespace Fools.cs.Api
{
	public class MissionControl
	{
		private readonly MailRoom _mail_room = new MailRoom();
		public MailRoom mail_room { get { return _mail_room; } }

		public Building create_building()
		{
			return new Building(this, _mail_room);
		}

		public TestRun create_test_run()
		{
			return new TestRun(this, _mail_room);
		}
	}
}
