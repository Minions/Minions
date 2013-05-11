// TestFrameworkPlatformComponents.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using FluentAssertions;
using Fools.cs.Api;
using NUnit.Framework;

namespace Fools.cs.Tests.TestFramework
{
	[TestFixture]
	public class TestFrameworkPlatformComponents
	{
		[Test]
		public void test_run_should_have_a_mail_room_that_is_a_satellite_of_mission_controls()
		{
			var mission_control = new MissionControl();
			var test_subject = mission_control.create_test_run();
			test_subject.mail_room.home_office.Should()
				.BeSameAs(mission_control.mail_room);
		}
	}
}
