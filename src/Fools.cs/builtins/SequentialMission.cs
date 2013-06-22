// SequentialMission.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;
using Fools.cs.Utilities;

namespace Fools.cs.builtins
{
	public class SequentialMission : MissionSpecification, IEquatable<SequentialMission>
	{
		public SequentialMission([NotNull] string name) : base(name) {}

		[CanBeNull]
		public SinglePartMission next_mission { get { return null; } }

		public override void execute(MissionOperator operation)
		{
			operation.execute(this);
		}

		public bool Equals(SequentialMission other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return _compare(other);
		}

		public void add_mission(SinglePartMission mission) {}
	}
}
