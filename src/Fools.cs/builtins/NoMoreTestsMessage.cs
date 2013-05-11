// NoMoreTestsMessage.cs
// 
// Copyright 2012 The Minions Project (http:/github.com/Minions).
// All rights reserved. Usage as permitted by the LICENSE.txt file for this project.

using System;
using Fools.cs.Api;

namespace Fools.cs.Tests.TestFramework
{
	public class NoMoreTestsMessage : MailMessage, IEquatable<NoMoreTestsMessage>
	{
		protected override bool _compare(MailMessage obj)
		{
			var other = obj as NoMoreTestsMessage;
			return other != null && base._compare(other);
		}

		public bool Equals(NoMoreTestsMessage other)
		{
			return Equals((object) other);
		}
	}
}
