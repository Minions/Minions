using System;
using System.Collections.Generic;
using System.Linq;
using Fools.Compilation.Tokenization;

namespace Fools.Ast
{
	public class Line : INode, IEquatable<Line>
	{
		public int IndentationLevel { get; private set; }
		public IList<Token> Contents { get; private set; }

		public Line(int indentationLevel, params Token[] contents)
		{
			IndentationLevel = indentationLevel;
			Contents = contents;
		}

		public override string ToString()
		{
			return string.Format("[Line ({0})] {1}", IndentationLevel, string.Join(" ", Contents));
		}

		public bool Equals(Line other)
		{
			if(ReferenceEquals(null, other)) return false;
			if(ReferenceEquals(this, other)) return true;
			return other.IndentationLevel == IndentationLevel && other.Contents.SequenceEqual(Contents);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Line);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (IndentationLevel*397) ^ Contents.GetHashCode();
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
