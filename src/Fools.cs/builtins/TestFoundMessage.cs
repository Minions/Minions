// TestFoundMessage.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;

namespace Fools.cs.builtins
{
	public class TestFoundMessage : MailMessage, IEquatable<TestFoundMessage>
	{
		public TestFoundMessage(MissionSpecification mission)
		{
			this.mission = mission;
		}

		public MissionSpecification mission { get; private set; }

		protected override bool _compare(MailMessage obj)
		{
			var other = obj as TestFoundMessage;
			return other != null && base._compare(other) && mission.Equals(other.mission);
		}

		public bool Equals(TestFoundMessage other)
		{
			return Equals((object) other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode()*397) ^ mission.GetHashCode();
			}
		}
	}
}
