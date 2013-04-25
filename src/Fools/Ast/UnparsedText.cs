using System;

namespace Fools.Ast
{
	public class UnparsedText : INode, IEquatable<UnparsedText>
	{
		public bool Equals(UnparsedText other)
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
			return string.Format("Unparsed({0})", text);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as UnparsedText);
		}

		public override int GetHashCode()
		{
			return (text != null ? text.GetHashCode() : 0);
		}

		public static bool operator ==(UnparsedText left, UnparsedText right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(UnparsedText left, UnparsedText right)
		{
			return !Equals(left, right);
		}
	}
}
