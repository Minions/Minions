using System;
using System.Collections.Generic;
using Fools.Tokenization;
using System.Linq;

namespace Fools.Ast
{
	public class Line : INode, IEquatable<Line>
	{
		public int IndentationLevel { get; private set; }
		public IList<Token> Tokens { get; private set; }

		public Line(int indentationLevel, params Token[] tokens)
		{
			IndentationLevel = indentationLevel;
			Tokens = tokens;
		}

		public override string ToString()
		{
			return string.Format("[Line ({0})] {1}", IndentationLevel, string.Join(" ", Tokens));
		}

		public bool Equals(Line other)
		{
			if(ReferenceEquals(null, other)) return false;
			if(ReferenceEquals(this, other)) return true;
			return other.IndentationLevel == IndentationLevel && other.Tokens.SequenceEqual(Tokens);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Line);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (IndentationLevel*397) ^ Tokens.GetHashCode();
			}
		}

		public static bool operator ==(Line left, Line right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Line left, Line right)
		{
			return !Equals(left, right);
		}
	}
}
