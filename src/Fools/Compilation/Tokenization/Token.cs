using System;

namespace Fools.Compilation.Tokenization
{
	public abstract class Token : IEquatable<Token>
	{
		public abstract bool Equals(Token other);

		public static bool operator ==(Token left, Token right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Token left, Token right)
		{
			return !Equals(left, right);
		}
	}
}