using System;
using System.Collections.Generic;
using System.Linq;
using Fools.Compilation.Tokenization;
using Fools.Utils;

namespace Fools.Ast
{
	public class Block : IStatement, IEquatable<Block>
	{
		public Block(IEnumerable<Token> header, params IStatement[] statements)
		{
			Require.that(
				header != null,
				() => new ArgumentNullException("header", "You cannot create a block with a null header line collection."));
			Require.that(
				statements != null,
				() => new ArgumentNullException("statements", "You cannot create a block with a null statements collection."));
			this.header = header;
			_statements = statements.ToList();
		}

		public bool Equals(Block other)
		{
			if(ReferenceEquals(null, other))
				return false;
			if(ReferenceEquals(this, other))
				return true;
			return header.SequenceEqual(other.header) && statements.SequenceEqual(other.statements);
		}

		private readonly List<IStatement> _statements;
		public IEnumerable<IStatement> statements { get { return _statements; } }

		public IEnumerable<Token> header { get; private set; }

		public override string ToString()
		{
			return string.Format(
				"[Block] {0}:{1}",
				string.Join(" ", header),
				("\r\n\t" + String.Join("\r\n\t", statements)));
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as Block);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return header.GetHashCode()*397 ^ statements.GetHashCode();
			}
		}

		public static bool operator ==(Block left, Block right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Block left, Block right)
		{
			return !Equals(left, right);
		}

		public void AddStatement(IStatement statement)
		{
			_statements.Add(statement);
		}
	}
}
