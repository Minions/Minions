using System;

namespace Fools.Compilation.Tokenization
{
	public class IndentationToken : Token, IEquatable<IndentationToken>
	{
		public int IndentationLevel { get; private set; }

		public IndentationToken(int indentationLevel)
		{
			IndentationLevel = indentationLevel;
		}

		public bool Equals(IndentationToken other)
		{
			if(ReferenceEquals(null, other)) return false;
			if(ReferenceEquals(this, other)) return true;
			return other.IndentationLevel == IndentationLevel;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as IndentationToken);
		}

		public override int GetHashCode()
		{
			return IndentationLevel;
		}

		public override bool Equals(Token other)
		{
			return Equals(other as IndentationToken);
		}

		public static bool operator ==(IndentationToken left, IndentationToken right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(IndentationToken left, IndentationToken right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return string.Format("IndentationLevel({0})", IndentationLevel);
		}
	}
}