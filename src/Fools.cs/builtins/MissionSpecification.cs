// MissionSpecification.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;
using Fools.cs.Utilities;

namespace Fools.cs.builtins
{
	public abstract class MissionSpecification : IEquatable<MissionSpecification>
	{
		[NotNull]
		private string name { get; set; }

		protected MissionSpecification([NotNull] string name)
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

		protected virtual bool _compare([NotNull] MissionSpecification other)
		{
			return GetType() == other.GetType() && name.Equals(other.name);
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
		
		public abstract void execute([NotNull] MissionOperator operation);
	}
}
