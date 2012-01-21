using System;
using MetaSharp.Transformation;

namespace Fools.Ast
{
	public class UnrecognizedStatement : INode, IEquatable<UnrecognizedStatement>, IStatement
	{
		public bool Equals(UnrecognizedStatement other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return Equals(other.text, text);
		}

		public string text { get; set; }

		public override string ToString()
		{
			return string.Format("Statement.Unknown({0})", text);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as UnrecognizedStatement);
		}

		public override int GetHashCode()
		{
			return (text != null ? text.GetHashCode() : 0);
		}

		public static bool operator ==(UnrecognizedStatement left, UnrecognizedStatement right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(UnrecognizedStatement left, UnrecognizedStatement right)
		{
			return !Equals(left, right);
		}
	}
}