// RunMissions.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Threading;
using FluentAssertions;
using Fools.cs.Api;
using Fools.cs.Utilities;
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
			using (var test_subject = new MissionControl())
			{
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

		[Test]
		public void mission_control_should_execute_mission_parts_when_messages_arrive()
		{
			using (var test_subject = new MissionControl())
			{
				var mission = orc_raid();
				should_be_no_orcs();
				test_subject.execute_as_needed(mission);
				test_subject.mail_room.announce(new ElvesFound());
				test_subject.mail_room.announce(new ElvesFound());
				should_have_spawned_orcs(2);
			}
		}

		[SetUp]
		public void init()
		{
			_orcs = new NonNullList<OrcishRaidProgress>();
			_all_orcs_are_sent = new ManualResetEventSlim(false);
			_target_number_of_orcs = 0;
		}

		[TearDown]
		public void tear_down()
		{
			_all_orcs_are_sent.Dispose();
			// ReSharper disable PossibleNullReferenceException
			_orcs.ForEach(o => o.Dispose());
			// ReSharper restore PossibleNullReferenceException
			_orcs.Clear();
		}

		[UsedImplicitly]
		private class OrcishRaidProgress : IDisposable
		{
			[NotNull] internal readonly ManualResetEventSlim went_raiding = new ManualResetEventSlim(false);

			public void Dispose()
			{
				went_raiding.Dispose();
			}
		}

		[NotNull] private NonNullList<OrcishRaidProgress> _orcs;
		[NotNull] private ManualResetEventSlim _all_orcs_are_sent;
		[NotNull] private readonly object _orc_counter = new object();
		private int _target_number_of_orcs;

		private void should_have_spawned_orcs(int count)
		{
			lock (_orc_counter)
			{
				_all_orcs_are_sent.Reset();
				_target_number_of_orcs = count;
				if (_target_number_of_orcs <= _orcs.Count) _all_orcs_are_sent.Set();
			}
			_all_orcs_are_sent.Wait(TimeSpan.FromMilliseconds(100))
				.Should()
				.BeTrue();
		}

		[NotNull]
		private MissionDescription<OrcishRaidProgress> orc_raid()
		{
			var raid = new MissionDescription<OrcishRaidProgress>();
			raid.spawns_when<ElvesFound>()
				.and_does(_started_new_raid);
			raid.responds_to_message<SayGo>(_begin_raiding);
			return raid;
		}

		private static void _begin_raiding([NotNull] OrcishRaidProgress lab, [NotNull] SayGo message)
		{
			lab.went_raiding.Set();
		}

		private void _started_new_raid([NotNull] OrcishRaidProgress lab, [NotNull] ElvesFound message)
		{
			lock (_orc_counter)
			{
				_orcs.Add(lab);
				if (_target_number_of_orcs > 0 && _orcs.Count >= _target_number_of_orcs) _all_orcs_are_sent.Set();
			}
		}

		private void should_be_no_orcs()
		{
			_orcs.Count.Should()
				.Be(0);
		}
	}

	internal class SayGo : MailMessage {}

	internal class ElvesFound : MailMessage {}
}
