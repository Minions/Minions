using System;

namespace Fools.Tokenization
{
	public class IdentifierToken : Token, IEquatable<IdentifierToken>
	{
		public string Value { get; private set; }

		public IdentifierToken(string value)
		{
			Require.that(
				!string.IsNullOrEmpty(value),
				() => new ArgumentNullException("value", "An identifier token must always have a non-null, non-empty value."));
			Value = value;
		}

		public bool Equals(IdentifierToken other)
		{
			if(ReferenceEquals(null, other)) return false;
			if(ReferenceEquals(this, other)) return true;
			return Equals(other.Value, Value);
		}

		public override bool Equals(Token other)
		{
			return Equals(other as IdentifierToken);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as IdentifierToken);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public static bool operator ==(IdentifierToken left, IdentifierToken right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(IdentifierToken left, IdentifierToken right)
		{
			return !Equals(left, right);
		}
	}
}