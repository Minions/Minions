using System;

namespace Fools.Ast
{
	public class PrintStatement : INode, IEquatable<PrintStatement>, IStatement
	{
		public bool Equals(PrintStatement other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.variable, variable);
		}

		public string variable { get; set; }

		public override string ToString()
		{
			return string.Format("print({0})", variable);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as PrintStatement);
		}

		public override int GetHashCode()
		{
			return (variable != null ? variable.GetHashCode() : 0);
		}

		public static bool operator ==(PrintStatement left, PrintStatement right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(PrintStatement left, PrintStatement right)
		{
			return !Equals(left, right);
		}
	}
}
