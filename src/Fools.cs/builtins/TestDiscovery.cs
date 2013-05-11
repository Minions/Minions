// TestDiscovery.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;
using Fools.cs.Tests.TestFramework;

namespace Fools.cs.builtins
{
	public abstract class TestDiscovery
	{
		public void discover_tests(MailRoom destination)
		{
			_locate_tests((mission) => destination.announce(new TestFoundMessage(mission)),
				() => destination.announce(new NoMoreTestsMessage()));
		}

		protected abstract void _locate_tests(Action<MissionSpecification> report_test, Action done_finding_tests);
	}
}
