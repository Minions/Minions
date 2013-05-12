// RunMissions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading;
using FluentAssertions;
using Fools.cs.Api;
using Fools.cs.builtins;
using NUnit.Framework;

namespace Fools.cs.Tests.CoreLanguage
{
	[TestFixture]
	public class RunMissions
	{
		[Test]
		public void mission_control_should_execute_a_simple_mission()
		{
			var test_subject = new MissionControl();
			var mission_ran = new ManualResetEventSlim();
			var mission = new SinglePartMission("signal the test", mission_ran.Set);
			mission_ran.IsSet.Should()
				.BeFalse();
			test_subject.accomplish(mission);
			mission_ran.Wait(TimeSpan.FromMilliseconds(100))
				.Should()
				.BeTrue();
			mission.is_complete.Should()
				.BeTrue();
		}
	}
}
