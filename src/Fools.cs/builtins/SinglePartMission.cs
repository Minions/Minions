// SinglePartMission.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;
using Fools.cs.Utilities;

namespace Fools.cs.builtins
{
	public class SinglePartMission : MissionSpecification, IEquatable<SinglePartMission>
	{
		public SinglePartMission([NotNull] string name,[NotNull] Action operation):base(name)
		{
			this.operation = operation;
			is_complete = false;
		}

		[NotNull]
		public Action operation { get; private set; }
		public bool is_complete { get; set; }

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

		public override void execute([NotNull] MissionOperator @operator)
		{
			@operator.execute(this);
		}
	}
}
