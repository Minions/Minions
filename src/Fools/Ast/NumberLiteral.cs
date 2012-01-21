using System;
using MetaSharp.Transformation;

namespace Fools.Ast
{
	public class NumberLiteral : INode, IEquatable<NumberLiteral>
	{
		public NumberLiteral() {}

		public NumberLiteral(int value)
		{
			this.value = value;
		}

		public bool Equals(NumberLiteral other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.value, value);
		}

		public int value { get; set; }

		public override string ToString()
		{
			return string.Format("{0}L", value);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as NumberLiteral);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public static bool operator ==(NumberLiteral left, NumberLiteral right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(NumberLiteral left, NumberLiteral right)
		{
			return !Equals(left, right);
		}
	}
}
