// TestRun.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class TestRun
	{
		// ReSharper disable NotAccessedField.Local
		[NotNull] private readonly MissionControl _mission_control;
		// ReSharper restore NotAccessedField.Local
		[NotNull] private readonly MailRoom _mail_room;

		public TestRun([NotNull] MissionControl mission_control, [NotNull] MailRoom home_office)
		{
			_mission_control = mission_control;
			_mail_room = home_office.create_satellite_office();
			view_model = new ViewModel();
		}

		[NotNull]
		public MailRoom mail_room { get { return _mail_room; } }

		[NotNull]
		public ViewModel view_model { get; private set; }

		public class ViewModel {}

		public void wait_for_all_tests_to_finish() {}
	}
}
