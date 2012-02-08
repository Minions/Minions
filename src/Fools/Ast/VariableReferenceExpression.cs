using System;
using Fools.Compilation.Tokenization;
using Fools.Utils;

namespace Fools.Ast
{
	public class VariableReferenceExpression : IExpression, IEquatable<VariableReferenceExpression>
	{
		private readonly IdentifierToken _identifier;

		public VariableReferenceExpression(IdentifierToken identifier)
		{
			Require.that(identifier != null, () => new ArgumentNullException("identifer"));
			_identifier = identifier;
		}

		public string variable_name { get { return _identifier.Value; } }

		public bool Equals(VariableReferenceExpression other)
		{
			if(ReferenceEquals(null, other)) return false;
			if(ReferenceEquals(this, other)) return true;
			return Equals(other._identifier, _identifier);
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj)) return false;
			if(ReferenceEquals(this, obj)) return true;
			if(obj.GetType() != typeof(VariableReferenceExpression)) return false;
			return Equals((VariableReferenceExpression) obj);
		}

		public override int GetHashCode()
		{
			return _identifier.GetHashCode();
		}

		public static bool operator ==(VariableReferenceExpression left, VariableReferenceExpression right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(VariableReferenceExpression left, VariableReferenceExpression right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return string.Format("VariableReference({0})", _identifier.Value);
		}
	}
}
