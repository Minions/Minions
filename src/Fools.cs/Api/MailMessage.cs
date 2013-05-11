// MailMessage.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.builtins;

namespace Fools.cs.Api
{
	public abstract class MailMessage : IEquatable<MailMessage>
	{
		public override string ToString()
		{
			return this.pretty_print();
		}

		public bool Equals(MailMessage other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return _compare(other);
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as MailMessage);
		}

		protected virtual bool _compare(MailMessage other)
		{
			return true;
		}

		public override int GetHashCode()
		{
			return 1;
		}

		public static bool operator ==(MailMessage left, MailMessage right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(MailMessage left, MailMessage right)
		{
			return !Equals(left, right);
		}
	}
}
