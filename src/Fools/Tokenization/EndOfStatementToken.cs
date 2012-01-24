using System;

namespace Fools.Tokenization
{
	public class EndOfStatementToken : Token, IEquatable<EndOfStatementToken>
	{
		public bool Equals(EndOfStatementToken other)
		{
			return !ReferenceEquals(null, other);
		}

		public override bool Equals(Token other)
		{
			return Equals(other as EndOfStatementToken);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as EndOfStatementToken);
		}

		public override int GetHashCode()
		{
			return 99;
		}

		public static bool operator ==(EndOfStatementToken left, EndOfStatementToken right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(EndOfStatementToken left, EndOfStatementToken right)
		{
			return !Equals(left, right);
		}
	}
}