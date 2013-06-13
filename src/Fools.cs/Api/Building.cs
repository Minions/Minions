// Building.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using Fools.cs.Utilities;

namespace Fools.cs.Api
{
	public class Building
	{
		[NotNull, UsedImplicitly] private readonly MissionControl _mission_control;
		[NotNull] private readonly MailRoom _mail_room;

		public Building([NotNull] MissionControl mission_control, [NotNull] MailRoom central_mail_room)
		{
			_mission_control = mission_control;
			_mail_room = central_mail_room.create_satellite_office();
		}

		public MailRoom mail_room { get { return _mail_room; } }
	}
}
