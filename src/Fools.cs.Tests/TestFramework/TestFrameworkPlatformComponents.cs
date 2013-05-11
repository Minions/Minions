// TestFrameworkPlatformComponents.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using ApprovalTests;
using ApprovalTests.Reporters;
using FluentAssertions;
using Fools.cs.Api;
using Fools.cs.Tests.Support;
using Fools.cs.builtins;
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

		[Test]
		public void fixed_test_discovery_should_present_hard_coded_set_of_tests_to_recipient()
		{
			var trap = new MessageTrap();
			var test_subject = new DiscoverTwoNoOpTests();
			test_subject.discover_tests(trap.mail_room);
			trap.log.received.Should()
				.Equal(new TestFoundMessage(DiscoverTwoNoOpTests.FIRST_TEST),
					new TestFoundMessage(DiscoverTwoNoOpTests.SECOND_TEST),
					new NoMoreTestsMessage());
		}

		[Test, UseReporter(typeof(DiffReporter))]
		public void test_run_should_start_test_missions_when_they_are_found()
		{
			var mission_control = new MissionControl();
			var test_subject = mission_control.create_test_run();
			test_subject.mail_room.announce(new TestFoundMessage(DiscoverTwoNoOpTests.FIRST_TEST));
			test_subject.mail_room.announce(new NoMoreTestsMessage());
			test_subject.wait_for_all_tests_to_finish();
			Approvals.Verify(test_subject.view_model.pretty_print());
		}
	}

	public class DiscoverTwoNoOpTests : TestDiscovery
	{
		public static readonly SinglePartMission FIRST_TEST = new SinglePartMission("first", Missions.pass);
		public static readonly SinglePartMission SECOND_TEST = new SinglePartMission("second", Missions.pass);

		protected override void _locate_tests(Action<MissionSpecification> report_test, Action done_finding_tests)
		{
			report_test(FIRST_TEST);
			report_test(SECOND_TEST);
			done_finding_tests();
		}
	}
}
