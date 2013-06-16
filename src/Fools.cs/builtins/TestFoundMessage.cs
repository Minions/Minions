// TestFoundMessage.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;
using Fools.cs.Utilities;

namespace Fools.cs.builtins
{
	public class TestFoundMessage : MailMessage, IEquatable<TestFoundMessage>
	{
		public TestFoundMessage([NotNull] MissionSpecification mission)
		{
			this.mission = mission;
		}

		[NotNull]
		public MissionSpecification mission { get; set; }

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
