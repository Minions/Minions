// TestDiscovery.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using System.Diagnostics;
using Fools.cs.Api;
using Fools.cs.Tests.TestFramework;
using Fools.cs.Utilities;

namespace Fools.cs.builtins
{
	public abstract class TestDiscovery
	{
		public void discover_tests([NotNull] MailRoom destination)
		{
			_locate_tests(mission => {
				Debug.Assert(mission != null, "mission != null");
				destination.announce(new TestFoundMessage(mission));
			},
				() => destination.announce(new NoMoreTestsMessage()));
		}

		protected abstract void _locate_tests([NotNull] Action<MissionSpecification> report_test,
			[NotNull] Action done_finding_tests);
	}
}
