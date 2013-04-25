using System;

namespace Fools.Ast
{
	public class AssignmentStatement : IEquatable<AssignmentStatement>, IStatement
	{
		public bool Equals(AssignmentStatement other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.variable, variable) && Equals(other.value, value);
		}

		public VariableReferenceExpression variable { get; set; }
		public INode value { get; set; }

		public override string ToString()
		{
			return string.Format("{0} := {1}", variable.variable_name, value);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as AssignmentStatement);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (variable.GetHashCode()*397) ^ value.GetHashCode();
			}
		}

		public static bool operator ==(AssignmentStatement left, AssignmentStatement right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(AssignmentStatement left, AssignmentStatement right)
		{
			return !Equals(left, right);
		}
	}
}
