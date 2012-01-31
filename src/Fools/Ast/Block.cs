using System;
using System.Collections.Generic;
using System.Linq;
using Fools.Utils;

namespace Fools.Ast
{
	public class Block : INode, IEquatable<Block>
	{
		private IEnumerable<IStatement> _statements;
		public Block(params IStatement[] statements) { this.statements = statements; }

		public bool Equals(Block other)
		{
			if(ReferenceEquals(null, other))
				return false;
			if(ReferenceEquals(this, other))
				return true;
			return statements.SequenceEqual(other.statements);
		}

		public IEnumerable<IStatement> statements
		{
			get { return _statements; }
			set
			{
				Require.that(value != null,
				   () => new ArgumentNullException("statements", "You cannot create a block with a null statements collection."));
				_statements = value;
			}
		}

		public override string ToString() { return string.Format("Block:{0}", ("\r\n" + String.Join("\r\n", statements)).Replace("\r\n", "\r\n\t")); }
		public override bool Equals(object obj) { return Equals(obj as Block); }
		public override int GetHashCode() { return (statements != null ? statements.GetHashCode() : 0); }
		public static bool operator ==(Block left, Block right) { return Equals(left, right); }
		public static bool operator !=(Block left, Block right) { return !Equals(left, right); }
	}
}
