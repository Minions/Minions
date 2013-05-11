// SinglePartMission.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;

namespace Fools.cs.builtins
{
	public class SinglePartMission : MissionSpecification, IEquatable<SinglePartMission>
	{
		public SinglePartMission(string name, Action operation):base(name)
		{
			this.operation = operation;
		}

		public Action operation { get; private set; }

		public bool Equals(SinglePartMission other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return _compare(other);
		}

		protected override bool _compare(MissionSpecification obj)
		{
			var other = obj as SinglePartMission;
			return other != null && base._compare(other) && operation.Equals(other.operation);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (base.GetHashCode()*397) ^ operation.GetHashCode();
			}
		}
	}
}
