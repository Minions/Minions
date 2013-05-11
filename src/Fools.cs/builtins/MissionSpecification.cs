// MissionSpecification.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;

namespace Fools.cs.builtins
{
	public abstract class MissionSpecification : IEquatable<MissionSpecification>
	{
		public string name { get; private set; }

		protected MissionSpecification(string name)
		{
			this.name = name;
		}

		public bool Equals(MissionSpecification other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return _compare(other);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as MissionSpecification);
		}

		protected virtual bool _compare(MissionSpecification other)
		{
			return name.Equals(other.name);
		}

		public override int GetHashCode()
		{
			return name.GetHashCode();
		}

		public static bool operator ==(MissionSpecification left, MissionSpecification right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(MissionSpecification left, MissionSpecification right)
		{
			return !Equals(left, right);
		}
	}
}
