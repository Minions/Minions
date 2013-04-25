using System;
using System.Collections.Generic;
using System.Linq;
using Fools.Compilation.Tokenization;

namespace Fools.Ast
{
	public class UnrecognizedStatement : INode, IEquatable<UnrecognizedStatement>, IStatement
	{
		public UnrecognizedStatement(params Token[] tokens)
		{
			contents = tokens;
		}

		public UnrecognizedStatement(IEnumerable<Token> tokens)
		{
			contents = tokens;
		}

		public bool Equals(UnrecognizedStatement other)
		{
			if (ReferenceEquals(null, other))
				return false;
			if (ReferenceEquals(this, other))
				return true;
			return contents.SequenceEqual(other.contents);
		}

		public IEnumerable<Token> contents { get; private set; }

		public override string ToString()
		{
			return string.Format("Statement.Unknown({0})", string.Join(", ", contents));
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as UnrecognizedStatement);
		}

		public override int GetHashCode()
		{
			return contents.GetHashCode();
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