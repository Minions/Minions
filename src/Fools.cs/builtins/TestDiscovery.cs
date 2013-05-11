// TestDiscovery.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;

namespace Fools.cs.builtins
{
	public abstract class TestDiscovery
	{
		public void discover_tests(MailRoom destination)
		{
			_locate_tests((mission)=>destination.announce(new TestFoundMessage(mission)));
		}

		protected abstract void _locate_tests(Action<MissionSpecification> action);
	}
}
